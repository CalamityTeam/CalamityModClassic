﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.PlaguebringerGoliath {
public class ToxicHeart : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Toxic Heart");
		////Tooltip.SetDefault("Blinds and weakens the player if above 75% life\nSummons a damaging plague aura around the player to destroy nearby enemies");
		Item.width = 28;
		Item.height = 32;
		Item.value = 500000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.statLife >= (player.statLifeMax * 0.75f))
		{
			player.blind = true;
			player.statDefense -= 5;
			player.moveSpeed -= 0.05f;
		}
		int plagueCounter = 0;
		Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.05f, 1f, 0.1f);
		int num = Mod.Find<ModBuff>("Plague").Type;
		float num2 = 200f;
		bool flag = plagueCounter % 60 == 0;
		int num3 = 30;
		int random = Main.rand.Next(10);
		if (player.whoAmI == Main.myPlayer)
		{
			if (random == 0)
			{
				for (int l = 0; l < 200; l++)
				{
					NPC nPC = Main.npc[l];
					if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] && Vector2.Distance(player.Center, nPC.Center) <= num2)
					{
						if (nPC.FindBuffIndex(num) == -1)
						{
							nPC.AddBuff(num, 120, false);
						}
						if (flag)
						{
							nPC.SimpleStrikeNPC(num3, 0, false);
							if (Main.netMode != 0)
							{
								NetMessage.SendData(28, -1, -1, null, l, (float)num3, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
		}
		plagueCounter++;
		if (plagueCounter >= 180)
		{
			plagueCounter = 0;
		}
	}
}}