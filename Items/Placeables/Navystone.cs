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
    public class Navystone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Navystone");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("Navystone").Type;
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
			recipe.AddIngredient(null, "NavystoneWallSafe", 4);
			recipe.AddTile(18);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("EutrophicPlatform").Type, 2);
			recipe.AddTile(null, "EutrophicCrafting");
			recipe.Register();
		}
	}
}