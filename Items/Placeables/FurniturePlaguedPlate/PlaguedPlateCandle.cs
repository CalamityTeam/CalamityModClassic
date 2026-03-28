using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurniturePlaguedPlate
{
	public class PlaguedPlateCandle : ModItem
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
			Item.createTile = Mod.Find<ModTile>("PlaguedPlateCandle").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "PlaguedPlate", 4);
            recipe.AddIngredient(null, "PlagueCellCluster", 2);
            recipe.AddIngredient(ItemID.Wire);
            recipe.AddTile(null, "PlagueInfuser");
            recipe.Register();
        }
	}
}