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
	public class Knowledge37 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Moon Lord");
			/* Tooltip.SetDefault("What a waste.\n" +
				"Had it been fully restored it would have been a force to behold, but what you fought was an empty shell.\n" +
				"However, that doesn't diminish the immense potential locked within it, released upon its death.\n" +
				"Place in your inventory to gain an improved Gravity Globe that gives you an increase to all stats while upside down."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.moonLordLore = true;
		}
	}
}