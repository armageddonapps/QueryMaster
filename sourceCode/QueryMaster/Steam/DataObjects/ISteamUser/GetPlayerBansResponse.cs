
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
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Contains response of GetPlayerBans method.
    /// </summary>
    [Serializable]
    public class GetPlayerBansResponse : SteamResponse
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("RootObject")]
        public GetPlayerBansResponseRootObject ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class GetPlayerBansResponseRootObject : DataObject
    {
        /// <summary>
        /// Collection of <see cref="GetPlayerBansResponsePlayer"/> instances.
        /// </summary>
        [JsonProperty("players")]
        public QueryMasterCollection<GetPlayerBansResponsePlayer> Players { get; internal set; }
    }

    [Serializable]
    public class GetPlayerBansResponsePlayer : DataObject 
    {
        /// <summary>
        /// Player's 64 bit steam ID.
        /// </summary>
        public ulong SteamId { get; internal set; }
        /// <summary>
        /// Indicates whether the player is banned from Community(http://steamcommunity.com/).
        /// </summary>
        [JsonProperty("CommunityBanned")]
        public bool IsCommunityBanned { get; internal set; }
        /// <summary>
        /// Indicates whether the player has VAC bans on record.
        /// </summary>
        [JsonProperty("VACBanned")]
        public bool? IsVACBanned { get; internal set; }
        /// <summary>
        /// Number of bans.
        /// </summary>
        public int? NumberOfVACBans { get; internal set; }
        /// <summary>
        /// Days since last ban.
        /// </summary>
        public int? DaysSinceLastBan { get; internal set; }
        /// <summary>
        /// Number of game bans.
        /// </summary>
        public int? NumberOfGameBans { get; internal set; }
        /// <summary>
        /// Player's ban status in the economy.
        /// </summary>
        public string EconomyBan { get; internal set; }
    }

}
