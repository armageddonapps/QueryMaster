
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
    /// Contains response of GetSchemaForGame method.
    /// </summary>
    [Serializable]
   public class GetSchemaForGameResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
       [JsonProperty("game")]
       public GetSchemaForGameResponseGame ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class GetSchemaForGameResponseGame : DataObject
    {
        /// <summary>
        /// Steam internal (non-localized) name of game.
        /// </summary>
        [JsonProperty("gameName")]
        public string Name { get; internal set; }
        /// <summary>
        /// Steam release version number currently live on Steam. 
        /// </summary>
        [JsonProperty("gameVersion")]
        public uint Version { get; internal set; }
        /// <summary>
        /// Available game stats(Optional).
        /// </summary>
        [JsonProperty("availableGameStats")]
        public GetSchemaForGameResponseAvailableGameStats AvailableGameStats { get; internal set; }
    }

    [Serializable]
    public class GetSchemaForGameResponseAvailableGameStats : DataObject
    {
        /// <summary>
        /// Collection of <see cref="GetSchemaForGameResponseStat"/> instances(Optional).
        /// </summary>
        [JsonProperty("stats")]
        public QueryMasterCollection<GetSchemaForGameResponseStat> Stats { get; internal set; }
        /// <summary>
        /// Collection of <see cref="GetSchemaForGameResponseAchievement"/> instances(Optional).
        /// </summary>
        [JsonProperty("achievements")]
        public QueryMasterCollection<GetSchemaForGameResponseAchievement> Achievements { get; internal set; }
    }

    [Serializable]
   public class GetSchemaForGameResponseStat : DataObject
   {
       /// <summary>
       /// API name of stat.
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Default value of stat. 
       /// </summary>
       [JsonProperty("defaultvalue")]
       public ulong DefaultValue { get; internal set; }
       /// <summary>
       /// Developer provided name of string. 
       /// </summary>
       [JsonProperty("displayName")]
       public string DisplayName { get; internal set; }
   }

    [Serializable]
   public class GetSchemaForGameResponseAchievement : DataObject
   {
       /// <summary>
       /// API Name of achievement. 
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Always 0 (player's default state is unachieved). 
       /// </summary>
       [JsonProperty("defaultvalue")]
       public int DefaultValue { get; internal set; }
       /// <summary>
       /// Display title string of achievement. 
       /// </summary>
       [JsonProperty("displayName")]
       public string displayName { get; internal set; }
       /// <summary>
       /// If achievement is hidden to the user before earning achievement.
       /// </summary>
       [JsonProperty("hidden")]
       public bool IsHidden { get; internal set; }
       /// <summary>
       /// Display description string of achievement. 
       /// </summary>
       [JsonProperty("description")]
       public string Description { get; internal set; }
       /// <summary>
       /// Absolute URL of earned achievement icon art. 
       /// </summary>
       [JsonProperty("icon")]
       public string Icon { get; internal set; }
       /// <summary>
       /// Absolute URL of un-earned achievement icon art. 
       /// </summary>
       [JsonProperty("icongray")]
       public string IconGray { get; internal set; }
   }



}
