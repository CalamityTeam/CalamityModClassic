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
	public class LuxorsGift : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Luxor's Gift");
			/* Tooltip.SetDefault("Weapons fire unique projectiles based on the damage type they have\n" +
				"Some weapons are unable to receive this bonus"); */
		}

		public override void SetDefaults()
		{
			Item.width = 58;
			Item.height = 48;
			Item.value = Item.buyPrice(0, 9, 0, 0);
			Item.rare = 3;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.luxorsGift = true;
		}
	}
}