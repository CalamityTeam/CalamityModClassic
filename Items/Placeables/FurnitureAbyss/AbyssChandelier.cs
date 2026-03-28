using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAbyss
{
	public class AbyssChandelier : ModItem
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
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("AbyssChandelier").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SmoothAbyssGravel", 4);
            recipe.AddIngredient(null, "Lumenite", 4);
            recipe.AddIngredient(ItemID.Chain);
            recipe.AddTile(null, "VoidCondenser");
            recipe.Register();
        }
	}
}