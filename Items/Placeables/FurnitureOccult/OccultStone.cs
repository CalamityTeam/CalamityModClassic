using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureOccult
{
    public class OccultStone : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.SetNameOverride("Otherworldly Stone");
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("OccultStone").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(150);
            recipe.AddIngredient(ItemID.StoneBlock, 150);
			recipe.AddIngredient(Mod.Find<ModItem>("DarkPlasma").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("ArmoredShell").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("TwistingNether").Type);
			recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "OccultStoneWall", 4);
            recipe.AddTile(18);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "OccultPlatform", 2);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}
