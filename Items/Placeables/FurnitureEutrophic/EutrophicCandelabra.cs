using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureEutrophic
{
	public class EutrophicCandelabra : ModItem
	{
		public override void SetStaticDefaults()
        {
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
			Item.createTile = Mod.Find<ModTile>("EutrophicCandelabra").Type;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("Navystone").Type, 5);
            recipe.AddIngredient(Mod.Find<ModItem>("SeaPrism").Type, 3);
            recipe.AddTile(null, "EutrophicCrafting");
            recipe.Register();
        }
    }
}