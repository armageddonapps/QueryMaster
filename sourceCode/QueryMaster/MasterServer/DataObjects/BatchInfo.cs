
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
using System.Net;
using System.Text;

namespace QueryMaster.MasterServer
{
    /// <summary>
    /// Contains information on the batch received from master server.
    /// </summary>
    [Serializable]
    public class BatchInfo:DataObject
    {
        internal BatchInfo() 
        { 
            Converters =new JsonConverter[]{new StringIpEndPointConverter()};
        }
        /// <summary>
        /// Master server EndPoint.
        /// </summary>
        public IPEndPoint Source { get; internal set; }
        /// <summary>
        /// Region.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Region Region { get; internal set; }
        /// <summary>
        /// Received Endpoints.
        /// </summary>
        public QueryMasterCollection<IPEndPoint> ReceivedEndpoints { get; internal set; }
        /// <summary>
        /// Whether this is the last batch.
        /// </summary>
        public bool IsLastBatch { get; internal set; }

    }
}
