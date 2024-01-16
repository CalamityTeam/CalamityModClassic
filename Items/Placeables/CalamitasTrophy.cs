using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Placeables
{
	public class CalamitasTrophy : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Calamitas Trophy");
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = 1;
			Item.createTile = Mod.Find<ModTile>("BossTrophy").Type;
			Item.placeStyle = 5;
		}
	}
}