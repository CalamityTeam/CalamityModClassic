using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurniturePlaguedPlate
{
    public class PlaguedPlate : ModItem
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
            Item.createTile = Mod.Find<ModTile>("PlaguedPlate").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
            recipe.AddIngredient(null, "PlagueCellCluster", 5);
            recipe.AddTile(null, "PlagueInfuser");
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "PlaguedPlateWall", 4);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "PlaguedPlatePlatform", 2);
            recipe.AddTile(null, "PlagueInfuser");
            recipe.Register();
        }
    }
}
