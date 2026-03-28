using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
	public class CrawCarapace : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Craw Carapace");
			/* Tooltip.SetDefault("5% increased damage reduction\n" +
				"Enemies take damage when they touch you"); */
		}
		
		public override void SetDefaults()
		{
			Item.defense = 3;
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 1;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.endurance += 0.05f;
			player.thorns = 0.25f;
		}
	}
}