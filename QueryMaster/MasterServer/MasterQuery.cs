
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
using System.Net;

namespace QueryMaster.MasterServer
{
    /// <summary>
    /// Provides method(s) to create MasterServer instance.
    /// </summary>
    public static class MasterQuery
    {
        /// <summary>
        /// Master server for Gold Source games.
        /// </summary>
        public static IPEndPoint GoldSrcServerEndPoint = new IPEndPoint(Dns.GetHostAddresses("hl1master.steampowered.com")[0], 27011);
        /// <summary>
        /// Master server for  Source games.
        /// </summary>
        public static IPEndPoint SourceServerEndPoint = new IPEndPoint(Dns.GetHostAddresses("hl2master.steampowered.com")[0], 27011);

        /// <summary>
        /// Gets the appropriate  masterserver query instance
        /// </summary>
        /// <param name="endPoint">Master server endpoint.</param>
        /// <param name="sendTimeout">Sets Socket's SendTimeout Property.</param>
        /// <param name="receiveTimeout">Sets Socket's ReceiveTimeout Property.</param>
        /// <param name="retries">Number of times to retry if first attempt fails.</param>
        /// <param name="attemptCallback">Called on every attempt made to fetch batch.</param>
        /// <returns>Master server instance</returns>
        public static Server GetServerInstance(IPEndPoint endPoint,int sendTimeout=3000,int receiveTimeout=3000,int retries=3,AttemptCallback attemptCallback=null)
        {
            Server server = null;
            ConnectionInfo conInfo = new ConnectionInfo
            {
                SendTimeout = sendTimeout,
                ReceiveTimeout = receiveTimeout,
                Retries = retries,
                EndPoint = endPoint
            };
            server = new Server(conInfo,attemptCallback);
            return server;
        }
    }
}
