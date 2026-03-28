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
	public class Knowledge41 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Polterghast");
			/* Tooltip.SetDefault("A creature born of hatred and anger, formed by countless human souls with all of their energy entirely devoted to consuming others.\n" +
				"It seems a waste to have had such a potent source of power ravage mindlessly through these empty halls.\n" +
				"Place in your inventory to gain increased item grab range."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.polterghastLore = true;
		}
	}
}