using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Polterghast {
public class Affliction : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.value = 500000;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		if (modPlayer.stressLevel300)
		{
			modPlayer.affliction = true;
			if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
			{
				int myPlayer = Main.myPlayer;
				if (Main.player[myPlayer].team == player.team && player.team != 0)
				{
					float arg = player.position.X - Main.player[myPlayer].position.X;
					float num3 = player.position.Y - Main.player[myPlayer].position.Y;
					if ((float)Math.Sqrt((double)(arg * arg + num3 * num3)) < 2800f)
					{
						Main.player[myPlayer].AddBuff(Mod.Find<ModBuff>("Afflicted").Type, 20, true);
					}
				}
			}
		}
		else if (modPlayer.stressLevel0)
		{
			modPlayer.affliction = true;
		}
	}
}}