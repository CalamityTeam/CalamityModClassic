using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureCosmilite
{
    public class CosmiliteBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("CosmiliteBrick").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(null, "CosmiliteBar", 1);
            recipe.AddIngredient(ItemID.StoneBlock, 20);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "CosmiliteBrickWall", 4);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "CosmilitePlatform", 2);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}
