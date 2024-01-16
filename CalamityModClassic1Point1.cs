using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.GameContent;
using Terraria.ModLoader;
using CalamityModClassic1Point1.NPCs.TheDevourerofGods;
using CalamityModClassic1Point1.NPCs.Calamitas;
using CalamityModClassic1Point1.NPCs.PlaguebringerGoliath;
using CalamityModClassic1Point1.NPCs.Yharon;
using CalamityModClassic1Point1.NPCs.Leviathan;
using CalamityModClassic1Point1.NPCs.Providence;
using CalamityModClassic1Point1.NPCs.SupremeCalamitas;
using CalamityModClassic1Point1.Tiles;

namespace CalamityModClassic1Point1
{
	[Autoload]
    public class CalamityModClassic1Point1 : Mod
    {
        public static Mod Instance;
            	
    	public override void Load()
		{
            Instance = this;
			if (!Main.dedServ)
			{
				Filters.Scene["CalamityModClassic1Point1:DevourerofGodsHead"] = new Filter(new DoGScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.1f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:DevourerofGodsHead"] = new DoGSky();
				
				Filters.Scene["CalamityModClassic1Point1:CalamitasRun3"] = new Filter(new CalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:CalamitasRun3"] = new CalSky();
				
				Filters.Scene["CalamityModClassic1Point1:PlaguebringerGoliath"] = new Filter(new PbGScreenShaderData("FilterMiniTower").UseColor(0.3f, 0.9f, 0.2f).UseOpacity(0.65f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:PlaguebringerGoliath"] = new PbGSky();
				
				Filters.Scene["CalamityModClassic1Point1:Yharon"] = new Filter(new YScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.75f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:Yharon"] = new YSky();
				
				Filters.Scene["CalamityModClassic1Point1:Leviathan"] = new Filter(new LevScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0.5f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:Leviathan"] = new LevSky();
				
				Filters.Scene["CalamityModClassic1Point1:Providence"] = new Filter(new ProvScreenShaderData("FilterMiniTower").UseColor(0.8f, 0.6f, 0f).UseOpacity(0.55f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:Providence"] = new ProvSky();
				
				Filters.Scene["CalamityModClassic1Point1:SupremeCalamitas"] = new Filter(new SCalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.65f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassic1Point1:SupremeCalamitas"] = new SCalSky();
			}
		}
    	
    	public override object Call(params object[] args)
        {
            if(args.Length <= 0 || !(args[0] is string)) return new Exception("CALL ERROR: NO METHOD NAME! First param MUST be a method name!");
            string methodName = (string)args[0];            
            if(methodName.Equals("Downed")) //returns a Func which will return a downed value based on player and name.
            {
                Func<string, bool> downed = (name) => 
                {
                    name = name.ToLower();
                    switch(name)
                    {
                        default: return false;
                        case "desertscourge": return CalamityWorld.downedDesertScourge;
                        case "hivemind": return CalamityWorld.downedHiveMind;
                        case "perforators": return CalamityWorld.downedPerforator;
                        case "slimegod": return CalamityWorld.downedSlimeGod;
                        case "cryogen": return CalamityWorld.downedCryogen;
                        case "calamitas": return CalamityWorld.downedCalamitas;
                        case "leviathan": return CalamityWorld.downedLeviathan;
                        case "plaguebringer": return CalamityWorld.downedPlaguebringer;
                        case "guardians": return CalamityWorld.downedGuardians;
                        case "providence": return CalamityWorld.downedProvidence;
                        case "sentinel": return CalamityWorld.downedSentinel;
                        case "devourerofgods": return CalamityWorld.downedDoG;
                        case "yharon": return CalamityWorld.downedYharon;
                        case "supremecalamitas": return CalamityWorld.downedSCal;
                    }
                };
                return downed;
            }
            return new Exception("CALL ERROR: NO METHOD FOUND: " + methodName);
        }

        public override void Unload()
        {
            Instance=  null;
        }
    }
}