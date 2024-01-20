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
	public class Elderberry : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.rare = ItemRarityID.Red;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (modPlayer.eBerry || player.statLifeMax < 500)
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
				modPlayer.eBerry = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeFruit, 15);
			recipe.AddIngredient(null, "UeliaceBar", 20);
			recipe.AddIngredient(ItemID.LunarBar, 30);
			recipe.AddIngredient(null, "UnholyEssence", 50);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}