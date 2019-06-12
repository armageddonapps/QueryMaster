
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

namespace QueryMaster.Steam
{
    /// <summary>
    /// Contains response of GetFriendList method.
    /// </summary>
    [Serializable]
    public class GetFriendListResponse : SteamResponse
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("friendslist")]
        public GetFriendListResponseFriendsList ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class GetFriendListResponseFriend : DataObject
    {
        /// <summary>
        /// The 64 bit Steam id of the friend.
        /// </summary>
        [JsonProperty("steamid")]
        public ulong SteamId { get; internal set; }
        /// <summary>
        /// Role in relation to the given steamid.
        /// </summary>
        [JsonProperty("relationship"), JsonConverter(typeof(StringEnumConverter))]
        public GetFriendListRelationship Relationship { get; internal set; }
        /// <summary>
        /// The time when friend was added to the list.
        /// </summary>
        [JsonProperty("friend_since"), JsonConverter(typeof(IntegerUnixTimeStampConverter))]
        public DateTime FriendSince { get; internal set; }
    }

    [Serializable]
    public class GetFriendListResponseFriendsList : DataObject
    {
        /// <summary>
        /// Collection of <see cref="GetFriendListResponseFriend"/> instances.
        /// </summary>
        [JsonProperty("friends")]
        public QueryMasterCollection<GetFriendListResponseFriend> Friends { get; internal set; }
    }
}
