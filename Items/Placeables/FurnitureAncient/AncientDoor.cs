using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureAncient
{
	public class AncientDoor : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 28;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
            Item.rare = 3;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("AncientDoorClosed").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "BrimstoneSlag", 6);
            recipe.AddTile(null, "AncientAltar");
            recipe.Register();
        }
    }
}