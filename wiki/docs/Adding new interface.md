## Adding new Steam Interface

Steps :-
* Create class with interface name.
* derive it from "InterfaceBase" class.
* Initialize "Interface" property with interface name.
* Create a method with same name as api method.
* Create classes for the response json string.(you may use sites like [this](http://json2csharp.com/) one to do it for u).
* use JSon.net library(already included) to rename all classes as per the naming convention which this library follows.Do same for properties too.
* Rename root object to ParsedResponse.
* Create an instance of SteamUrl using interface name,method name,version and parameters.
* if the method requires api key,then set AppendKey flag of SteamUrl to true.
* pass it to GetParsedResponse method and return its response.


This is an implementaion of IPlayerService interface's GetCommunityBadgeProgress method.

{{
    public class IPlayerService : InterfaceBase 
    {
        internal IPlayerService()
        {
            Interface = "IPlayerService";
        }
        public GetCommunityBadgeProgressResponse GetCommunityBadgeProgress(ulong steamId)
        {
            SteamUrl url = new SteamUrl { Interface = Interface, Method = "GetCommunityBadgeProgress", Version = 1, AppendKey = true };
            url.Parameters.Add(new Parameter { Name = "steamid", Value = steamId.ToString(CultureInfo.InvariantCulture) });
            return GetParsedResponse<GetCommunityBadgeProgressResponse>(url);
        }
   }
}}

