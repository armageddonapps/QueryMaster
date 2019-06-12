
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
   internal static class QueryMsg
    {

        internal static readonly byte[] InfoQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 };
        internal static readonly byte[] ObsoleteInfoQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x64, 0x65, 0x74, 0x61, 0x69, 0x6C, 0x73 };

        internal static readonly byte[] RuleChallengeQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x56, 0xFF, 0xFF, 0xFF, 0xFF };
        internal static readonly byte[] PlayerChallengeQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF };

        internal static readonly byte[] PlayerQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 };
        internal static readonly byte[] ObsoletePlayerQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x70, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x73 };

        internal static readonly byte[] RuleQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x56 };
        internal static readonly byte[] ObsoleteRuleQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x72, 0x75, 0x6C, 0x65, 0x73 };

        internal static readonly byte[] ObsoletePingQuery = { 0xFF, 0xFF, 0xFF, 0xFF, 0x69 };


    }
}
