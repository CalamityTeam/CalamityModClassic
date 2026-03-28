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
	public class Knowledge16 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Queen Bee");
			/* Tooltip.SetDefault("As crude as the giant insects are they can prove useful in certain situations...given the ability to control them.\n" +
				"Place in your inventory to make small bees and weak hornets friendly."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 3;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.queenBeeLore = true;
		}
	}
}