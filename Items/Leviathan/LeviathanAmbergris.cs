using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Leviathan {
public class LeviathanAmbergris : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Leviathan Ambergris");
		////Tooltip.SetDefault("You leave behind poisonous seawater as you move\n75% increased movement speed, 10% increase to all damage, and plus 20 defense while submerged in liquid\nIf you are damaged while submerged in liquid you will gain a damaging aura for a short time\nBeing outside of liquid decreases all damage done by 5% but increases damage reduction by 5%");
		Item.width = 20;
		Item.height = 22;
		Item.value = 900000;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.ignoreWater = true;
		bool wet = player.wet;
		bool honey = player.honeyWet;
		bool lava = player.lavaWet;
		if (!wet && !honey && !lava)
		{
			player.endurance += 0.05f;
			player.GetDamage(DamageClass.Melee) *= 0.95f;
	        player.GetDamage(DamageClass.Throwing) *= 0.95f;
	        player.GetDamage(DamageClass.Ranged) *= 0.95f;
	        player.GetDamage(DamageClass.Magic) *= 0.95f;
	        player.GetDamage(DamageClass.Summon) *= 0.95f;
		}
		if (wet || honey || lava)
		{
			player.GetDamage(DamageClass.Melee) *= 1.1f;
	        player.GetDamage(DamageClass.Throwing) *= 1.1f;
	        player.GetDamage(DamageClass.Ranged) *= 1.1f;
	        player.GetDamage(DamageClass.Magic) *= 1.1f;
	        player.GetDamage(DamageClass.Summon) *= 1.1f;
	        player.statDefense += 20;
	        player.moveSpeed += 0.75f;
		}
		if (((double)player.velocity.X > 0 || (double)player.velocity.Y > 0 || (double)player.velocity.X < -0.1 || (double)player.velocity.Y < -0.1))
		{
			int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PoisonousSeawater").Type, 50, 5f, player.whoAmI, 0f, 0f);
			Main.projectile[projectile1].timeLeft = 10;
		}
		int seaCounter = 0;
		Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0f, 0.5f, 1.25f);
		int num = BuffID.Venom;
		float num2 = 200f;
		bool flag = seaCounter % 60 == 0;
		int num3 = 15;
		int random = Main.rand.Next(5);
		if (player.whoAmI == Main.myPlayer)
		{
			if (random == 0 && player.immune && (wet || honey || lava))
			{
				for (int l = 0; l < 200; l++)
				{
					NPC nPC = Main.npc[l];
					if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] && Vector2.Distance(player.Center, nPC.Center) <= num2)
					{
						if (nPC.FindBuffIndex(num) == -1)
						{
							nPC.AddBuff(num, 300, false);
						}
						if (flag)
						{
							nPC.SimpleStrikeNPC(num3, 0, false);
							if (Main.netMode != 0)
							{
								NetMessage.SendData(28, -1, -1,null, l, (float)num3, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
		}
		seaCounter++;
		if (seaCounter >= 180)
		{
			seaCounter = 0;
		}
	}
}}