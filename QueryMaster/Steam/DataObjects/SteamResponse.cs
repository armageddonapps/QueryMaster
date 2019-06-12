
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
    /// Base of all Steam web api method responses.
    /// </summary>
   public class SteamResponse : DataObject
    {
       /// <summary>
       /// Request Url.
       /// </summary>
       public SteamUrl RequestUrl { get; set; }
       /// <summary>
       /// true if parsing was successful; otherwise, false.
       /// </summary>
       public bool IsSuccess { get; internal set; }
       internal string ReceivedResponse { get; set; }
       internal Format ReceivedFormat = Format.Json;
       internal SteamSocket Socket = null;
       /// <summary>
       /// Gets response in json/xml/vdf format.
       /// </summary>
       /// <param name="format"></param>
       /// <returns></returns>
       public virtual string GetRawResponse(Format format)
       {
           if (Socket == null)
               Socket = new SteamSocket();
           string response=string.Empty;
           if (format == ReceivedFormat)
               response = ReceivedResponse;
           else
           {
               RequestUrl.Format = format;
               response = Socket.GetResponse(RequestUrl.ToString());
           }
           return response;
       }
    }
}
