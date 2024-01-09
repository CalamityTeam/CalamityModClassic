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
	public class GiantTortoiseShell : ModItem
	{		
		public override void SetDefaults()
		{
			Item.defense = 12;
			Item.width = 20;
			Item.height = 24;
			Item.value = 100000;
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed -= 0.1f;
			player.thorns = 0.5f;
		}
	}
}