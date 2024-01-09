using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class ChaosAmulet : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Chaos Amulet");
			//Tooltip.SetDefault("Spelunker effect");
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.lifeRegen = 2;
			Item.value = 150000;
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.findTreasure = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CruptixBar", 2);
			recipe.AddIngredient(ItemID.SpelunkerPotion, 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}