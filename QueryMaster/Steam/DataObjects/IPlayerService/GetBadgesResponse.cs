
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
    /// Contains response of GetBadges method.
    /// </summary>
    [Serializable]
    public class GetBadgesResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("response")]
        public GetBadgesResponseResponse ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class GetBadgesResponseBadge : DataObject
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonProperty("badgeid")]
        public uint Id { get; internal set; }
        /// <summary>
        /// Application Id of the game(Optional).
        /// </summary>
        [JsonProperty("appid")]
        public uint AppId { get; internal set; }
        /// <summary>
        /// Level.
        /// </summary>
        [JsonProperty("level")]
        public uint Level { get; internal set; }
        /// <summary>
        /// The date/time when the steam user acquired the badge.
        /// </summary>
        [JsonProperty("completion_time"), JsonConverter(typeof(IntegerUnixTimeStampConverter))]
        public DateTime CompletionTime { get; internal set; }
        /// <summary>
        /// The experience this badge is worth, contributing toward the steam account's player_xp. 
        /// </summary>
        [JsonProperty("xp")]
        public uint Xp { get; internal set; }
        /// <summary>
        /// The amount of people who has this badge. 
        /// </summary>
        [JsonProperty("scarcity")]
        public uint Scarcity { get; internal set; }
        /// <summary>
        /// Provided if the badge relates to an app (trading cards)(Optional).
        /// </summary>
         [JsonProperty("communityitemid ")]
        public uint CommunityItemId { get; internal set; }
        /// <summary>
         /// Provided if the badge relates to an app (trading cards)(Optional).
        /// </summary>
        [JsonProperty("border_color ")]
         public string BorderColor { get; internal set; }
    }

    [Serializable]
    public class GetBadgesResponseResponse : DataObject
    {
        /// <summary>
        /// Collection of <see cref="GetBadgesResponseBadge"/> instances.
        /// </summary>
        [JsonProperty("badges")]
        public QueryMasterCollection<GetBadgesResponseBadge> Badges { get; internal set; }
        /// <summary>
        /// Player Xp.
        /// </summary>
        [JsonProperty("player_xp")]
        public uint PlayerXp { get; internal set; }
        /// <summary>
        /// Player level.
        /// </summary>
        [JsonProperty("player_level")]
        public uint PlayerLevel { get; internal set; }
        /// <summary>
        /// Amount of hp needed to level up.
        /// </summary>
        [JsonProperty("player_xp_needed_to_level_up")]
        public uint PlayerXpNeededToLevelUp { get; internal set; }
        /// <summary>
        /// Amount of hp needed to complete current level.
        /// </summary>
        [JsonProperty("player_xp_needed_current_level")]
        public uint PlayerXpNeededCurrentLevel { get; internal set; }
    }
}
