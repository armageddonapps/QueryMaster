## GameServer
Has methods to query game server,listen to logs remotely and to send commands to server using rcon password.

### Fixes
Main fixes include:-
* Handling multipacket responses from Gold Source Rcon.
* Log messages got truncated in some cases.this has been fixed.
* Obsolete game server's GetInfo() response return an empty string for Environment property in some cases.this has been fixed.
* Replaced Environment,ServerType,TheShip property of ServerInfo  with enums.
* Parsing issues of ExtraInfo present in ServerInfo has been fixed.ExtraInfo property would be set in every case. Hence not need to perform null check.
* The Ping method would now return -1 if server is not reachable/responding.(Its wait time = ReceiveTimeout)

## Features
* Retry functionality has been added. In case of any exception,it would retry to complete the said operation.This applies to GetInfo(),GetRules() and GetPlayers().
* In relation to the new Retry Functionality,Users can pass an AttemptCallback delegate that would be invoked on every attempt made to complete the said operation.This applies to GetInfo(),GetRules() and GetPlayers().
* A new flag has been added(_ThrowExceptions_).If set to false, it wont throw any exception, instead it would return a null value.This applies to GetInfo(),GetRules() and GetPlayers().
* The ToString() method of any object returned by GameServer, would now return a Json string.
* The SendTimeout and ReceiveTimeout is now public.Now it can b changed at anytime.
* Server Instance now exposes server endpoint.

### Rcon Update
The GetControl method now returns a bool value instead of an rcon instance.If rcon password is valid then it would return true and set the Rcon property else false.

sample code :-

{{
if (server.GetControl(rconPassword))
     Console.WriteLine(server.Rcon.SendCommand("status"));
else
     Console.WriteLine("invalid password");
}}

### Log Update
The event mechanism is now separated from the main Log Class.
The new method GetEventsInstance()  would return an instance of LogEvents that encapsulates the event mechanism.
Calling GetEventsInstance method would return a new LogEvents instance.

LogEvents supports filtering.

There are 3 types of filter,
* Player filter : filter by player.
* String filter : filter by string.
* Regex filter : filter by custom pattern.

All these filters have an action property.this can b set to "Allow" or "block" to filter in/out the log message.
Any number of filters can be added.Filtering is applied in the order they were added.
Any filter can be enabled/disabled at any time.
Filters affect all events present in that instance.

LogEvents also has LogReceived event which is same as listen.
Only difference is filtering is applied on it.

eg:-
If i add a playerfilter with name as "abc",then
* if Action is set to "Allow" then,
	* Player related events like PlayerAcquiredWeapon,PlayerInjured,Say would be invoked only for player 'abc'.
	* LogReceived event would be invoked on server messages and messages that were created by player 'abc''s action.
* if Action is set to "Block" then,
	* Player related events like PlayerAcquiredWeapon,PlayerInjured,Say would not be invoked for player 'abc'.
	* LogReceived event would be invoked on server messages and messages that were not created by player 'abc'' action.
sample code :-

{{
var event1 = logs.GetEventsInstance();
event1.Filters.Add(new PlayerFilter { Name = "abc", Action = LogFilterAction.Allow });
//Invoked only when abc says something.
event1.Say += (o, e) => Console.WriteLine(e.Message);
var event2 = logs.GetEventsInstance();
event2.Filters.Add(new PlayerFilter { Name = "xyz", Action = LogFilterAction.Allow });
//Invoked only when xyz says something.
event2.Say += (o, e) => Console.WriteLine(e.Message);
}}


Read documentation for more details.





