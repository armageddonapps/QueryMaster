
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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Parent of all interfaces.
    /// </summary>
   public class InterfaceBase
    {
       /// <summary>
       /// Name of the interface.
       /// </summary>
       internal string Interface { get; set; }
       internal T GetParsedResponse<T>(SteamUrl url,bool AddRootObject=false,params JsonConverter[] jsonConverters) where T:SteamResponse,new()
       {
           string reply = GetResponse(url);
           T response = ParseResponse<T>(reply, AddRootObject, jsonConverters);
           response.RequestUrl = url;
           return response;
       }

       internal T ParseResponse<T>(string reply, bool AddRootObject = false, params JsonConverter[] jsonConverters) where T : SteamResponse, new()
       {
           string jsonString = string.Empty;
           T response = null;
           try
           {
               if (AddRootObject)
               {
                   JObject rootObject = new JObject();
                   rootObject.Add("RootObject", JToken.Parse(reply));
                   jsonString = rootObject.ToString();
               }
               else
                   jsonString = reply;
               response = JsonConvert.DeserializeObject<T>(jsonString, jsonConverters);
               if (response != null)
                   response.IsSuccess = true;
           }
           catch (JsonSerializationException)
           {
               response = new T { IsSuccess = false };
               
           }
           finally
           {
               response.ReceivedResponse = reply;              
               response.Converters = jsonConverters;
           }
           return response;
       }

       internal string GetResponse(SteamUrl url)
       {
          return new SteamSocket().GetResponse(url.ToString());
       }

    }
}
