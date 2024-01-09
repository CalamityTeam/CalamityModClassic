using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.SupremeCalamitas
{
	public class EyeofExtinction : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Eye of Extinction");
			//Tooltip.SetDefault("Death");
		}
		
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
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
	                line2.OverrideColor = new Color(108, 45, 199);
	            }
	        }
	    }
		
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SupremeCalamitas").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "NightmareFuel", 3);
        	recipe.AddIngredient(null, "EndothermicEnergy", 3);
        	recipe.AddIngredient(null, "DarksunFragment");
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "Phantoplasm", 5);
			recipe.AddIngredient(null, "BlightedEyeball");
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}