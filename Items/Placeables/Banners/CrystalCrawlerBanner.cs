using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables.Banners
{
	public class CrystalCrawlerBanner : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 24;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.rare = 1;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.createTile = Mod.Find<ModTile>("MonsterBanner").Type;
			Item.placeStyle = 73;
		}
	}
}