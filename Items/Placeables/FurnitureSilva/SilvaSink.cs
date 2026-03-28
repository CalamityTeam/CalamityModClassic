using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureSilva
{
	public class SilvaSink : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Counts as a water source");
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
			Item.createTile = Mod.Find<ModTile>("SilvaSink").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaCrystal", 6);
            recipe.AddIngredient(ItemID.WaterBucket);
            recipe.AddTile(null, "SilvaBasin");
            recipe.Register();
        }
	}
}