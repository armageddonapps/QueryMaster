
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
using System.Globalization;
using System.Linq;
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Represents the ISteamUser interface.
    /// </summary>
    public class ISteamUser : InterfaceBase 
    {
        internal ISteamUser()
        {
            Interface = "ISteamUser";
        }

        /// <summary>
        /// Gets the friend list (GetFriendList web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64 bit ID of the user to retrieve a list for.</param>
        /// <param name="relationship">Filter by a given role.</param>
        /// <returns>Instance of <see cref="GetFriendListResponse"/>.</returns>
        /// <remarks>Returns the list of friends if the profile is public or there are entries for the given relationship.</remarks>
        public GetFriendListResponse GetFriendList(ulong steamId,GetFriendListRelationship relationship= GetFriendListRelationship.All)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetFriendList", Version = 1,AppendKey=true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "relationship", Value = relationship.ToString() });
            return GetParsedResponse<GetFriendListResponse>(url);
        }

        /// <summary>
        /// Get Player ban/probation status.
        /// </summary>
        /// <param name="steamIds">SteamIDs</param>
        /// <returns>Instance of <see cref="GetPlayerBansResponse"/>.</returns>
        public GetPlayerBansResponse GetPlayerBans(params ulong[] steamIds)
        {
            if (steamIds.Length==0)
                throw new SteamException("Please pass 64-bit steamid(s)");
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetPlayerBans", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamids", Value = string.Join(",",steamIds) });
            return GetParsedResponse<GetPlayerBansResponse>(url,true);
            
        }
        /// <summary>
        /// Gets Users' profile data(GetPlayerSummaries web api method(version 2)).
        /// </summary>
        /// <param name="steamIds">SteamIDs</param>
        /// <returns>Instance of <see cref="GetPlayerSummariesResponse"/>.</returns>
        public GetPlayerSummariesResponse GetPlayerSummaries(params ulong[] steamIds)
        {
            if (steamIds.Length == 0)
                throw new SteamException("Please pass 64-bit steamid(s)");
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetPlayerSummaries", Version = 2, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamids", Value = string.Join(",", steamIds) });
            return GetParsedResponse<GetPlayerSummariesResponse>(url, jsonConverters: new JsonConverter[] { new StringIpEndPointConverter(), new IntegerUnixTimeStampConverter() });
        }
        /// <summary>
        /// Lists Group ID(s) linked with 64 bit-ID(GetUserGroupList web api method(version 1)).
        /// </summary>
        /// <param name="steamId">SteamID</param>
        /// <returns>Instance of <see cref="GetUserGroupListResponse"/>.</returns>
        public GetUserGroupListResponse GetUserGroupList(ulong steamId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetUserGroupList", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetUserGroupListResponse>(url);
        }
        /// <summary>
        /// Resolve vanity URL parts to a 64 bit ID(ResolveVanityURL web api method(version 1)).
        /// </summary>
        /// <param name="vanityUrl">The user's vanity URL.(eg:-Vanity Url for "http://steamcommunity.com/id/abcd" will be abcd). </param>
        /// <returns>Instance of <see cref="ResolveVanityURLResponse"/>.</returns>
        public ResolveVanityURLResponse ResolveVanityURL(string vanityUrl)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "ResolveVanityURL", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "vanityurl", Value = vanityUrl });
            return GetParsedResponse<ResolveVanityURLResponse>(url);
        }
    }


}
