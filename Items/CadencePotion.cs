using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class CadencePotion : ModItem
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
			Item.buffType = Mod.Find<ModBuff>("Cadence").Type;
			Item.buffTime = 18000;
			Item.value = 10000;
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
		}
	}
}