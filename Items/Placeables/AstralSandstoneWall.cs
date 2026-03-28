using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class AstralSandstoneWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Sandstone Wall");
        }

        public override void SetDefaults()
        {
            Item.createWall = Mod.Find<ModWall>("AstralSandstoneWall").Type;
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
            Recipe recipe = CreateRecipe(4);
            recipe.AddTile(18);
            recipe.AddIngredient(Mod.Find<ModItem>("AstralSandstone").Type);
            recipe.Register();
            base.AddRecipes();
        }
    }
}
