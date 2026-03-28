using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAshen
{
	public class SmoothBrimstoneSlagWall : ModItem
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
			Item.useTime = 7;
			Item.useStyle = 1;
            Item.rare = 3;
            Item.consumable = true;
			Item.createWall = Mod.Find<ModWall>("SmoothBrimstoneSlagWall").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "SmoothBrimstoneSlag");
            recipe.AddTile(18);
            recipe.Register();
        }
	}
}