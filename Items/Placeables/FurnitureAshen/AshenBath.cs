using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAshen
{
	public class AshenBath : ModItem
	{
		public override void SetStaticDefaults()
        {
        }

		public override void SetDefaults()
        {
            Item.SetNameOverride("Ashen Bathtub");
            Item.width = 28;
			Item.height = 20;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
            Item.rare = 3;
            Item.value = 0;
            Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("AshenBath").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SmoothBrimstoneSlag", 14);
            recipe.AddTile(null, "AshenAltar");
            recipe.Register();
        }
	}
}