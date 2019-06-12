
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace QueryMaster.Steam
{
    /// <summary>
    /// Represents the ISteamGroup interface(not part of steam's web api).
    /// </summary>
    public class ISteamGroup : InterfaceBase
    {
        XmlDocument doc = new XmlDocument();
       internal ISteamGroup()
       {
           Interface = "ISteamGroup";
       }
        /// <summary>
        /// Gets group details.
        /// </summary>
        /// <param name="groupName">Name of group.</param>
        /// <param name="pageNum">Page Number.</param>
       /// <returns>Instance of <see cref="GetGroupDetailsResponse"/>.</returns>
       public GetGroupDetailsResponse GetGroupDetails(string groupName,int pageNum=1)
       {
           string url = String.Format(CultureInfo.InvariantCulture, @"http://steamcommunity.com/groups/{0}/memberslistxml/?xml=1&p={1}", groupName, pageNum);
          return GetGroupDetails(url);
       }
        /// <summary>
       /// Gets group details.
        /// </summary>
        /// <param name="gId">Gid of the group.</param>
       /// <param name="pageNum">Page Number.</param>
       /// <returns>Instance of <see cref="GetGroupDetailsResponse"/>.</returns>
       public GetGroupDetailsResponse GetGroupDetails(ulong gId, int pageNum = 1)
       {
           string url = String.Format(CultureInfo.InvariantCulture , @"http://steamcommunity.com/gid/{0}/memberslistxml/?xml=1&p={1}", gId, pageNum);
           return GetGroupDetails(url);
       }
        private GetGroupDetailsResponse GetGroupDetails(string url)
       {
           string reply = new SteamSocket().GetResponse(url);
           doc.LoadXml(reply);
           doc.RemoveChild(doc.FirstChild);
           RemoveCData("memberList/groupDetails/groupName");
           RemoveCData("memberList/groupDetails/groupURL");
           RemoveCData("memberList/groupDetails/headline");
           RemoveCData("memberList/groupDetails/summary");
           RemoveCData("memberList/groupDetails/avatarIcon");
           RemoveCData("memberList/groupDetails/avatarMedium");
           RemoveCData("memberList/groupDetails/avatarFull");
           doc.SelectSingleNode("memberList").RemoveChild(doc.SelectSingleNode("memberList/memberCount"));
           doc.SelectSingleNode("memberList").InnerXml += doc.SelectSingleNode("memberList/members").InnerXml;
           doc.SelectSingleNode("memberList").RemoveChild(doc.SelectSingleNode("memberList/members"));
           string jsonString = JsonConvert.SerializeXmlNode(doc);
           GetGroupDetailsResponse response = ParseResponse<GetGroupDetailsResponse>(jsonString);
           response.ReceivedResponse = reply;
           return response;
       }

        private void RemoveCData(string xPath)
        {
            XmlNode node = null;
            node = doc.SelectSingleNode(xPath);
            if(node.FirstChild != null)
                node.InnerText = node.FirstChild.InnerText;
        }

        
    }
}
