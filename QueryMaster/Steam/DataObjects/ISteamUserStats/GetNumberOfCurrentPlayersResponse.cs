
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
    /// Contains response of GetNumberOfCurrentPlayers method.
    /// </summary>
    [Serializable]
   public class GetNumberOfCurrentPlayersResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("response")]
       public GetNumberOfCurrentPlayersResponseResponse ParsedResponse { get; internal set; }
    }

    [Serializable]
   public class GetNumberOfCurrentPlayersResponseResponse : DataObject
   {
        /// <summary>
        /// Count of players.
        /// </summary>
        [JsonProperty("player_count")]
       public int Count { get; internal set; }
        /// <summary>
        /// Result.
        /// </summary>
        [JsonProperty("result")]
       public int Result { get; internal set; }
   }


}
