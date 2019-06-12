
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
using QueryMaster;

namespace QueryMaster.GameServer
{
    /// <summary>
    /// Provides methods to create Server instance.
    /// </summary>
    public static class ServerQuery
    {
        /// <summary>
        /// Returns an object that represents the server
        /// </summary>
        /// <param name="type">Base engine which game uses.</param>
        /// <param name="ip">IP-Address of server.</param>
        /// <param name="port">Port number of server.</param>
        /// <param name="isObsolete">Obsolete Gold Source servers reply only to half life protocol.if set to true then it would use half life protocol.If set to null,then protocol is identified at runtime[Default : false].</param>
        /// <param name="sendTimeout">Sets Socket's SendTimeout Property.</param>
        /// <param name="receiveTimeout">Sets Socket's ReceiveTimeout.</param>
        /// <param name="retries">Number of times to retry if first attempt fails.</param>
        /// <param name="throwExceptions">Whether to throw any exceptions.</param>
        /// <returns>Instance of server class that represents the connected server.</returns>
        public static Server GetServerInstance(EngineType type, string ip, ushort port, bool? isObsolete = false, int sendTimeout = 3000, int receiveTimeout = 3000, int retries= 3,bool throwExceptions=false)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            return GetServerInstance(type, endPoint, isObsolete, sendTimeout, receiveTimeout, retries, throwExceptions);
        }

        /// <summary>
        /// Returns an object that represents the server.
        /// </summary>
        /// <param name="type">Base engine which game uses.</param>
        /// <param name="endPoint">Socket address of server.</param>
        /// <param name="isObsolete">Obsolete Gold Source servers reply only to half life protocol.if set to true then it would use half life protocol.If set to null,then protocol is identified at runtime.</param>
        /// <param name="sendTimeout">Sets Socket's SendTimeout Property.</param>
        /// <param name="receiveTimeout">Sets Socket's ReceiveTimeout.</param>
        /// <param name="retries">Number of times to retry if first attempt fails.</param>
        /// <param name="throwExceptions">Whether to throw any exceptions.</param>
        /// <returns>Instance of server class that represents the connected server</returns>
        public static Server GetServerInstance(EngineType type, IPEndPoint endPoint, bool? isObsolete = false, int sendTimeout = 3000, int receiveTimeout = 3000, int retries = 3, bool throwExceptions = false)
        {
            Server server = null;
            ConnectionInfo conInfo = new ConnectionInfo
            {
                SendTimeout = sendTimeout,
                ReceiveTimeout = receiveTimeout,
                EndPoint=endPoint,
                Retries=retries,
                ThrowExceptions=throwExceptions
            };
            switch (type)
            {
                case EngineType.GoldSource: server = new GoldSource(conInfo, isObsolete); break;
                case EngineType.Source: server = new Source(conInfo); break;
                default: throw new ArgumentException("An invalid EngineType was specified.");
            }
            return server;
        }

        /// <summary>
        /// Returns an object that represents the server
        /// </summary>
        /// <param name="game">Name of game</param>
        /// <param name="endPoint">Socket address of server</param>
        /// <param name="isObsolete">Obsolete Gold Source servers reply only to half life protocol.if set to true then it would use half life protocol.If set to null,then protocol is identified at runtime.</param>
        /// <param name="sendTimeout">Sets Socket's SendTimeout Property.</param>
        /// <param name="receiveTimeout">Sets Socket's ReceiveTimeout.</param>
        /// <param name="retries">Number of times to retry if first attempt fails.</param>
        /// <param name="throwExceptions">Whether to throw any exceptions.</param>
        /// <returns>Instance of server class that represents the connected server</returns>
        public static Server GetServerInstance(Game game, IPEndPoint endPoint, bool? isObsolete = false, int sendTimeout = 3000, int receiveTimeout = 3000, int retries = 3, bool throwExceptions = false)
        {
            if ((int)game <= 130)
                return GetServerInstance(EngineType.GoldSource, endPoint, isObsolete, sendTimeout, receiveTimeout, retries, throwExceptions);
            else
                return GetServerInstance(EngineType.Source, endPoint, isObsolete, sendTimeout, receiveTimeout, retries, throwExceptions);
        }

        /// <summary>
        /// Returns an object that represents the server
        /// </summary>
        /// <param name="game">Name of game</param>
        /// <param name="ip">IP-Address of server</param>
        /// <param name="port">Port number of server</param>
        /// <param name="isObsolete">Obsolete Gold Source servers reply only to half life protocol.if set to true then it would use half life protocol.If set to null,then protocol is identified at runtime.</param>
        /// <param name="sendTimeout">Sets Socket's SendTimeout Property.</param>
        /// <param name="receiveTimeout">Sets Socket's ReceiveTimeout.</param>
        /// <param name="retries">Number of times to retry if first attempt fails.</param>
        /// <param name="throwExceptions">Whether to throw any exceptions.</param>
        /// <returns>Instance of server class that represents the connected server</returns>
        public static Server GetServerInstance(Game game, string ip, ushort port, bool? isObsolete = false, int sendTimeout = 3000, int receiveTimeout = 3000, int retries = 3, bool throwExceptions = false)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            if ((int)game <= 130)
                return GetServerInstance(EngineType.GoldSource, endPoint, isObsolete, sendTimeout, receiveTimeout, retries, throwExceptions);
            else
                return GetServerInstance(EngineType.Source, endPoint, isObsolete, sendTimeout, receiveTimeout, retries, throwExceptions);
        }

    }
}
