using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureSilva
{
    public class SilvaCrystal : ModItem
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
            Item.createTile = Mod.Find<ModTile>("SilvaCrystal").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(400);
            recipe.AddIngredient(ItemID.CrystalBlock, 200);
            recipe.AddIngredient(ItemID.GoldBar, 25);
            recipe.AddIngredient(Mod.Find<ModItem>("DarksunFragment").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("EffulgentFeather").Type, 5);
            recipe.AddIngredient(Mod.Find<ModItem>("CosmiliteBar").Type, 5);
            recipe.AddIngredient(Mod.Find<ModItem>("NightmareFuel").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("EndothermicEnergy").Type);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
			recipe = CreateRecipe(400);
			recipe.AddIngredient(ItemID.CrystalBlock, 200);
			recipe.AddIngredient(ItemID.PlatinumBar, 25);
			recipe.AddIngredient(Mod.Find<ModItem>("DarksunFragment").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("EffulgentFeather").Type, 5);
			recipe.AddIngredient(Mod.Find<ModItem>("CosmiliteBar").Type, 5);
			recipe.AddIngredient(Mod.Find<ModItem>("NightmareFuel").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("EndothermicEnergy").Type);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
			recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaWall", 4);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaPlatform", 2);
            recipe.AddTile(null, "SilvaBasin");
            recipe.Register();
        }
    }
}
