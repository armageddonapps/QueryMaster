
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

namespace QueryMaster.GameServer
{
    /// <summary>
    /// Provides methods to access server using rcon password.
    /// </summary>
    public abstract class Rcon: QueryMasterBase
    {
        /// <summary>
        /// Enable logging on server.
        /// </summary>
        public virtual void Enablelogging()
        {
            ThrowIfDisposed();
            SendCommand("log on");
        }
        /// <summary>
        /// Disable logging on server.
        /// </summary>
        public virtual void Disablelogging()
        {
            ThrowIfDisposed();
            SendCommand("log off");
        }

        /// <summary>
        /// Send a Command to server.
        /// </summary>
        /// <param name="cmd">Server command.</param>
        /// <param name="isMultiPacketResponse">Whether the reply could be/is larger than 1400 bytes.</param>
        /// <param name="callback">Called on every attempt made to send a command.</param>
        /// <returns>Reply from server in string format.</returns>
        public abstract string SendCommand(string cmd, bool isMultiPacketResponse=false);
        /// <summary>
        /// Add a client socket to server's logaddress list.
        /// </summary>
        /// <param name="ip">IP-Address of client.</param>
        /// <param name="port">Port number of client.</param>
        public abstract void AddlogAddress(string ip, ushort port);

        /// <summary>
        /// Delete a client socket from server's logaddress list.
        /// </summary>
        /// <param name="ip">IP-Address of client.</param>
        /// <param name="port">Port number of client.</param>
        public abstract void RemovelogAddress(string ip, ushort port);

    }
}
