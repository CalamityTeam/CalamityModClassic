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
	public class FungalSymbiote : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fungal Symbiote");
			/* Tooltip.SetDefault("True melee weapons emit mushrooms when swung\n" +
				"Boosts true melee damage by 25%"); */
		}

		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 36;
			Item.value = Item.buyPrice(0, 9, 0, 0);
			Item.rare = 3;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.fungalSymbiote = true;
		}
	}
}