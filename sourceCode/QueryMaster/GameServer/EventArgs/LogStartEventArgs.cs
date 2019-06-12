
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
    /// Provides data for log start event.
    /// </summary>
    [Serializable]
    public class LogStartEventArgs : LogEventArgs
    {
        /// <summary>
        /// Gets Filename.
        /// </summary>
        public string FileName { get; internal set; }
        /// <summary>
        /// Gets Game name.
        /// </summary>
        public string Game { get; internal set; }
        /// <summary>
        /// Gets Protocol version.
        /// </summary>
        public string Protocol { get; internal set; }
        /// <summary>
        /// Gets Release version.
        /// </summary>
        public string Release { get; internal set; }
        /// <summary>
        /// Gets Build version.
        /// </summary>
        public string Build { get; internal set; }
    }
}
