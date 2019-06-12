# Query Master Server
Create an instance that would represent the Master Server.

{{
MasterServer server = MasterQuery.GetMasterServerInstance(EngineType.Source);
}}
Now You can  query MasterServer using MasterServer instance.

**Get Server Addresses**

{{
server.GetAddresses(Region.Asia, recv);

static void recv(ReadOnlyCollection<IPEndPoint> endPoints)
{
    foreach (IPEndPoint i in endPoints)
    {
        //"0.0.0.0:0" is the last address 
        if (i.Address.ToString() != "0.0.0.0")
            Console.WriteLine(i);
    }
}
}}

You can filter the result by using IPFilter Instance
eg:-Get CS:Source servers thats running "de_dust2" map in linux environment

{{
server.GetAddresses(Region.Asia, recv, new IpFilter() { GameDirectory="cstrike",IsLinux=true,Map="de_dust2" });
}}

Call dispose method to free the resources used by MasterServer Instance.

{{
server.Dispose();
}}





