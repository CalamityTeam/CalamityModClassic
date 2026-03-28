using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class ZenPotion : ModItem
	{
		public override void SetStaticDefaults()
	 	{
	 		// DisplayName.SetDefault("Zen Potion");
	 		// Tooltip.SetDefault("Reduces spawn rates...a lot...");
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
			Item.buffType = Mod.Find<ModBuff>("Zen").Type;
			Item.buffTime = 36000;
			Item.value = 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EssenceofEleum", 3);
            recipe.AddIngredient(null, "EbonianGel", 2);
            recipe.AddIngredient(ItemID.PinkGel);
            recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodOrb", 20);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.Register();
        }
	}
}