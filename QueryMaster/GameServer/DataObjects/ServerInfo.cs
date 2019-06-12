
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryMaster.GameServer
{
    /// <summary>
    /// Contains information about the server.
    /// </summary>
    [Serializable]
    public class ServerInfo : DataObject
    {
        /// <summary>
        /// Returns true if server replies with Obsolete response format.
        /// </summary>
        public bool IsObsolete { get; internal set; }
        /// <summary>
        /// Socket address of server.
        /// </summary>
        public string Address { get; internal set; }
        /// <summary>
        /// Protocol version used by the server. 
        /// </summary>
        public byte Protocol { get; internal set; }
        /// <summary>
        /// Name of the server. 
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Map the server has currently loaded. 
        /// </summary>
        public string Map { get; internal set; }
        /// <summary>
        /// Name of the folder containing the game files. 
        /// </summary>
        public string Directory { get; internal set; }
        /// <summary>
        /// Full name of the game. 
        /// </summary>
        public string Description { get; internal set; }
        /// <summary>
        /// Steam Application ID of game. 
        /// </summary>
        public ushort Id { get; internal set; }
        /// <summary>
        /// Number of players on the server. 
        /// </summary>
        public int Players { get; internal set; }
        /// <summary>
        /// Maximum number of players the server reports it can hold. 
        /// </summary>
        public byte MaxPlayers { get; internal set; }
        /// <summary>
        /// Number of bots on the server. 
        /// </summary>
        public byte Bots { get; internal set; }
        /// <summary>
        /// Indicates the type of server.(Dedicated/Non-dedicated/Proxy)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GameServertype ServerType { get; internal set; }
        /// <summary>
        /// Indicates the operating system of the server.(Linux/Windows/Mac)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GameEnvironment Environment { get; internal set; }
        /// <summary>
        /// Indicates whether the server requires a password.
        /// </summary>
        public bool IsPrivate { get; internal set; }
        /// <summary>
        /// Specifies whether the server uses VAC.
        /// </summary>
        public bool IsSecure { get; internal set; }
        /// <summary>
        /// Version of the game installed on the server. 
        /// </summary>
        public string GameVersion { get; internal set; }
        /// <summary>
        /// Round-trip delay time.
        /// </summary>
        public long Ping { get; internal set; }
        /// <summary>
        /// Additional information provided by server.
        /// </summary>
        public ExtraInfo ExtraInfo { get; internal set; }
        /// <summary>
        /// Valid only if the server is running The Ship. 
        /// </summary>
        public ShipInfo ShipInfo { get; internal set; }
        /// <summary>
        /// Indicates whether the game is a mod(Halflife/HalfLifeMod).
        /// </summary>
        /// <remarks>Present only  in Obsolete server responses.</remarks>
        public bool IsModded { get; internal set; }
        /// <summary>
        /// Valid only if the game is a mod(i.e. if IsModded = true).
        /// </summary>
        /// <remarks>Present only in Obsolete server responses.</remarks>
        public ModInfo ModInfo { get; internal set; }

    }
}
