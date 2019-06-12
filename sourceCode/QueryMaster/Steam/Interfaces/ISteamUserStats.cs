
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
using System.Xml;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Represents the ISteamUserStats Interface.
    /// </summary>
    public class ISteamUserStats :InterfaceBase 
    {
        internal ISteamUserStats()
        {
            Interface = "ISteamUserStats";
        }
        /// <summary>
        /// Statistics showing how much of the player base have unlocked various achievements(GetGlobalAchievementPercentagesForApp web api method(version 2)).
        /// </summary>
        /// <param name="gameId">The ID of the game to retrieve achievement percentages for. This can be the ID of any Steamworks game with achievements available.</param>
        /// <returns>Instance of <see cref="GetGlobalAchievementPercentagesForAppResponse"/>.</returns>
        public GetGlobalAchievementPercentagesForAppResponse GetGlobalAchievementPercentagesForApp(ulong gameId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetGlobalAchievementPercentagesForApp", Version = 2};
            url.Parameters.Add(new Parameter { Name = "gameid", Value = gameId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetGlobalAchievementPercentagesForAppResponse>(url);
        }
        /// <summary>
        /// Gets global stats(GetGlobalStatsForGame web api method(version 1)).
        /// </summary>
        /// <param name="appId">Application ID</param>
        /// <param name="StartDate">Start date for daily totals (unix epoch timestamp).</param>
        /// <param name="EndDate">End date for daily totals (unix epoch timestamp).</param>
        /// <param name="names">Names of stat to get data for.</param>
        /// <returns>Instance of <see cref="GetGlobalStatsForGameResponse"/>.</returns>
        public GetGlobalStatsForGameResponse GetGlobalStatsForGame(uint appId, UInt32? StartDate = null, UInt32? EndDate = null, params string[] names)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetGlobalStatsForGame", Version = 1 };
            url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "count", Value = names.Length.ToString(CultureInfo.InvariantCulture) });
            for (int i = 0; i < names.Length;i++ )
            {
                url.Parameters.Add(new Parameter { Name = "name[" + i + "]", Value = names[i] });
            }
            if (StartDate != null)
                url.Parameters.Add(new Parameter { Name = "startdate", Value = StartDate.ToString() });
            if (EndDate != null)
                url.Parameters.Add(new Parameter { Name = "enddate", Value = EndDate.ToString() });
            return GetParsedResponse<GetGlobalStatsForGameResponse>(url);
        }

        /// <summary>
        /// Returns the current number of players for an app(GetNumberOfCurrentPlayers web api method(version 1)).
        /// </summary>
        /// <param name="appId">AppID that we're getting user count for.</param>
        /// <returns>Instance of <see cref="GetNumberOfCurrentPlayersResponse"/>.</returns>
        public GetNumberOfCurrentPlayersResponse GetNumberOfCurrentPlayers(UInt32 appId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetNumberOfCurrentPlayers", Version = 1 };
            url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetNumberOfCurrentPlayersResponse>(url);
        }

        /// <summary>
        /// Gets game achievements(GetPlayerAchievements web api method(version 1)).
        /// </summary>
        /// <param name="steamId">64 bit Steam ID.</param>
        /// <param name="appId">Application Id.</param>
        /// <param name="language">localized language to return.</param>
        /// <returns>Instance of <see cref="GetPlayerAchievementsResponse"/>.</returns>
        public GetPlayerAchievementsResponse GetPlayerAchievements(ulong steamId, uint appId, string language = "English")
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetPlayerAchievements", Version = 1,AppendKey=true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "l", Value = language });
            return GetParsedResponse<GetPlayerAchievementsResponse>(url);
        }
        /// <summary>
        /// Gets the game schema(GetSchemaForGame web api method(version 2)).
        /// </summary>
        /// <param name="appId">Application Id.</param>
        /// <param name="language">localized language to return.</param>
        /// <returns>Instance of <see cref="GetSchemaForGameResponse"/>.</returns>
        public GetSchemaForGameResponse GetSchemaForGame(uint appId,string language="English")
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetSchemaForGame", Version = 2, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "l", Value = language });
            return GetParsedResponse<GetSchemaForGameResponse>(url);
        }
        /// <summary>
        /// Gets user's stats for a game(GetUserStatsForGame web api method(version 2)).
        /// </summary>
        /// <param name="steamId">64 bit Steam ID.</param>
        /// <param name="appId">Application Id.</param>
        /// <returns>Instance of <see cref="GetUserStatsForGameResponse"/>.</returns>
        public GetUserStatsForGameResponse GetUserStatsForGame(ulong steamId, uint appId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetUserStatsForGame", Version = 2, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetUserStatsForGameResponse>(url);
        }

    }
}
