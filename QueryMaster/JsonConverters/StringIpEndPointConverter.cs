
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;

namespace QueryMaster
{
    class StringIpEndPointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPEndPoint));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.ReadAsString();
            //if (value.AsEnumerable().Count(x => x == ':') > 1)
            //{
            //    string[] parts = value.Split(':');

            //}
            if (objectType == typeof(QueryMasterCollection<IPEndPoint>))
            {
                List<IPEndPoint> endPoints = new List<IPEndPoint>();
                while (Regex.Match(value, @"^(\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}):(\d{1,5})$").Success)
                {
                    endPoints.Add(Util.ToIPEndPoint(value));
                    value = reader.ReadAsString();
                    if (String.IsNullOrEmpty(value))
                        break;
                }
                return new QueryMasterCollection<IPEndPoint>(endPoints);
            }
            else if (objectType == typeof(IPEndPoint))
            {
                if (Regex.Match(value, @"^(\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}):(\d{1,5})$").Success)
                {
                    IPEndPoint endPoint = Util.ToIPEndPoint(value);
                    return endPoint;
                }
                
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(QueryMasterCollection<IPEndPoint>))
            {
                var endPoints=value as QueryMasterCollection<IPEndPoint>;
                writer.WriteValue(endPoints.ToString());
            }
            else if (value.GetType() == typeof(IPEndPoint))
            {
                IPEndPoint ip = (IPEndPoint)value;
                writer.WriteValue(ip.ToString());
            }

            
        }
    }
}
