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
	public class Knowledge30 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astrum Aureus");
			/* Tooltip.SetDefault("A titanic cyborg infected by a star-borne disease expelled from the belly of an ancient god.\n" +
                "The destruction of this creature will not prevent the spread of the disease.\n" +
				"Place in your inventory to gain increased jump speed in space."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 7;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.astrumAureusLore = true;
		}
	}
}