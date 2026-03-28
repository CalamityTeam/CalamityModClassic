using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class AstralSandstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Sandstone");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("AstralSandstone").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddTile(18);
            recipe.AddIngredient(Mod.Find<ModItem>("AstralSandstoneWall").Type, 4);
            recipe.Register();
            base.AddRecipes();
        }
    }
}
