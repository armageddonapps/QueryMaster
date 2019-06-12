
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
using System.Net;
using System.Text;

namespace QueryMaster
{
    /// <summary>
    /// References a method to be called when an exception occurs.
    /// </summary>
    /// <param name="ex">Thrown exception.</param>
    public delegate void ErrorCallback(Exception ex);
    /// <summary>
    /// References a method to be called when an attempt to perform some action is made.
    /// </summary>
    /// <param name="attempt"></param>
    public delegate void AttemptCallback(int attempt);
  internal static class Util
    {
        private static Dictionary<string, ushort> GoldSourceGames = new Dictionary<string, ushort>
       {
            {"Counter-Strike",10},
            { "Team Fortress Classic",20},
            { "Day of Defeat",30},
            { "Deathmatch Classic",40},
            { "Opposing Force",50},
            {"Ricochet",60},
            { "Half-Life",70},
            { "Condition Zero",80},
            { "Counter-Strike 1.6 dedicated server",90},
            {"Condition Zero Deleted Scenes",100},
            {"Half-Life: Blue Shift",130},
       };

        internal static  int[] ShipIds = new int[] {2400, 2401, 2402, 2412, 2430, 2406, 2405 };

        internal static ushort GetGameId(string name)
        {
            ushort id = 0;
            if (GoldSourceGames.ContainsKey(name))
                id= GoldSourceGames[name];
            return id;
        }

        internal static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        internal static string BytesToString(byte[] bytes, int index , int count )
        {
            return Encoding.UTF8.GetString(bytes, index, count);
        }

        internal static byte[] StringToBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        internal static byte[] MergeByteArrays(byte[] array1, byte[] array2)
        {
            byte[] newArray = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, newArray, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, newArray, array1.Length, array2.Length);
            return newArray;
        }

        internal static byte[] MergeByteArrays(byte[] array1, byte[] array2, byte[] array3)
        {
            byte[] newArray = new byte[array1.Length + array2.Length + array3.Length];
            Buffer.BlockCopy(array1, 0, newArray, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, newArray, array1.Length, array2.Length);
            Buffer.BlockCopy(array3, 0, newArray, array1.Length + array2.Length, array3.Length);
            return newArray;
        }

        internal static IPEndPoint ToIPEndPoint(string endPointStr)
        {
            IPEndPoint iPEndPoint = null;
            IPAddress address;
            int port;
            string[] endpoints = endPointStr.Split(':');
            if (endpoints.Length == 2 && IPAddress.TryParse(endpoints[0], out address) && int.TryParse(endpoints[1], out port))
                iPEndPoint = new IPEndPoint(address, port);
            return iPEndPoint;
        }



    }
}