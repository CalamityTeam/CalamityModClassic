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
	public class Knowledge12 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Eater of Worlds");
			/* Tooltip.SetDefault("Perhaps it was just a giant worm infected by the microbe, given centuries to feed and grow its festering body.\n" +
                "Seems likely, given the origins of this place.\n" +
				"Deadly microbes spawn around you while this is placed in your inventory."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 2;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.eaterOfWorldsLore = true;
		}
	}
}