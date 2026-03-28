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
	public class ManaJelly : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mana Jelly");
			/* Tooltip.SetDefault("+20 max mana\n" +
				"Standing still boosts mana regen"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 6, 0, 0);
            Item.rare = 1;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statManaMax2 += 20;
			if ((double)Math.Abs(player.velocity.X) < 0.05 && (double)Math.Abs(player.velocity.Y) < 0.05 && player.itemAnimation == 0)
			{
				player.manaRegenBonus += 2;
			}
		}
	}
}