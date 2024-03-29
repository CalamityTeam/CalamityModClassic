﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2;
using Terraria.WorldBuilding;

namespace CalamityModClassic1Point2.NPCs.SlimeGod
{
	public class SlimeGodRunSplit : ModNPC
	{
		public float bossLife;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crimulan Slime God");
			Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.lifeMax = 1500;
			NPC.damage = 50;
			NPC.width = 150;
			NPC.height = 92;
			NPC.scale = 0.8f;
			NPC.defense = 20;
			NPC.knockBackResist = 0f;
			AnimationType = 50;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.boss = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			Music = MusicID.Boss1;
			NPC.aiStyle = -1;
			AIType = -1;
			//bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("SlimeGodBag").Type;
		}
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld1Point2.revenge;
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if ((Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("SlimeGod").Type)) || (Main.npc[num569].active && Main.npc[num569].type == Mod.Find<ModNPC>("SlimeGodSplit").Type))
				{
					flag100 = true;
				}
			}
			if (!flag100)
			{
				NPC.defense = revenge ? 45 : 30;
			}
			if (!flag100)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[0] += (float)Main.rand.Next(5);
					if (expertMode)
					{
						if (NPC.localAI[0] >= (float)Main.rand.Next(2100, 2400))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
							{
								float num179 = revenge ? 9f : 11f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = 20;
								int num185 = Mod.Find<ModProjectile>("AbyssMine2").Type;
								value9.X += num180;
								value9.Y += num182;
								for (int num186 = 0; num186 < 6; num186++)
								{
									num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
									num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 12f / num183;
									num180 += (float)Main.rand.Next(-360, 361);
									num182 += (float)Main.rand.Next(-360, 361);
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
					}
					if (NPC.localAI[0] >= (float)Main.rand.Next(2100, 2400))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float num179 = revenge ? 12f : 11f;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num184 = expertMode ? 21 : 23;
							int num185 = Mod.Find<ModProjectile>("AbyssBallVolley2").Type;
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 6; num186++)
							{
								num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
								num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = 12f / num183;
								num180 += (float)Main.rand.Next(-60, 61);
								num182 += (float)Main.rand.Next(-60, 61);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
			}
         	NPC.aiAction = 0;
            NPC.knockBackResist = 0.2f * Main.GameModeInfo.KnockbackToEnemiesMultiplier;
            NPC.dontTakeDamage = false;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.reflectsProjectiles = false;
            if (NPC.ai[0] != 7f && Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.ai[0] = 7f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                }
            }
            else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
            if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(true);
                Vector2 vector271 = Main.player[NPC.target].Center - NPC.Center;
                NPC.ai[0] = 1f;
                NPC.ai[1] = 0f;
            }
            else if (NPC.ai[0] == 1f)
            {
                NPC.ai[1] += 2f;
                if (NPC.ai[1] > 36f)
                {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    return;
                }
            }
            else if (NPC.ai[0] == 2f)
            {
                if ((Main.player[NPC.target].Center - NPC.Center).Length() > 1200f)
                {
                    NPC.ai[0] = 5f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                }
                if (NPC.velocity.Y == 0f)
                {
                    NPC.TargetClosest(true);
                    NPC.velocity.X = NPC.velocity.X * 0.85f;
                    NPC.ai[1] += 1f;
                    float num1879 = 15f + 30f * ((float)NPC.life / (float)NPC.lifeMax);
                    float num1880 = 3f + 4f * (1f - (float)NPC.life / (float)NPC.lifeMax);
                    float num1881 = 4f;
                    if (!Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                    {
                        num1881 += 2f;
                    }
                    if (NPC.ai[1] > num1879)
                    {
                        NPC.ai[3] += 1f;
                        if (NPC.ai[3] >= 3f)
                        {
                            NPC.ai[3] = 0f;
                            num1881 *= 2f;
                            num1880 /= 2f;
                        }
                        NPC.ai[1] = 0f;
                        NPC.velocity.Y = NPC.velocity.Y - num1881;
                        NPC.velocity.X = num1880 * (float)NPC.direction;
                    }
                }
                else
                {
                    NPC.knockBackResist = 0f;
                    NPC.velocity.X = NPC.velocity.X * 0.99f;
                    if (NPC.direction < 0 && NPC.velocity.X > -1f)
                    {
                        NPC.velocity.X = -1f;
                    }
                    if (NPC.direction > 0 && NPC.velocity.X < 1f)
                    {
                        NPC.velocity.X = 1f;
                    }
                }
                NPC.ai[2] += 1f;
                if ((double)NPC.ai[2] > 210.0 && NPC.velocity.Y == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int num1882 = Main.rand.Next(3);
                    if (num1882 == 0)
                    {
                        NPC.ai[0] = 3f;
                    }
                    else if (num1882 == 1)
                    {
                        NPC.ai[0] = 4f;
                        NPC.noTileCollide = true;
                        NPC.velocity.Y = -8f;
                    }
                    else if (num1882 == 2)
                    {
                        NPC.ai[0] = 6f;
                    }
                    else
                    {
                        NPC.ai[0] = 2f;
                    }
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    return;
                }
            }
            else if (NPC.ai[0] == 3f)
            {
                NPC.velocity.X = NPC.velocity.X * 0.85f;
                NPC.ai[1] += 1f;
                if (expertMode)
                {
                	NPC.localAI[0] += (float)Main.rand.Next(10, 25);
	                if (NPC.ai[1] >= 120f)
	                {
	                    NPC.ai[0] = 2f;
	                    NPC.ai[1] = 0f;
	                }
                }
                else
                {
                	NPC.localAI[0] += (float)Main.rand.Next(5, 15);
                	if (NPC.ai[1] >= 120f)
                	{
                    	NPC.ai[0] = 2f;
                    	NPC.ai[1] = 0f;
                	}
                }
            }
            else if (NPC.ai[0] == 4f)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                NPC.knockBackResist = 0f;
                if (NPC.velocity.X < 0f)
                {
                    NPC.direction = -1;
                }
                else
                {
                    NPC.direction = 1;
                }
                NPC.spriteDirection = NPC.direction;
                NPC.TargetClosest(true);
                Vector2 center40 = Main.player[NPC.target].Center;
                center40.Y -= 350f;
                Vector2 vector272 = center40 - NPC.Center;
                if (NPC.ai[2] == 1f)
                {
                    NPC.ai[1] += 1f;
                    vector272 = Main.player[NPC.target].Center - NPC.Center;
                    vector272.Normalize();
                    vector272 *= 8f;
                    NPC.velocity = (NPC.velocity * 4f + vector272) / 5f;
                    if (NPC.ai[1] > 6f)
                    {
                        NPC.ai[1] = 0f;
                        NPC.ai[0] = 4.1f;
                        NPC.ai[2] = 0f;
                        NPC.velocity = vector272;
                        return;
                    }
                }
                else
                {
                    if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 40f && NPC.Center.Y < Main.player[NPC.target].Center.Y - 300f)
                    {
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 1f;
                        return;
                    }
                    vector272.Normalize();
                    vector272 *= 12f;
                    NPC.velocity = (NPC.velocity * 5f + vector272) / 6f;
                    return;
                }
            }
            else if (NPC.ai[0] == 4.1f)
            {
                NPC.knockBackResist = 0f;
                if (NPC.ai[2] == 0f && Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1) && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[2] = 1f;
                }
                if (NPC.position.Y + (float)NPC.height >= Main.player[NPC.target].position.Y || NPC.velocity.Y <= 0f)
                {
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] > 10f)
                    {
                        NPC.ai[0] = 2f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                        if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                        {
                            NPC.ai[0] = 5f;
                        }
                    }
                }
                else if (NPC.ai[2] == 0f)
                {
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    NPC.knockBackResist = 0f;
                }
                NPC.velocity.Y = NPC.velocity.Y + 0.2f;
                if (NPC.velocity.Y > 16f)
                {
                    NPC.velocity.Y = 16f;
                    return;
                }
            }
            else
            {
                if (NPC.ai[0] == 5f)
                {
                    if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.direction = -1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.noTileCollide = true;
                    NPC.noGravity = true;
                    NPC.knockBackResist = 0f;
                    Vector2 value74 = Main.player[NPC.target].Center - NPC.Center;
                    value74.Y -= 4f;
                    if (value74.Length() < 200f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[0] = 2f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                    }
                    if (value74.Length() > 10f)
                    {
                        value74.Normalize();
                        value74 *= 10f;
                    }
                    NPC.velocity = (NPC.velocity * 4f + value74) / 5f;
                    return;
                }
                if (NPC.ai[0] == 6f)
                {
                    NPC.knockBackResist = 0f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.TargetClosest(true);
                        NPC.velocity.X = NPC.velocity.X * 0.8f;
                        NPC.ai[1] += 1f;
                        if (NPC.ai[1] > 5f)
                        {
                            NPC.ai[1] = 0f;
                            NPC.velocity.Y = NPC.velocity.Y - 4f;
                            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height < NPC.Center.Y)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 1.25f;
                            }
                            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height < NPC.Center.Y - 40f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 1.5f;
                            }
                            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height < NPC.Center.Y - 80f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 1.75f;
                            }
                            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height < NPC.Center.Y - 120f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 2f;
                            }
                            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height < NPC.Center.Y - 160f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 2.25f;
                            }
                            if (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height < NPC.Center.Y - 200f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 2.5f;
                            }
                            if (!Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1))
                            {
                                NPC.velocity.Y = NPC.velocity.Y - 2f;
                            }
                            NPC.velocity.X = (float)(12 * NPC.direction);
                            NPC.ai[2] += 1f;
                        }
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.98f;
                        if (NPC.direction < 0 && NPC.velocity.X > -8f)
                        {
                            NPC.velocity.X = -8f;
                        }
                        if (NPC.direction > 0 && NPC.velocity.X < 8f)
                        {
                            NPC.velocity.X = 8f;
                        }
                    }
                    if (NPC.ai[2] >= 3f && NPC.velocity.Y == 0f)
                    {
                        NPC.ai[0] = 2f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                        return;
                    }
                }
                else if (NPC.ai[0] == 7f)
                {
                    NPC.damage = 0;
                    NPC.life = NPC.lifeMax;
                    NPC.defense = 9999;
                    NPC.noTileCollide = true;
                    NPC.alpha += 7;
                    if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
                    if (NPC.alpha > 255)
                    {
                        NPC.alpha = 255;
                    }
                    NPC.velocity.X = NPC.velocity.X * 0.98f;
                    return;
                }
            }
            int num658 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SparksMech, NPC.velocity.X, NPC.velocity.Y, 255, new Color(0, 80, 255, 80), NPC.scale * 1.5f);
			Main.dust[num658].noGravity = true;
			Main.dust[num658].velocity *= 0.5f;
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	float num644 = 1f;
	       	if (NPC.life > 0)
			{
				float num659 = (float)NPC.life / (float)NPC.lifeMax;
				num659 = num659 * 0.5f + 0.75f;
				num659 *= num644;
				if (num659 != NPC.scale)
				{
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)NPC.height;
					NPC.scale = num659 * 0.75f;
					NPC.width = (int)(150f * NPC.scale);
					NPC.height = (int)(92f * NPC.scale);
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)NPC.height;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.05);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						int num661 = 1;
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("SlimeSpawnCrimson").Type;
							if (Main.rand.NextBool(3))
							{
								num663 = Mod.Find<ModNPC>("SlimeSpawnCrimson2").Type;
							}
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(num663);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
							Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
							Main.npc[num664].ai[1] = 0f;
							if (Main.netMode == NetmodeID.Server && num664 < 200)
							{
								NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						return;
					}
				}
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TintableDust, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.ManaSickness, 50, true);
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 50);
		}
	}
}