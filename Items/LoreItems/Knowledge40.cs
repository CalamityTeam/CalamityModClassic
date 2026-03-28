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
	public class Knowledge40 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Sentinels of the Devourer");
			/* Tooltip.SetDefault("Signus. The Void. The Weaver.\n" +
				"Each represent one of the Devourer�s largest spheres of influence.\n" +
				"Dispatching them has most likely invoked its anger and marked you as a target for destruction."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}