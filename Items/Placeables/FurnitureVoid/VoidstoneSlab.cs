using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureVoid
{
	public class VoidstoneSlab : ModItem
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
			Item.createTile = Mod.Find<ModTile>("VoidstoneSlab").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SmoothVoidstone");
            recipe.AddTile(null, "VoidCondenser");
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "VoidstoneSlabWall", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
	}
}
