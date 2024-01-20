using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2
{
	public class ModSupport1Point2
    {	
		public static object Call(params object[] args)
		{
			if (args.Length <= 0 || !(args[0] is string)) return new Exception("CalamityModClassic1Point2 Error: NO METHOD NAME! First param MUST be a method name!");
			string methodName = (string)args[0];		
			if (methodName.Equals("Downed")) //returns a Func which will return a downed value based on name.
			{
				Func<string, bool> downed = (name) => 
				{
					name = name.ToLower();
					switch (name)
					{
						default: return false;
						case "desertscourge": return CalamityWorld1Point2.downedDesertScourge;
						case "crabulon": return CalamityWorld1Point2.downedCrabulon;
						case "hivemind": return CalamityWorld1Point2.downedHiveMind;
						case "perforator":
						case "perforators": return CalamityWorld1Point2.downedPerforator;
						case "slimegod": return CalamityWorld1Point2.downedSlimeGod;
						case "cryogen": return CalamityWorld1Point2.downedCryogen;
						case "brimstoneelemental": return CalamityWorld1Point2.downedBrimstoneElemental;
						case "calamitas": return CalamityWorld1Point2.downedCalamitas;
						case "leviathan": return CalamityWorld1Point2.downedLeviathan;
						case "astrumdeus": return CalamityWorld1Point2.downedStarGod;
						case "plaguebringer": return CalamityWorld1Point2.downedPlaguebringer;
						case "ravager": return CalamityWorld1Point2.downedScavenger;
						case "guardians":  return CalamityWorld1Point2.downedGuardians;
						case "providence": return CalamityWorld1Point2.downedProvidence;
						case "polterghast": return CalamityWorld1Point2.downedPolterghast;
						case "sentinelany": return (CalamityWorld1Point2.downedSentinel1 || CalamityWorld1Point2.downedSentinel2 || CalamityWorld1Point2.downedSentinel3);
						case "sentinelall": return (CalamityWorld1Point2.downedSentinel1 && CalamityWorld1Point2.downedSentinel2 && CalamityWorld1Point2.downedSentinel3);
						case "sentinel1": return CalamityWorld1Point2.downedSentinel1;
						case "sentinel2": return CalamityWorld1Point2.downedSentinel2;
						case "sentinel3": return CalamityWorld1Point2.downedSentinel3;
						case "devourerofgods": return CalamityWorld1Point2.downedDoG;
						case "bumblebirb": return CalamityWorld1Point2.downedBumble;
						case "yharon": return CalamityWorld1Point2.downedYharon;
						case "supremecalamitas": return CalamityWorld1Point2.downedSCal;		
					}
				};
				return downed;
			}
			else
			if (methodName.Equals("InZone")) //returns a Func which will return a zone value based on player and name.
			{
				Func<Player, string, bool> inZone = (p, name) => { return ModSupport1Point2.InZone(p, name); };
				return inZone;
			}
			return new Exception("CalamityModClassic1Point2 Error: NO METHOD FOUND: " + methodName);
		}

        public static bool InZone(Player p, string zoneName)
        {
			Mod calamity = ModLoader.GetMod("CalamityModClassic1Point2");
			zoneName = zoneName.ToLower();
            switch (zoneName)
            {
                case "calamity": return p.GetModPlayer<CalamityPlayer1Point2>().ZoneCalamity;
                case "astral": return p.GetModPlayer<CalamityPlayer1Point2>().ZoneAstral;
            }
            return false;
        }
    }
}