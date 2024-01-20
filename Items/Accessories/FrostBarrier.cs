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
	public class FrostBarrier : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 4;
			Item.width = 20;
			Item.height = 24;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			modPlayer.fBarrier = true;
			player.buffImmune[46] = true;
		}
	}
}