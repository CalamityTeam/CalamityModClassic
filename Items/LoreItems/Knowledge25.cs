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
	public class Knowledge25 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plantera");
			/* Tooltip.SetDefault("Well done, you killed a plant.\n" +
                "It was used as a vessel to house the spirits of those unfortunate enough to find their way down here.\n" +
                "I wish you luck in dealing with the fallout.\n" +
				"Place in your inventory to gain a boost to your item grab range."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 6;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.planteraLore = true;
		}
	}
}