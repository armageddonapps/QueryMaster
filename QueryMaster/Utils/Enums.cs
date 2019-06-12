
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

namespace QueryMaster.Utils
{
    /// <summary>
    /// Steam Universe.
    /// </summary>
    public enum Universe
    {
        /// <summary>
        /// Invalid.
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// Pubic.
        /// </summary>
        Public,
        /// <summary>
        /// Beta.
        /// </summary>
        Beta,
        /// <summary>
        /// Internl.
        /// </summary>
        Internal,
        /// <summary>
        /// Dev
        /// </summary>
        Dev
    }
    /// <summary>
    /// Steam Account type
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Invalid.
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// Individual.
        /// </summary>
        Individual,
        /// <summary>
        /// MultiSeat
        /// </summary>
        MultiSeat,
        /// <summary>
        /// Game Server.
        /// </summary>
        GameServer,
        /// <summary>
        /// Anonymous Game Server
        /// </summary>
        AnonGameServer,
        /// <summary>
        /// Pending.
        /// </summary>
        Pending,
        /// <summary>
        /// Content Server.
        /// </summary>
        ContentServer,
        /// <summary>
        /// Clan.
        /// </summary>
        Clan,
        /// <summary>
        /// Chat.
        /// </summary>
        Chat,
        /// <summary>
        /// Console user.
        /// </summary>
        ConsoleUser,
        /// <summary>
        /// Anonymous user.
        /// </summary>
        AnonUser,
        /// <summary>
        /// Max.
        /// </summary>
        Max,
        /// <summary>
        /// P2PSuperSeeder.
        /// </summary>
        P2PSuperSeeder
    }
    /// <summary>
    /// Steam Instance
    /// </summary>
    public enum Instance
    {
        /// <summary>
        /// Invalid.
        /// </summary>
        Invalid=-1,
        /// <summary>
        /// All.
        /// </summary>
        All=0,
        /// <summary>
        /// Desktop.
        /// </summary>
        Desktop=1,
        /// <summary>
        /// Console.
        /// </summary>
        Console=2,
        /// <summary>
        /// Web.
        /// </summary>
        Web=4,
        
    }
}
