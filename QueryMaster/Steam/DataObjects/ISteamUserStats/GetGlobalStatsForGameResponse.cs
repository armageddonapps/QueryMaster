
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
    /// Contains response of GetGlobalStatsForGame method.
    /// </summary>
    [Serializable]
   public class GetGlobalStatsForGameResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("response")]
       public GetGlobalStatsForGameResponseResult ParsedResponse { get; internal set; }
    }

    [Serializable]
   public class GetGlobalStatsForGameResponseResult : DataObject
    {
        /// <summary>
        /// Collection of <see cref="GetGlobalStatsForGameResponseStat"/> instances.
        /// </summary>
        [JsonProperty("globalstats"),JsonConverter(typeof(GetGlobalStatsForGameResponseStatConverter))]
       public QueryMasterCollection<GetGlobalStatsForGameResponseStat> Stats { get; internal set; }
    }

    [Serializable]
   public class GetGlobalStatsForGameResponseStat : DataObject
    {
        /// <summary>
        /// The name of the requested stat.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// The value of the stat.
        /// </summary>
        public ulong Total { get; internal set; }
    }

}
