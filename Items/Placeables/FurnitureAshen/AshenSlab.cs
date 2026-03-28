using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAshen
{
	public class AshenSlab : ModItem
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
            Item.rare = 3;
            Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("AshenSlab").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(null, "SmoothBrimstoneSlag", 4);
            recipe.AddIngredient(null, "UnholyCore", 1);
            recipe.AddTile(null, "AshenAltar");
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "AshenSlabWall", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
