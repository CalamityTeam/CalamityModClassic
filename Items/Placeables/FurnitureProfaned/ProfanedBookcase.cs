using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureProfaned
{
	public class ProfanedBookcase : ModItem
	{
		public override void SetStaticDefaults()
        {
        }

		public override void SetDefaults()
        {
            Item.width = 28;
			Item.height = 20;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("ProfanedBookcase").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "ProfanedRock", 20);
            recipe.AddIngredient(ItemID.Book, 10);
            recipe.AddTile(null, "ProfanedBasin");
            recipe.Register();
        }
	}
}