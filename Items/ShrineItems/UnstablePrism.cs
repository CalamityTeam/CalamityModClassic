using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.ShrineItems
{
	public class UnstablePrism : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Unstable Prism");
			// Tooltip.SetDefault("Three sparks are released on critical hits");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 38;
			Item.value = Item.buyPrice(0, 9, 0, 0);
			Item.rare = 3;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.unstablePrism = true;
		}
	}
}