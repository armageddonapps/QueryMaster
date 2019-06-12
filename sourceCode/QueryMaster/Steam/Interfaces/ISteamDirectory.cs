
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
    /// Represents the ISteamDirectory interface.
    /// </summary>
   public class ISteamDirectory : InterfaceBase 
    {
       internal ISteamDirectory()
       {
           Interface = "ISteamDirectory";
       }
       /// <summary>
       /// GetCMList web api method(version 1).
       /// </summary>
       /// <param name="cellId">Client's Steam cell ID</param>
       /// <param name="maxCount">Max number of servers to return.</param>
       /// <returns>Instance of <see cref="GetCMListResponse"/>.</returns>
       public GetCMListResponse GetCMList(UInt32 cellId,UInt32 maxCount=10)
       {
           SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetCMList", Version = 1 };
           url.Parameters.Add(new Parameter { Name = "cellid", Value = cellId.ToString(CultureInfo.InvariantCulture) });
           url.Parameters.Add(new Parameter { Name = "maxcount", Value = maxCount.ToString(CultureInfo.InvariantCulture) });
           return GetParsedResponse<GetCMListResponse>(url);
       }
    }
}
