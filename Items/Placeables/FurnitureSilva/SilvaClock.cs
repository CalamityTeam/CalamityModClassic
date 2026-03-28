using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureSilva
{
	public class SilvaClock : ModItem
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
			Item.createTile = Mod.Find<ModTile>("SilvaClock").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaCrystal", 10);
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddIngredient(ItemID.Glass, 6);
            recipe.AddTile(null, "SilvaBasin");
            recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "SilvaCrystal", 10);
			recipe.AddIngredient(ItemID.PlatinumBar, 3);
			recipe.AddIngredient(ItemID.Glass, 6);
			recipe.AddTile(null, "SilvaBasin");
			recipe.Register();
		}
	}
}