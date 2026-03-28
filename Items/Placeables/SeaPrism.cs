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
    public class SeaPrism : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sea Prism");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("SeaPrism").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 13;
            Item.height = 10;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 2;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "PrismShard", 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}