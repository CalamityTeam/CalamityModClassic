using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class CalamitasBrew : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Calamitas' Brew");
			/* Tooltip.SetDefault("Adds abyssal flames to your melee projectiles and melee attacks\n" +
							   "Increases your movement speed by 15%"); */
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
			Item.buffType = Mod.Find<ModBuff>("AbyssalWeapon").Type;
			Item.buffTime = 36000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "CalamityDust");
			recipe.AddTile(TileID.ImbuingStation);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 20);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.Register();
		}
	}
}