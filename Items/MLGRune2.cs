using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Items
{
	public class MLGRune2 : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
	        //texture =("CalamityModClassic1Point1/Items/Accessories/MLGRune2");
	        return true;
	    }
		
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Celestial Onion");
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 1;
			////Tooltip.SetDefault("Buffs all bosses if used\nEffect stacks with Demon Trophy\nPrepare to cry edition will only be active while both effects are stacked\nReturns all bosses to normal if used again");
			Item.rare = 1;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item119;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (!CalamityGlobalNPC1Point1.superBossBuff && CalamityGlobalNPC1Point1.bossBuff)
			{
				Main.NewText("Welcome to Terraria: Prepare to Cry Edition", Color.Red.R, Color.Purple.G, Color.Blue.B);
				CalamityGlobalNPC1Point1.superBossBuff = true;
			}
			else if (!CalamityGlobalNPC1Point1.superBossBuff && !CalamityGlobalNPC1Point1.bossBuff)
			{
				Main.NewText("Welcome to Terraria: Prepare to Die Edition", Color.Red.R, Color.Purple.G, Color.Blue.B);
				CalamityGlobalNPC1Point1.superBossBuff = true;
			}
			else if (CalamityGlobalNPC1Point1.superBossBuff && CalamityGlobalNPC1Point1.bossBuff)
			{
				Main.NewText("Welcome to Terraria: Prepare to Die Edition", Color.Red.R, Color.Purple.G, Color.Blue.B);
				CalamityGlobalNPC1Point1.superBossBuff = false;
			}
			else if (CalamityGlobalNPC1Point1.superBossBuff && !CalamityGlobalNPC1Point1.bossBuff)
			{
				Main.NewText("Welcome to Terraria", Color.Red.R, Color.Purple.G, Color.Blue.B);
				CalamityGlobalNPC1Point1.superBossBuff = false;
			}
			return true;
		}
	}
}