using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class ChaoticBrick : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 25;
            Item.maxStack = 999;
            Item.value = 0;
            Item.rare = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("ChaoticBrick").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddIngredient(null, "ChaoticOre", 1);
            recipe.AddTile(17);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "ChaoticBrickWall", 4);
            recipe.AddTile(18);
            recipe.Register();
        }
    }
}