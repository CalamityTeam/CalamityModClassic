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
	public class Knowledge27 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Scourge");
			/* Tooltip.SetDefault("A horror born of pollution and insatiable hunger; based on size alone this was merely a juvenile.\n" +
                "These scourge creatures are the largest aquatic predators and very rarely do they frequent such shallow waters.\n" +
				"Place in your inventory to gain immunity to the sulphurous waters and increase the stat gains from the Well Fed buff."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.aquaticScourgeLore = true;
		}
	}
}