﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BrinyTyphoonBubble : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bubble");
			Main.projFrames[Projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
        }
        
        public override void AI()
        {
			if (Projectile.ai[1] > 0f)
			{
				int num625 = (int)Projectile.ai[1] - 1;
				if (num625 < 255)
				{
					Projectile.localAI[0] += 1f;
					if (Projectile.localAI[0] > 10f)
					{
						int num626 = 6;
						for (int num627 = 0; num627 < num626; num627++)
						{
							Vector2 vector45 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
							vector45 = vector45.RotatedBy((double)(num627 - (num626 / 2 - 1)) * 3.1415926535897931 / (double)((float)num626), default(Vector2)) + Projectile.Center;
							Vector2 value15 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
							int num628 = Dust.NewDust(vector45 + value15, 0, 0, DustID.Flare_Blue, value15.X * 2f, value15.Y * 2f, 100, new Color(53, Main.DiscoG, 255), 1.4f);
							Main.dust[num628].noGravity = true;
							Main.dust[num628].noLight = true;
							Main.dust[num628].velocity /= 4f;
							Main.dust[num628].velocity -= Projectile.velocity;
						}
						Projectile.alpha -= 5;
						if (Projectile.alpha < 100)
						{
							Projectile.alpha = 100;
						}
						Projectile.rotation += Projectile.velocity.X * 0.1f;
						Projectile.frame = (int)(Projectile.localAI[0] / 3f) % 3;
					}
					Vector2 value16 = Main.player[num625].Center - Projectile.Center;
					float num629 = 4f;
					num629 += Projectile.localAI[0] / 20f;
					Projectile.velocity = Vector2.Normalize(value16) * num629;
					if (value16.Length() < 50f)
					{
						Projectile.Kill();
					}
				}
			}
			else
			{
				float num630 = 0.209439516f;
				float num631 = 4f;
				float num632 = (float)(Math.Cos((double)(num630 * Projectile.ai[0])) - 0.5) * num631;
				Projectile.velocity.Y = Projectile.velocity.Y - num632;
				Projectile.ai[0] += 1f;
				num632 = (float)(Math.Cos((double)(num630 * Projectile.ai[0])) - 0.5) * num631;
				Projectile.velocity.Y = Projectile.velocity.Y + num632;
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] > 10f)
				{
					Projectile.alpha -= 5;
					if (Projectile.alpha < 100)
					{
						Projectile.alpha = 100;
					}
					Projectile.rotation += Projectile.velocity.X * 0.1f;
					Projectile.frame = (int)(Projectile.localAI[0] / 3f) % 3;
				}
			}
			if (Projectile.wet)
			{
				Projectile.position.Y = Projectile.position.Y - 16f;
				Projectile.Kill();
				return;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	bool kingSlime = NPC.downedSlimeKing;
			bool eyeOfCthulhu = NPC.downedBoss1;
			bool eaterOrBrain = NPC.downedBoss2;
			bool queenBee = NPC.downedQueenBee;
			bool skeletron = NPC.downedBoss3;
			bool wallOfFlesh = Main.hardMode;
			bool anyMechBoss = NPC.downedMechBossAny;
			bool plantera = NPC.downedPlantBoss;
			bool golem = NPC.downedGolemBoss;
			bool dukeFish = NPC.downedFishron;
			bool cultist = NPC.downedAncientCultist;
			bool moonLord = NPC.downedMoonlord;
			bool providence = CalamityWorld1Point2.downedProvidence;
			bool devourerOfGods = CalamityWorld1Point2.downedDoG;
			bool yharon = CalamityWorld1Point2.downedYharon;
			float sizeAdd = 1f +
				(kingSlime ? 1f : 0f) + //1
				(eyeOfCthulhu ? 1f : 0f) + //2
				(eaterOrBrain ? 1f : 0f) + //3
				(queenBee ? 1f : 0f) + //4
				(skeletron ? 1f : 0f) + //5
				(wallOfFlesh ? 1f : 0f) + //6
				(anyMechBoss ? 1f : 0f) + //7
				(plantera ? 1f : 0f) + //8
				(golem ? 1f : 0f) + //9
				(dukeFish ? 1f : 0f) + //10
				(cultist ? 1f : 0f) + //11
				(moonLord ? 1f : 0f) + //12
				(providence ? 1f : 0f) + //13
				(devourerOfGods ? 1f : 0f) + //14
				(yharon ? 1f : 0f); //15
        	SoundEngine.PlaySound(SoundID.Item96, Projectile.Center);
			int num226 = 36;
			for (int num227 = 0; num227 < num226; num227++)
			{
				Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
				vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
				Vector2 vector7 = vector6 - Projectile.Center;
				int num228 = Dust.NewDust(vector6 + vector7, 0, 0, DustID.Flare_Blue, vector7.X * 2f, vector7.Y * 2f, 100, new Color(53, Main.DiscoG, 255), 1.4f);
				Main.dust[num228].noGravity = true;
				Main.dust[num228].noLight = true;
				Main.dust[num228].velocity = vector7;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				if (dukeFish)
				{
					int num330 = (int)(Projectile.Center.Y / 16f);
					int num331 = (int)(Projectile.Center.X / 16f);
					int num332 = 100;
					if (num331 < 10)
					{
						num331 = 10;
					}
					if (num331 > Main.maxTilesX - 10)
					{
						num331 = Main.maxTilesX - 10;
					}
					if (num330 < 10)
					{
						num330 = 10;
					}
					if (num330 > Main.maxTilesY - num332 - 10)
					{
						num330 = Main.maxTilesY - num332 - 10;
					}
					for (int num333 = num330; num333 < num330 + num332; num333++)
					{
						Tile tile = Main.tile[num331, num333];
						if (tile.HasTile && (Main.tileSolid[(int)tile.TileType] || tile.LiquidAmount != 0))
						{
							num330 = num333;
							break;
						}
					}
					int num335 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), (float)(num331 * 16 + 8), (float)(num330 * 16 - 24), 0f, 0f, Mod.Find<ModProjectile>("BrinySpout").Type, Projectile.damage, 6f, Main.myPlayer, 16f, 15f + sizeAdd);
					Main.projectile[num335].netUpdate = true;
				}
				else
				{
					int num230 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - (float)(Projectile.direction * 30), Projectile.Center.Y - 4f, (float)(-(float)Projectile.direction) * 0.01f, 0f, Mod.Find<ModProjectile>("BrinySpout").Type, Projectile.damage, 3f, Projectile.owner, 16f, 5f + sizeAdd);
					Main.projectile[num230].netUpdate = true;
				}
			}
        }
    }
}