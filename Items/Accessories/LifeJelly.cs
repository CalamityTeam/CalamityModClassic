using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class LifeJelly : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = 30000;
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 += 20;
			if ((double)Math.Abs(player.velocity.X) < 0.05 && (double)Math.Abs(player.velocity.Y) < 0.05 && player.itemAnimation == 0)
			{
				player.lifeRegen += 5;
			}
		}
	}
}