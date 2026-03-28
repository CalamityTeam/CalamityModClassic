using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureEutrophic
{
	public class EutrophicSink : ModItem
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
			Item.createTile = Mod.Find<ModTile>("EutrophicSink").Type;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("Navystone").Type, 6);
            recipe.AddIngredient(ItemID.WaterBucket);
            recipe.AddTile(null, "EutrophicCrafting");
            recipe.Register();
        }
    }
}