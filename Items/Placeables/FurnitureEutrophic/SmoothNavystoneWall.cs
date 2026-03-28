using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureEutrophic
{
	public class SmoothNavystoneWall : ModItem
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
			Item.consumable = true;
			Item.createWall = Mod.Find<ModWall>("SmoothNavystoneWall").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(Mod.Find<ModItem>("SmoothNavystone").Type);
            recipe.AddTile(18);
            recipe.Register();
        }
	}
}