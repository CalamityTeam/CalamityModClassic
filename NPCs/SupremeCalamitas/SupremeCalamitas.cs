using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.SupremeCalamitas
{
	[AutoloadBossHead]
	public class SupremeCalamitas : ModNPC
	{
		public float bossLife;
		public bool halfLife = false;
		public bool secondStage = false;
		internal int dpsCap = CalamityWorld1Point2.downedSCal ? 80000 : 40000; //100
		private int damageTotal = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Supreme Calamitas");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 800;
			NPC.npcSlots = 15f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 350;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 2700000 : 2500000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.dontTakeDamage = false;
			NPC.chaseable = true;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/TerrariaBoss2");
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Calamity.")

            });
        }

        public override void AI()
		{
			CalamityGlobalNPC1Point2.supremeCalamitas = NPC.whoAmI;
			bool bossBuff = CalamityWorld1Point2.demonMode;
			bool superBossBuff = CalamityWorld1Point2.onionMode;
			bool expertMode = Main.expertMode;
			Player player = Main.player[NPC.target];
			damageTotal -= dpsCap;
			if (damageTotal < 0)
			{
				damageTotal = 0;
			}
			if (!halfLife && (NPC.life <= NPC.lifeMax * 0.4f))
			{
				string key = "He demands your blood.  It has to be this way...";
				Color messageColor = Color.Orange;
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(key, messageColor);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
				}
				halfLife = true;
			}
			if (NPC.life <= NPC.lifeMax * 0.25f)
			{
				if (secondStage == false && Main.netMode != NetmodeID.MultiplayerClient) 
				{
					SoundEngine.PlaySound(SoundID.Item74, NPC.position);
					for (int I = 0; I < 41; I++) 
					{
						int FireEye = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.Center.X + (Math.Sin(I * 18) * 1200)), (int)(NPC.Center.Y + (Math.Cos(I * 18) * 1200)), Mod.Find<ModNPC>("SoulSeekerSupreme").Type, NPC.whoAmI, 0, 0, 0, -1);
						NPC Eye = Main.npc[FireEye];
						Eye.ai[0] = I * 18;
						Eye.ai[3] = I * 18;
					}
					secondStage = true;
				}
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.55);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						int respawn = 1;
						for (int num662 = 0; num662 < respawn; num662++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RedSpawnSupreme").Type, 0, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GraySpawnSupreme").Type, 0, 0f, Main.myPlayer, 0f, 0f);
							string key = "We are eternal...";
							Color messageColor = Color.Orange;
							if (Main.netMode == NetmodeID.SinglePlayer)
							{
								Main.NewText((key), messageColor);
							}
							else if (Main.netMode == NetmodeID.Server)
							{
								ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
							}
						}
						return;
					}
				}
	       	}
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			float num801 = NPC.position.X + (float)(NPC.width / 2) - player.position.X - (float)(player.width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - player.position.Y - (float)(player.height / 2);
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
			if (Main.rand.NextBool(5))
			{
				int num805 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), DustID.LifeDrain, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_2F45E_cp_0 = Main.dust[num805];
				expr_2F45E_cp_0.velocity.X = expr_2F45E_cp_0.velocity.X * 0.5f;
				Dust expr_2F47E_cp_0 = Main.dust[num805];
				expr_2F47E_cp_0.velocity.Y = expr_2F47E_cp_0.velocity.Y * 0.1f;
			}
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, -10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft > 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f)
				{
					float num823 = 40f;
					float num824 = 0.4f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					float num826 = player.position.Y + (float)(player.height / 2) - 540f - vector82.Y;
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
					num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					num826 = player.position.Y + (float)(player.height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.localAI[1] += 2f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.95)
						{
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.85)
						{
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.7)
						{
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 0.5f;
						}
						if (NPC.localAI[1] > 150f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							NPC.localAI[1] = 0f;
							float num828 = 13f;
							int num829 = expertMode ? 100 : 110;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = num828 / num183;
							num180 *= num183;
							num182 *= num183;
							value9.X += num180;
							value9.Y += num182;
							int randomShot = Main.rand.Next(3);
							if (randomShot == 0)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
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
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 8f / num183;
									num180 += (float)Main.rand.Next(-180, 181);
									num182 += (float)Main.rand.Next(-180, 181);
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
					float num383 = 45f;
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
					float num384 = player.position.X + (float)(player.width / 2) - vector37.X;
					float num385 = player.position.Y + (float)(player.height / 2) - vector37.Y;
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
						if (NPC.ai[3] >= 3f) 
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
					NPC.TargetClosest(true);
					float num412 = 40f;
					float num413 = 1.5f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width) 
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = player.position.X + (float)(player.width / 2) + (float)(num414 * 600) - vector40.X;
					float num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
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
						if (!player.dead) 
						{
							NPC.ai[3] += 5f;
							if ((double)NPC.life < (double)NPC.lifeMax * 0.7) 
							{
								NPC.ai[3] += 2f;
							}
						}
						if (NPC.ai[3] >= 100f) 
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = player.position.X + (float)(player.width / 2) - vector40.X;
							num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
							if (Main.netMode != NetmodeID.MultiplayerClient) 
							{
								float num418 = 12f;
								int num419 = expertMode ? 100 : 110;
								int num420 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
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
				else if (NPC.ai[1] == 4f)
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num831 = -1;
					}
					float num832 = 40f;
					float num833 = 1.5f;
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 600) - vector83.X;
					float num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
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
					num834 = player.position.X + (float)(player.width / 2) - vector83.X;
					num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
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
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 0.5f;
						}
						if (NPC.localAI[1] > 180f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							NPC.localAI[1] = 0f;
							float num837 = 12f;
							int num838 = expertMode ? 100 : 110;
							int num839 = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
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
					int num871 = Main.rand.Next(4);
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
					else
					{
						num871 = 4;
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
							Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeDrain, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
						}
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
					}
				}
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeDrain, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
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
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f)
				{
					float num823 = 40f;
					float num824 = 0.4f;
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					float num826 = player.position.Y + (float)(player.height / 2) - 540f - vector82.Y;
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
					num825 = player.position.X + (float)(player.width / 2) - vector82.X;
					num826 = player.position.Y + (float)(player.height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.localAI[1] += 1f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.35)
						{
							NPC.localAI[1] += 2f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.3)
						{
							NPC.localAI[1] += 2f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.2)
						{
							NPC.localAI[1] += 2f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 3f;
						}
						if (NPC.localAI[1] > 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							NPC.localAI[1] = 0f;
							float num828 = 15f;
							int num829 = expertMode ? 150 : 160;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = num828 / num183;
							num180 *= num183;
							num182 *= num183;
							value9.X += num180;
							value9.Y += num182;
							int randomShot = Main.rand.Next(3);
							if (randomShot == 0)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
								num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
								num827 = num828 / num827;
								num825 *= num827;
								num826 *= num827;
								vector82.X += num825 * 15f;
								vector82.Y += num826 * 15f;
								int shot = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[shot].tileCollide = false;
							}
							else if (randomShot == 1)
							{
								randomShot = Mod.Find<ModProjectile>("BrimstoneBarrage").Type;
								for (int num186 = 0; num186 < 8; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 9f / num183;
									num180 += (float)Main.rand.Next(-180, 181);
									num182 += (float)Main.rand.Next(-180, 181);
									num180 *= num183;
									num182 *= num183;
									int shot = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[shot].tileCollide = false;
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
								int shot = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, randomShot, num829, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[shot].tileCollide = false;
							}
							return;
						}
					}
				}
				else if (NPC.ai[1] == 1f)
				{
					NPC.rotation = num803; //change
					float num383 = 55f;
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
					float num384 = player.position.X + (float)(player.width / 2) - vector37.X;
					float num385 = player.position.Y + (float)(player.height / 2) - vector37.Y;
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
						if (NPC.ai[3] >= 3f) 
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
					NPC.TargetClosest(true);
					float num412 = 40f;
					float num413 = 1.5f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width) 
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = player.position.X + (float)(player.width / 2) + (float)(num414 * 600) - vector40.X;
					float num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
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
						if (!player.dead) 
						{
							NPC.ai[3] += 3f;
							if ((double)NPC.life < (double)NPC.lifeMax * 0.3) 
							{
								NPC.ai[3] += 1f;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.2) 
							{
								NPC.ai[3] += 2f;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
							{
								NPC.ai[3] += 3f;
							}
						}
						if (NPC.ai[3] >= 60f) 
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = player.position.X + (float)(player.width / 2) - vector40.X;
							num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
							if (Main.netMode != NetmodeID.MultiplayerClient) 
							{
								float num418 = 12f;
								int num419 = expertMode ? 150 : 160;
								int num420 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
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
				else if (NPC.ai[1] == 4f)
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num831 = -1;
					}
					float num832 = 40f;
					float num833 = 1.5f;
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 600) - vector83.X;
					float num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
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
					num834 = player.position.X + (float)(player.width / 2) - vector83.X;
					num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
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
							NPC.localAI[1] += 2f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 4f;
						}
						if (NPC.localAI[1] > 240f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							NPC.localAI[1] = 0f;
							float num837 = 12f;
							int num838 = expertMode ? 150 : 160;
							int num839 = Mod.Find<ModProjectile>("BrimstoneFireblast").Type;
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							int shot = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[shot].tileCollide = false;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 180f)
					{
						NPC.ai[1] = -1f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
				if (NPC.ai[1] == -1f) 
				{
					int num871 = Main.rand.Next(4);
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
					else
					{
						num871 = 4;
					}
					NPC.ai[1] = (float)num871;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					return;
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			LeadingConditionRule expert = new LeadingConditionRule(new Conditions.IsExpert());
			expert.OnSuccess(new CommonDrop(ModContent.ItemType<CalamitousEssence>(), 1, 10, 20));
			expert.OnSuccess(ItemDropRule.OneFromOptions(1, new int[] {
				ModContent.ItemType<Animus>(),
                ModContent.ItemType<Azathoth>(),
                ModContent.ItemType<Items.Weapons.Contagion>(),
                ModContent.ItemType<DraconicDestruction>(),
                ModContent.ItemType<Items.Weapons.Earth>(),
                ModContent.ItemType<Megafleet>(),
                ModContent.ItemType<RedSun>(),
                ModContent.ItemType<RoyalKnives>(),
                ModContent.ItemType<RoyalKnivesMelee>(),
                ModContent.ItemType<Svantechnical>(),
                ModContent.ItemType<TriactisTruePaladinianMageHammerofMight>(),
                ModContent.ItemType<TriactisTruePaladinianMageHammerofMightMelee>()
            }));
			npcLoot.Add(expert);
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = Mod.Find<ModItem>("SupremeHealingPotion").Type;
		}
		
		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Base);
		}

		public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Base);
		}
		
		public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		private void ModifyHit(ref float damage)
		{
			if (damage > NPC.lifeMax / 8)
			{
				damage = NPC.lifeMax / 8;
			}
		}
		
		private void OnHit(int damage)
		{
			damageTotal += damage * 60;
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				ModPacket netMessage = GetPacket(SupremeCalamitasMessageType.Damage);
				netMessage.Write(damage * 60);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					netMessage.Write(Main.myPlayer);
				}
				netMessage.Send();
			}
		}
		
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (damageTotal >= dpsCap * 60)
			{
				modifiers.FinalDamage *= 0;
			}
			bool flag = Main.netMode == NetmodeID.SinglePlayer;
			if (!NPC.active || NPC.life <= 0)
			{
				return;
			}
			double newDamage = modifiers.FinalDamage.Base;
			int newDefense = NPC.defense;
			if (NPC.ichor)
			{
				newDefense -= 20;
			}
			if (newDefense < 0)
			{
				newDefense = 0;
			}
			newDamage = newDamage - (double)newDefense * 0.25;
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			/*if (crit)
			{
				newDamage *= 2;
			}*/
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - (NPC.ichor ? 0.25f : 0.33f)) * newDamage));
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
				if (flag)
				{
					NPC.PlayerInteraction(Main.myPlayer);
				}
				NPC.justHit = true;
				if (!NPC.immortal)
				{
					if (NPC.realLife >= 0)
					{
						Main.npc[NPC.realLife].life -= (int)newDamage;
						NPC.life = Main.npc[NPC.realLife].life;
						NPC.lifeMax = Main.npc[NPC.realLife].lifeMax;
					}
					else
					{
						NPC.life -= (int)newDamage;
					}
				}
				NPC.HitEffect(modifiers.HitDirection, newDamage);
				if (NPC.HitSound != null)
				{
					SoundEngine.PlaySound(NPC.HitSound, NPC.position);
				}
				if (NPC.realLife >= 0)
				{
					Main.npc[NPC.realLife].checkDead();
				}
				else
				{
					NPC.checkDead();
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
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
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeDrain, hit.HitDirection, -1f, 0, default(Color), 1f);
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
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
			target.AddBuff(Mod.Find<ModBuff>("VulnerabilityHex").Type, 300, true);
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
		}
		
		private ModPacket GetPacket(SupremeCalamitasMessageType type)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)CalamityModClassic1Point2MessageType.SupremeCalamitas);
			packet.Write(NPC.whoAmI);
			packet.Write((byte)type);
			return packet;
		}
		
		public void HandlePacket(BinaryReader reader)
		{
			SupremeCalamitasMessageType type = (SupremeCalamitasMessageType)reader.ReadByte();
			if (type == SupremeCalamitasMessageType.Damage)
			{
				int damage = reader.ReadInt32();
				damageTotal += damage;
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket netMessage = GetPacket(SupremeCalamitasMessageType.Damage);
					int ignore = reader.ReadInt32();
					netMessage.Write(damage);
					netMessage.Send(-1, ignore);
				}
			}
		}
	}
	
	enum SupremeCalamitasMessageType : byte
	{
		Damage
	}
}