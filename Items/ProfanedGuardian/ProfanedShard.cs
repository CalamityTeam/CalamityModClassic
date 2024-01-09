using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassic1Point2.Items.ProfanedGuardian
{
	public class ProfanedShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Profaned Shard");
			//Tooltip.SetDefault("A shard of the unholy flame");
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
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("ProfanedGuardianBoss").Type) && Main.dayTime && (player.ZoneHallow || player.ZoneUnderworldHeight);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("ProfanedGuardianBoss").Type);
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("ProfanedGuardianBoss2").Type);
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("ProfanedGuardianBoss3").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyEssence", 15);
			recipe.AddIngredient(ItemID.LunarBar, 3);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}