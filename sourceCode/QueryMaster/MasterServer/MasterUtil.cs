
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
using System.Collections.ObjectModel;
using System.Net;
using QueryMaster;
namespace QueryMaster.MasterServer
{
    static class MasterUtil
    {
        private static readonly byte Header = 0x31;
        internal static byte[] BuildPacket(string endPoint, Region region, IpFilter filter)
        {
            List<byte> msg = new List<byte>();
            msg.Add(Header);
            msg.Add((byte)region);
            msg.AddRange(Util.StringToBytes(endPoint));
            msg.Add(0x00);
            if (filter != null)
                msg.AddRange(Util.StringToBytes(ProcessFilter(filter)));
            msg.Add(0x00);
            return msg.ToArray();
        }
        internal static List<IPEndPoint> ProcessPacket(byte[] packet)
        {
            Parser parser = new Parser(packet);
            List<IPEndPoint> endPoints = new List<IPEndPoint>();
            parser.SkipBytes(6);
            int counter = 6;
            string ip = string.Empty; ;
            int port = 0;
            while (counter != packet.Length)
            {
                ip = parser.ReadByte() + "." + parser.ReadByte() + "." + parser.ReadByte() + "." + parser.ReadByte();
                byte portByte1 = parser.ReadByte();
                byte portByte2 = parser.ReadByte();
                if (BitConverter.IsLittleEndian)
                {
                    port = BitConverter.ToUInt16(new byte[] { portByte2, portByte1 }, 0);
                }
                else
                {
                    port = BitConverter.ToUInt16(new byte[] { portByte1, portByte2 }, 0);
                }
                endPoints.Add(new IPEndPoint(IPAddress.Parse(ip), port));
                counter += 6;
            }
            return endPoints;

        }

        internal static string ProcessFilter(IpFilter filter,bool isSubFilter=false)
        {
            StringBuilder filterStr = new StringBuilder();
            if (filter.IsDedicated)
                filterStr.Append(@"\type\d");
            if (filter.IsSecure)
                filterStr.Append(@"\secure\1");
            if (!string.IsNullOrEmpty(filter.GameDirectory))
                filterStr.Append(@"\gamedir\" + filter.GameDirectory);
            if (!string.IsNullOrEmpty(filter.Map))
                filterStr.Append(@"\map\" + filter.Map);
            if (filter.IsLinux)
                filterStr.Append(@"\linux\1");
            if (filter.IsNotEmpty)
                filterStr.Append(@"\empty\1");
            if (filter.IsNotFull)
                filterStr.Append(@"\full\1");
            if (filter.IsProxy)
                filterStr.Append(@"\proxy\1");
            if (filter.NAppId != 0)
                filterStr.Append(@"\napp\" + (ulong)filter.NAppId);
            if (filter.IsNoPlayers)
                filterStr.Append(@"\noplayers\1");
            if (filter.IsWhiteListed)
                filterStr.Append(@"\white\1");
            if (!string.IsNullOrEmpty(filter.Tags))
                filterStr.Append(@"\gametype\" + filter.Tags);
            if (!string.IsNullOrEmpty(filter.HiddenTagsAll))
                filterStr.Append(@"\gamedata\" + filter.HiddenTagsAll);
            if (!string.IsNullOrEmpty(filter.HiddenTagsAny))
                filterStr.Append(@"\gamedataor\" + filter.HiddenTagsAny);
            if (filter.AppId != 0)
                filterStr.Append(@"\appid\" + (ulong)filter.AppId);
            if (!string.IsNullOrEmpty(filter.HostName))
                filterStr.Append(@"\name_match\" + filter.HostName);
            if (!string.IsNullOrEmpty(filter.Version))
                filterStr.Append(@"\version_match\" + filter.Version);
            if (filter.IsUniqueIPAddress)
                filterStr.Append(@"\collapse_addr_hash\1");
            if (!string.IsNullOrEmpty(filter.IPAddress))
                filterStr.Append(@"\gameaddr\" + filter.IPAddress);
            if(filter.ExcludeAny != null)
            {
                filterStr.Append("\0nor");
                filterStr.Append(ProcessFilter(filter.ExcludeAny, true));
            }
            if (filter.ExcludeAll != null)
            {
                filterStr.Append("\0nand");
                filterStr.Append(ProcessFilter(filter.ExcludeAll, true));
            }
            if(!isSubFilter)
            {
                string[] Parts = null;
                string norStr = string.Empty, nandStr = string.Empty;
                Parts = filterStr.ToString().Split('\0');
                filterStr = new StringBuilder(Parts[0]);
                for(int i=1;i<Parts.Length;i++)
                {
                    if (Parts[i].StartsWith("nor",StringComparison.OrdinalIgnoreCase))
                    {
                        norStr+=Parts[i].Substring(3);
                    }
                    if (Parts[i].StartsWith("nand", StringComparison.OrdinalIgnoreCase))
                    {
                        nandStr+=Parts[i].Substring(4);    
                    }
                }
                if(!String.IsNullOrEmpty(norStr))
                {
                    filterStr.Append(@"\nor\");
                    filterStr.Append(norStr.Count(x=>x=='\\')/2);
                    filterStr.Append(norStr);
                }
                if (!String.IsNullOrEmpty(nandStr))
                {
                    filterStr.Append(@"\nand\");
                    filterStr.Append(nandStr.Count(x => x == '\\') / 2);
                    filterStr.Append(nandStr);
                }
            }
            return filterStr.ToString();
        }
    }
}
