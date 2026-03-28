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
	public class Knowledge29 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astrum Deus");
			/* Tooltip.SetDefault("God of the stars and largest vessel for the Astral Infection.\n" +
				"Though struck down from its place among the stars its remnants have gathered strength, aiming to take its rightful place in the cosmos once more.\n" +
				"Place in your inventory to gain increased movement speed in space."); */
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
			modPlayer.astrumDeusLore = true;
		}
	}
}