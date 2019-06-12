using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryMaster
{


    /// <summary>
    /// Specifies the type of engine used by server
    /// </summary>
    public enum EngineType
    {
        /// <summary>
        /// Source Engine
        /// </summary>
        Source,
        /// <summary>
        /// Gold Source Engine
        /// </summary>
        GoldSource
    }

    

    /// <summary>
    /// Specifies the Region
    /// </summary>
    public enum Region : byte
    {
        /// <summary>
        /// US East coast 
        /// </summary>
        US_East_coast,
        /// <summary>
        /// 	US West coast 
        /// </summary>
        US_West_coast,
        /// <summary>
        /// South America
        /// </summary>
        South_America,
        /// <summary>
        /// Europe
        /// </summary>
        Europe,
        /// <summary>
        /// Asia
        /// </summary>
        Asia,
        /// <summary>
        /// Australia
        /// </summary>
        Australia,
        /// <summary>
        /// Middle East 
        /// </summary>
        Middle_East,
        /// <summary>
        /// Africa
        /// </summary>
        Africa,
        /// <summary>
        /// Rest of the world 
        /// </summary>
        Rest_of_the_world = 0xFF
    }

    enum SocketType
    {
        Udp,
        Tcp
    }

    enum ResponseMsgHeader : byte
    {
        A2S_INFO = 0x49,
        A2S_INFO_Obsolete = 0x6D,
        A2S_PLAYER = 0x44,
        A2S_RULES = 0x45,
        A2S_SERVERQUERY_GETCHALLENGE = 0x41,
    }

    //Used in Source Rcon
    enum PacketId
    {
        Empty = 10,
        ExecCmd = 11
    }

    enum PacketType
    {
        Auth = 3,
        AuthResponse = 2,
        Exec = 2,
        ExecResponse = 0
    }







}
