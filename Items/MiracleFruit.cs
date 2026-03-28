using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class MiracleFruit : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Miracle Fruit");
			/* Tooltip.SetDefault("Permanently increases maximum life by 25\n" +
			                   "Can only be used if the max amount of life fruit has been consumed"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.rare = 7;
			Item.useTime = 30;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (modPlayer.mFruit || player.statLifeMax < 500)
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
					player.HealEffect(25);
				}
				CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
				modPlayer.mFruit = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeFruit, 5);
			recipe.AddIngredient(null, "AstralBar", 5);
			recipe.AddIngredient(null, "LivingShard", 10);
			recipe.AddIngredient(null, "Stardust", 20);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}