using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
	public class AmidiasSpark : ModItem
	{
		public override void SetStaticDefaults()
		{
				// DisplayName.SetDefault("Amidias' Spark");
				/* Tooltip.SetDefault("Taking damage releases a blast of sparks\n" +
								   "Sparks do extra damage in Hardmode"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 1;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.aSpark = true;
		}
	}
}