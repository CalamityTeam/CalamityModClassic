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
	public class GiantTortoiseShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Giant Tortoise Shell");
			/* Tooltip.SetDefault("10% reduced movement speed\n" +
				"Enemies take damage when they hit you"); */
		}
		
		public override void SetDefaults()
		{
			Item.defense = 8;
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.moveSpeed -= 0.1f;
			player.thorns = 0.25f;
		}
	}
}