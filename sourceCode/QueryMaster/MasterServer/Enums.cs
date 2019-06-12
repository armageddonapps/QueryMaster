
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

namespace QueryMaster.MasterServer
{
    /// <summary>
    /// Specifies the Region.
    /// </summary>
    public enum Region 
    {
        /// <summary>
        /// US East coast 
        /// </summary>
        US_East_coast,
        /// <summary>
        /// 	US West coast 
        /// </summary>
        US_West_coast,
        /// <summary>
        /// South America
        /// </summary>
        South_America,
        /// <summary>
        /// Europe
        /// </summary>
        Europe,
        /// <summary>
        /// Asia
        /// </summary>
        Asia,
        /// <summary>
        /// Australia
        /// </summary>
        Australia,
        /// <summary>
        /// Middle East 
        /// </summary>
        Middle_East,
        /// <summary>
        /// Africa
        /// </summary>
        Africa,
        /// <summary>
        /// Rest of the world 
        /// </summary>
        Rest_of_the_world = 0xFF
    }
}
