using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureEutrophic
{
	public class EutrophicCrafting : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Used for special crafting");
        }

		public override void SetDefaults()
        {
            Item.SetNameOverride("Eutrophic Shelf");
            Item.width = 28;
			Item.height = 20;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("EutrophicCrafting").Type;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("Navystone").Type, 10);
            recipe.AddIngredient(Mod.Find<ModItem>("SeaPrism").Type, 5);
            recipe.AddIngredient(Mod.Find<ModItem>("PrismShard").Type, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}