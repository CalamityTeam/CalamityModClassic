using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items
{
	public class MLGRune2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Celestial Onion");
			/* Tooltip.SetDefault("Doesn't do anything currently...or does it!?\n" +
			                   "Consuming it does something that cannot be reversed"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.rare = 10;
			Item.maxStack = 99;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (modPlayer.extraAccessoryML)
			{
				return false;
			}
			return true;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.itemAnimation > 0 && !modPlayer.extraAccessoryML && player.itemTime == 0)
			{
				player.itemTime = Item.useTime;
				modPlayer.extraAccessoryML = true;
				if (!CalamityWorldPreTrailer.onionMode)
				{
					CalamityWorldPreTrailer.onionMode = true;
				}
			}
			return true;
		}
	}
}