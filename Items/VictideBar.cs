using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
	public class VictideBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Victide Bar");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 50, 0);
			Item.rare = 2;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "VictoryShard");
			recipe.AddIngredient(ItemID.Coral);
			recipe.AddIngredient(ItemID.Starfish);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
	}
}