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
	public class Knowledge18 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wall of Flesh");
			/* Tooltip.SetDefault("I see the deed is done.\n" +
                "The unholy amalgamation of flesh and hatred has been defeated.\n" +
                "Prepare to face the terrors that lurk in the light and dark parts of this world.\n" +
				"Place in your inventory to gain increased item grab range."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 4;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.wallOfFleshLore = true;
		}
	}
}