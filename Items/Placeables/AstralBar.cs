using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
    public class AstralBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Bar");
        }

        public override void SetDefaults()
        {
            Item.createTile = Mod.Find<ModTile>("AstralBar").Type;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = 7;
			Item.value = Item.buyPrice(0, 4, 50, 0);
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "Stardust", 12);
            recipe.AddIngredient(Mod.Find<ModItem>("AstralOre").Type, 8);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}
