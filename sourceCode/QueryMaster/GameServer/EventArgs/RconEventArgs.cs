
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
    /// Provides data for rcon event.
    /// </summary>
    [Serializable]
    public class RconEventArgs : LogEventArgs
    {
        /// <summary>
        /// Gets Challenge Id of remote client.
        /// </summary>
        public string Challenge { get; internal set; }
        /// <summary>
        /// Gets Password.
        /// </summary>
        public string Password { get; internal set; }
        /// <summary>
        /// Gets command sent by remote client.
        /// </summary>
        public string Command { get; internal set; }
        /// <summary>
        /// Gets IP-Address of client.
        /// </summary>
        public string Ip { get; internal set; }
        /// <summary>
        /// Gets Port number of client.
        /// </summary>
        public ushort Port { get; internal set; }
        /// <summary>
        /// Returns true if password sent is valid.
        /// </summary>
        public bool IsValid { get; internal set; }
    }
}
