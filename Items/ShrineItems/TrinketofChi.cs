using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.ShrineItems
{
	public class TrinketofChi : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trinket of Chi");
			/* Tooltip.SetDefault("After 2 seconds of standing still and not attacking you gain a buff\n" +
				"This buff boosts your damage by 50% and decreases damage taken by 15%\n" +
				"The buff deactivates after you move or attack once"); */
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 32;
			Item.value = Item.buyPrice(0, 9, 0, 0);
			Item.rare = 3;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.trinketOfChi = true;
		}
	}
}