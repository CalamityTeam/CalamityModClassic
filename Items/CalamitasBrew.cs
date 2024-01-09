using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class CalamitasBrew : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.rare = ItemRarityID.Orange;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.buffType = Mod.Find<ModBuff>("AbyssalWeapon").Type;
			Item.buffTime = 36000;
			Item.value = 2000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CalamityDust");
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.Register();
		}
	}
}