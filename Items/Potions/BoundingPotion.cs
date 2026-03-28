using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class BoundingPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bounding Potion");
			// Tooltip.SetDefault("Increases jump height, jump speed, and fall damage resistance");
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
			Item.buffType = Mod.Find<ModBuff>("BoundingBuff").Type;
			Item.buffTime = 10800;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.Frog);
			recipe.AddIngredient(null, "ManeaterBulb");
			recipe.AddTile(TileID.Bottles);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 20);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}