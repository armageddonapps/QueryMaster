
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
using System.Net;
using System.Text;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Represents the ISteamApps interface.
    /// </summary>
    public class ISteamApps:InterfaceBase
    {
        internal ISteamApps()
        {
            Interface = "ISteamApps";
        }
        /// <summary>
        /// Full list of every publicly facing program in the store/library(GetAppList web api method(version 2)).
        /// </summary>
        /// <returns>Instance of <see cref="GetAppListResponse"/>.</returns>
        public GetAppListResponse GetAppList()
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetAppList", Version = 2 };
            return GetParsedResponse<GetAppListResponse>(url);
        }
        /// <summary>
        /// Calls GetServersAtAddress web api method(version 1).
        /// </summary>
        /// <param name="ipEndPoint">IP EndPoint of server.</param>
        /// <returns>Instance of <see cref="GetServersAtAddressResponse"/>.</returns>
        public GetServersAtAddressResponse GetServersAtAddress(IPEndPoint ipEndPoint)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetServersAtAddress", Version = 1 };
            url.Parameters.Add(new Parameter { Name = "addr", Value = ipEndPoint.ToString() });
            return GetParsedResponse<GetServersAtAddressResponse>(url);
        }
        /// <summary>
        /// Calls UpToDateCheck web api method(version 1).
        /// </summary>
        /// <param name="appId">Application Id of the game</param>
        /// <param name="version">The installed version of the game.</param>
        /// <returns>Instance of <see cref="UpToDateCheckResponse"/>.</returns>
        public UpToDateCheckResponse UpToDateCheck(UInt32 appId,UInt32 version)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "UpToDateCheck", Version = 1 };
            url.Parameters.Add(new Parameter { Name = "appid", Value = appId.ToString(CultureInfo.InvariantCulture) });
            url.Parameters.Add(new Parameter { Name = "version", Value = version.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<UpToDateCheckResponse>(url);
        }
      
    }
}
