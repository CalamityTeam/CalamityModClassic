using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class EutrophicSand : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eutrophic Sand");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("EutrophicSand").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 13;
            Item.height = 10;
            Item.maxStack = 999;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "EutrophicSandWallSafe", 4);
			recipe.AddTile(18);
			recipe.Register();
		}
	}
}