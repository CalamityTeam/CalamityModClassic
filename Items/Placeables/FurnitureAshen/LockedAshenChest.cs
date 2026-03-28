using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAshen
{
	public class LockedAshenChest : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
            Item.rare = 3;
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("AshenChestLocked").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SmoothBrimstoneSlag", 8);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
            recipe.AddIngredient(null, "AshenKey", 1);
            recipe.AddTile(null, "AshenAltar");
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "AshenChest", 1);
            recipe.AddIngredient(null, "AshenKey", 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
            recipe.AddTile(null, "AshenAltar");
            recipe.Register();
        }
    }
}