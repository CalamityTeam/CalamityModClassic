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

namespace CalamityModClassicPreTrailer
{
	public class ModSupportPreTrailer
	{
		public static object Call(params object[] args)
		{
			if (args.Length <= 0 || !(args[0] is string)) return new Exception("CalamityModClassicPreTrailer Error: NO METHOD NAME! First param MUST be a method name!");
			string methodName = (string)args[0];
			if (methodName.Equals("Downed")) //returns a Func which will return a downed value based on name.
			{
				Func<string, bool> downed = (name) =>
				{
					name = name.ToLower();
					switch (name)
					{
						default: return false;
						case "desertscourge": return CalamityWorldPreTrailer.downedDesertScourge;
						case "crabulon": return CalamityWorldPreTrailer.downedCrabulon;
						case "hivemind": return CalamityWorldPreTrailer.downedHiveMind;
						case "perforator":
						case "perforators": return CalamityWorldPreTrailer.downedPerforator;
						case "slimegod": return CalamityWorldPreTrailer.downedSlimeGod;
						case "cryogen": return CalamityWorldPreTrailer.downedCryogen;
						case "brimstoneelemental": return CalamityWorldPreTrailer.downedBrimstoneElemental;
						case "calamitas": return CalamityWorldPreTrailer.downedCalamitas;
						case "leviathan": return CalamityWorldPreTrailer.downedLeviathan;
						case "astrumdeus": return CalamityWorldPreTrailer.downedStarGod;
						case "plaguebringer": return CalamityWorldPreTrailer.downedPlaguebringer;
						case "ravager": return CalamityWorldPreTrailer.downedScavenger;
						case "guardians": return CalamityWorldPreTrailer.downedGuardians;
						case "providence": return CalamityWorldPreTrailer.downedProvidence;
						case "polterghast": return CalamityWorldPreTrailer.downedPolterghast;
						case "sentinelany": return (CalamityWorldPreTrailer.downedSentinel1 || CalamityWorldPreTrailer.downedSentinel2 || CalamityWorldPreTrailer.downedSentinel3);
						case "sentinelall": return (CalamityWorldPreTrailer.downedSentinel1 && CalamityWorldPreTrailer.downedSentinel2 && CalamityWorldPreTrailer.downedSentinel3);
						case "sentinel1": return CalamityWorldPreTrailer.downedSentinel1;
						case "sentinel2": return CalamityWorldPreTrailer.downedSentinel2;
						case "sentinel3": return CalamityWorldPreTrailer.downedSentinel3;
						case "devourerofgods": return CalamityWorldPreTrailer.downedDoG;
						case "bumblebirb": return CalamityWorldPreTrailer.downedBumble;
						case "yharon": return CalamityWorldPreTrailer.downedYharon;
						case "supremecalamitas": return CalamityWorldPreTrailer.downedSCal;
					}
				};
				return downed;
			}
			else
			if (methodName.Equals("InZone")) //returns a Func which will return a zone value based on player and name.
			{
				Func<Player, string, bool> inZone = (p, name) => { return ModSupportPreTrailer.InZone(p, name); };
				return inZone;
			}
			return new Exception("CalamityModClassicPreTrailer Error: NO METHOD FOUND: " + methodName);
		}

		public static bool InZone(Player p, string zoneName)
		{
			Mod calamity = ModLoader.GetMod("CalamityModClassicPreTrailer");
			zoneName = zoneName.ToLower();
			switch (zoneName)
			{
				case "calamity": return p.GetModPlayer<CalamityPlayerPreTrailer>().ZoneCalamity;
				case "astral": return p.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral;
			}
			return false;
		}
	}
}