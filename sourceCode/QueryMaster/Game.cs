
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
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QueryMaster
{
    /// <summary>
    /// Specifies the game.
    /// </summary>
    public enum Game
    {
        //Gold Source Games
        /// <summary>
        /// Counter-Strike
        /// </summary>
        [Description("Counter-Strike")]
        CounterStrike = 10,
        /// <summary>
        /// Team Fortress Classic
        /// </summary>
        [Description("Team Fortress Classic")]
        Team_Fortress_Classic = 20,
        /// <summary>
        /// Day Of Defeat
        /// </summary>
        [Description("Day of Defeat")]
        Day_Of_Defeat = 30,
        /// <summary>
        /// Deathmatch Classic
        /// </summary>
        [Description("Deathmatch Classic")]
        Deathmatch_Classic = 40,
        /// <summary>
        /// Half-Life: Opposing Force
        /// </summary>
        [Description("Half-Life: Opposing Force")]
        Opposing_Force = 50,
        /// <summary>
        /// Ricochet
        /// </summary>
        [Description("Ricochet")]
        Ricochet = 60,
        /// <summary>
        /// Half-Life
        /// </summary>
        [Description("Half-Life")]
        Half_Life = 70,
        /// <summary>
        /// Condition Zero
        /// </summary>
        [Description("Condition Zero")]
        Condition_Zero = 80,
        /// <summary>
        /// Counter-Strike 1.6 dedicated server
        /// </summary>
        [Description("Counter-Strike 1.6 dedicated server")]
        CounterStrike_1_6_dedicated_server = 90,
        /// <summary>
        /// Condition Zero Deleted Scenes
        /// </summary>
        [Description("Condition Zero Deleted Scenes")]
        Condition_Zero_Deleted_Scenes = 100,
        /// <summary>
        /// Half-Life: Blue Shift
        /// </summary>
        [Description("Half-Life: Blue Shift")]
        Half_Life_Blue_Shift = 130,
        //Source Games
        /// <summary>
        /// Half-Life 2
        /// </summary>
        [Description("Half-Life 2")]
        Half_Life_2 = 220,
        /// <summary>
        /// Counter-Strike: Source
        /// </summary>
        [Description("Counter-Strike: Source")]
        CounterStrike_Source = 240,
        /// <summary>
        /// Half-Life: Source
        /// </summary>
        [Description("Half-Life: Source")]
        Half_Life_Source = 280,
        /// <summary>
        /// Day of Defeat: Source
        /// </summary>
        [Description("Day of Defeat: Source")]
        Day_Of_Defeat_Source = 300,
        /// <summary>
        /// Half-Life 2: Deathmatch
        /// </summary>
        [Description("Half-Life 2: Deathmatch")]
        Half_Life_2_Deathmatch = 320,
        /// <summary>
        /// Half-Life 2: Lost Coast
        /// </summary>
        [Description("Half-Life 2: Lost Coast")]
        Half_Life_2_Lost_Coast = 340,
        /// <summary>
        /// Half-Life Deathmatch: Source
        /// </summary>
        [Description("Half-Life Deathmatch: Source")]
        Half_Life_Deathmatch_Source = 360,
        /// <summary>
        /// Half-Life 2: Episode One
        /// </summary>
        [Description("Half-Life 2: Episode One")]
        Half_Life_2_Episode_One = 380,
        /// <summary>
        /// Portal
        /// </summary>
        [Description("Portal")]
        Portal = 400,
        /// <summary>
        /// Half-Life 2: Episode Two
        /// </summary>
        [Description("Half-Life 2: Episode Two")]
        Half_Life_2_Episode_Two = 420,
        /// <summary>
        /// Team Fortress 2
        /// </summary>
        [Description("Team Fortress 2")]
        Team_Fortress_2 = 440,
        /// <summary>
        /// Left 4 Dead
        /// </summary>
        [Description("Left 4 Dead")]
        Left_4_Dead = 500,
        /// <summary>
        /// Left 4 Dead 2
        /// </summary>
        [Description("Left 4 Dead 2")]
        Left_4_Dead_2 = 550,
        /// <summary>
        /// Dota 2 
        /// </summary>
        [Description("Dota 2")]
        Dota_2 = 570,
        /// <summary>
        /// Portal 2
        /// </summary>
        [Description("Portal 2")]
        Portal_2 = 620,
        /// <summary>
        /// Alien Swarm
        /// </summary>
        [Description("Alien Swarm")]
        Alien_Swarm = 630,
        /// <summary>
        /// Counter-Strike: Global Offensive
        /// </summary>
        [Description("Counter-Strike: Global Offensive")]
        CounterStrike_Global_Offensive = 730,
        /// <summary>
        /// SiN Episodes: Emergence
        /// </summary>
        [Description("SiN Episodes: Emergence")]
        SiN_Episodes_Emergence = 1300,
        /// <summary>
        /// Dark Messiah of Might and Magic
        /// </summary>
        [Description("Dark Messiah of Might and Magic")]
        Dark_Messiah_Of_Might_And_Magic = 2100,
        /// <summary>
        /// Dark Messiah Might and Magic Multi-Player
        /// </summary>
        [Description("Dark Messiah Might and Magic Multi-Player")]
        Dark_Messiah_Might_And_Magic_MultiPlayer = 2130,
        /// <summary>
        /// The Ship
        /// </summary>
        [Description("The Ship")]
        The_Ship = 2400,
        /// <summary>
        /// Bloody Good Time
        /// </summary>
        [Description("Bloody Good Time")]
        Bloody_Good_Time = 2450,
        /// <summary>
        /// Vampire The Masquerade - Bloodlines
        /// </summary>
        [Description("Vampire The Masquerade - Bloodlines")]
        Vampire_The_Masquerade_Bloodlines = 2600,
        /// <summary>
        /// Garry's Mod
        /// </summary>
        [Description("Garry's Mod")]
        Garrys_Mod = 4000,
        /// <summary>
        /// Zombie Panic! Source
        /// </summary>
        [Description("Zombie Panic! Source")]
        Zombie_Panic_Source = 17500,
        /// <summary>
        /// Age of Chivalry
        /// </summary>
        [Description("Age of Chivalry")]
        Age_of_Chivalry = 17510,
        /// <summary>
        /// Synergy
        /// </summary>
        [Description("Synergy")]
        Synergy = 17520,
        /// <summary>
        /// D.I.P.R.I.P.
        /// </summary>
        [Description("D.I.P.R.I.P.")]
        D_I_P_R_I_P = 17530,
        /// <summary>
        /// Eternal Silence
        /// </summary>
        [Description("Eternal Silence")]
        Eternal_Silence = 17550,
        /// <summary>
        /// Pirates, Vikings, and Knights II
        /// </summary>
        [Description("Pirates, Vikings, & Knights II")]
        Pirates_Vikings_And_Knights_II = 17570,
        /// <summary>
        /// Dystopia
        /// </summary>
        [Description("Dystopia")]
        Dystopia = 17580,
        /// <summary>
        /// Insurgency: Modern Infantry Combat
        /// </summary>
        [Description("Insurgency: Modern Infantry Combat")]
        Insurgency_Modern_Infantry_Combat = 17700,
        /// <summary>
        /// Nuclear Dawn
        /// </summary>
        [Description("Nuclear Dawn")]
        Nuclear_Dawn = 17710,
        /// <summary>
        /// Smashball
        /// </summary>
        [Description("Smashball")]
        Smashball = 17730,
        /// <summary>
        /// Insurgency
        /// </summary>
        [Description("Insurgency")]
        Insurgency = 222880,
        /// <summary>
        /// ARK: Survival Evolved
        /// </summary>
        [Description("ARK: Survival Evolved")]
        ARK_Survival_Evolved = 346110,
        /// <summary>
        /// Sniper Elite V2
        /// </summary>
        [Description("Sniper Elite V2")]
        Sniper_Elite_V2 = 63380,
        /// <summary>
        /// Sniper Elite 3
        /// </summary>
        [Description("Sniper Elite 3")]
        Sniper_Elite_3 = 238090,
        /// <summary>
        /// Arma 2
        /// </summary>
        [Description("Arma 2")]
        Arma_2 = 33900,
        /// <summary>
        /// Arma 2: Operation Arrowhead
        /// </summary>
        [Description("Arma 2: Operation Arrowhead")]
        Arma_2_Operation_Arrowhead = 33930,
        /// <summary>
        /// Arma 3
        /// </summary>
        [Description("Arma 3")]
        Arma_3 = 107410,
        /// <summary>
        /// Rust
        /// </summary>
        [Description("Rust")]
        Rust = 252490,
        /// <summary>
        /// H-Hour: World's Elite
        /// </summary>
        [Description("H-Hour: World's Elite")]
        H_Hour_Worlds_Elite = 293220,
        /// <summary>
        /// Killing Floor
        /// </summary>
        [Description("Killing Floor")]
        Killing_Floor = 1250,
        /// <summary>
        /// Killing Floor 2
        /// </summary>
        [Description("Killing Floor 2")]
        Killing_Floor_2 = 232090,
        /// <summary>
        /// DayZ
        /// </summary>
        [Description("DayZ")]
        DayZ = 221100,
        /// <summary>
        /// Space Engineers
        /// </summary>
        [Description("Space Engineers")]
        Space_Engineers = 244850,
        /// <summary>
        /// Red Orchestra: Ostfront 41-45
        /// </summary>
        [Description("Red Orchestra: Ostfront 41-45")]
        Red_Orchestra_Ostfront = 1200,
        /// <summary>
        /// Rising Storm/Red Orchestra 2 Multiplayer
        /// </summary>
        [Description("Rising Storm/Red Orchestra 2 Multiplayer")]
        Red_Orchestra_2 = 35450,
    }
}
