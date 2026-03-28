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
    public class MeldiateBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Meld Bar");
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 12;
            Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 5, 50, 0);
			Item.rare = 9;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(3);
            recipe.AddIngredient(ItemID.Ectoplasm);
            recipe.AddIngredient(ItemID.HallowedBar);
            recipe.AddIngredient(null, "MeldBlob", 6);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}