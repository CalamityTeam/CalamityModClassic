using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
	public class ChaosCandle: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Candle");
			// Tooltip.SetDefault("The mere presence of this candle enrages surrounding enemies drastically");
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
			Item.createTile = Mod.Find<ModTile>("ChaosCandle").Type;
		}
		
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<CalamityPlayerPreTrailer>().chaosCandle = true;
		}

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.WaterCandle, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.AddIngredient(null, "CoreofChaos", 6);
            recipe.AddIngredient(null, "ZergPotion");
            recipe.AddTile(18);
            recipe.Register();
        }
    }
}