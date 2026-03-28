using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.ShrineItems
{
	public class CorruptionEffigy: ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Corruption Effigy");
			/* Tooltip.SetDefault("When placed down nearby players have their movement speed increased by 15% and crit chance by 10%\n" +
				"Nearby players also suffer a 20% decrease to their damage reduction"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 9, 0, 0);
			Item.rare = 3;
			Item.createTile = Mod.Find<ModTile>("CorruptionEffigy").Type;
		}
    }
}