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
	public class Knowledge32 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Plaguebringer Goliath");
			/* Tooltip.SetDefault("A horrific amalgam of steel, flesh, and infection, capable of destroying an entire civilization in just one onslaught.\n" +
				"Its plague nuke barrage can leave an entire area uninhabitable for months. A shame that it came to this but the plague must be contained.\n" +
				"Place in your inventory to gain increased wing flight time but at the cost of reduced life regen."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 8;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.plaguebringerGoliathLore = true;
		}
	}
}