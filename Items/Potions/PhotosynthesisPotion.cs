using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class PhotosynthesisPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Photosynthesis Potion");
			/* Tooltip.SetDefault("You regen life quickly while not moving, this effect is five times as strong during daytime\n" +
				"Dropped hearts heal more HP"); */
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
			Item.buffType = Mod.Find<ModBuff>("PhotosynthesisBuff").Type;
			Item.buffTime = 18000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BeetleJuice", 2);
			recipe.AddIngredient(null, "ManeaterBulb");
			recipe.AddIngredient(null, "TrapperBulb");
			recipe.AddIngredient(null, "EssenceofCinder");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 40);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}