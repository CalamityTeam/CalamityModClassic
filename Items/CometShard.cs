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
	public class CometShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Comet Shard");
			//Tooltip.SetDefault("Permanently increases maximum mana by 50");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.rare = ItemRarityID.Pink;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item29;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (modPlayer.cShard)
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
					player.ManaEffect(50);
				}
				CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
				modPlayer.cShard = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ItemID.FallenStar, 50);
			recipe.AddIngredient(null, "Stardust", 150);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}