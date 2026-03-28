using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAncient
{
	public class AncientChair : ModItem
	{
		public override void SetStaticDefaults()
		{
			//// Tooltip.SetDefault("This is a modded chair.");
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
            Item.rare = 3;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("AncientChair").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "BrimstoneSlag", 4);
            recipe.AddTile(null, "AncientAltar");
            recipe.Register();
        }
    }
}