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
	public class Knowledge38 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Profaned Guardians");
			/* Tooltip.SetDefault("The ever-rejuvenating guardians of the profaned flame.\n" +
				"Much like a phoenix from the ashes their deaths are simply a part of their life cycle.\n" +
				"Many times my forces have had to destroy these beings in search of the Profaned Goddess."); */
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