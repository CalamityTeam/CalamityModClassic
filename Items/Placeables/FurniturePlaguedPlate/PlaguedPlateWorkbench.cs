using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurniturePlaguedPlate
{
	public class PlaguedPlateWorkbench : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
        {
            Item.SetNameOverride("Plagued Plate Work Bench");
            Item.width = 8;
			Item.height = 10;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("PlaguedPlateWorkbench").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "PlaguedPlate", 10);
            recipe.AddIngredient(null, "PlagueCellCluster", 2);
            recipe.AddTile(null, "PlagueInfuser");
            recipe.Register();
        }
	}
}