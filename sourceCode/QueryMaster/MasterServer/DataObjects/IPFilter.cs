
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
using System.Net;
using System.Text;

namespace QueryMaster.MasterServer
{
    /// <summary>
    /// Allows you to restrict the results to servers running a certain game.
    /// </summary>
    public class IpFilter : DataObject
    {
        /// <summary>
        /// Servers running dedicated. 
        /// </summary>
        public bool IsDedicated { get; set; }
        /// <summary>
        /// Servers using anti-cheat technology.(eg:-VAC)
        /// </summary>
        public bool IsSecure { get; set; }
        /// <summary>
        /// Servers running the specified modification.(ex. cstrike) 
        /// </summary>
        public string GameDirectory { get; set; }
        /// <summary>
        /// Servers running the specified map. 
        /// </summary>
        public string Map { get; set; }
        /// <summary>
        /// Servers running on a Linux platform. 
        /// </summary>
        public bool IsLinux { get; set; }
        /// <summary>
        /// Servers that are not empty. 
        /// </summary>
        public bool IsNotEmpty { get; set; }
        /// <summary>
        /// Servers that are not full. 
        /// </summary>
        public bool IsNotFull { get; set; }
        /// <summary>
        /// Servers that are spectator proxies. 
        /// </summary>
        public bool IsProxy { get; set; }
        /// <summary>
        /// Servers that are NOT running a game(AppId)(This was introduced to block Left 4 Dead games from the Steam Server Browser).
        /// </summary>
        public Game NAppId { get; set; }
        /// <summary>
        /// Servers that are empty. 
        /// </summary>
        public bool IsNoPlayers { get; set; }
        /// <summary>
        /// Servers that are whitelisted. 
        /// </summary>
        public bool IsWhiteListed { get; set; }
        /// <summary>
        /// Servers with all of the given tag(s) in sv_tags(separated by comma). 
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Servers with all of the given tag(s) in their 'hidden' tags (L4D2)(separated by comma). 
        /// </summary>
        public string HiddenTagsAll { get; set; }
        /// <summary>
        /// Servers with any of the given tag(s) in their 'hidden' tags (L4D2)(separated by comma). 
        /// </summary>
        public string HiddenTagsAny { get; set; }
        /// <summary>
        /// Servers that are running game that has mentioned Application Id.
        /// </summary>
        public Game AppId { get; set; }
        /// <summary>
        /// Servers with the mentioned hostname.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Servers running mentioned version.
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Return only one server for each unique IP address matched.
        /// </summary>
        public bool IsUniqueIPAddress { get; set; }
        /// <summary>
        /// Return only servers on the specified End Point(Port is optional).
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// Exclude servers that match any of the mentioned conditions(Nor filter).
        /// </summary>
        public IpFilter ExcludeAny { get; set; }
        /// <summary>
        /// Exclude servers that match all of the mentioned conditions(Nand filter).
        /// </summary>
        public IpFilter ExcludeAll { get; set; }
    }
}
