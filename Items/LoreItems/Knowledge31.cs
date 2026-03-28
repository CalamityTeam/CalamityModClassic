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
	public class Knowledge31 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Golem");
			/* Tooltip.SetDefault("A primitive construct.\n" +
				"I admire the lihzahrd race for their ingenuity, though finding faith in such a flawed idol would invariably lead to their downfall.\n" +
				"Place in your inventory to gain increased defense while standing still."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 8;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.golemLore = true;
		}
	}
}