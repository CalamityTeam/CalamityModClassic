using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class TitanScalePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Titan Scale Potion");
			//Tooltip.SetDefault("Increases knockback, defense by 5, and damage reduction by 5%");
		}
		
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
			Item.buffType = Mod.Find<ModBuff>("TitanScale").Type;
			Item.buffTime = 18000;
			Item.value = 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TitanPotion);
			recipe.AddIngredient(ItemID.BeetleHusk);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}