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
	public class Knowledge20 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Mechanical Bosses");
			/* Tooltip.SetDefault("I see you have awakened Draedon's old toys.\n" +
                "Once useful tools turned into savage beasts when their AIs went rogue, a mistake that Draedon failed to rectify in time."); */
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
	}
}