using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureStatigel
{
	public class StatigelBlock : ModItem
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
			Item.createTile = Mod.Find<ModTile>("StatigelBlock").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(10);
			recipe.AddIngredient(null, "PurifiedGel");
            recipe.AddTile(null, "StaticRefiner");
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "StatigelPlatform", 2);
            recipe.AddTile(null, "StaticRefiner");
            recipe.Register();
        }
	}
}
