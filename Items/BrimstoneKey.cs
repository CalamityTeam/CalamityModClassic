using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class BrimstoneKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Grants access to Locked Ashen Chests");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.value = 100;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddTile(18);
            recipe.Register();
        }
	}
}
