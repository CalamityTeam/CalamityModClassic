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
	public class Knowledge9 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Corruption");
			/* Tooltip.SetDefault("The rotten and forever-deteriorating landscape of infected life, brought upon by a deadly microbe long ago.\n" +
				"It is rumored that the microbe was created through experimentation by a long-dead race, predating the Terrarians."); */
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
	}
}