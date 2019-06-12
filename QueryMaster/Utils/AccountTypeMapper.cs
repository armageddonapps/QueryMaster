
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
   internal class AccountTypeMapper
    {
        private static List<Tuple<int, char, AccountType>> accountTypes = new List<Tuple<int, char, AccountType>>();
        static AccountTypeMapper obj = null;
        internal static AccountTypeMapper Instance { get { if (obj == null)obj = new AccountTypeMapper(); return obj; } }
        AccountTypeMapper()
        {
            accountTypes.Add(new Tuple<int, char, AccountType>(0, 'I', AccountType.Invalid));
            accountTypes.Add(new Tuple<int, char, AccountType>(1, 'U', AccountType.Individual));
            accountTypes.Add(new Tuple<int, char, AccountType>(2, 'M', AccountType.MultiSeat));
            accountTypes.Add(new Tuple<int, char, AccountType>(3, 'G', AccountType.GameServer));
            accountTypes.Add(new Tuple<int, char, AccountType>(4, 'A', AccountType.AnonGameServer));
            accountTypes.Add(new Tuple<int, char, AccountType>(5, 'P', AccountType.Pending));
            accountTypes.Add(new Tuple<int, char, AccountType>(6, 'C', AccountType.ContentServer));
            accountTypes.Add(new Tuple<int, char, AccountType>(7, 'g', AccountType.Clan));
            accountTypes.Add(new Tuple<int, char, AccountType>(8, 'T', AccountType.Chat));
            accountTypes.Add(new Tuple<int, char, AccountType>(9, ' ', AccountType.P2PSuperSeeder));
            accountTypes.Add(new Tuple<int, char, AccountType>(10, 'a', AccountType.AnonUser));
        }

        internal AccountType this[char character]
        {
            get
            {
                if (character == 'c' || character == 'L')
                    character = 'T';
                if(accountTypes.Where(x=>x.Item2==character).Count()>0)
                    return accountTypes.Where(x => x.Item2 == character).First().Item3;
                return AccountType.Invalid;
            }
        }

        internal Char this[AccountType type]
        {
            get 
            {
                if (accountTypes.Where(x => x.Item3 == type).Count() > 0)
                    return accountTypes.Where(x => x.Item3 == type).First().Item2;
                return 'I';
            }
        }

    }
}
