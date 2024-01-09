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
	public class CrawCarapace : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 3;
			Item.width = 20;
			Item.height = 24;
			Item.value = 30000;
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.endurance += 0.05f;
			player.thorns = 0.25f;
		}
	}
}