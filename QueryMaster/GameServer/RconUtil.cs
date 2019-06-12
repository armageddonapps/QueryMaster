
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
using QueryMaster;
namespace QueryMaster.GameServer
{
    static class RconUtil
    {
        internal static byte[] GetBytes(RconSrcPacket packet)
        {
            byte[] command = Util.StringToBytes(packet.Body);
            packet.Size = 10 + command.Length;
            List<byte> y = new List<byte>(packet.Size + 4);
            y.AddRange(BitConverter.GetBytes(packet.Size));
            y.AddRange(BitConverter.GetBytes(packet.Id));
            y.AddRange(BitConverter.GetBytes(packet.Type));
            y.AddRange(command);
            //part of string
            y.Add(0x00);
            //end terminater
            y.Add(0x00);
            return y.ToArray();
        }

        internal static RconSrcPacket ProcessPacket(byte[] data)
        {
            RconSrcPacket packet = new RconSrcPacket();
            try
            {
                Parser parser = new Parser(data);
                packet.Size = parser.ReadInt();
                packet.Id = parser.ReadInt();
                packet.Type = parser.ReadInt();
                byte[] body = parser.GetUnParsedBytes();
                if (body.Length == 2)
                    packet.Body = string.Empty;
                else
                    packet.Body = Util.BytesToString(body, 0, body.Length - 3);
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", data == null ? new byte[1] : data);
                throw;
            }
            return packet;
        }
    }
}