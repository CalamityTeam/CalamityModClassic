using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class HolyWrathPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Holy Wrath Potion");
			/* Tooltip.SetDefault("Increases damage by 12% and increases movement and horizontal flight speed by 5%\n" +
				"Attacks inflict holy fire"); */
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
			Item.buffType = Mod.Find<ModBuff>("HolyWrathBuff").Type;
			Item.buffTime = 10800;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WrathPotion);
			recipe.AddIngredient(null, "UnholyEssence");
			recipe.AddIngredient(null, "GalacticaSingularity");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "BloodOrb", 40);
			recipe.AddIngredient(null, "UnholyEssence");
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}