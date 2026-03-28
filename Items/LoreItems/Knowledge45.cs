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
	public class Knowledge45 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Calamitas");
			/* Tooltip.SetDefault("The witch unrivaled. Perhaps the only one amongst my cohorts to have ever given me cause for doubt.\n" +
				"Now that you have defeated her your destiny is clear.\n" +
				"Come now, face me.\n" +
				"Place in your inventory."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.SCalLore = true;
		}
	}
}