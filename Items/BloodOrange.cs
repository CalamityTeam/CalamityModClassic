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
	public class BloodOrange : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.rare = ItemRarityID.Pink;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (modPlayer.bOrange || player.statLifeMax < 500)
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
				CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
				modPlayer.bOrange = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LifeFruit, 5);
			recipe.AddIngredient(null, "EssenceofChaos", 10);
			recipe.AddIngredient(null, "EssenceofCinder", 10);
			recipe.AddIngredient(null, "EssenceofEleum", 10);
			recipe.AddIngredient(ItemID.SoulofNight, 30);
			recipe.AddIngredient(ItemID.SoulofLight, 30);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}