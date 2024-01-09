using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassic1Point2.Items.Providence
{
	public class ProfanedCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Profaned Core");
			//Tooltip.SetDefault("The core of the unholy flame");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.rare = ItemRarityID.Cyan;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("Providence").Type) && Main.dayTime && (player.ZoneHallow || player.ZoneUnderworldHeight);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Providence").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }
	}
}