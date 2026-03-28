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
    public class CruptixBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaotic Bar");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ChaoticOre", 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}