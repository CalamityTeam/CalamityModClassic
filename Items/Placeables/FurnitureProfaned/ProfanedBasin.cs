using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureProfaned
{
	public class ProfanedBasin : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Used for special crafting");
        }

		public override void SetDefaults()
        {
            Item.SetNameOverride("Profaned Crucible");
            Item.width = 8;
			Item.height = 10;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("ProfanedBasin").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "ProfanedRock", 10);
            recipe.AddIngredient(null, "UnholyEssence", 5);
            recipe.AddTile(412);
            recipe.Register();
        }
	}
}