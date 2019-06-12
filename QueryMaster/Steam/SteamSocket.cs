
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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace QueryMaster.Steam
{
   internal class SteamSocket
    {
       internal string GetResponse(string url,string postData="")
       {
           if (string.IsNullOrEmpty(postData))
           {
               string responseStr = string.Empty;
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
               using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
               using (StreamReader reader = new StreamReader(response.GetResponseStream()))
               {
                   responseStr = reader.ReadToEnd();
               }
               return responseStr;
           }
           else
           {
               string responseStr = string.Empty;
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
               request.Method = "POST";
               request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
               using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
               {
                   writer.Write(postData);
               }
               using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
               using (StreamReader reader = new StreamReader(response.GetResponseStream()))
               {
                   responseStr = reader.ReadToEnd();
               }
               return responseStr;
           }
       }

    }
}
