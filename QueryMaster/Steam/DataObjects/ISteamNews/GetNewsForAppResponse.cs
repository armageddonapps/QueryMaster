
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
    /// Contains response of GetNewsForApp method.
    /// </summary>
    [Serializable]
    public class GetNewsForAppResponse : SteamResponse
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
        [JsonProperty("appnews")]
        public GetNewsForAppResponseAppNews ParsedResponse { get; internal set; }
    }

    [Serializable]
    public class GetNewsForAppResponseNewsItem : DataObject
    {
        /// <summary>
        /// The unique identifier of the news item.
        /// </summary>
        [JsonProperty("gid")]
        public string GId { get; internal set; }
        /// <summary>
        /// Title of the news item. 
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; internal set; }
        /// <summary>
        /// Permanent link to the item.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; internal set; }
        /// <summary>
        /// Indicates whether the given url is from external website.
        /// </summary>
        [JsonProperty("is_external_url")]
        public bool IsExternalUrl { get; internal set; }
        /// <summary>
        /// The author of the news item.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; internal set; }
        /// <summary>
        /// The article body with a length equal to the given length with an appended ellipsis if it is exceeded. 
        /// </summary>
        [JsonProperty("contents")]
        public string Contents { get; internal set; }
        /// <summary>
        /// The category label of the news item. 
        /// </summary>
        [JsonProperty("feedlabel")]
        public string FeedLabel { get; internal set; }
        /// <summary>
        ///  Date the item was posted.
        /// </summary>
        [JsonProperty("date"),JsonConverter(typeof(IntegerUnixTimeStampConverter))]
        public DateTime Date { get; internal set; }
        /// <summary>
        /// An internal tag that describes the source of the news item.
        /// </summary>
        [JsonProperty("feedname")]
        public string FeedName { get; internal set; }
    }

    [Serializable]
    public class GetNewsForAppResponseAppNews : DataObject
    {
        /// <summary>
        /// The appid of the item news is fetched for.
        /// </summary>
        [JsonProperty("appid")]
        public int AppId { get; internal set; }
        /// <summary>
        /// Collection of <see cref="GetNewsForAppResponseNewsItem"/> describing each news item. 
        /// </summary>
        [JsonProperty("newsitems")]
        public QueryMasterCollection<GetNewsForAppResponseNewsItem> NewsItems { get;internal set; }
    }
}
