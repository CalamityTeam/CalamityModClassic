using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class PinkCandle: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vigorous Candle");
			/* Tooltip.SetDefault("When placed down nearby players regenerate 0.4% of their maximum health per second\n" +
							   "This is separate from normal life regeneration, bypassing caps and not being affected by damage taken or current movement"); */
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 40;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.rare = 6;
			Item.createTile = Mod.Find<ModTile>("PinkCandle").Type;
		}
    }
}