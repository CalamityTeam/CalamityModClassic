using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
	public class TranquilityCandle: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tranquility Candle");
			// Tooltip.SetDefault("The mere presence of this candle calms surrounding enemies drastically");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 20;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 500;
			Item.createTile = Mod.Find<ModTile>("TranquilityCandle").Type;
		}
		
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().tranquilityCandle = true;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.PeaceCandle, 3);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddIngredient(ItemID.PixieDust, 4);
            recipe.AddIngredient(null, "ZenPotion");
            recipe.AddTile(18);
            recipe.Register();
        }
    }
}