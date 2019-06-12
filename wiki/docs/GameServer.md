# Query Game Server
Create an instance that would represent the server.

{{
//overloaded method with optional parameters.
Server server = ServerQuery.GetServerInstance(EngineType.Source, "<server ip>", <server port>);
}}
Now You can query the server using Server instance.

**Server Information**

{{
//Information sent by server is stored in ServerInfo instance.
ServerInfo info = server.GetInfo();
Console.WriteLine(info.Map);
}}
**Server Rules**

{{
ReadOnlyCollection<Rule> rules = server.GetRules();
foreach (Rule i in rules)
    Console.WriteLine(i.Name + " = " + i.Value);
}}
**Player information**

{{
ReadOnlyCollection<Player> players = server.GetPlayers();
foreach (Player i in players)
    Console.WriteLine("Name : " + i.Name +"\nScore : "+i.Score+"\nTime : "+i.Time);
}}
**Rcon access**

{{
Rcon rcon = server.GetControl("<rcon password>");
if (rcon == null)
{
Console.WriteLine("invalid password");
}
else
{
//send server commmand
string serverResponse= rcon.SendCommand("<server command>");

//add ip-address to server's log address list
rcon.AddlogAddress("<ip>",<port>);

//remove ip-address from server's log address list
rcon.RemovelogAddress("<ip>",<port>);
}
}}
**Logs**

Listen to logs.

{{
Logs logs = server.GetLogs(<local port>);
logs.Listen(LogListener);

static void LogListener(string logMsg)
{
Console.WriteLine(logMsg);
}
}}
Subscribe to logged events.
You can subscribe to 35 different logged events.

{{
//subscribing to say event.
logs.Say += new EventHandler<ChatEventArgs>(logs_Say);

static void logs_Say(object sender, ChatEventArgs e)
{
            Console.WriteLine("Sender :");
            Console.WriteLine("\tName : " + e.Player.Name);
            Console.WriteLine("\tUid : " + e.Player.Uid);
            Console.WriteLine("\tWonid : " + e.Player.WonId);
            Console.WriteLine("\tTeam : " + e.Player.Team);
            Console.WriteLine("Message : " + e.Message);
            Console.WriteLine("Timestamp : " + e.Timestamp);
}
}}

Call dispose method  to free the resources used by Server Instance.

{{
server.Dispose();
}}




