using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class CadencePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cadance Potion");
			/* Tooltip.SetDefault("Gives the cadence buff which reduces shop prices and enemy aggro\n" +
							   "Increases life regen and increases max life by 10%\n" +
							   "Increases heart pickup range"); */
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
			Item.buffType = Mod.Find<ModBuff>("Cadence").Type;
			Item.buffTime = 18000;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LovePotion);
			recipe.AddIngredient(ItemID.HeartreachPotion);
			recipe.AddIngredient(ItemID.LifeforcePotion);
			recipe.AddIngredient(ItemID.RegenerationPotion);
			recipe.AddIngredient(ItemID.CalmingPotion);
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