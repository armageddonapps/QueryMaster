
#region License
/*
Copyright (c) 2015 Betson Roy

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using QueryMaster;
using System.Threading;
using System.Globalization;

namespace QueryMaster.GameServer
{
    /// <summary>
    /// Represents the connected server.Provides methods to query,listen to server logs and control the server.
    /// </summary>
    public abstract class Server : QueryMasterBase
    {
        private EngineType Type;
        private long Latency;
        private Logs logs=null;
        private byte[] PlayerChallengeId = null;
        private byte[] RuleChallengeId = null;
        private bool IsPlayerChallengeId;
        private bool IsRuleChallengeId;
        private Rcon rcon = null;

        internal UdpQuery UdpSocket =null;
        internal ConnectionInfo ConInfo = null;
        /// <summary>
        /// Server Endpoint.
        /// </summary>
        public IPEndPoint EndPoint { get; protected set; }
        /// <summary>
        /// Provides method(s) to perform admin level operations.
        /// </summary>
        public Rcon Rcon { get; protected set;}

        /// <summary>
        /// Returns true if server replies only to half life protocol messages.
        /// </summary>
        public bool IsObsolete { get; protected set; }

        /// <summary>
        /// Gets or sets Socket's SendTimeout Property.
        /// </summary>
        public int SendTimeout
        {
            get { return UdpSocket.Socket.SendTimeout; }
            set { UdpSocket.Socket.SendTimeout = value; }
        }
        /// <summary>
        /// Gets or sets Socket's ReceiveTimeout.
        /// </summary>
        public int ReceiveTimeout
        {
            get { return UdpSocket.Socket.ReceiveTimeout; }
            set { UdpSocket.Socket.ReceiveTimeout = value; }
        }

        internal Server(ConnectionInfo conInfo, EngineType type, bool? isObsolete)
        {
            ConInfo = conInfo;
            EndPoint = conInfo.EndPoint;
            UdpSocket = new UdpQuery(conInfo);
            Type = type;
            if (isObsolete == null)
            {
                try
                {
                    if (UdpSocket.GetResponse(QueryMsg.ObsoleteInfoQuery, Type)[0] == 0x6D)
                        IsObsolete = true;
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode  == SocketError.TimedOut)
                        IsObsolete = false;
                }
            }
            else
                IsObsolete = isObsolete == true ? true : false;
        }

        /// <summary>
        /// Retrieves information about the server.
        /// </summary>
        /// <param name="callback">Called on every attempt made to connect to server(max. attempts = Retries + 1).</param>
        /// <returns>Instance of ServerInfo.</returns>
        public virtual ServerInfo GetInfo(AttemptCallback callback = null)
        {
            ThrowIfDisposed();
            return Invoke<ServerInfo>(getInfo, ConInfo.Retries + 1,callback,ConInfo.ThrowExceptions);
        }

        private ServerInfo getInfo()
        {
            byte[] recvData = null;
            ServerInfo serverInfo = null;
            try
            {
                byte[] Query = QueryMsg.InfoQuery;
                if (IsObsolete)
                    Query = QueryMsg.ObsoleteInfoQuery;

                recvData = new byte[UdpSocket.BufferSize];

                Stopwatch sw = Stopwatch.StartNew();
                recvData = UdpSocket.GetResponse(Query, Type);
                sw.Stop();
                Latency = sw.ElapsedMilliseconds;
                switch (recvData[0])
                {
                    case 0x49: serverInfo = Current(recvData); break;
                    case 0x6D: serverInfo = Obsolete(recvData); break;
                    default: throw new InvalidHeaderException("packet header is not valid");
                }
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvData == null ? new byte[1] : recvData);
                throw;
            }
            return serverInfo;

        }

        private ServerInfo Current(byte[] data)
        {
            ServerInfo server = null;
            Parser parser = new Parser(data);
            if (parser.ReadByte() != (byte)ResponseMsgHeader.A2S_INFO)
                throw new InvalidHeaderException("A2S_INFO message header is not valid");
            server = new ServerInfo();
            server.IsObsolete = false;
            server.Protocol = parser.ReadByte();
            server.Name = parser.ReadString();
            server.Map = parser.ReadString();
            server.Directory = parser.ReadString();
            server.Description = parser.ReadString();
            server.Id = parser.ReadUShort();
            server.Players = parser.ReadByte();
            server.MaxPlayers = parser.ReadByte();
            server.Bots = parser.ReadByte();
            server.ServerType = (new Func<GameServertype>(() => 
            { 
                switch ((char)parser.ReadByte()) 
                { 
                    case 'l':return GameServertype.Listen; 
                    case 'd':return GameServertype.Dedicated; 
                    case 'p':return GameServertype.SourceTV;
                }
                return GameServertype.Invalid; 
            }))();
            server.Environment = (new Func<GameEnvironment>(() => 
            { 
                switch ((char)parser.ReadByte()) 
                { 
                    case 'l':return GameEnvironment.Linux; 
                    case 'w':return GameEnvironment.Windows; 
                    case 'm':return GameEnvironment.Mac;
                    case 'o': return GameEnvironment.Mac;
                }
                return GameEnvironment.Invalid; 
            }))();
            server.IsPrivate = parser.ReadByte() > 0;
            server.IsSecure = parser.ReadByte() > 0;
            if ( Util.ShipIds.Contains(server.Id))
            {
                ShipInfo ship = new ShipInfo();
                switch (parser.ReadByte())
                {
                    case 0: ship.Mode = ShipMode.Hunt; break;
                    case 1: ship.Mode = ShipMode.Elimination; break;
                    case 2: ship.Mode = ShipMode.Duel; break;
                    case 3: ship.Mode = ShipMode.Deathmatch; break;
                    case 4: ship.Mode = ShipMode.VIPTeam; break;
                    case 5: ship.Mode = ShipMode.TeamElimination; break;
                    default: ship.Mode =ShipMode.Invalid; break;
                }
                ship.Witnesses = parser.ReadByte();
                ship.Duration = parser.ReadByte();
                server.ShipInfo = ship;
            }

            server.GameVersion = parser.ReadString();
            server.ExtraInfo = new ExtraInfo();
            if (parser.HasUnParsedBytes)
            {
                byte edf = parser.ReadByte();
                server.ExtraInfo.Port = (edf & 0x80) > 0 ? parser.ReadUShort() : (ushort)0;
                server.ExtraInfo.SteamId = (edf & 0x10) > 0 ? parser.ReadULong() : 0;
                if ((edf & 0x40) > 0)
                    server.ExtraInfo.SpecInfo = new SourceTVInfo() { Port = parser.ReadUShort(), Name = parser.ReadString() };
                server.ExtraInfo.Keywords = (edf & 0x20) > 0 ? parser.ReadString() : string.Empty;
                server.ExtraInfo.GameId = (edf & 0x10) > 0 ? parser.ReadULong() : 0;
            }
            server.Address = UdpSocket.Address.ToString();
            server.Ping = Latency;

            return server;
        }

        private ServerInfo Obsolete(byte[] data)
        {
            ServerInfo server = null;
            Parser parser = new Parser(data);
            if (parser.ReadByte() != (byte)ResponseMsgHeader.A2S_INFO_Obsolete)
                throw new InvalidHeaderException("A2S_INFO(obsolete) message header is not valid");
            server = new ServerInfo();
            server.IsObsolete = true;
            server.Address = parser.ReadString();
            server.Name = parser.ReadString();
            server.Map = parser.ReadString();
            server.Directory = parser.ReadString();
            server.Id = Util.GetGameId(parser.ReadString());
            server.Players = parser.ReadByte();
            server.MaxPlayers = parser.ReadByte();
            server.Protocol = parser.ReadByte();
            server.ServerType = (new Func<GameServertype>(() => 
            {
                switch (Char.ToUpper((char)parser.ReadByte(), CultureInfo.InvariantCulture)) 
                { 
                    case 'L':return GameServertype.NonDedicated; 
                    case 'D':return GameServertype.Dedicated; 
                    case 'P':return GameServertype.HLTVServer; 
                } 
                return GameServertype.Invalid; 
            }))();
            server.Environment = (new Func<GameEnvironment>(() => 
            { 
                switch (Char.ToUpper((char)parser.ReadByte())) 
                {
                    case 'L': return GameEnvironment.Linux;
                    case 'W': return GameEnvironment.Windows; 
                }
                return GameEnvironment.Invalid; 
            }))();
            server.IsPrivate = parser.ReadByte() > 0;
            byte mod = parser.ReadByte();
            server.IsModded = mod > 0 ;
            if (server.IsModded)
            {
                ModInfo modinfo = new ModInfo();
                modinfo.Link = parser.ReadString();
                modinfo.DownloadLink = parser.ReadString();
                parser.ReadByte();//0x00
                modinfo.Version = parser.ReadInt();
                modinfo.Size = parser.ReadInt();
                modinfo.IsOnlyMultiPlayer = parser.ReadByte() > 0;
                modinfo.IsHalfLifeDll = parser.ReadByte() > 0;
                server.ModInfo = modinfo;
            }
            server.IsSecure = parser.ReadByte() > 0;
            server.Bots = parser.ReadByte();
            server.GameVersion = "server is obsolete,does not provide this information";
            server.Ping = Latency;

            return server;
        }
        /// <summary>
        /// Retrieves information about the players currently on the server.
        /// </summary>
        /// <param name="callback">called on every attempt made to connect to server(max. attempts = Retries + 1).</param>
        /// <returns>Collection of <see cref="PlayerInfo"/> instances.</returns>
        public virtual QueryMasterCollection<PlayerInfo> GetPlayers(AttemptCallback callback = null)
        {
            ThrowIfDisposed();
            return Invoke<QueryMasterCollection<PlayerInfo>>(getPlayers, ConInfo.Retries + 1, callback, ConInfo.ThrowExceptions);
        }

        private QueryMasterCollection<PlayerInfo> getPlayers()
        {
            byte[] recvData = null;
            List<PlayerInfo> players = null;
            Parser parser = null;
            try
            {
                if (IsObsolete)
                {
                    recvData = UdpSocket.GetResponse(QueryMsg.ObsoletePlayerQuery, Type);
                }
                else
                {
                    if (PlayerChallengeId == null)
                    {
                        recvData = GetPlayerChallengeId();
                        if (IsPlayerChallengeId)
                            PlayerChallengeId = recvData;
                    }
                    if (IsPlayerChallengeId)
                        recvData = UdpSocket.GetResponse(Util.MergeByteArrays(QueryMsg.PlayerQuery, PlayerChallengeId), Type);
                }

                parser = new Parser(recvData);
                if (parser.ReadByte() != (byte)ResponseMsgHeader.A2S_PLAYER)
                    throw new InvalidHeaderException("A2S_PLAYER message header is not valid");
                int playerCount = parser.ReadByte();
                players = new List<PlayerInfo>(playerCount);
                for (int i = 0; i < playerCount; i++)
                {
                    parser.ReadByte();
                    players.Add(new PlayerInfo()
                    {
                        Name = parser.ReadString(),
                        Score = parser.ReadInt(),
                        Time = TimeSpan.FromSeconds(parser.ReadFloat())
                    });
                }
                if (playerCount == 1 && players[0].Name == "Max Players")
                    players.Clear();
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvData == null ? new byte[1] : recvData);
                throw;
            }
            return new QueryMasterCollection<PlayerInfo>(players);
        }

        private byte[] GetPlayerChallengeId()
        {
            byte[] recvBytes = null;
            byte header = 0;
            Parser parser = null;
            recvBytes = UdpSocket.GetResponse(QueryMsg.PlayerChallengeQuery, Type);
            try
            {
                parser = new Parser(recvBytes);
                header = parser.ReadByte();
                switch (header)
                {
                    case (byte)ResponseMsgHeader.A2S_SERVERQUERY_GETCHALLENGE: IsPlayerChallengeId = true; return parser.GetUnParsedBytes();
                    case (byte)ResponseMsgHeader.A2S_PLAYER: IsPlayerChallengeId = false; return recvBytes;
                    default: throw new InvalidHeaderException("A2S_SERVERQUERY_GETCHALLENGE message header is not valid");
                }
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvBytes == null ? new byte[1] : recvBytes);
                throw;
            }
        }

        /// <summary>
        /// Retrieves server rules.
        /// </summary>
        /// <param name="callback">called on every attempt made to connect to server(max. attempts = <see cref="ConnectionInfo.Retries"/> + 1).</param>
        /// <returns>Collection of <see cref="Rule"/> instances.</returns>
        public virtual QueryMasterCollection<Rule> GetRules(AttemptCallback callback = null)
        {
            ThrowIfDisposed();
            return Invoke<QueryMasterCollection<Rule>>(getRules, ConInfo.Retries + 1, callback, ConInfo.ThrowExceptions);
        }

        private QueryMasterCollection<Rule> getRules()
        {
            byte[] recvData = null;
            List<Rule> rules = null;
            Parser parser = null;
            try
            {
                if (IsObsolete)
                {
                    recvData = UdpSocket.GetResponse(QueryMsg.ObsoleteRuleQuery, Type);
                }
                else
                {
                    if (RuleChallengeId == null)
                    {
                        recvData = GetRuleChallengeId();
                        if (IsRuleChallengeId)
                            RuleChallengeId = recvData;
                    }
                    if (IsRuleChallengeId)
                        recvData = UdpSocket.GetResponse(Util.MergeByteArrays(QueryMsg.RuleQuery, RuleChallengeId), Type);
                }

                parser = new Parser(recvData);
                if (parser.ReadByte() != (byte)ResponseMsgHeader.A2S_RULES)
                    throw new InvalidHeaderException("A2S_RULES message header is not valid");

                int ruleCount = parser.ReadUShort();
                rules = new List<Rule>(ruleCount);
                for (int i = 0; i < ruleCount; i++)
                {
                    rules.Add(new Rule() { Name = parser.ReadString(), Value = parser.ReadString() });
                }
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvData == null ? new byte[1] : recvData);
                throw;
            }
            return new QueryMasterCollection<Rule>(rules);
        }

        private byte[] GetRuleChallengeId()
        {
            byte[] recvBytes = null;
            byte header = 0;
            Parser parser = null;
            recvBytes = UdpSocket.GetResponse(QueryMsg.RuleChallengeQuery, Type);
            try
            {
                parser = new Parser(recvBytes);
                header = parser.ReadByte();

                switch (header)
                {
                    case (byte)ResponseMsgHeader.A2S_SERVERQUERY_GETCHALLENGE: IsRuleChallengeId = true; return BitConverter.GetBytes(parser.ReadInt());
                    case (byte)ResponseMsgHeader.A2S_RULES: IsRuleChallengeId = false; return recvBytes;
                    default: throw new InvalidHeaderException("A2S_SERVERQUERY_GETCHALLENGE message header is not valid");

                }
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvBytes == null ? new byte[1] : recvBytes);
                throw;
            }
        }

        /// <summary>
        /// Listen to server logs.
        /// </summary>
        /// <param name="port">Local port.</param>
        /// <returns>Instance of <see cref="Logs"/> class.</returns>
        /// <remarks>Receiver's socket address must be added to server's logaddress list before listening.</remarks>
        public virtual Logs GetLogs(int port)
        {
            ThrowIfDisposed();
            if(logs==null)
                logs = new Logs(Type, port, EndPoint);
            return logs;
        }

        /// <summary>
        /// Gets valid rcon instance that can be used to send rcon commands to server..
        /// </summary>
        /// <param name="pass">Rcon password of server.</param>
        /// <returns>true if server accepted rcon password.</returns>
        public abstract bool GetControl(string pass);
        /// <summary>
        /// Gets round-trip delay time.
        /// </summary>
        /// <returns>Elapsed milliseconds(-1 if server is not responding).</returns>
        public virtual long Ping()
        {
            ThrowIfDisposed();
            Stopwatch sw = null;
            sw = Stopwatch.StartNew();
            try
            {
                if (IsObsolete)
                    UdpSocket.GetResponse(QueryMsg.ObsoletePingQuery, Type);
                else
                    UdpSocket.GetResponse(QueryMsg.InfoQuery, Type);
            }
            catch (SocketException)
            {
                sw.Stop();
                return -1;
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        /// <summary>
        /// Disposes all the resources used by this instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (Rcon != null)
                        Rcon.Dispose();
                    Rcon = null;
                    if (logs != null)
                        logs.Dispose();
                    logs = null;
                    if (UdpSocket != null)
                        UdpSocket.Dispose();
                    UdpSocket = null;
                }
                base.Dispose(disposing);
                IsDisposed = true;
            }
        }



    }
}
