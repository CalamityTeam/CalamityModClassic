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
	public class Knowledge34 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Red Moon");
			/* Tooltip.SetDefault("We long ago feared the light of the red moon.\n" +
                "Many went mad, others died, but a scant few became blessed with a wealth of cosmic understanding."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 9;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}