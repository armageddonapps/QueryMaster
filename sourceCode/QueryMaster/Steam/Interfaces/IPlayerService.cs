
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
    /// Represents the IPlayerService interface.
    /// </summary>
    public class IPlayerService : InterfaceBase 
    {
        internal IPlayerService()
        {
            Interface = "IPlayerService";
        }

        /// <summary>
        /// Gets information about a player's recently played games(GetRecentlyPlayedGames web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64-bit SteamID of the player.</param>
        /// /// <param name="count">The number of games to return.</param>
        /// <returns>Instance of <see cref="GetRecentlyPlayedGamesResponse"/>.</returns>
        public GetRecentlyPlayedGamesResponse GetRecentlyPlayedGames(ulong steamId,uint count=0)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetRecentlyPlayedGames", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "count", Value = count.ToString(CultureInfo.InvariantCulture) });
            GetRecentlyPlayedGamesResponse response = GetParsedResponse<GetRecentlyPlayedGamesResponse>(url);
            if (response.ParsedResponse.Games == null)
                response.ParsedResponse.Games = new QueryMasterCollection<GetRecentlyPlayedGamesResponseGame>(new List<GetRecentlyPlayedGamesResponseGame>());
            else
                foreach (var i in response.ParsedResponse.Games)
                {
                    i.IconUrl = String.IsNullOrWhiteSpace(i.IconUrl) ? string.Empty : "http://media.steampowered.com/steamcommunity/public/images/apps/" + i.AppId + "/" + i.IconUrl + ".jpg";
                    i.LogoUrl = String.IsNullOrWhiteSpace(i.IconUrl) ? string.Empty : "http://media.steampowered.com/steamcommunity/public/images/apps/" + i.AppId + "/" + i.LogoUrl + ".jpg";
                }
            return response;
        }

        /// <summary>
        /// Return a list of games owned by the player(GetOwnedGames web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64 bit SteamID of the player.</param>
        /// <param name="includeAppInfo">Whether or not to include additional details(name, icon) about each game.</param>
        /// <param name="IncludeFreeGames">Whether or not to include free games.</param>
        /// <param name="filters">Restricts results to contain only mentioned appids.</param>
        /// <returns>Instance of <see cref="GetOwnedGamesResponse"/>.</returns>
        public GetOwnedGamesResponse GetOwnedGames(ulong steamId, bool includeAppInfo = true ,bool IncludeFreeGames=true,params uint[] filters)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetOwnedGames", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "include_appinfo", Value = includeAppInfo.ToString() });
            url.Parameters.Add(new Parameter { Name = "include_played_free_games", Value = IncludeFreeGames.ToString() });
            for (int i = 0; i < filters.Length;i++ )
            {
                url.Parameters.Add(new Parameter { Name = "appids_filter[" + i + "]", Value = filters[i].ToString(CultureInfo.InvariantCulture) });
            }
            GetOwnedGamesResponse response = GetParsedResponse<GetOwnedGamesResponse>(url);
            if (response.ParsedResponse.Games == null)
                response.ParsedResponse.Games = new QueryMasterCollection<GetOwnedGamesResponseGame>(new List<GetOwnedGamesResponseGame>());
            else
                foreach (var i in response.ParsedResponse.Games)
                {
                    i.IconUrl = String.IsNullOrWhiteSpace(i.IconUrl) ? string.Empty : "http://media.steampowered.com/steamcommunity/public/images/apps/" + i.AppId + "/" + i.IconUrl + ".jpg";
                    i.LogoUrl = String.IsNullOrWhiteSpace(i.IconUrl) ? string.Empty : "http://media.steampowered.com/steamcommunity/public/images/apps/" + i.AppId + "/" + i.LogoUrl + ".jpg";
                }
            return response;
        }
        /// <summary>
        /// Returns the Steam Level of a user(GetSteamLevel web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64 bit SteamID of the player.</param>
        /// <returns>Instance of <see cref="GetSteamLevelResponse"/>.</returns>
        public GetSteamLevelResponse GetSteamLevel(ulong steamId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetSteamLevel", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetSteamLevelResponse>(url);
        }

        /// <summary>
        /// Gets badges that are owned by a specific user(GetBadges web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64 bit SteamID of the player.</param>
        /// <returns>Instance of <see cref="GetBadgesResponse"/>.</returns>
        public GetBadgesResponse GetBadges(ulong steamId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetBadges", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetBadgesResponse>(url);
        }
        /// <summary>
        /// calls GetCommunityBadgeProgress web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64 bit SteamID of the player.</param>
        /// <returns>Instance of <see cref="GetCommunityBadgeProgressResponse"/>.</returns>
        public GetCommunityBadgeProgressResponse GetCommunityBadgeProgress(ulong steamId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetCommunityBadgeProgress", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetCommunityBadgeProgressResponse>(url);
        }
        /// <summary>
        /// calls IsPlayingSharedGame web api method(version 1)).
        /// </summary>
        /// <param name="steamId">The 64 bit SteamID of the player.</param>
        /// <returns>Instance of <see cref="IsPlayingSharedGameResponse"/>.</returns>
        public IsPlayingSharedGameResponse IsPlayingSharedGame(ulong steamId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "IsPlayingSharedGame", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<IsPlayingSharedGameResponse>(url);
        }

    }
}
