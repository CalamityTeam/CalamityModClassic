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
	public class AerialiteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aerialite Bar");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 3;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "AerialiteOre", 4);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
	}
}