using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureSilva
{
	public class SilvaBasin : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Used for special crafting");
        }

		public override void SetDefaults()
        {
            Item.SetNameOverride("Effulgent Manipulator");
            Item.width = 28;
			Item.height = 20;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("SilvaBasin").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaCrystal", 10);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "SilvaCrystal", 10);
			recipe.AddIngredient(ItemID.PlatinumBar, 5);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}