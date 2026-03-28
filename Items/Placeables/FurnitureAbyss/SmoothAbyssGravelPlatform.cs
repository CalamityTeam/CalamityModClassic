using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAbyss
{
	public class SmoothAbyssGravelPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 10;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("SmoothAbyssGravelPlatform").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(null, "SmoothAbyssGravel");
            recipe.AddTile(null, "VoidCondenser");
            recipe.Register();
        }
	}
}