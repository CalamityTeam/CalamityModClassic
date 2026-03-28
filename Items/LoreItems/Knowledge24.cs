using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.LoreItems
{
	public class Knowledge24 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Calamitas Doppelganger");
			/* Tooltip.SetDefault("You are indeed stronger than I thought.\n" +
                "Though the bloody inferno still lingers, observing your progress.\n" +
				"Place in your inventory to gain a boost to your minion slots but at the cost of reduced max health."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.calamitasLore = true;
		}
	}
}