
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Contains response of GetServersAtAddress method.
    /// </summary>
    [Serializable]
   public class GetServersAtAddressResponse : SteamResponse
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("response")]
        public GetServersAtAddressResponseResponse ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class GetServersAtAddressResponseResponse : DataObject
    {
        /// <summary>
        /// Returns true if ip address is valid, does not mean server is functioning properly. 
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccess { get; internal set; }
        /// <summary>
        /// Collection of every server from sent ip address. 
        /// </summary>
        [JsonProperty("servers")]
        public QueryMasterCollection<GetServersAtAddressResponseServer> Servers { get; internal set; }
    }

    [Serializable]
    public class GetServersAtAddressResponseServer : DataObject
    {
        /// <summary>
        /// Server endpoint.
        /// </summary>
        [JsonProperty("addr"), JsonConverter(typeof(StringIpEndPointConverter))]
        public IPEndPoint Endpoints { get; internal set; }
        /// <summary>
        /// gmsindex.
        /// </summary>
        [JsonProperty("gmsindex")]
        public int GMSIndex { get; internal set; }
        /// <summary>
        /// Application Id of the game.
        /// </summary>
        [JsonProperty("appid")]
        public uint AppId { get; internal set; }
        /// <summary>
        /// Game Directory.
        /// </summary>
        [JsonProperty("gamedir")]
        public string GameDirectory { get; internal set; }
        /// <summary>
        /// Region of the server.
        /// </summary>
        [JsonProperty("region")]
        public int Region { get; internal set; }
        /// <summary>
        /// Indicates whether the server is secure.
        /// </summary>
        [JsonProperty("secure")]
        public bool IsSecure { get; internal set; }
        /// <summary>
        /// Indicates whether the server is a lan game.
        /// </summary>
        [JsonProperty("lan")]
        public bool IsLan { get; internal set; }
        /// <summary>
        ///  Port number for the server.
        /// </summary>
        [JsonProperty("gameport")]
        public int GamePort { get; internal set; }
        /// <summary>
        /// Spectator port.
        /// </summary>
        [JsonProperty("specport")]
        public int SpecPort { get; internal set; }
    }

}
