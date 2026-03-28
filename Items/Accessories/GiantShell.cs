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
	public class GiantShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Giant Shell");
			/* Tooltip.SetDefault("15% reduced movement speed\n" +
				"Taking a hit will make you move very fast for a short time"); */
		}
		
		public override void SetDefaults()
		{
			Item.defense = 6;
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 1;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed -= 0.15f;
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.gShell = true;
		}
	}
}