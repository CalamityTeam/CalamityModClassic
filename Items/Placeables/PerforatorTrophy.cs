using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassic1Point2.Items.Placeables
{
	public class PerforatorTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Perforator Trophy");
		}
		
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = Mod.Find<ModTile>("BossTrophy").Type;
			Item.placeStyle = 1;
		}
	}
}