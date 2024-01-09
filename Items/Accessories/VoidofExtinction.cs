using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class VoidofExtinction : ModItem
{
	public const int FireProjectiles = 2;
	public const float FireAngleSpread = 120;
	public int FireCountdown = 0;
	
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Void of Extinction");
		//Tooltip.SetDefault("No longer cursed\nDrops brimstone fireballs from the sky occasionally\n10% increase to all damage\nBrimstone fire rains down while invincibility is active");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 500000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "Gehenna");
		recipe.AddIngredient(null, "CalamityRing");
		recipe.AddTile(TileID.MythrilAnvil);
		recipe.Register();
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetDamage(DamageClass.Throwing) += 0.1f;
        player.GetDamage(DamageClass.Ranged) += 0.1f;
        player.GetDamage(DamageClass.Magic) += 0.1f;
        player.GetDamage(DamageClass.Summon) += 0.1f;
		if (player.immune)
		{
			if (Main.rand.NextBool(8))
			{
				if (player.whoAmI == Main.myPlayer)
        		{
			 	    for (int l = 0; l < 1; l++)
					{
						float x = player.position.X + (float)Main.rand.Next(-400, 400);
						float y = player.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num15 = player.position.X + (float)(player.width / 2) - vector.X;
						float num16 = player.position.Y + (float)(player.height / 2) - vector.Y;
						num15 += (float)Main.rand.Next(-100, 101);
						int num17 = 22;
						float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
						num18 = (float)num17 / num18;
						num15 *= num18;
						num16 *= num18;
						int num19 = Projectile.NewProjectile(player.GetSource_FromThis(), x, y, num15, num16, Mod.Find<ModProjectile>("StandingFire").Type, 40, 5f, player.whoAmI, 0f, 0f);
						Main.projectile[num19].ai[1] = player.position.Y;
					}
				}
			}
        }
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
								int projectile = Projectile.NewProjectile(player.GetSource_FromThis(), spawn.X, spawn.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BrimstoneHellfireballFriendly2").Type, 70, 5f, Main.myPlayer, 0f, 0f);
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