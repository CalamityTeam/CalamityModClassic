using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class MushroomPlasmaRoot : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Plasma Root");
			/* Tooltip.SetDefault("Permanently makes Rage Mode do 15% (60% in Death Mode) more damage\n" +
                "Revengeance drop"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.rare = 2;
			Item.useTime = 30;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item122;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (modPlayer.rageBoostOne)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
				modPlayer.rageBoostOne = true;
			}
			return true;
		}
	}
}