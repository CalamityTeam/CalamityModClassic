using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAncient
{
	public class AncientBasin : ModItem
	{
		public override void SetStaticDefaults()
        {
        }

		public override void SetDefaults()
        {
            Item.width = 26;
			Item.height = 26;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
            Item.rare = 3;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("AncientBasin").Type;
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "BrimstoneSlag", 10);
            recipe.AddIngredient(null, "CharredOre", 5);
            recipe.AddTile(null, "AncientAltar");
            recipe.Register();
        }
    }
}