
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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace QueryMaster.GameServer
{
    /// <summary>
    /// Provides mechanism to subscribe and filter logged events.
    /// </summary>
   public class LogEvents:QueryMasterBase
    {
        internal IPEndPoint ServerEndPoint;
        internal Regex LineSplit = new Regex(": ");
        internal Regex RegPlayer = new Regex("^([^\"]+)<([^\"]+)><([^\"]+)><([^\"]*)>$");
        internal Regex RegIsPlayerMsg = new Regex("^.*<\\d+><.+><.*>.*$");
        internal char[] QuoteSplitPattern = { '\"' };

       /// <summary>
       /// Represents a collection of filters.
       /// </summary>
        public LogFilterCollection Filters { get; set; }
        /// <summary>
        /// Occurs when Server cvar starts(In TFC, if tfc_clanbattle is 1, this doesn't happen.).
        /// </summary>
        public event EventHandler<LogEventArgs> CvarStartMsg; //001.1
        /// <summary>
        /// Occurs when someone changes a cvar over rcon.
        /// </summary>
        public event EventHandler<CvarEventArgs> ServerCvar;   //001.2
        /// <summary>
        /// Occurs when Server cvar ends(In TFC, if tfc_clanbattle is 0, this doesn't happen.).
        /// </summary>
        public event EventHandler<LogEventArgs> CvarEndMsg;   //001.3
        /// <summary>
        /// Occurs when Logging to file is started.
        /// </summary>
        public event EventHandler<LogStartEventArgs> LogFileStarted;   //002.1
        /// <summary>
        /// Occurs when Log file is closed.
        /// </summary>
        public event EventHandler<LogEventArgs> LogFileClosed;    //002.2
        /// <summary>
        /// Occurs when map is loaded.
        /// </summary>
        public event EventHandler<MapLoadEventArgs> MapLoaded;    //003.1
        /// <summary>
        /// Occurs when Map starts.
        /// </summary>
        public event EventHandler<MapStartEventArgs> MapStarted;   //003.2
        /// <summary>
        /// Occurs when an rcon message is sent to server.
        /// </summary>
        public event EventHandler<RconEventArgs> RconMsg;      //004(1,2)
        /// <summary>
        /// Occurs when server name is displayed.
        /// </summary>
        public event EventHandler<ServerNameEventArgs> ServerName;   //005
        /// <summary>
        /// Occurs when Server says.
        /// </summary>
        public event EventHandler<ServerSayEventArgs> ServerSay;    //006
        /// <summary>
        /// Occurs when a player is connected.
        /// </summary>
        public event EventHandler<ConnectEventArgs> PlayerConnected;  //050
        /// <summary>
        /// Occurs when a player is validated.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerValidated;  //050b
        /// <summary>
        /// Occurs when a player is enters game.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerEnteredGame;    //51
        /// <summary>
        /// Occurs when a player is disconnected.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerDisconnected;   //52
        /// <summary>
        /// Occurs when a player is kicked.
        /// </summary>
        public event EventHandler<KickEventArgs> PlayerKicked;   //052b
        /// <summary>
        /// Occurs when a player commit suicide.
        /// </summary>
        public event EventHandler<SuicideEventArgs> PlayerSuicided;   //053
        /// <summary>
        /// Occurs when a player Join team.
        /// </summary>
        public event EventHandler<TeamSelectionEventArgs> PlayerJoinedTeam;   //054
        /// <summary>
        /// Occurs when a player change role.
        /// </summary>
        public event EventHandler<RoleSelectionEventArgs> PlayerChangedRole;   //055
        /// <summary>
        /// Occurs when a player changes name.
        /// </summary>
        public event EventHandler<NameChangeEventArgs> PlayerChangedName;   //056
        /// <summary>
        /// Occurs when a player is killed.
        /// </summary>
        public event EventHandler<KillEventArgs> PlayerKilled;        //057
        /// <summary>
        /// Occurs when a player is injured.
        /// </summary>
        public event EventHandler<InjureEventArgs> PlayerInjured;       //058
        /// <summary>
        /// Occurs when a player triggers  something on another player(in TFC this event may cover medic healings and infections, sentry gun destruction, spy uncovering.etc).
        /// </summary>
        public event EventHandler<PlayerOnPlayerEventArgs> PlayerOnPLayerTriggered;      //059
        /// <summary>
        ///  Occurs when a player triggers an action.
        /// </summary>
        public event EventHandler<PlayerActionEventArgs> PlayerTriggered;        //060
        /// <summary>
        ///  Occurs when a team triggers an action(eg:team winning).
        /// </summary>
        public event EventHandler<TeamActionEventArgs> TeamTriggered;           //061
        /// <summary>
        ///  Occurs when server triggers an action(eg:roundstart,game events).
        /// </summary>
        public event EventHandler<WorldActionEventArgs> WorldTriggered;          //062
        /// <summary>
        ///  Occurs when a player says. 
        /// </summary>
        public event EventHandler<ChatEventArgs> Say;                     //063.1
        /// <summary>
        ///  Occurs when a player uses teamsay.
        /// </summary>
        public event EventHandler<ChatEventArgs> TeamSay;                 //063.2
        /// <summary>
        ///  Occurs when a team forms alliance with another team.
        /// </summary>
        public event EventHandler<TeamAllianceEventArgs> TeamAlliance;            //064
        /// <summary>
        ///  Occurs when Team Score Report is displayed at round end.
        /// </summary>
        public event EventHandler<TeamScoreReportEventArgs> TeamScoreReport;         //065
        /// <summary>
        /// Occurs when a private message is sent.
        /// </summary>
        public event EventHandler<PrivateChatEventArgs> PrivateChat;             //066
        /// <summary>
        /// Occurs when Player Score Report is displayed at round end.
        /// </summary>
        public event EventHandler<PlayerScoreReportEventArgs> PlayerScoreReport;       //067
        /// <summary>
        /// Occurs when Player selects a weapon.
        /// </summary>
        public event EventHandler<WeaponEventArgs> PlayerSelectedWeapon;    //068
        /// <summary>
        /// Occurs when Player acquires a weapon.
        /// </summary>
        public event EventHandler<WeaponEventArgs> PlayerAcquiredWeapon;    //069
        /// <summary>
        /// Occurs when server shuts down.
        /// </summary>
        public event EventHandler<LogEventArgs> Shutdown;                   //new
        /// <summary>
        /// Occurs when a log message cannot be parsed.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Exception;
        /// <summary>
        /// Occurs when a log message is received.
        /// </summary>
        public event EventHandler<LogReceivedEventArgs> LogReceived;
        /// <summary>
        /// Occurs when a log comment is received.
        /// </summary>
        public event EventHandler<CommentReceivedEventArgs> CommentReceived;
       /// <summary>
        /// Initializes LogEvents.
       /// </summary>
        /// <param name="endPoint">server EndPoint.</param>
        protected internal LogEvents(IPEndPoint endPoint)
        {
            ServerEndPoint = endPoint;
            Filters = new LogFilterCollection();
        }
       /// <summary>
       /// Processes received log messages.
       /// </summary>
       /// <param name="logLine"></param>
        protected internal void ProcessLog(string logLine)
        {
            ThrowIfDisposed();
            DateTime Timestamp;
            string info;
            try
            {
                string[] data = LineSplit.Split(logLine, 2);
                Timestamp = DateTime.ParseExact(data[0], "MM/dd/yyyy - HH:mm:ss", CultureInfo.InvariantCulture);
                info = data[1].Remove(data[1].Length - 2);
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", Util.StringToBytes(logLine));
                throw;
            }

            info = ApplyFilters(info);
            if (String.IsNullOrEmpty(info))
                return;

            if (info.StartsWith("//",StringComparison.OrdinalIgnoreCase))
                OnCommentReceive(Timestamp, info);

            OnLogReceive(Timestamp, info);

            string[] result = info.Split(QuoteSplitPattern, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (info[0] == '\"')
                {
                    switch (result[1])
                    {
                        case " connected, address ": OnConnection(Timestamp, result); break;    // 50
                        case " STEAM USERID validated": OnValidation(Timestamp, result); break;           //50b
                        case " entered the game": OnEnterGame(Timestamp, result); break;           //51
                        case " disconnected": OnDisconnection(Timestamp, result); break;                   //52
                        case " committed suicide with ": OnSuicide(Timestamp, result); break;             //53
                        case " joined team ": OnTeamSelection(Timestamp, result); break;                    //54
                        case " changed role to ": OnRoleSelection(Timestamp, result); break;                  //55
                        case " changed name to ": OnNameChange(Timestamp, result); break;                //56
                        case " killed ": OnKill(Timestamp, result); break;                          //57
                        case " attacked ": OnInjure(Timestamp, result); break;                      //58
                        case " triggered ":                                                         //59 ,60
                            {
                                if (result.Length > 3 && result[3] == " against ")
                                    OnPlayer_PlayerAction(Timestamp, result);
                                else
                                    OnPlayerAction(Timestamp, result);
                                break;
                            }
                        case " say ": OnSay(Timestamp, result); break;                                //63a
                        case " say_team ": OnTeamSay(Timestamp, result); break;                          //63b
                        case " tell ": OnPrivateChat(Timestamp, result); break;                                //66
                        case " selected weapon ": OnWeaponSelection(Timestamp, result); break;            //68
                        case " acquired weapon ": OnWeaponPickup(Timestamp, result); break;          //69
                        default: OnException(Timestamp, info); break;

                    }
                }
                else
                {
                    switch (result[0])
                    {
                        case "Server cvars start": OnCvarStart(Timestamp); break;             //001.1
                        case "Server cvar ": OnServerCvar(Timestamp, result); break;            //001.2
                        case "Server cvars end": OnCvarEnd(Timestamp); break;               //001.3
                        case "Log file started (file ": OnLogFileStart(Timestamp, result); break; //002.1
                        case "Log file closed": OnLogFileClose(Timestamp); break;           //002.2
                        case "Loading map ": OnMapLoading(Timestamp, result); break;                //003.1
                        case "Started map ": OnMapStart(Timestamp, result); break;                  //003.2
                        case "Rcon: ": OnRconMsg(Timestamp, result); break;                         //004.1
                        case "Bad Rcon: ": OnRconMsg(Timestamp, result); break;                     //004.2
                        case "Server name is ": OnserverName(Timestamp, result); break;             //005
                        case "Server say ": OnServerSay(Timestamp, result); break;                  //006
                        case "Kick: ": OnKick(Timestamp, result); break;                            //0052b
                        case "Team ":
                            {
                                switch (result[2])
                                {
                                    case " triggered ": OnTeamAction(Timestamp, result); break;     //061
                                    case " formed alliance with team ": OnTeamAlliance(Timestamp, result); break;   //064
                                    case " scored ": OnTeamScoreReport(Timestamp, result); break;        //065
                                }
                                break;

                            }
                        case "World triggered ": OnWorldAction(Timestamp, result); break;       //062
                        case "Player ": OnPlayerAction(Timestamp, result); break;               //60
                        case "Server shutdown": OnShutdown(Timestamp); break;                   //new
                        default: OnException(Timestamp, info); break;
                    }
                }
            }
            catch (Exception)
            {
                Exception.Fire(ServerEndPoint, new ExceptionEventArgs() { Timestamp = Timestamp, Message = info });
            }



        }

        private string ApplyFilters(string logLine)
        {
            ThrowIfDisposed();
           foreach( LogFilter i in Filters )
           {
               if (i.Enabled)
               {
                   if (i.RegexInstance == null)
                       i.RegexInstance = new Regex(i.ToString());

                   switch(i.Action)
                   {
                       case LogFilterAction.Allow :
                           {
                               if (RegIsPlayerMsg.IsMatch(logLine))
                               {
                                   if (i.RegexInstance.IsMatch(logLine) == false)
                                       logLine = string.Empty;
                               }
                               break;
                           }
                       case LogFilterAction.Block :
                           {
                               if (RegIsPlayerMsg.IsMatch(logLine))
                               {
                                   if (i.RegexInstance.IsMatch(logLine))
                                       logLine = string.Empty;
                               }
                               break;
                           }
                   }

               }

           }
           return logLine;
        }
       /// <summary>
       /// Disposes all the resources used by this instance.
       /// </summary>
       /// <param name="disposing"></param>
       protected override void Dispose(bool disposing)
        {
           if(!IsDisposed)
           {
               if(disposing)
               {
                   CvarStartMsg = null;
                   ServerCvar = null;
                   CvarEndMsg = null;
                   LogFileStarted = null;
                   LogFileClosed = null;
                   MapLoaded = null;
                   MapStarted = null;
                   RconMsg = null;
                   ServerName = null;
                   ServerSay = null;
                   PlayerConnected = null;
                   PlayerValidated = null;
                   PlayerEnteredGame = null;
                   PlayerDisconnected = null;
                   PlayerKicked = null;
                   PlayerSuicided = null;
                   PlayerJoinedTeam = null;
                   PlayerChangedRole = null;
                   PlayerChangedName = null;
                   PlayerKilled = null;
                   PlayerInjured = null;
                   PlayerOnPLayerTriggered = null;
                   PlayerTriggered = null;
                   TeamTriggered = null;
                   WorldTriggered = null;
                   Say = null;
                   TeamSay = null;
                   TeamAlliance = null;
                   TeamScoreReport = null;
                   PrivateChat = null;
                   PlayerScoreReport = null;
                   PlayerSelectedWeapon = null;
                   PlayerAcquiredWeapon = null;
                   Shutdown = null;
                   Exception = null;
               }
               base.Dispose(disposing);
               IsDisposed = true;
           }
        }


        /// <summary>
        /// Raises the <see cref="CommentReceived"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="CommentReceived"/> event was fired.</param>
        /// <param name="message">Comment line.</param>
        protected virtual void OnCommentReceive(DateTime timestamp, string message)
        {
            CommentReceived.Fire(ServerEndPoint, new CommentReceivedEventArgs { Timestamp = timestamp, Comment = message.Remove(0, 2) });
        }

        /// <summary>
        /// Raises the <see cref="LogReceived"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which log message was received.</param>
        /// <param name="message">Received log line.</param>
        protected virtual void OnLogReceive(DateTime timestamp, string message)
        {
            LogReceived.Fire(ServerEndPoint, new LogReceivedEventArgs { Timestamp = timestamp, Message = message });
        }


        //Server cvars start    [001.1]
        /// <summary>
        /// Raises the <see cref="CvarStartMsg"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="CvarStartMsg"/> event was fired.</param>
        protected virtual void OnCvarStart(DateTime timestamp)
        {
            CvarStartMsg.Fire(ServerEndPoint, new LogEventArgs() { Timestamp = timestamp });
        }

        //Server cvar "var" = "value"   [001.2]
        /// <summary>
        /// Raises the <see cref="ServerCvar"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="ServerCvar"/> event was fired.</param>
        /// <param name="info">Information about <see cref="ServerCvar"/> event.</param>
        protected virtual void OnServerCvar(DateTime timestamp, string[] info)
        {
            CvarEventArgs eventArgs = new CvarEventArgs()
            {
                Timestamp = timestamp,
                Cvar = info[1],
                Value = info[3]
            };
            ServerCvar.Fire(ServerEndPoint, eventArgs);
        }


        //Server cvars end  [001.3]
        /// <summary>
        /// Raises the <see cref="CvarEndMsg"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="CvarEndMsg"/> event was fired.</param>
        protected virtual void OnCvarEnd(DateTime timestamp)
        {
            CvarEndMsg.Fire(ServerEndPoint, new LogEventArgs() { Timestamp = timestamp });
        }

        //Log file started (file "filename") (game "game") (version "protocol/release/build")   [002.1]
        /// <summary>
        /// Raises the <see cref="LogFileStarted"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="LogFileStarted"/> event was fired.</param>
        /// <param name="info">Information about <see cref="LogFileStarted"/> event.</param>
        protected virtual void OnLogFileStart(DateTime timestamp, string[] info)
        {
            string[] tmp = info[5].Split('/');
            LogStartEventArgs eventArgs = new LogStartEventArgs()
            {
                Timestamp = timestamp,
                FileName = info[1],
                Game = info[3],
                Protocol = tmp[0],
                Release = tmp[1],
                Build = tmp[2]
            };
            LogFileStarted.Fire(ServerEndPoint, eventArgs);
        }

        //Log file closed   [002.2]
        /// <summary>
        /// Raises the <see cref="LogFileClosed"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="LogFileClosed"/> event was fired.</param>
        protected virtual void OnLogFileClose(DateTime timestamp)
        {
            LogFileClosed.Fire(ServerEndPoint, new LogEventArgs() { Timestamp = timestamp });
        }

        //Loading map "map"  [003.1]
        /// <summary>
        /// Raises the <see cref="MapLoaded"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="MapLoaded"/> event was fired.</param>
        /// <param name="info">Information about <see cref="MapLoaded"/> event.</param>
        protected virtual void OnMapLoading(DateTime timestamp, string[] info)
        {
            MapLoadEventArgs eventArgs = new MapLoadEventArgs()
            {
                Timestamp = timestamp,
                MapName = info[1]
            };
            MapLoaded.Fire(ServerEndPoint, eventArgs);
        }
        //Started map "map" (CRC "crc") [003.2]
        /// <summary>
        /// Raises the <see cref="MapStarted"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="MapStarted"/> event was fired.</param>
        /// <param name="info">Information about <see cref="MapStarted"/> event.</param>
        protected virtual void OnMapStart(DateTime timestamp, string[] info)
        {
            MapStartEventArgs eventArgs = new MapStartEventArgs()
            {
                Timestamp = timestamp,
                MapName = info[1],
                MapCRC = info[3]
            };
            MapStarted.Fire(ServerEndPoint, eventArgs);
        }
        //Rcon: "rcon challenge "password" command" from "ip:port"  [004.1]
        //Bad Rcon: "rcon challenge "password" command" from "ip:port"  [004.2]
        /// <summary>
        /// Raises the <see cref="RconMsg"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="RconMsg"/> event was fired.</param>
        /// <param name="info">Information about <see cref="RconMsg"/> event.</param>
        protected virtual void OnRconMsg(DateTime timestamp, string[] info)
        {
            string[] s = info[5].Split(':');
            RconEventArgs eventArgs = new RconEventArgs()
            {
                Timestamp = timestamp,
                IsValid = info[0] == "Rcon: " ? true : false,
                Challenge = info[1].Split(' ')[1],
                Password = info[2],
                Command = info[3],
                Ip = s[0],
                Port = ushort.Parse(s[1],CultureInfo.InvariantCulture)

            };
            RconMsg.Fire(ServerEndPoint, eventArgs);
        }

        //Server name is "hostname" [005]
        /// <summary>
        /// Raises the <see cref="ServerName"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="ServerName"/> event was fired.</param>
        /// <param name="info">Information about <see cref="ServerName"/> event.</param>
        protected virtual void OnserverName(DateTime timestamp, string[] info)
        {
            ServerNameEventArgs eventArgs = new ServerNameEventArgs()
            {
                Timestamp = timestamp,
                Name = info[1]
            };
            ServerName.Fire(ServerEndPoint, eventArgs);
        }
        //Server say "message"  [006]
        /// <summary>
        /// Raises the <see cref="ServerSay"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="ServerSay"/> event was fired.</param>
        /// <param name="info">Information about <see cref="ServerSay"/> event.</param>
        protected virtual void OnServerSay(DateTime timestamp, string[] info)
        {
            ServerSayEventArgs eventArgs = new ServerSayEventArgs()
            {
                Timestamp = timestamp,
                Message = info[1]
            };
            ServerSay.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><>" connected, address "ip:port"  [50]
        /// <summary>
        /// Raises the <see cref="PlayerConnected"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerConnected"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerConnected"/> event.</param>
        protected virtual void OnConnection(DateTime timestamp, string[] info)
        {
            string[] s = info[2].Split(':');
            ConnectEventArgs eventArgs = new ConnectEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Ip = s[0],
                Port = ushort.Parse(s[1],CultureInfo.InvariantCulture)
            };
            PlayerConnected.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><>" STEAM USERID validated   [50b]
        /// <summary>
        /// Raises the <see cref="PlayerValidated"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerValidated"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerValidated"/> event.</param>
        protected virtual void OnValidation(DateTime timestamp, string[] info)
        {
            PlayerEventArgs eventArgs = new PlayerEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
            };
            PlayerValidated.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><>" entered the game [51]
        /// <summary>
        /// Raises the <see cref="PlayerEnteredGame"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerEnteredGame"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerEnteredGame"/> event.</param>
        protected virtual void OnEnterGame(DateTime timestamp, string[] info)
        {
            PlayerEventArgs eventArgs = new PlayerEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
            };
            PlayerEnteredGame.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" disconnected [52]
        /// <summary>
        /// Raises the <see cref="PlayerDisconnected"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerDisconnected"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerDisconnected"/> event.</param>
        protected virtual void OnDisconnection(DateTime timestamp, string[] info)
        {
            PlayerEventArgs eventArgs = new PlayerEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
            };
            PlayerDisconnected.Fire(ServerEndPoint, eventArgs);
        }
        //Kick: "Name<uid><wonid><>" was kicked by "Console" (message "") [52b]
        /// <summary>
        /// Raises the <see cref="PlayerKicked"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerKicked"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerKicked"/> event.</param>
        protected virtual void OnKick(DateTime timestamp, string[] info)
        {
            KickEventArgs eventArgs = new KickEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[1]),
                Kicker = info[3],
                Message = info.Length == 7 ? info[5] : string.Empty
            };
            PlayerKicked.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" committed suicide with "weapon" [53]
        /// <summary>
        /// Raises the <see cref="PlayerSuicided"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerSuicided"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerSuicided"/> event.</param>
        protected virtual void OnSuicide(DateTime timestamp, string[] info)
        {
            SuicideEventArgs eventArgs = new SuicideEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Weapon = info[2]
            };
            PlayerSuicided.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" joined team "team"  [54]
        /// <summary>
        /// Raises the <see cref="PlayerJoinedTeam"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerJoinedTeam"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerJoinedTeam"/> event.</param>
        protected virtual void OnTeamSelection(DateTime timestamp, string[] info)
        {
            TeamSelectionEventArgs eventArgs = new TeamSelectionEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Team = info[2]
            };
            PlayerJoinedTeam.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" changed role to "role"    [55]
        /// <summary>
        /// Raises the <see cref="PlayerChangedRole"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerChangedRole"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerChangedRole"/> event.</param>
        protected virtual void OnRoleSelection(DateTime timestamp, string[] info)
        {
            RoleSelectionEventArgs eventArgs = new RoleSelectionEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Role = info[2]
            };
            PlayerChangedRole.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" changed name to "Name" [56]
        /// <summary>
        /// Raises the <see cref="PlayerChangedName"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerChangedName"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerChangedName"/> event.</param>
        protected virtual void OnNameChange(DateTime timestamp, string[] info)
        {
            NameChangeEventArgs eventArgs = new NameChangeEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                NewName = info[2]
            };
            PlayerChangedName.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" killed "Name<uid><wonid><team>" with "weapon" [57]
        /// <summary>
        /// Raises the <see cref="PlayerKilled"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerKilled"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerKilled"/> event.</param>
        protected virtual void OnKill(DateTime timestamp, string[] info)
        {
            KillEventArgs eventArgs = new KillEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Victim = GetPlayerInfo(info[2]),
                Weapon = info[4]
            };
            PlayerKilled.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" attacked "Name<uid><wonid><team>" with "weapon" (damage "damage") [58]
        /// <summary>
        /// Raises the <see cref="PlayerInjured"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerInjured"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerInjured"/> event.</param>
        protected virtual void OnInjure(DateTime timestamp, string[] info)
        {
            InjureEventArgs eventArgs = new InjureEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Victim = GetPlayerInfo(info[2]),
                Weapon = info[4],
                Damage = info[6]
            };
            PlayerInjured.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" triggered "action" against "Name<uid><wonid><team>" [59]
        /// <summary>
        /// Raises the <see cref="PlayerOnPLayerTriggered"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerOnPLayerTriggered"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerOnPLayerTriggered"/> event.</param>
        protected virtual void OnPlayer_PlayerAction(DateTime timestamp, string[] info)
        {
            PlayerOnPlayerEventArgs eventArgs = new PlayerOnPlayerEventArgs()
            {
                Timestamp = timestamp,
                Source = GetPlayerInfo(info[0]),
                Action = info[2],
                Target = GetPlayerInfo(info[4])
            };
            PlayerOnPLayerTriggered.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" triggered "action" [60]
        /// <summary>
        /// Raises the <see cref="PlayerTriggered"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerTriggered"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerTriggered"/> event.</param>
        protected virtual void OnPlayerAction(DateTime timestamp, string[] info)
        {
            string s = string.Empty;
            if (info.Length > 3)
            {
                for (int i = 3; i < info.Length; i++)
                    s += info[i];
            }
            PlayerActionEventArgs eventArgs = new PlayerActionEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Action = info[2],
                ExtraInfo = s
            };
            PlayerTriggered.Fire(ServerEndPoint, eventArgs);
        }

        //Team "team" triggered "action" [61]
        /// <summary>
        /// Raises the <see cref="TeamTriggered"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="TeamTriggered"/> event was fired.</param>
        /// <param name="info">Information about <see cref="TeamTriggered"/> event.</param>
        protected virtual void OnTeamAction(DateTime timestamp, string[] info)
        {
            TeamActionEventArgs eventArgs = new TeamActionEventArgs()
            {
                Timestamp = timestamp,
                Team = info[1],
                Action = info[3]
            };
            TeamTriggered.Fire(ServerEndPoint, eventArgs);
        }

        //World triggered "action" [62]
        /// <summary>
        /// Raises the <see cref="WorldTriggered"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="WorldTriggered"/> event was fired.</param>
        /// <param name="info">Information about <see cref="WorldTriggered"/> event.</param>
        protected virtual void OnWorldAction(DateTime timestamp, string[] info)
        {
            WorldActionEventArgs eventArgs = new WorldActionEventArgs()
            {
                Timestamp = timestamp,
                Action = info[1]
            };
            WorldTriggered.Fire(ServerEndPoint, eventArgs);
        }
        //"Name<uid><wonid><team>" say "message" [63.1]
        /// <summary>
        /// Raises the <see cref="Say"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="Say"/> event was fired.</param>
        /// <param name="info">Information about <see cref="Say"/> event.</param>
        protected virtual void OnSay(DateTime timestamp, string[] info)
        {
            ChatEventArgs eventArgs = new ChatEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Message = info.Length == 3 ? info[2] : string.Empty
            };
            Say.Fire(ServerEndPoint, eventArgs);
        }

        // "Name<uid><wonid><team>" say_team "message" [63.2]
        /// <summary>
        /// Raises the <see cref="TeamSay"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="TeamSay"/> event was fired.</param>
        /// <param name="info">Information about <see cref="TeamSay"/> event.</param>
        protected virtual void OnTeamSay(DateTime timestamp, string[] info)
        {
            ChatEventArgs eventArgs = new ChatEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Message = info.Length == 3 ? info[2] : string.Empty
            };
            TeamSay.Fire(ServerEndPoint, eventArgs);
        }

        //Team "team" formed alliance with team "team"   [64]
        /// <summary>
        /// Raises the <see cref="TeamAlliance"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="TeamAlliance"/> event was fired.</param>
        /// <param name="info">Information about <see cref="TeamAlliance"/> event.</param>
        protected virtual void OnTeamAlliance(DateTime timestamp, string[] info)
        {
            TeamAllianceEventArgs eventArgs = new TeamAllianceEventArgs()
            {
                Timestamp = timestamp,
                Team1 = info[1],
                Team2 = info[3]
            };
            TeamAlliance.Fire(ServerEndPoint, eventArgs);
        }

        //Team "team" scored "score" with "numplayers" players + extra info  [65]
        /// <summary>
        /// Raises the <see cref="TeamScoreReport"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="TeamScoreReport"/> event was fired.</param>
        /// <param name="info">Information about <see cref="TeamScoreReport"/> event.</param>
        protected virtual void OnTeamScoreReport(DateTime timestamp, string[] info)
        {
            string details = string.Empty;
            if (info.Length > 6)
            {
                for (int i = 6; i < info.Length; i++)
                    details += info[i];
            }
            TeamScoreReportEventArgs eventArgs = new TeamScoreReportEventArgs()
            {
                Timestamp = timestamp,
                Team = info[1],
                Score = info[3],
                PlayerCount = info[5],
                ExtraInfo = details
            };
            TeamScoreReport.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" tell "Name<uid><wonid><team>" message "message"   [66]
        /// <summary>
        /// Raises the <see cref="PrivateChat"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PrivateChat"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PrivateChat"/> event.</param>
        protected virtual void OnPrivateChat(DateTime timestamp, string[] info)
        {
            PrivateChatEventArgs eventArgs = new PrivateChatEventArgs()
            {
                Timestamp = timestamp,
                Sender = GetPlayerInfo(info[0]),
                Receiver = GetPlayerInfo(info[2]),
                Message = info.Length == 5 ? info[4] : string.Empty
            };
            PrivateChat.Fire(ServerEndPoint, eventArgs);
        }

        //Player "Name<uid><wonid><team>" scored "score" + extra info   [67]
        /// <summary>
        /// Raises the <see cref="PlayerScoreReport"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerScoreReport"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerScoreReport"/> event.</param>
        protected virtual void OnPlayerScoreReport(DateTime timestamp, string[] info)
        {
            string details = string.Empty;
            if (info.Length > 4)
            {
                for (int i = 4; i < info.Length; i++)
                    details += info[i];
            }
            PlayerScoreReportEventArgs eventArgs = new PlayerScoreReportEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[1]),
                Score = info[3],
                ExtraInfo = details

            };
            PlayerScoreReport.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" selected weapon "weapon"     [68]
        /// <summary>
        /// Raises the <see cref="PlayerSelectedWeapon"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerSelectedWeapon"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerSelectedWeapon"/> event.</param>
        protected virtual void OnWeaponSelection(DateTime timestamp, string[] info)
        {
            WeaponEventArgs eventArgs = new WeaponEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Weapon = info[2]
            };
            PlayerSelectedWeapon.Fire(ServerEndPoint, eventArgs);
        }

        //"Name<uid><wonid><team>" acquired weapon "weapon"  [69]
        /// <summary>
        /// Raises the <see cref="PlayerSelectedWeapon"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerSelectedWeapon"/> event was fired.</param>
        /// <param name="info">Information about <see cref="PlayerSelectedWeapon"/> event.</param>
        protected virtual void OnWeaponPickup(DateTime timestamp, string[] info)
        {
            WeaponEventArgs eventArgs = new WeaponEventArgs()
            {
                Timestamp = timestamp,
                Player = GetPlayerInfo(info[0]),
                Weapon = info[2]
            };
            PlayerAcquiredWeapon.Fire(ServerEndPoint, eventArgs);
        }
        /// <summary>
        /// Raises the <see cref="PlayerSelectedWeapon"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="PlayerSelectedWeapon"/> event was fired.</param>
        protected virtual void OnShutdown(DateTime timestamp)
        {
            Shutdown.Fire(ServerEndPoint, new LogEventArgs() { Timestamp = timestamp });
        }
        /// <summary>
        /// Raises the <see cref="Exception"/> event.
        /// </summary>
        /// <param name="timestamp">Time at which <see cref="Exception"/> event was fired.</param>
        /// <param name="info">Information about <see cref="Exception"/> event.</param>
        protected virtual void OnException(DateTime timestamp, string info)
        {
            ExceptionEventArgs eventArgs = new ExceptionEventArgs()
            {
                Timestamp = timestamp,
                Message = info
            };
            Exception.Fire(ServerEndPoint, eventArgs);

        }

        //Name<uid><wonid><team>
        private LogPlayerInfo GetPlayerInfo(string s)
        {
            Match match = RegPlayer.Match(s);
            LogPlayerInfo info = new LogPlayerInfo()
            {
                Name = match.Groups[1].Value,
                Uid = match.Groups[2].Value,
                WonId = match.Groups[3].Value,
                Team = match.Groups[4].Value,
            };
            return info;
        }
    }
}
