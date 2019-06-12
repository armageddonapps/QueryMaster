
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
using System.Net.Sockets;

namespace QueryMaster.GameServer
{
    internal class TcpQuery : ServerSocket
    {
        private byte[] EmptyPkt = new byte[] { 0x0a, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        internal TcpQuery(ConnectionInfo conInfo)
            : base(conInfo, ProtocolType.Tcp) { }

        internal byte[] GetResponse(byte[] msg)
        {
            byte[] recvData;
            SendData(msg);
            recvData = ReceiveData();//Response value packet
            recvData = ReceiveData();//Auth response packet

            return recvData;
        }

        internal List<byte[]> GetMultiPacketResponse(byte[] msg)
        {
            List<byte[]> recvBytes = new List<byte[]>();
            bool isRemaining = true;
            byte[] recvData;
            SendData(msg);
            SendData(EmptyPkt);//Empty packet
            recvData = ReceiveData();//reply
            recvBytes.Add(recvData);

            do
            {
                recvData = ReceiveData();//may or may not be an empty packet
                if (BitConverter.ToInt32(recvData, 4) == (int)PacketId.Empty)
                    isRemaining = false;
                else
                    recvBytes.Add(recvData);
            } while (isRemaining);

            return recvBytes;
        }
    }
}