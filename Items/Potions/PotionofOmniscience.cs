using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class PotionofOmniscience : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Potion of Omniscience");
			// Tooltip.SetDefault("Gives creature, danger, and treasure detection");
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
			Item.buffType = Mod.Find<ModBuff>("Omniscience").Type;
			Item.buffTime = 18000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HunterPotion);
			recipe.AddIngredient(ItemID.SpelunkerPotion);
			recipe.AddIngredient(ItemID.TrapsightPotion);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 20);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}