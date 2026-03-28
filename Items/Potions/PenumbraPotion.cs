using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class PenumbraPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Penumbra Potion");
			// Tooltip.SetDefault("Rogue stealth always builds during nighttime and twice as fast during a solar eclipse, even while moving");
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
			Item.buffType = Mod.Find<ModBuff>("PenumbraBuff").Type;
			Item.buffTime = 18000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "CalamityDust", 3);
			recipe.AddIngredient(ItemID.LunarTabletFragment, 2);
			recipe.AddIngredient(null, "EssenceofChaos");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 30);
			recipe.AddIngredient(ItemID.LunarTabletFragment);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}