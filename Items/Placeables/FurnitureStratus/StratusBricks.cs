using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureStratus
{
    public class StratusBricks : ModItem
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
            Item.createTile = Mod.Find<ModTile>("StratusBricks").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(40);
            recipe.AddIngredient(ItemID.StoneBlock, 40);
            recipe.AddIngredient(null, "Lumenite", 3);
            recipe.AddIngredient(null, "RuinousSoul", 1);
            recipe.AddTile(412);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "StratusWall", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "StratusPlatform", 2);
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
