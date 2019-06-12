
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
    /// Specifies the type of action filter should perform.
    /// </summary>
    public enum LogFilterAction
    {
        /// <summary>
        /// Allow.
        /// </summary>
        Allow,
        /// <summary>
        /// Block.
        /// </summary>
        Block
    }

    enum ResponseMsgHeader : byte
    {
        A2S_INFO = 0x49,
        A2S_INFO_Obsolete = 0x6D,
        A2S_PLAYER = 0x44,
        A2S_RULES = 0x45,
        A2S_SERVERQUERY_GETCHALLENGE = 0x41
    }

    internal enum PacketId
    {
        Empty = 10,
        ExecCmd = 11
    }

    internal enum PacketType
    {
        Auth = 3,
        AuthResponse = 2,
        Exec = 2,
        ExecResponse = 1
    }
    /// <summary>
    /// Game Server's type.
    /// </summary>
    public enum GameServertype
    {
        /// <summary>
        /// Server returned an invalid value.
        /// </summary>
        Invalid,
        /// <summary>
        /// Dedicated.
        /// </summary>
        Dedicated,
        /// <summary>
        /// Non Dedicated.
        /// </summary>
        NonDedicated,
        /// <summary>
        /// Listen.
        /// </summary>
        Listen,
        /// <summary>
        /// Source TV.
        /// </summary>
        SourceTV,
        /// <summary>
        /// HLTV Server
        /// </summary>
        HLTVServer
    }
    /// <summary>
    /// Server's operating system.
    /// </summary>
    public enum GameEnvironment
    {
        /// <summary>
        /// Server returned an invalid value.
        /// </summary>
        Invalid,
        /// <summary>
        /// Linux.
        /// </summary>
        Linux,
        /// <summary>
        /// Windows.
        /// </summary>
        Windows,
        /// <summary>
        /// Mac.
        /// </summary>
        Mac
    }
    /// <summary>
    /// Ship Mode
    /// </summary>
    public enum ShipMode
    {
        /// <summary>
        /// Server returned an invalid value.
        /// </summary>
        Invalid,
        /// <summary>
        /// Hunt.
        /// </summary>
        Hunt,
        /// <summary>
        /// Elimination.
        /// </summary>
        Elimination,
        /// <summary>
        /// Duel.
        /// </summary>
        Duel,
        /// <summary>
        /// Deathmatch.
        /// </summary>
        Deathmatch,
        /// <summary>
        /// VIP Team.
        /// </summary>
        VIPTeam,
        /// <summary>
        /// Team Elimination.
        /// </summary>
        TeamElimination
    }

}
