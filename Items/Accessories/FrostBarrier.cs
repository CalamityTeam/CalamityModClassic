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
	public class FrostBarrier : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frost Barrier");
			/* Tooltip.SetDefault("You will freeze enemies near you when you are struck\n" +
			                   "You are immune to the chilled debuff"); */
		}
		
		public override void SetDefaults()
		{
			Item.defense = 4;
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.rare = 3;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.fBarrier = true;
			player.buffImmune[46] = true;
		}
	}
}