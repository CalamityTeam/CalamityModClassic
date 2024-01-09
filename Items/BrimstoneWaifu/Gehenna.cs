﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.BrimstoneWaifu {
public class Gehenna : ModItem
{
	public const int FireProjectiles = 2;
	public const float FireAngleSpread = 120;
	public int FireCountdown = 0;

	public override void SetStaticDefaults()
 	{
 		//DisplayName.SetDefault("Gehenna");
 		//Tooltip.SetDefault("Drops brimstone fireballs from the sky occasionally");
 	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 100000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (FireCountdown == 0)
		{
			FireCountdown = 300;
		}
		if (FireCountdown > 0)
		{
			FireCountdown--;
			if (FireCountdown == 0)
			{
				if (player.whoAmI == Main.myPlayer)
        		{
					for (int playerIndex = 0; playerIndex < 255; playerIndex++)
					{
						if (Main.player[playerIndex].active)
						{
							Player player2 = Main.player[playerIndex];
							int speed2 = 25;
							float spawnX = Main.rand.Next(1000) - 500 + player2.Center.X;
							float spawnY = -1000 + player2.Center.Y;
							Vector2 baseSpawn = new Vector2(spawnX, spawnY);
							Vector2 baseVelocity = player2.Center - baseSpawn;
							baseVelocity.Normalize();
							baseVelocity = baseVelocity * speed2;
							for (int i = 0; i < FireProjectiles; i++)
							{
								Vector2 spawn = baseSpawn;
								spawn.X = spawn.X + i * 30 - (FireProjectiles * 15);
								Vector2 velocity = baseVelocity;
								velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-FireAngleSpread / 2 + (FireAngleSpread * i / (float)FireProjectiles)));
								velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
								int projectile = Projectile.NewProjectile(player.GetSource_FromThis(), spawn.X, spawn.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BrimstoneHellfireballFriendly2").Type, 54, 5f, Main.myPlayer, 0f, 0f);
								Main.projectile[projectile].tileCollide = false;
								Main.projectile[projectile].timeLeft = 50;
							}
						}
					}
				}
			}
		}
	}
}}