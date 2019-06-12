
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
using System.Net;
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Contains response of GetPlayerSummaries method.
    /// </summary>
    [Serializable ]
   public class GetPlayerSummariesResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
       [JsonProperty("response")]
        public GetPlayerSummariesResponseResponse ParsedResponse { get; internal set; }
    }

    [Serializable]
   public class GetPlayerSummariesResponsePlayer : DataObject 
   {
        /// <summary>
       /// The user's 64 bit ID.
        /// </summary>
       [JsonProperty("steamid")]
       public ulong SteamId { get; internal set; }
        /// <summary>
       /// Describes the access setting of the profile.
        /// </summary>
       [JsonProperty("communityvisibilitystate"),JsonConverter(typeof(StringEnumConverter))]
       public CommunityVisibilityState CommunityVisibilityState { get; internal set; }
        /// <summary>
        /// Indicates whether user has configured his profile.
        /// </summary>
       [JsonProperty("profilestate")]
       public bool ProfileState { get; internal set; }
        /// <summary>
       /// User's display name.
        /// </summary>
       [JsonProperty("personaname")]
       public string PersonaName { get; internal set; }
        /// <summary>
       /// User's last log off time. 
        /// </summary>
       [JsonProperty("lastlogoff"), JsonConverter(typeof(IntegerUnixTimeStampConverter))]
       public DateTime LastLogOff { get; internal set; }
        /// <summary>
       /// The URL to the user's Steam Community profile. 
        /// </summary>
       [JsonProperty("profileurl")]
       public string ProfileUrl { get; internal set; }
        /// <summary>
       /// The URL to 32x32 image.
        /// </summary>
       [JsonProperty("avatar")]
       public string Avatar { get; internal set; }
        /// <summary>
       /// The URL to 64x64 image.
        /// </summary>
       [JsonProperty("avatarmedium")]
       public string AvatarMedium { get; internal set; }
        /// <summary>
       /// The URL to 184x184 image.
        /// </summary>
       [JsonProperty("avatarfull")]
       public string AvatarFull { get; internal set; }
        /// <summary>
       /// The user's status.
        /// </summary>
       [JsonProperty("personastate"), JsonConverter(typeof(StringEnumConverter))]
       public PersonaState PersonaState { get; internal set; }
        /// <summary>
       /// If present the profile allows public comments(Optional). 
        /// </summary>
       [JsonProperty("commentpermission")]
       public bool? CommentPermission { get; internal set; }
        /// <summary>
       /// The user's real name(Optional).
        /// </summary>
       [JsonProperty("realname")]
       public string RealName { get; internal set; }
        /// <summary>
       /// The 64 bit ID of the user's primary group(Optional).
        /// </summary>
       [JsonProperty("primaryclanid")]
       public ulong? PrimaryClanId { get; internal set; }
        /// <summary>
       /// A unix timestamp of the date the profile was created(Optional).
        /// </summary>
       [JsonProperty("timecreated"), JsonConverter(typeof(IntegerUnixTimeStampConverter))]
       public DateTime? TimeCreated { get; internal set; }
        /// <summary>
       /// ISO 3166 code of where the user is located(Optional). 
        /// </summary>
       [JsonProperty("loccountrycode")]
       public string LocCountryCode { get; internal set; }
        /// <summary>
       /// Variable length code representing the state the user is located in(Optional). 
        /// </summary>
       [JsonProperty("locstatecode ")]
       public string  LocStateCode { get; internal set; }
        /// <summary>
       /// An integer ID internal to Steam representing the user's city(Optional). 
        /// </summary>
       [JsonProperty("loccityid")]
       public string LocCityId { get; internal set; }
        /// <summary>
       /// If the user is in game this will be set to it's app ID as a string(Optional). 
        /// </summary>
       [JsonProperty("gameid")]
       public uint GameId { get; internal set; }
        /// <summary>
       /// The title of the game.
        /// </summary>
       [JsonProperty("gameextrainfo")]
       public string GameExtraInfo { get; internal set; }
        /// <summary>
       /// The server URL given as an IP address and port number separated by a colon(Optional).
        /// </summary>
       [JsonProperty("gameserverip"), JsonConverter(typeof(StringIpEndPointConverter))]
       public IPEndPoint GameServerIp { get; internal set; }
   }

    [Serializable]
   public class GetPlayerSummariesResponseResponse : DataObject 
   {
        /// <summary>
       /// Collection of <see cref="GetPlayerSummariesResponsePlayer"/> instances.
        /// </summary>
       [JsonProperty("players")]
        public QueryMasterCollection<GetPlayerSummariesResponsePlayer> Players { get; internal set; }
   }
}
