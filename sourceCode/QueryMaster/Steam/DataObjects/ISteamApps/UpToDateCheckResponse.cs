
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
    /// Contains response of UpToDateCheck method.
    /// </summary>
    [Serializable]
    public class UpToDateCheckResponse : SteamResponse
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
       [JsonProperty("response")]
        public UpToDateCheckResponseResponse ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class UpToDateCheckResponseResponse : DataObject
    {
        /// <summary>
        /// Indicates whether request was successful. 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; internal set; }
        /// <summary>
        /// Indicates whether the given version number is the most current version. 
        /// </summary>
        [JsonProperty("up_to_date")]
        public bool IsUpToUpdate { get; internal set; }
        /// <summary>
        /// Indicates whether the given version can be listed in public changelogs.
        /// </summary>
        [JsonProperty("version_is_listable")]
        public bool IsVersionListable { get; internal set; }
        /// <summary>
        /// Most current version of the app available(Optional).
        /// </summary>
        [JsonProperty("required_version")]
        public int? RequiredVersion { get; internal set; }
        /// <summary>
        /// Status message(Optional).
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; internal set; }
    }
}
