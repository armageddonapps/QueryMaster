# ServerInfo Update
Information in Server.Extra can be accessed in following manner

{{
Server server = ServerQuery.GetServerInstance(EngineType.Source, "<server ip>", <Server Port>);
ServerInfo info=server.GetInfo();
Console.WriteLine(info.Extra.GameId);//Game id of server
Console.WriteLine(info.Extra.Keywords);//Tags that describe the game according to the server.
Console.WriteLine(info.Extra.Port);//The server's game port number.
Console.WriteLine(info.Extra.SteamID);//Server's SteamID. 
if (info.Extra.SpecInfo != null)
{
Console.WriteLine(info.Extra.SpecInfo.Name);//Spectator port number for SourceTV. 
Console.WriteLine(info.Extra.SpecInfo.Port);//Name of the spectator server for SourceTV. 
}
}}

# Exception handling
The library queries server using the unreliable UDP protocol and parses the received UDP packets.
Exceptions raised during parsing the packets/data would now  include the partially parsed data (byte array) that caused the  exception.
Its recommended to include calls to library methods within try catch block to handle
-Socket time out exceptions.
-bugs(if any) in parsing system.

Recommended way:
{{
try
{
//call to a library method
}
//incase of socket exception,recvdata would be null.
catch (Exception ex)
{
byte[]()() recvData = ex.Data["ReceivedData"](_ReceivedData_) as byte[]()();
if(recvData!=null)
Console.WriteLine(Encoding.UTF8.GetString(recvData));
}
}}

_Note:This is added to find bugs easily._











