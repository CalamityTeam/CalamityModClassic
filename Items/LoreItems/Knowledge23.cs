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
	public class Knowledge23 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Skeletron Prime");
			/* Tooltip.SetDefault("What a silly and pointless contraption for something created with the essence of pure terror.\n" +
                "Draedon obviously took several liberties with its design...I am not impressed.\n" +
				"Place in your inventory to gain a boost to your armor penetration."); */
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
			modPlayer.skeletronPrimeLore = true;
		}
	}
}