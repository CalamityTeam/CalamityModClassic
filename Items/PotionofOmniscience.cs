using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class PotionofOmniscience : ModItem
	{
		public override void SetStaticDefaults()
	 	{
	 		//DisplayName.SetDefault("Potion of Omniscience");
	 		//Tooltip.SetDefault("Gives creature, danger, and treasure detection");
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
			Item.buffType = Mod.Find<ModBuff>("Omniscience").Type;
			Item.buffTime = 18000;
			Item.value = 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HunterPotion);
			recipe.AddIngredient(ItemID.SpelunkerPotion);
			recipe.AddIngredient(ItemID.TrapsightPotion);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.Register();
		}
	}
}