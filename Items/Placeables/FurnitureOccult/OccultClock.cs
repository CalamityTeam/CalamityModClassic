using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureOccult
{
	public class OccultClock : ModItem
	{
		public override void SetStaticDefaults()
        {
        }

		public override void SetDefaults()
        {
            Item.SetNameOverride("Otherworldly Clock");
            Item.width = 28;
			Item.height = 20;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("OccultClock").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "OccultStone", 10);
            recipe.AddIngredient(Mod.Find<ModItem>("CosmiliteBrick").Type, 3);
            recipe.AddIngredient(ItemID.Glass, 6);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
	}
}