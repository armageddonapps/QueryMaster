
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
    /// Contains extra information about the Ship server.
    /// </summary>
    [Serializable]
    public class ShipInfo : DataObject
    {
        /// <summary>
        /// Indicates the game mode.
        /// </summary>
        public ShipMode Mode { get; internal set; }
        /// <summary>
        /// The number of witnesses necessary to have a player arrested. 
        /// </summary>
        public byte Witnesses { get; internal set; }
        /// <summary>
        /// Time (in seconds) before a player is arrested while being witnessed.
        /// </summary>
        public byte Duration { get; internal set; }

    }
}
