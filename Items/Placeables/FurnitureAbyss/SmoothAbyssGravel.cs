using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAbyss
{
	public class SmoothAbyssGravel : ModItem
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
			Item.createTile = Mod.Find<ModTile>("SmoothAbyssGravel").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssGravel", 1);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SmoothAbyssGravelWall", 4);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SmoothAbyssGravelPlatform", 2);
            recipe.AddTile(null, "VoidCondenser");
            recipe.Register();
        }
	}
}
