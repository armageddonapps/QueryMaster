
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
using System.IO;
using Ionic.BZip2;
using System.Net;
using Ionic.Crc;
using System.Net.Sockets;
using QueryMaster;

namespace QueryMaster.GameServer
{
    internal class UdpQuery : ServerSocket
    {
        private const int SinglePacket = -1;
        private const int MultiPacket = -2;
        private EngineType Type;

        internal UdpQuery(ConnectionInfo conInfo)
            : base(conInfo, ProtocolType.Udp) { }

        internal byte[] GetResponse(byte[] msg, EngineType type,bool isMultiPacket=false)
        {
            int header;
            byte[] recvData = null, parsedData = null;
            Type = type;

            SendData(msg);
            recvData = ReceiveData();
            if(isMultiPacket)
            {
                List<byte> data = new List<byte>();
                data.AddRange(recvData);
                while(true)
                {
                    try
                    {
                        recvData = ReceiveData();
                        data.AddRange(recvData);
                    }
                    catch(SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.TimedOut)
                        {
                            recvData = data.ToArray();
                            break;
                        }
                        else
                        {
                            Dispose();
                            throw;
                        }
                    }
                }
            }
            try
            {
                header = BitConverter.ToInt32(recvData, 0);
                switch (header)
                {
                    case SinglePacket: parsedData = ParseSinglePkt(recvData); break;
                    case MultiPacket: parsedData = ParseMultiPkt(recvData); break;
                    default: throw new InvalidHeaderException("Protocol header is not valid");
                }
            }
            catch (Exception e)
            {
                e.Data.Add("ReceivedData", recvData == null ? new byte[1] : recvData);
                Dispose();
                throw;
            }

            return parsedData;
        }

        private byte[] ParseSinglePkt(byte[] data)
        {
            return data.Skip(4).ToArray();
        }

        private byte[] ParseMultiPkt(byte[] data)
        {
            byte[] parsedData = null;
            switch (Type)
            {
                case EngineType.Source: parsedData = SourcePackets(data); break;
                case EngineType.GoldSource: parsedData = GoldSourcePackets(data); break;
                default: throw new ArgumentException("An invalid EngineType was specified.");
            }

            return parsedData;
        }

        private byte[] GoldSourcePackets(byte[] data)
        {
            byte[] recvData = null;
            List<byte> byteList = null;

            var pktCount = data[8] & 0x0F;

            List<KeyValuePair<int, byte[]>> pktList = new List<KeyValuePair<int, byte[]>>(pktCount);
            pktList.Add(new KeyValuePair<int, byte[]>(data[8] >> 4, data));     
       
            for (int i = 1; i < pktCount; i++)
            {
                recvData = new byte[BufferSize];
                recvData = ReceiveData();
                pktList.Add(new KeyValuePair<int, byte[]>(recvData[8] >> 4, recvData));
            }

            pktList.Sort((x, y) => x.Key.CompareTo(y.Key));
            byteList = new List<byte>();
            byteList.AddRange(pktList[0].Value.Skip(13));

            for (int i = 1; i < pktList.Count; i++)
            {
                byteList.AddRange(pktList[i].Value.Skip(9));
            }

            return byteList.ToArray<byte>();
        }

        private byte[] SourcePackets(byte[] data)
        {           
            bool isCompressed = false;
            int checksum = 0;
            byte[] recvData = null;
            List<byte> recvList = null;
            Parser parser = null;

            byte pktCount = data[8];

            List<KeyValuePair<byte, byte[]>> pktList = new List<KeyValuePair<byte, byte[]>>(pktCount);
            pktList.Add(new KeyValuePair<byte, byte[]>(data[9], data));

            for (int i = 1; i < pktCount; i++)
            {
                recvData = ReceiveData();
                pktList.Add(new KeyValuePair<byte, byte[]>(recvData[9], recvData));
            }

            pktList.Sort((x, y) => x.Key.CompareTo(y.Key));
            recvList = new List<byte>();
            parser = new Parser(pktList[0].Value);
            parser.SkipBytes(4);//header
            if (parser.ReadInt() < 0)//ID
                isCompressed = true;
            parser.ReadByte();//total
            int pktId = parser.ReadByte();// packet id
            parser.ReadUShort();//size
            if (isCompressed)
            {
                parser.SkipBytes(2);//decompressed size of data
                checksum = parser.ReadInt();//Checksum
            }
            recvList.AddRange(parser.GetUnParsedBytes());

            for (int i = 1; i < pktList.Count; i++)
            {
                parser = new Parser(pktList[i].Value);
                parser.SkipBytes(12);//multipacket header 
                recvList.AddRange(parser.GetUnParsedBytes());
            }

            recvData = recvList.ToArray<byte>();
            if (isCompressed)
            {
                recvData = Decompress(recvData);
                if (!IsValid(recvData, checksum))
                    throw new InvalidPacketException("packet's checksum value does not match with the calculated checksum");
            }

            return recvData.Skip(4).ToArray<byte>();
        }

        private byte[] Decompress(byte[] data)
        {
            using (var input = new MemoryStream(data))
            using (var output = new MemoryStream())
            using (var unZip = new BZip2InputStream(input))
            {
                int ch = unZip.ReadByte();

                while (ch != -1)
                {
                    output.WriteByte((byte)ch);
                    ch = unZip.ReadByte();
                }

                output.Flush();

                return output.ToArray();
            }
        }

        private bool IsValid(byte[] data, int Checksum)
        {
            bool isValid;
            using (var Input = new MemoryStream(data))
            {
                isValid = Checksum == new CRC32().GetCrc32(Input);
            }

            return isValid;
        }
    }
}
