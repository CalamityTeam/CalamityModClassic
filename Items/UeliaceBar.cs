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
    public class UeliaceBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Uelibloom Bar");
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 12;
            Item.maxStack = 999;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 6, 50, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UelibloomOre", 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}