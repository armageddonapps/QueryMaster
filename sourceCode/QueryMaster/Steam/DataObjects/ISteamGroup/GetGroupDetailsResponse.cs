
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
    /// Contains response of GetGroupDetails method.
    /// </summary>
    [Serializable]
   public class GetGroupDetailsResponse : SteamResponse
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("memberList")]
        public GetGroupDetailsResponseMemberList ParsedResponse { get; set; }
        /// <summary>
        /// Gets raw response.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public override string GetRawResponse(Format format)
        {
            switch(format)
            {
                case Format.Json: return string.Empty;
                case Format.Vdf: return string.Empty;
                case Format.Xml: return ReceivedResponse;
                default: return string.Empty;
            }
        }
    }

    [Serializable]
    public class GetGroupDetailsResponseMemberList : DataObject
    {
        /// <summary>
        /// 64 bit steam id of group.
        /// </summary>
        [JsonProperty("groupID64")]
        public string SteamId { get; set; }
        /// <summary>
        /// Group details.
        /// </summary>
        [JsonProperty("groupDetails")]
        public GetGroupDetailsResponseDetails Details { get; set; }
        /// <summary>
        /// Total number of Pages.
        /// </summary>
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Current Page number.
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// 64 bit steamid starting Member.
        /// </summary>
        [JsonProperty("startingMember")]
        public ulong StartingMember { get; set; }
        /// <summary>
        /// Collection of 64 bit steamids of members.
        /// </summary>
        [JsonProperty("steamID64")]
        public QueryMasterCollection<ulong> Members { get; set; }
    }
   
    [Serializable]
    public class GetGroupDetailsResponseDetails : DataObject
    {
        /// <summary>
        /// Group Name(may contain html tags).
        /// </summary>
        [JsonProperty("groupName")]
        public string Name { get; set; }
        /// <summary>
        /// Group Url.
        /// </summary>
        [JsonProperty("groupURL")]
        public string URL { get; set; }
        /// <summary>
        /// Headline(may contain html tags).
        /// </summary>
        [JsonProperty("headline")]
        public string Headline { get; set; }
        /// <summary>
        /// Summary(may contain html tags).
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }
        /// <summary>
        /// Avatar icon.
        /// </summary>
        [JsonProperty("avatarIcon")]
        public string AvatarIcon { get; set; }
        /// <summary>
        /// Avatar Medium.
        /// </summary>
        [JsonProperty("avatarMedium")]
        public string AvatarMedium { get; set; }
        /// <summary>
        /// Avatar Full.
        /// </summary>
        [JsonProperty("avatarFull")]
        public string AvatarFull { get; set; }
        /// <summary>
        /// Total number of members.
        /// </summary>
        [JsonProperty("memberCount")]
        public string MemberCount { get; set; }
        /// <summary>
        /// Count of members in chat.
        /// </summary>
        [JsonProperty("membersInChat")]
        public string MembersInChat { get; set; }
        /// <summary>
        /// Count of members who are in game.
        /// </summary>
        [JsonProperty("membersInGame")]
        public string MembersInGame { get; set; }
        /// <summary>
        /// Count of members who are currently online.
        /// </summary>
        [JsonProperty("membersOnline")]
        public string MembersOnline { get; set; }
    }


}
