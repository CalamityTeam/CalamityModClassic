using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAbyss
{
	public class AbyssBed : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 20;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("AbyssBed").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SmoothAbyssGravel", 15);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(null, "VoidCondenser");
            recipe.Register();
        }
	}
}