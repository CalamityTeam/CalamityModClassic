using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class RevivifyPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Revivify Potion");
			// Tooltip.SetDefault("Causes enemy attacks to heal you for a fraction of their damage for 15 seconds");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.rare = 3;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
				modPlayer.revivifyTimer = 900;
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HolyWater, 5);
			recipe.AddIngredient(null, "Stardust", 20);
			recipe.AddIngredient(ItemID.CrystalShard, 5);
			recipe.AddIngredient(null, "EssenceofCinder", 3);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HolyWater, 5);
			recipe.AddIngredient(null, "BloodOrb", 50);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}