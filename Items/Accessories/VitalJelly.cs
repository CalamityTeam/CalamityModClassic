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
	public class VitalJelly : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vital Jelly");
			/* Tooltip.SetDefault("10% increased movement speed\n" +
				"100% increased jump speed"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed += 0.1f;
        	player.jumpSpeedBoost += 1.0f;
		}
	}
}