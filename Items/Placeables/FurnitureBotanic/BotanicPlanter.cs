using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.FurnitureBotanic
{
	public class BotanicPlanter : ModItem
	{
		public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Used for special crafting");
        }

		public override void SetDefaults()
        {
            Item.width = 28;
			Item.height = 20;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
            Item.value = 0;
            Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("BotanicPlanter").Type;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "UelibloomBrick", 20);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddTile(412);
            recipe.Register();
        }
	}
}