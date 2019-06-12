
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
using QueryMaster;
namespace QueryMaster.GameServer
{
    class RconSource : Rcon
    {
        internal TcpQuery socket;
        private ConnectionInfo ConInfo;

        private RconSource(ConnectionInfo conInfo)
        {
            ConInfo = conInfo;
        }

        internal static Rcon Authorize(ConnectionInfo conInfo, string msg)
        {

            return new QueryMasterBase().Invoke<Rcon>(() =>
                {
                    RconSource obj = new RconSource(conInfo);
                    obj.socket = new TcpQuery(conInfo);
                    byte[] recvData = new byte[50];
                    RconSrcPacket packet = new RconSrcPacket() { Body = msg, Id = (int)PacketId.ExecCmd, Type = (int)PacketType.Auth };
                    recvData = obj.socket.GetResponse(RconUtil.GetBytes(packet));
                    int header;
                    try
                    {
                        header = BitConverter.ToInt32(recvData, 4);
                    }
                    catch (Exception e)
                    {
                        e.Data.Add("ReceivedData", recvData == null ? new byte[1] : recvData);
                        throw;
                    }
                    if (header != -1)
                    {
                        return obj;
                    }
                    return obj;
                }, conInfo.Retries + 1, null, conInfo.ThrowExceptions);
        }

        public override string SendCommand(string command,bool isMultipacketResponse=false)
        {
            ThrowIfDisposed();
            return Invoke<string>(() => sendCommand(command, isMultipacketResponse), 1, null, ConInfo.ThrowExceptions);
        }

        private string sendCommand(string command,bool isMultipacketResponse)
        {
            RconSrcPacket senPacket = new RconSrcPacket() { Body = command, Id = (int)PacketId.ExecCmd, Type = (int)PacketType.Exec };
            List<byte[]> recvData = socket.GetMultiPacketResponse(RconUtil.GetBytes(senPacket));
            StringBuilder str = new StringBuilder();
            try
            {
                for (int i = 0; i < recvData.Count; i++)
                {
                    //consecutive rcon command replies start with an empty packet 
                    if (BitConverter.ToInt32(recvData[i], 4) == (int)PacketId.Empty)
                        continue;
                    if (recvData[i].Length - BitConverter.ToInt32(recvData[i], 0) == 4)
                    {
                        str.Append(RconUtil.ProcessPacket(recvData[i]).Body);
                    }
                    else
                    {
                        str.Append(RconUtil.ProcessPacket(recvData[i]).Body + Util.BytesToString(recvData[++i].Take(recvData[i].Length - 2).ToArray()));
                    }
                }
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvData.SelectMany(x => x).ToArray());
                throw;
            }
            return str.ToString();
        }

        public override void AddlogAddress(string ip, ushort port)
        {
            ThrowIfDisposed();
            SendCommand("logaddress_add " + ip + ":" + port);
        }

        public override void RemovelogAddress(string ip, ushort port)
        {
            ThrowIfDisposed();
            SendCommand("logaddress_del " + ip + ":" + port);
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (socket != null)
                        socket.Dispose();
                }
                base.Dispose(disposing);
                IsDisposed = true;
            }
        }
    }
}
