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
	public class Knowledge43 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Bumblebirbs");
			/* Tooltip.SetDefault("A failure of twisted scientific ambition; it appears our faulted arrogance over life has shown once more in the results.\n" +
				"Originally intended to be a clone of the Jungle Dragon, these were left to roam about the jungle, attacking anything in their path."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}