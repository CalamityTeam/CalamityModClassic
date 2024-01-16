using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.SupremeCalamitas
{
	[AutoloadBossHead]
	public class SupremeCalamitas : ModNPC
	{
		public float bossLife;
		public int halfLife = 0;
		
		public override void SetDefaults()
		{
			//NPC.name = "Supreme Calamitas");
			//Tooltip.SetDefault("Calamitas");
			NPC.damage = 480;
			NPC.npcSlots = 5f;
			NPC.width = 100; //324
			NPC.height = 110; //216
			NPC.defense = 150;
			NPC.lifeMax = 2354000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			Main.npcFrameCount[NPC.type] = 6;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.dontTakeDamage = false;
			NPC.chaseable = true;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.timeLeft = NPC.activeTime * 30;
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TerrariaBoss2");
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("The Calamity herself. What a Calamity.")

            });
        }

        public override void AI()
		{
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if ((Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("SupremeCatastrophe").Type)) || (Main.npc[num569].active && Main.npc[num569].type == Mod.Find<ModNPC>("SupremeCataclysm").Type))
				{
					flag100 = true;
				}
			}
			if (flag100)
			{
				NPC.defense = 300;
			}
			else if (!flag100)
			{
				NPC.defense = 150;
			}
			if (halfLife == 0 && (NPC.life <= NPC.lifeMax * 0.4f))
			{
				Main.NewText("He demands your blood.  It has to be this way...", Color.Orange.R, Color.Orange.G, Color.Orange.B);
				halfLife++;
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.7);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						int respawn = 1;
						for (int num662 = 0; num662 < respawn; num662++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RedSpawnSupreme").Type, 0, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GraySpawnSupreme").Type, 0, 0f, Main.myPlayer, 0f, 0f);
							Main.NewText("We are eternal...", Color.Orange.R, Color.Orange.G, Color.Orange.B);
						}
						return;
					}
				}
	       	}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			bool dead2 = Main.player[NPC.target].dead;
			float num801 = NPC.position.X + (float)(NPC.width / 2) - Main.player[NPC.target].position.X - (float)(Main.player[NPC.target].width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - Main.player[NPC.target].position.Y - (float)(Main.player[NPC.target].height / 2);
			float num803 = (float)Math.Atan2((double)num802, (double)num801) + 1.57f;
			if (num803 < 0f)
			{
				num803 += 6.283f;
			}
			else if ((double)num803 > 6.283)
			{
				num803 -= 6.283f;
			}
			float num804 = 0.1f;
			if (NPC.rotation < num803)
			{
				if ((double)(num803 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num804;
				}
				else
				{
					NPC.rotation += num804;
				}
			}
			else if (NPC.rotation > num803)
			{
				if ((double)(NPC.rotation - num803) > 3.1415)
				{
					NPC.rotation += num804;
				}
				else
				{
					NPC.rotation -= num804;
				}
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num805 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), 235, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_2F45E_cp_0 = Main.dust[num805];
				expr_2F45E_cp_0.velocity.X = expr_2F45E_cp_0.velocity.X * 0.5f;
				Dust expr_2F47E_cp_0 = Main.dust[num805];
				expr_2F47E_cp_0.velocity.Y = expr_2F47E_cp_0.velocity.Y * 0.1f;
			}
			if (Main.netMode != 1 && !dead2 && NPC.timeLeft < 10)
			{
				for (int num806 = 0; num806 < 200; num806++)
				{
					if (num806 != NPC.whoAmI && Main.npc[num806].active && Main.npc[num806].timeLeft - 1 > NPC.timeLeft)
					{
						NPC.timeLeft = Main.npc[num806].timeLeft - 1;
					}
				}
			}
			if (dead2)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 2f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 2f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
			}
			else if (NPC.ai[0] == 0f)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f)
				{
					float num823 = 17f;
					float num824 = 0.3f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					float num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 300f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 240f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
					}
					vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 2f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.95)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.85)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.7)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 2f;
						}
						if (NPC.localAI[1] > 260f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num828 = 9f;
							int num829 = 113;
							if (Main.expertMode)
							{
								num829 = 66;
							}
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							//npc.netUpdate = true;
							num183 = num828 / num183;
							num180 *= num183;
							num182 *= num183;
							value9.X += num180;
							value9.Y += num182;
							int randomShot = Main.rand.Next(3);
							if (randomShot == 0)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneShot").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							else if (randomShot == 1)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneBarrage").Type;
								for (int num186 = 0; num186 < 8; num186++)
								{
									num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
									num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 12f / num183;
									num180 += (float)Main.rand.Next(-60, 61);
									num182 += (float)Main.rand.Next(-60, 61);
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
								}
							}
							else
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							return;
						}
					}
				}
				else if (NPC.ai[1] == 1f) //charge
				{
					NPC.rotation = num803; //change
					float num383 = 34f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.95) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.85) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.7) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.6) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num383 += 1f;
					}
					Vector2 vector37 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num384 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector37.X;
					float num385 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector37.Y;
					float num386 = (float)Math.Sqrt((double)(num384 * num384 + num385 * num385));
					num386 = num383 / num386;
					NPC.velocity.X = num384 * num386;
					NPC.velocity.Y = num385 * num386;
					NPC.ai[1] = 2f;
				} 
				else if (NPC.ai[1] == 2f)
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 25f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.96f;
						NPC.velocity.Y = NPC.velocity.Y * 0.96f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1) 
						{
							NPC.velocity.Y = 0f;
						}
					} 
					else 
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 70f) 
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num803; //change
						if (NPC.ai[3] >= 4f) 
						{
							NPC.ai[1] = -1f;
						} 
						else
						{
							NPC.ai[1] = 1f;
						}
					}
				}
				else if (NPC.ai[1] == 3f) //charge
				{
					NPC.rotation = num803; //change
					float num421 = 30f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.95) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.85) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.7) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.6) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num421 += 1f;
					}
					Vector2 vector41 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num422 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector41.X;
					float num423 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector41.Y;
					float num424 = (float)Math.Sqrt((double)(num422 * num422 + num423 * num423));
					num424 = num421 / num424;
					NPC.velocity.X = num422 * num424;
					NPC.velocity.Y = num423 * num424;
					NPC.ai[1] = 4f;
				}
				else if (NPC.ai[1] == 4f) 
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 12f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
						NPC.velocity.Y = NPC.velocity.Y * 0.9f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1) 
						{
							NPC.velocity.Y = 0f;
						}
					} 
					else 
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 42f) 
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num803; //change
						if (NPC.ai[3] >= 10f) 
						{
							NPC.ai[1] = -1f;
						} 
						else
						{
							NPC.ai[1] = 3f;
						}
					}
				}
				else if (NPC.ai[1] == 5f) 
				{
					NPC.TargetClosest(true);
					float num412 = 30f;
					float num413 = 1.2f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width) 
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num414 * 400) - vector40.X;
					float num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector40.Y;
					float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
					num417 = num412 / num417;
					num415 *= num417;
					num416 *= num417;
					if (NPC.velocity.X < num415) 
					{
						NPC.velocity.X = NPC.velocity.X + num413;
						if (NPC.velocity.X < 0f && num415 > 0f) 
						{
							NPC.velocity.X = NPC.velocity.X + num413;
						}
					} 
					else if (NPC.velocity.X > num415) 
					{
						NPC.velocity.X = NPC.velocity.X - num413;
						if (NPC.velocity.X > 0f && num415 < 0f) 
						{
							NPC.velocity.X = NPC.velocity.X - num413;
						}
					}
					if (NPC.velocity.Y < num416) 
					{
						NPC.velocity.Y = NPC.velocity.Y + num413;
						if (NPC.velocity.Y < 0f && num416 > 0f) 
						{
							NPC.velocity.Y = NPC.velocity.Y + num413;
						}
					} 
					else if (NPC.velocity.Y > num416) 
					{
						NPC.velocity.Y = NPC.velocity.Y - num413;
						if (NPC.velocity.Y > 0f && num416 < 0f) 
						{
							NPC.velocity.Y = NPC.velocity.Y - num413;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 440f) 
					{
						NPC.ai[1] = -1f;
						NPC.target = 255;
						NPC.netUpdate = true;
					} 
					else
					{
						if (!Main.player[NPC.target].dead) 
						{
							NPC.ai[3] += 3f;
							if ((double)NPC.life < (double)NPC.lifeMax * 0.7) 
							{
								NPC.ai[3] += 2f;
							}
						}
						if (NPC.ai[3] >= 100f) 
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector40.X;
							num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector40.Y;
							if (Main.netMode != 1) 
							{
								float num418 = 10f;
								int num419 = 180;
								int num420 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
								if (Main.expertMode) 
								{
									num419 = 100;
								}
								num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
								num417 = num418 / num417;
								num415 *= num417;
								num416 *= num417;
								num415 += (float)Main.rand.Next(-20, 21) * 0.05f;
								num416 += (float)Main.rand.Next(-20, 21) * 0.05f;
								vector40.X += num415 * 4f;
								vector40.Y += num416 * 4f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				else if (NPC.ai[1] == 6f)
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
					{
						num831 = -1;
					}
					float num832 = 17f;
					float num833 = 0.38f;
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num831 * 340) - vector83.X;
					float num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
					num836 = num832 / num836;
					num834 *= num836;
					num835 *= num836;
					if (NPC.velocity.X < num834)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
						if (NPC.velocity.X < 0f && num834 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num833;
						}
					}
					else if (NPC.velocity.X > num834)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
						if (NPC.velocity.X > 0f && num834 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num833;
						}
					}
					if (NPC.velocity.Y < num835)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
						if (NPC.velocity.Y < 0f && num835 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num833;
						}
					}
					else if (NPC.velocity.Y > num835)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
						if (NPC.velocity.Y > 0f && num835 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num833;
						}
					}
					vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector83.X;
					num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 3.5f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.95)
						{
							NPC.localAI[1] += 0.75f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.85)
						{
							NPC.localAI[1] += 0.75f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.7)
						{
							NPC.localAI[1] += 1.25f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 2.5f;
						}
						if (NPC.localAI[1] > 240f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num837 = 10f;
							int num838 = 120;
							int num839 = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
							if (Main.expertMode)
							{
								num838 = 70;
							}
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 120f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
				if (NPC.ai[1] == -1f) 
				{
					int num871 = Main.rand.Next(5);
					if (num871 == 0)
					{
						num871 = 0;
					}
					else if (num871 == 1)
					{
						num871 = 1;
					}
					else if (num871 == 2)
					{
						num871 = 3;
					}
					else if (num871 == 3)
					{
						num871 = 5;
					}
					else
					{
						num871 = 6;
					}
					NPC.ai[1] = (float)num871;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					return;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.4) 
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 1f || NPC.ai[0] == 2f)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.ai[0] == 1f) 
				{
					NPC.ai[2] += 0.005f;
					if ((double)NPC.ai[2] > 0.5) 
					{
						NPC.ai[2] = 0.5f;
					}
				} 
				else 
				{
					NPC.ai[2] -= 0.005f;
					if (NPC.ai[2] < 0f) 
					{
						NPC.ai[2] = 0f;
					}
				}
				NPC.rotation += NPC.ai[2];
				NPC.ai[1] += 1f;
				if (NPC.ai[1] == 100f) 
				{
					NPC.ai[0] += 1f;
					NPC.ai[1] = 0f;
					if (NPC.ai[0] == 3f) 
					{
						NPC.ai[2] = 0f;
					} 
					else 
					{
						SoundEngine.PlaySound(SoundID.NPCHit1, NPC.position);
						for (int num388 = 0; num388 < 50; num388++) 
						{
							Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
						}
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
					}
				}
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
				NPC.velocity.X = NPC.velocity.X * 0.98f;
				NPC.velocity.Y = NPC.velocity.Y * 0.98f;
				if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
				{
					NPC.velocity.X = 0f;
				}
				if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1) 
				{
					NPC.velocity.Y = 0f;
					return;
				}
			}
			else
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = false;
				if (NPC.ai[1] == 0f)
				{
					float num823 = 18f;
					float num824 = 0.32f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					float num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 300f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 180f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
					}
					vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 2f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.35)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.3)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.2)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 2f;
						}
						if (NPC.localAI[1] > 200f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num828 = 11f;
							int num829 = 133;
							if (Main.expertMode)
							{
								num829 = 79;
							}
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							//npc.netUpdate = true;
							num183 = num828 / num183;
							num180 *= num183;
							num182 *= num183;
							value9.X += num180;
							value9.Y += num182;
							int randomShot = Main.rand.Next(3);
							if (randomShot == 0)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneShot").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							else if (randomShot == 1)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneBarrage").Type;
								for (int num186 = 0; num186 < 8; num186++)
								{
									num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
									num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 12f / num183;
									num180 += (float)Main.rand.Next(-60, 61);
									num182 += (float)Main.rand.Next(-60, 61);
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
								}
							}
							else
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneGigaBlast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
							}
							return;
						}
					}
				}
				else if (NPC.ai[1] == 1f)
				{
					NPC.rotation = num803; //change
					float num383 = 38f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.35) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.2) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.15) 
					{
						num383 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num383 += 1f;
					}
					Vector2 vector37 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num384 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector37.X;
					float num385 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector37.Y;
					float num386 = (float)Math.Sqrt((double)(num384 * num384 + num385 * num385));
					num386 = num383 / num386;
					NPC.velocity.X = num384 * num386;
					NPC.velocity.Y = num385 * num386;
					NPC.ai[1] = 2f;
				} 
				else if (NPC.ai[1] == 2f) 
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 25f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.96f;
						NPC.velocity.Y = NPC.velocity.Y * 0.96f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1) 
						{
							NPC.velocity.Y = 0f;
						}
					} 
					else 
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 70f) 
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num803; //change
						if (NPC.ai[3] >= 2f) 
						{
							NPC.ai[1] = -1f;
						} 
						else
						{
							NPC.ai[1] = 1f;
						}
					}
				}
				else if (NPC.ai[1] == 3f) 
				{
					NPC.rotation = num803; //change
					float num421 = 33f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.35) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.2) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.15) 
					{
						num421 += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num421 += 1f;
					}
					Vector2 vector41 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num422 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector41.X;
					float num423 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector41.Y;
					float num424 = (float)Math.Sqrt((double)(num422 * num422 + num423 * num423));
					num424 = num421 / num424;
					NPC.velocity.X = num422 * num424;
					NPC.velocity.Y = num423 * num424;
					NPC.ai[1] = 4f;
				} 
				else if (NPC.ai[1] == 4f) 
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 12f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
						NPC.velocity.Y = NPC.velocity.Y * 0.9f;
						if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
						{
							NPC.velocity.X = 0f;
						}
						if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1) 
						{
							NPC.velocity.Y = 0f;
						}
					} 
					else 
					{
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
					}
					if (NPC.ai[2] >= 42f) 
					{
						NPC.ai[3] += 1f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.rotation = num803; //change
						if (NPC.ai[3] >= 6f) 
						{
							NPC.ai[1] = -1f;
						} 
						else
						{
							NPC.ai[1] = 3f;
						}
					}
				}
				else if (NPC.ai[1] == 5f) 
				{
					NPC.TargetClosest(true);
					float num412 = 30f;
					float num413 = 1.2f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width) 
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num414 * 400) - vector40.X;
					float num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector40.Y;
					float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
					num417 = num412 / num417;
					num415 *= num417;
					num416 *= num417;
					if (NPC.velocity.X < num415) 
					{
						NPC.velocity.X = NPC.velocity.X + num413;
						if (NPC.velocity.X < 0f && num415 > 0f) 
						{
							NPC.velocity.X = NPC.velocity.X + num413;
						}
					}
					else if (NPC.velocity.X > num415) 
					{
						NPC.velocity.X = NPC.velocity.X - num413;
						if (NPC.velocity.X > 0f && num415 < 0f) 
						{
							NPC.velocity.X = NPC.velocity.X - num413;
						}
					}
					if (NPC.velocity.Y < num416) 
					{
						NPC.velocity.Y = NPC.velocity.Y + num413;
						if (NPC.velocity.Y < 0f && num416 > 0f) 
						{
							NPC.velocity.Y = NPC.velocity.Y + num413;
						}
					}
					else if (NPC.velocity.Y > num416) 
					{
						NPC.velocity.Y = NPC.velocity.Y - num413;
						if (NPC.velocity.Y > 0f && num416 < 0f) 
						{
							NPC.velocity.Y = NPC.velocity.Y - num413;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 360f) 
					{
						NPC.ai[1] = -1f;
						NPC.target = 255;
						NPC.netUpdate = true;
					}
					else
					{
						if (!Main.player[NPC.target].dead) 
						{
							NPC.ai[3] += 3f;
							if ((double)NPC.life < (double)NPC.lifeMax * 0.2) 
							{
								NPC.ai[3] += 2f;
							}
						}
						if (NPC.ai[3] >= 80f) 
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector40.X;
							num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector40.Y;
							if (Main.netMode != 1) 
							{
								float num418 = 12f;
								int num419 = 195;
								int num420 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
								if (Main.expertMode) 
								{
									num419 = 120;
								}
								num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
								num417 = num418 / num417;
								num415 *= num417;
								num416 *= num417;
								num415 += (float)Main.rand.Next(-20, 21) * 0.05f;
								num416 += (float)Main.rand.Next(-20, 21) * 0.05f;
								vector40.X += num415 * 4f;
								vector40.Y += num416 * 4f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				else if (NPC.ai[1] == 6f)
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
					{
						num831 = -1;
					}
					float num832 = 18f;
					float num833 = 0.39f;
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num831 * 340) - vector83.X;
					float num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
					num836 = num832 / num836;
					num834 *= num836;
					num835 *= num836;
					if (NPC.velocity.X < num834)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
						if (NPC.velocity.X < 0f && num834 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num833;
						}
					}
					else if (NPC.velocity.X > num834)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
						if (NPC.velocity.X > 0f && num834 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num833;
						}
					}
					if (NPC.velocity.Y < num835)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
						if (NPC.velocity.Y < 0f && num835 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num833;
						}
					}
					else if (NPC.velocity.Y > num835)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
						if (NPC.velocity.Y > 0f && num835 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num833;
						}
					}
					vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector83.X;
					num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 3.5f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.35)
						{
							NPC.localAI[1] += 0.75f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.3)
						{
							NPC.localAI[1] += 0.75f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.2)
						{
							NPC.localAI[1] += 1.25f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 2.5f;
						}
						if (NPC.localAI[1] > 210f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num837 = 11f;
							int num838 = 150;
							int num839 = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
							if (Main.expertMode)
							{
								num838 = 90;
							}
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 80f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
				if (NPC.ai[1] == -1f) 
				{
					int num871 = Main.rand.Next(5);
					if (num871 == 0)
					{
						num871 = 0;
					}
					else if (num871 == 1)
					{
						num871 = 1;
					}
					else if (num871 == 2)
					{
						num871 = 3;
					}
					else if (num871 == 3)
					{
						num871 = 5;
					}
					else
					{
						num871 = 6;
					}
					NPC.ai[1] = (float)num871;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					return;
				}
			}
		}
		
		public override void FindFrame(int frameHeight) //9 total frames
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter < 7.0)
			{
				NPC.frame.Y = 0;
			}
			else if (NPC.frameCounter < 14.0)
			{
				NPC.frame.Y = frameHeight;
			}
			else if (NPC.frameCounter < 21.0)
			{
				NPC.frame.Y = frameHeight * 2;
			}
			else
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = 0;
			}
			if (NPC.ai[0] > 1f)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight * 3;
				return;
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = NPC.TypeName;
			potionType = ItemID.SuperHealingPotion;
		}


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<ShadowEssence>(), 1, 5, 8));
        }
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
	}
}