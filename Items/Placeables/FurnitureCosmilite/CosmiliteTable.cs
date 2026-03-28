using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureCosmilite
{
	public class CosmiliteTable: ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
        {
            Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("CosmiliteTable").Type;
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "CosmiliteBrick", 8);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
	}
}