
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
    /// Contains response of GetUserStatsForGame method.
    /// </summary>
    [Serializable]
   public class GetUserStatsForGameResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
       [JsonProperty("playerstats")]
       public GetUserStatsForGameResponsePlayerstats ParsedResponse { get; internal set; }
    }
    [Serializable]
    public class GetUserStatsForGameResponsePlayerstats : DataObject
    {
        /// <summary>
        /// 64 bit steam id.
        /// </summary>
        [JsonProperty("steamID")]
        public ulong SteamID { get; internal set; }
        /// <summary>
        /// Game name.
        /// </summary>
        [JsonProperty("gameName")]
        public string GameName { get; internal set; }
        /// <summary>
        ///Collection of <see cref="GetUserStatsForGameResponseStat"/> instances.
        /// </summary>
        [JsonProperty("stats")]
        public QueryMasterCollection<GetUserStatsForGameResponseStat> Stats { get; internal set; }
        /// <summary>
        /// Collection of <see cref="GetUserStatsForGameResponseAchievement"/> instances.
        /// </summary>
        [JsonProperty("achievements")]
        public QueryMasterCollection<GetUserStatsForGameResponseAchievement> Achievements { get; internal set; }
    }
    [Serializable]
   public class GetUserStatsForGameResponseStat : DataObject
   {
       /// <summary>
       /// Name of stat.
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Value of stat.
       /// </summary>
       [JsonProperty("value")]
       public int Value { get; internal set; }
   }
    [Serializable]
   public class GetUserStatsForGameResponseAchievement : DataObject
   {
       /// <summary>
       /// Name of achievement.
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Indicates whether or not the achievement has been unlocked by the user. 
       /// </summary>
       [JsonProperty("achieved")]
       public bool HasAchieved { get; internal set; }
   }

}
