
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;

namespace QueryMaster
{
    /// <summary>
    /// Wrapper on ReadOnlyCollection that returns its json representation on calling ToString().
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class QueryMasterCollection<T> : ReadOnlyCollection<T>
    {
       /// <summary>
        /// Initializes QueryMasterCollection/>
       /// </summary>
       /// <param name="collection"></param>
       public QueryMasterCollection(IList<T> collection)
           : base(collection) { }
       /// <summary>
       /// Returns Json string.
       /// </summary>
       /// <returns>Json string.</returns>
       public override string ToString()
       {
           if(typeof(T)==typeof(IPEndPoint))
               return JsonConvert.SerializeObject(this, Formatting.Indented,new StringIpEndPointConverter());
           return JsonConvert.SerializeObject(this, Formatting.Indented);
       }
    }
}
