
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
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Represents Steam Url.
    /// </summary>
   public class SteamUrl
    {
       /// <summary>
       /// Name of interface.
       /// </summary>
       public string Interface { get; set; }
       /// <summary>
       /// Name of method.
       /// </summary>
       public string Method { get; set; }
       /// <summary>
       /// Version.
       /// </summary>
       public int Version { get; set; }
       /// <summary>
       /// List of parameters.each represent a parameter to the web method.
       /// </summary>
       public List<Parameter> Parameters { get; set; }
       private bool appendKey=false;
       /// <summary>
       /// Whether to append Api key.
       /// </summary>
       public bool AppendKey { 
           get { return appendKey; }
           set 
           {
               if (value && String.IsNullOrWhiteSpace(ApiKey))
                   throw new SteamException("Api key is not set.");
               appendKey = value;
           }
       }
       /// <summary>
       /// Reply format.
       /// </summary>
       [JsonConverter(typeof(StringEnumConverter))]
       public Format Format { get; set; }
       internal static string ApiKey { get; set; }
       internal static readonly string BaseUrl = "http://api.steampowered.com";
       public SteamUrl()
       {
           Parameters = new List<Parameter>();
       }
       /// <summary>
       /// returns string representation of Steam Url.
       /// </summary>
       /// <returns></returns>
       public override string ToString()
       {
           Parameter formatParameter = new Parameter { Name = "format", Value = Format.ToString() };
           Parameters.Add(formatParameter);
           Parameter apiKeyParameter = null;
           if (AppendKey)
           {
               apiKeyParameter = new Parameter { Name = "key", Value = ApiKey };
               Parameters.Add(apiKeyParameter);
           }
           string url = BaseUrl + "/" + Interface + "/" + Method + "/v000" + Version + "/?" + String.Join("&", Parameters.Select(x => x.Name + "=" + x.Value));
           Parameters.Remove(formatParameter);
           if (apiKeyParameter != null)
               Parameters.Remove(apiKeyParameter);
           return url;
       }
    }
}
