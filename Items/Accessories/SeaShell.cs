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
	public class SeaShell : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 2;
			Item.width = 20;
			Item.height = 24;
			Item.value = 30000;
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
			{
				player.statDefense += 3;
				player.endurance += 0.05f;
				player.moveSpeed += 0.15f;
			}
		}
	}
}