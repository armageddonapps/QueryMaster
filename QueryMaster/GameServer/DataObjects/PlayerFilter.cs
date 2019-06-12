
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
    /// Represents a filter that filters by player.
    /// </summary>
   public class PlayerFilter:LogFilter 
    {
       /// <summary>
       /// Name of the player.
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// User id of the player.
       /// </summary>
       public string  UserId { get; set; }
       /// <summary>
       /// SteamId of the player.
       /// </summary>
       public string SteamId { get; set; }
       /// <summary>
       /// Team in which the player is in.
       /// </summary>
       public string Team { get; set; }
       /// <summary>
       /// Creates a regex filter pattern based on name,userid,steamid and team. 
       /// </summary>
       /// <returns>Regex filter pattern.</returns>
       public override string ToString()
       {
           if(string.IsNullOrEmpty(FilterString))
           {
               StringBuilder strBuilder = new StringBuilder("^.*");
               if (!string.IsNullOrEmpty(Name))
                   strBuilder.Append(Name);
               strBuilder.Append("<");
               if (string.IsNullOrEmpty(UserId))
                   strBuilder.Append("\\d+");
               else
                  strBuilder.Append(UserId);
               strBuilder.Append("><");
               if (string.IsNullOrEmpty(SteamId))
                   strBuilder.Append(".+");
               else
                  strBuilder.Append(SteamId);
               strBuilder.Append("><");
               if (string.IsNullOrEmpty(Team))
                   strBuilder.Append(".*");
               else
                   strBuilder.Append(Team);
               strBuilder.Append(">.*$");
               FilterString = strBuilder.ToString();
           }
  
           return FilterString;
       }
    }
}
