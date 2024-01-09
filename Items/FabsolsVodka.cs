using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items
{
	public class FabsolsVodka : ModItem
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
			Item.buffType = Mod.Find<ModBuff>("FabsolVodka").Type;
			Item.buffTime = 54000;
			Item.value = 10000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ale);
			recipe.AddIngredient(ItemID.PixieDust, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 5);
			recipe.AddIngredient(ItemID.UnicornHorn);
			recipe.AddTile(TileID.Kegs);
			recipe.Register();
		}
	}
}