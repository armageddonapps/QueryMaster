
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
using System.Globalization;
using System.Linq;
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Represents the ISteamNews interface.
    /// </summary>
   public class ISteamNews : InterfaceBase
    {
       internal ISteamNews()
       {
           Interface = "ISteamNews";
       }
       /// <summary>
       /// Calls GetNewsForApp web api method(version 2).
       /// </summary>
       /// <param name="appId">Game/item to retrieve news for. This can be any valid app ID as seen in the Steam store.</param>
       /// <param name="maxLength">Max length of the contents field.</param>
       /// <param name="endDate">Unix timestamp, returns posts before this date.</param>
       /// <param name="count">Max number of news items to retrieve. Default: 20. </param>
       /// <param name="feeds">Comma-seperated list of feed names to return news for.</param>
       /// <returns>Instance of <see cref="GetNewsForAppResponse"/>.</returns>
       public GetNewsForAppResponse GetNewsForApp(UInt32 appId, UInt32? maxLength = null, UInt32? endDate=null,UInt32? count=null,string feeds=null)
       {
           SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetNewsForApp", Version = 2 };
           url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
           if(maxLength !=null)
               url.Parameters.Add(new Parameter { Name = "maxlength", Value = maxLength.ToString() });
           if(endDate !=null)
               url.Parameters.Add(new Parameter { Name = "enddate", Value = endDate.ToString() });
           if (count != null)
               url.Parameters.Add(new Parameter { Name = "count", Value = count.ToString() });
           if (feeds != null)
               url.Parameters.Add(new Parameter { Name = "feeds", Value = feeds.ToString() });
           return GetParsedResponse<GetNewsForAppResponse>(url);
       }
    }
}
