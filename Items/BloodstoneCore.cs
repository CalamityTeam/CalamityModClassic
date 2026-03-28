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
    public class BloodstoneCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodstone Core");
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 12;
            Item.maxStack = 999;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 7, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(null, "Bloodstone", 5);
            recipe.AddIngredient(null, "BloodOrb", 2);
            recipe.AddIngredient(null, "Phantoplasm");
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}