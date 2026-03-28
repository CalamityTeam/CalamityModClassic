using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class TitanScalePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Titan Scale Potion");
			// Tooltip.SetDefault("Increases knockback, defense by 5, and damage reduction by 5%");
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
			Item.buffType = Mod.Find<ModBuff>("TitanScale").Type;
			Item.buffTime = 18000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TitanPotion);
			recipe.AddIngredient(ItemID.BeetleHusk);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 10);
			recipe.AddIngredient(ItemID.BeetleHusk);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}