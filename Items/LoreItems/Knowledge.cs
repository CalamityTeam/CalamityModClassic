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
	public class Knowledge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Desert Scourge");
			/* Tooltip.SetDefault("The great sea worm appears to have survived the extreme heat and has even adapted to it.\n" +
                "What used to be a majestic beast swimming through the water has now become a dried-up and\n" +
                "gluttonous husk, constantly on a voracious search for its next meal.\n" +
				"Place in your inventory for an increase to defense while in the desert or sunken sea."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 1;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			if (player.ZoneDesert || player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				player.GetModPlayer<CalamityPlayerPreTrailer>().desertScourgeLore = true;
			}
		}
	}
}