using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Polterghast
{
	public class NecroplasmicBeacon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Necroplasmic Beacon");
			//Tooltip.SetDefault("It's spooky");
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
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
	                line2.OverrideColor = new Color(0, 255, 0);
	            }
	        }
	    }
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneDungeon && !NPC.AnyNPCs(Mod.Find<ModNPC>("Polterghast").Type);
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Polterghast").Type);
			SoundEngine.PlaySound(SoundID.Roar, player.position);
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Phantoplasm", 10);
			recipe.AddIngredient(null, "RuinousSoul");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}