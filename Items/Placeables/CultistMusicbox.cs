using System;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
	public class CultistMusicbox : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Music Box (Cultist)");
		}

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("CultistMusicbox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 4;
			Item.value = 100000;
			Item.accessory = true;
		}
	}
}
