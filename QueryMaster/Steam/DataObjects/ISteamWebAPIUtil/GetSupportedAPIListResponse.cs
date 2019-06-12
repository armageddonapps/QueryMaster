
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
    /// Contains response of GetSupportedAPIList method.
    /// </summary>
    [Serializable]
   public class GetSupportedAPIListResponse : SteamResponse 
    {
        /// <summary>
        /// Parsed response.
        /// </summary>
       [JsonProperty("apilist")]
       public GetSupportedAPIListResponseApilist ParsedResponse { get; internal set; }
    }

    [Serializable]
   public class GetSupportedAPIListResponseApilist : DataObject
   {
       /// <summary>
       /// Collection of <see cref="GetSupportedAPIListResponseInterface"/> instances.
       /// </summary>
       [JsonProperty("interfaces")]
       public QueryMasterCollection<GetSupportedAPIListResponseInterface> Interfaces { get; internal set; }
   }

    [Serializable]
   public class GetSupportedAPIListResponseInterface : DataObject
   {
       /// <summary>
       /// Name of Interface. 
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Collection of <see cref="GetSupportedAPIListResponseMethod"/> instances.
       /// </summary>
       [JsonProperty("methods")]
       public QueryMasterCollection<GetSupportedAPIListResponseMethod> Methods { get; internal set; }
   }

    [Serializable]
   public class GetSupportedAPIListResponseMethod : DataObject
   {
       /// <summary>
       /// Name of method. 
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Version of method.
       /// </summary>
       [JsonProperty("version")]
       public int Version { get; internal set; }
       /// <summary>
       /// Allowed HTTP method for method (GET, POST). 
       /// </summary>
       [JsonProperty("httpmethod")]
       public string HttpMethod { get; internal set; }
       /// <summary>
       /// Collection of <see cref="GetSupportedAPIListResponseParameter"/> instances.
       /// </summary>
       [JsonProperty("parameters")]
       public List<GetSupportedAPIListResponseParameter> Parameters { get; internal set; }
   }

    [Serializable]
   public class GetSupportedAPIListResponseParameter : DataObject
   {
       /// <summary>
       /// Name of parameter. 
       /// </summary>
       [JsonProperty("name")]
       public string Name { get; internal set; }
       /// <summary>
       /// Expected type of value. 
       /// </summary>
       [JsonProperty("type")]
       public string Type { get; internal set; }
       /// <summary>
       /// Is input optional for the method. 
       /// </summary>
       [JsonProperty("optional")]
       public bool IsOptional { get; internal set; }
       /// <summary>
       /// API Documentation of parameter. 
       /// </summary>
       [JsonProperty("description")]
       public string Description { get; internal set; }
   }
}
