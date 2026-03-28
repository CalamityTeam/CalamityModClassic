using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class AshenKey : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Used in crafting to lock/unlock ashen chests");
        }

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.value = 100;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.GoldenKey);
            recipe.AddTile(18);
            recipe.Register();
        }
	}
}
