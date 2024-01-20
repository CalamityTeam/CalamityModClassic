using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items
{
	public class Dragonfruit : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (modPlayer.dFruit || player.statLifeMax < 500)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				if (Main.myPlayer == player.whoAmI)
				{
					player.HealEffect(50);
				}
				CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
				modPlayer.dFruit = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeFruit, 20);
			recipe.AddIngredient(null, "Phantoplasm", 20);
			recipe.AddIngredient(null, "CosmiliteBar", 20);
			recipe.AddIngredient(null, "EffulgentFeather", 30);
			recipe.AddIngredient(ItemID.FragmentSolar, 50);
			recipe.AddIngredient(null, "NightmareFuel", 10);
        	recipe.AddIngredient(null, "EndothermicEnergy", 10);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
		}
	}
}