using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureProfaned
{
	public class ProfanedRock : ModItem
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
			Item.createTile = Mod.Find<ModTile>("ProfanedRock").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ProfanedRockWall", 4);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe(20);
            recipe.AddIngredient(null, "UnholyEssence");
            recipe.AddIngredient(ItemID.StoneBlock, 20);
            recipe.AddTile(412);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "ProfanedPlatform", 2);
            recipe.AddTile(412);
            recipe.Register();
        }
	}
}
