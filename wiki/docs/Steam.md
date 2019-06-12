## Steam
This namespace is an interface to Steam's web api.

This is a c# wrapper for the steam web api found [here](http://steamcommunity.com/dev).
Most of the methods require an api key which you can get it [here](steamcommunity.com/dev/apikey).
With this namespace, users can easily call web api methods without worrying about sockets,parsing etc.

Steam Web Api define some interfaces and each interface contain methods.
Interfaces are represented by classes and the methods in the interface as methods in the classes.

This is still a work in progress.
Currently following interfaces have been implemented,
* IPlayerService
* ISteamApps
* ISteamDirectory
* ISteamNews
* ISteamUser
* ISteamUserStats
* ISteamWebAPIUtil
It also includes an unofficial interface,ISteamGroup that provides information on groups(since official version didn't have an interface/method to get group information).

Methods return an instance of SteamResponse.
It has following properties and methods
IsSuccess : returns true if parsing was successful.
ParsedResponse : the actual parsed response from the method.
RequestUrl : The Url used to query the steam api.
GetRawResponse(Format format) : returns the raw unparsed response.Supports 3 formats: json,xml and vdf(library internally uses json format).

Classes that define different objects of response json string follow this naming convention,
Root Object has a name,<MethodName>+Response (eg:-GetNewsForAppResponse)
Other object has name <Parent class name>+<class Name> (eg:-GetNewsForAppResponseAppNews)

I strongly recommend use of var keyword  instead of actual name.

Sample Code:-
{{
//Get list of all apps and display it in <appid> : <name> format
SteamQuery query = new SteamQuery("<my api key>");
var response = query.ISteamApps.GetAppList();
if(response.IsSuccess)
  Console.WriteLine(String.Join("\n",response.ParsedResponse.Apps.Select(x => x.AppId + "\t:\t" + x.Name)));
Console.ReadLine();
}}

### Want to contribute?
You can contribute by implementing the interfaces not yet implemented.

Credits will be given for your work [here](https://querymaster.codeplex.com/wikipage?title=Steam%20Web%20Api%20Contributors).

All methods in the interface should be implemented.

Use ISteamWebApiUtil.GetSupportedAPIList() method to get list of interfaces.

Check [here](https://querymaster.codeplex.com/wikipage?title=Steam%20Web%20Api%20Contributors) if the interface you selected is already implemented or not.

Click [here](https://querymaster.codeplex.com/wikipage?title=Adding%20new%20interface) for the steps


Read the documentation for details





