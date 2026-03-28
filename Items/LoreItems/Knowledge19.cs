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
	public class Knowledge19 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryogen");
			/* Tooltip.SetDefault("You managed to bring down what dozens of sorcerers long ago could not.\n" +
                "I am unsure if it has grown weaker over the decades of imprisonment.\n" +
				"Place in your inventory to gain a frost dash that freezes enemies."); */
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
			modPlayer.dashMod = 6;
		}
	}
}