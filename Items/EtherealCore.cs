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
	public class EtherealCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ethereal Core");
			//Tooltip.SetDefault("Permanently increases maximum mana by 100");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.rare = ItemRarityID.Red;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item29;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			if (modPlayer.eCore)
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
					player.ManaEffect(100);
				}
				CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
				modPlayer.eCore = true;
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MeteoriteBar, 25);
			recipe.AddIngredient(null, "AstralBar", 25);
			recipe.AddIngredient(ItemID.FragmentNebula, 50);
			recipe.AddIngredient(ItemID.FallenStar, 100);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}