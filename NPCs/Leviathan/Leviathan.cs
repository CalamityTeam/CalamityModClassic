using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items.Leviathan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	[AutoloadBossHead]
	public class Leviathan : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Leviathan");
			Main.npcFrameCount[NPC.type] = 3;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("Theorized to be the very last of her kind, she only awakens if her slumber is disturbed, or if anyone falls for the Siren's illusions... to devour whoever strayed too far from shore.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 20f;
			NPC.damage = 90;
			NPC.width = 850;
			NPC.height = 450;
			NPC.defense = 40;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 90700 : 69000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 189750;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 8000000 : 7000000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 15, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath60;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
			NPC.boss = true;
			NPC.netAlways = true;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/LeviathanAndSiren");
		}

		public override void AI()
		{
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			Vector2 vector = NPC.Center;
			Player player = Main.player[NPC.target];
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			int npcType = Mod.Find<ModNPC>("Siren").Type;
			bool sirenAlive = false;
			if (NPC.CountNPCS(npcType) > 0)
			{
				sirenAlive = true;
			}
			SoundStyle soundChoiceRage = SoundID.Zombie92;
			SoundStyle soundChoice = Utils.SelectRandom(Main.rand, new SoundStyle[]
			{
				SoundID.Zombie38,
				SoundID.Zombie39,
				SoundID.Zombie40
			});
			if (Main.rand.Next(600) == 0)
			{
				SoundEngine.PlaySound(((sirenAlive && !CalamityWorldPreTrailer.death && !CalamityWorldPreTrailer.bossRushActive) ? soundChoice : soundChoiceRage), NPC.position);
			}
			bool flag6 = player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400));
			if (flag6 || !sirenAlive || CalamityWorldPreTrailer.death)
			{
				NPC.defense = 80;
			}
			else
			{
				NPC.defense = 40;
			}
			NPC.dontTakeDamage = flag6 && !CalamityWorldPreTrailer.bossRushActive;
			int num1038 = 0;
			for (int num1039 = 0; num1039 < 255; num1039++)
			{
				if (Main.player[num1039].active && !Main.player[num1039].dead && (NPC.Center - Main.player[num1039].Center).Length() < 1000f)
				{
					num1038++;
				}
			}
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			if (NPC.timeLeft < 3000)
			{
				NPC.timeLeft = 3000;
			}
			if (!player.active || player.dead || Vector2.Distance(player.Center, vector) > 5600f)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead || Vector2.Distance(player.Center, vector) > 5600f)
				{
					NPC.velocity = new Vector2(0f, 10f);
					if ((double)NPC.position.Y > Main.rockLayer * 16.0)
					{
						for (int x = 0; x < 200; x++)
						{
							if (Main.npc[x].type == Mod.Find<ModNPC>("Siren").Type)
							{
								Main.npc[x].active = false;
								Main.npc[x].netUpdate = true;
							}
						}
						NPC.active = false;
						NPC.netUpdate = true;
					}
					return;
				}
			}
			else
			{
				if (NPC.ai[0] == 0f)
				{
					NPC.TargetClosest(true);
					float num412 = sirenAlive ? 3.5f : 7f;
					float num413 = sirenAlive ? 0.1f : 0.2f;
					if (CalamityWorldPreTrailer.bossRushActive)
					{
						num412 = 12f;
						num413 = 0.4f;
					}
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = player.position.X + (float)(player.width / 2) + (float)(num414 * 800) - vector40.X;
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
					NPC.ai[1] += 1f;
					if (NPC.ai[1] >= ((CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) ? 120f : 240f))
					{
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.target = 255;
						NPC.netUpdate = true;
					}
					else
					{
						if (!player.dead)
						{
							NPC.ai[2] += 1f;
							if (!sirenAlive)
							{
								NPC.ai[2] += 2f;
							}
							else
							{
								if (sirenAlive)
								{
									if (Siren.phase2)
									{
										NPC.ai[2] += 0.5f;
									}
									if (Siren.phase3)
									{
										NPC.ai[2] += 0.5f;
									}
								}
							}
						}
						if (NPC.ai[2] >= 90f)
						{
							NPC.ai[2] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = player.position.X + (float)(player.width / 2) - vector40.X;
							num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
							if (Main.netMode != 1)
							{
								float num418 = sirenAlive ? 13.5f : 16f;
								int num419 = 48;
								int num420 = Mod.Find<ModProjectile>("LeviathanBomb").Type;
								if (expertMode)
								{
									num418 = sirenAlive ? 14f : 17f;
									num419 = 33;
								}
								if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
								{
									num418 = 22f;
								}
								num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
								num417 = num418 / num417;
								num415 *= num417;
								num416 *= num417;
								num415 += (float)Main.rand.Next(-5, 6) * 0.05f;
								num416 += (float)Main.rand.Next(-5, 6) * 0.05f;
								vector40.X += num415 * 4f;
								vector40.Y += num416 * 4f;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector40.X, vector40.Y, num415, num416, num420, num419, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				else if (NPC.ai[0] == 1f)
				{
					NPC.TargetClosest(true);
					Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
					Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
					float num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
					float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
					NPC.ai[1] += 1f;
					NPC.ai[1] += (float)(num1038 / 2);
					if (revenge)
					{
						NPC.ai[1] += 1f;
					}
					if (!sirenAlive || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.ai[1] += 2f;
					}
					else
					{
						if ((Siren.phase2 && sirenAlive) || CalamityWorldPreTrailer.bossRushActive)
						{
							NPC.ai[1] += 0.5f;
						}
						if ((Siren.phase3 && sirenAlive) || CalamityWorldPreTrailer.bossRushActive)
						{
							NPC.ai[1] += 0.5f;
						}
					}
					bool flag103 = false;
					int spawnLimit = sirenAlive ? 2 : 4;
					int spawnLimit2 = sirenAlive ? 5 : 10;
					if (NPC.ai[1] > 80f) //changed from 40 not a prob
					{
						NPC.ai[1] = 0f;
						NPC.ai[2] += 1f;
						flag103 = true;
					}
					if (Collision.CanHit(vector119, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && flag103)
					{
						SoundEngine.PlaySound(soundChoice, NPC.position);
						if (Main.netMode != 1 && NPC.CountNPCS(Mod.Find<ModNPC>("Parasea").Type) < spawnLimit2 && NPC.CountNPCS(Mod.Find<ModNPC>("AquaticAberration").Type) < spawnLimit)
						{
							int num1061;
							int value = CalamityWorldPreTrailer.death ? 2 : 3;
							if (CalamityWorldPreTrailer.bossRushActive)
								value++;
							if (Main.rand.Next(value) == 0)
							{
								num1061 = Mod.Find<ModNPC>("AquaticAberration").Type;
							}
							else
							{
								num1061 = Mod.Find<ModNPC>("Parasea").Type;
							}
							int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.01f;
							Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.01f;
							Main.npc[num1062].netUpdate = true;
						}
					}
					if (num1060 > 400f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num1063 = sirenAlive ? 7f : 8f; //changed from 14 not a prob
						float num1064 = sirenAlive ? 0.05f : 0.065f; //changed from 0.1 not a prob
						if (CalamityWorldPreTrailer.bossRushActive)
						{
							num1063 = 10f;
							num1064 = 0.075f;
						}
						vector120 = vector119;
						num1058 = player.position.X + (float)(player.width / 2) - vector120.X;
						num1059 = player.position.Y + (float)(player.height / 2) - vector120.Y;
						num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
						num1060 = num1063 / num1060;
						if (NPC.velocity.X < num1058)
						{
							NPC.velocity.X = NPC.velocity.X + num1064;
							if (NPC.velocity.X < 0f && num1058 > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num1064;
							}
						}
						else if (NPC.velocity.X > num1058)
						{
							NPC.velocity.X = NPC.velocity.X - num1064;
							if (NPC.velocity.X > 0f && num1058 < 0f)
							{
								NPC.velocity.X = NPC.velocity.X - num1064;
							}
						}
						if (NPC.velocity.Y < num1059)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1064;
							if (NPC.velocity.Y < 0f && num1059 > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num1064;
							}
						}
						else if (NPC.velocity.Y > num1059)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1064;
							if (NPC.velocity.Y > 0f && num1059 < 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y - num1064;
							}
						}
					}
					else
					{
						NPC.velocity *= 0.9f;
					}
					NPC.spriteDirection = NPC.direction;
					if (NPC.ai[2] > 3f)
					{
						NPC.ai[0] = ((double)NPC.life < (double)NPC.lifeMax * 0.5 ? 2f : 0f);
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.netUpdate = true;
						return;
					}
				}
				else if (NPC.ai[0] == 2f)
				{
					Vector2 distFromPlayer = Main.player[NPC.target].Center - NPC.Center;
					if (NPC.ai[1] > 1f || distFromPlayer.Length() > 2400f)
					{
						NPC.ai[0] = 0f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.netUpdate = true;
						return;
					}
					if (NPC.ai[1] % 2f == 0f)
					{
						int num24 = 7;
						for (int j = 0; j < num24; j++)
						{
							Vector2 arg_E1C_0 = (Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f).RotatedBy((double)(j - (num24 / 2 - 1)) * 3.1415926535897931 / (double)((float)num24), default(Vector2)) + vector;
							Vector2 vector4 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
							int num25 = Dust.NewDust(arg_E1C_0 + vector4, 0, 0, 172, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].noLight = true;
							Main.dust[num25].velocity /= 4f;
							Main.dust[num25].velocity -= NPC.velocity;
						}
						NPC.TargetClosest(true);
						if (Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2))) < 20f)
						{
							NPC.localAI[1] = 1f;
							NPC.ai[1] += 1f;
							NPC.ai[2] = 0f;
							float num1044 = revenge ? 20f : 18f; //16
							if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
							{
								num1044 += 2f; //2 not a prob
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
							{
								num1044 += 2f; //2 not a prob
							}
							if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num1044 += 4f;
							}
							Vector2 vector117 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num1045 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector117.X;
							float num1046 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector117.Y;
							float num1047 = (float)Math.Sqrt((double)(num1045 * num1045 + num1046 * num1046));
							num1047 = num1044 / num1047;
							NPC.velocity.X = num1045 * num1047;
							NPC.velocity.Y = num1046 * num1047;
							NPC.spriteDirection = NPC.direction;
							SoundEngine.PlaySound(soundChoiceRage, NPC.position);
							return;
						}
						NPC.localAI[1] = 0f;
						float num1048 = revenge ? 7.5f : 6.5f; //12 not a prob
						float num1049 = revenge ? 0.12f : 0.11f; //0.15 not a prob
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							num1048 += 2f; //2 not a prob
							num1049 += 0.05f; //0.05 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
						{
							num1048 += 2f; //2 not a prob
							num1049 += 0.1f; //0.1 not a prob
						}
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							num1048 += 3f;
							num1049 += 0.2f;
						}
						if (NPC.position.Y + (float)(NPC.height / 2) < (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2)))
						{
							NPC.velocity.Y = NPC.velocity.Y + num1049;
						}
						else
						{
							NPC.velocity.Y = NPC.velocity.Y - num1049;
						}
						if (NPC.velocity.Y < -12f)
						{
							NPC.velocity.Y = -num1048;
						}
						if (NPC.velocity.Y > 12f)
						{
							NPC.velocity.Y = num1048;
						}
						if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) > 600f)
						{
							NPC.velocity.X = NPC.velocity.X + 0.15f * (float)NPC.direction;
						}
						else if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) < 300f)
						{
							NPC.velocity.X = NPC.velocity.X - 0.15f * (float)NPC.direction;
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X * 0.8f;
						}
						if (NPC.velocity.X < -16f)
						{
							NPC.velocity.X = -16f;
						}
						if (NPC.velocity.X > 16f)
						{
							NPC.velocity.X = 16f;
						}
						NPC.spriteDirection = NPC.direction;
						return;
					}
					else
					{
						if (NPC.velocity.X < 0f)
						{
							NPC.direction = -1;
						}
						else
						{
							NPC.direction = 1;
						}
						NPC.spriteDirection = NPC.direction;
						int num1050 = sirenAlive ? 1100 : 900; //600 not a prob
						int num1051 = 1;
						if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))
						{
							num1051 = -1;
						}
						if (NPC.direction == num1051 && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) > (float)num1050)
						{
							NPC.ai[2] = 1f;
						}
						if (NPC.ai[2] != 1f)
						{
							NPC.localAI[1] = 1f;
							return;
						}
						NPC.TargetClosest(true);
						NPC.spriteDirection = NPC.direction;
						NPC.localAI[1] = 0f;
						NPC.velocity *= 0.9f;
						float num1052 = revenge ? 0.11f : 0.1f; //0.1
						if (NPC.life < NPC.lifeMax / 3 || CalamityWorldPreTrailer.bossRushActive)
						{
							NPC.velocity *= 0.9f;
							num1052 += 0.05f; //0.05
						}
						if (NPC.life < NPC.lifeMax / 5 || CalamityWorldPreTrailer.bossRushActive)
						{
							NPC.velocity *= 0.9f;
							num1052 += 0.05f; //0.05
						}
						if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num1052)
						{
							NPC.ai[2] = 0f;
							NPC.ai[1] += 1f;
							return;
						}
					}
				}
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f,
						0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}

				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f,
						0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f,
						100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}

				if (Main.netMode != NetmodeID.Server)
				{
					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Leviathangib1").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Leviathangib2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Leviathangib3").Type, 1f);
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			LeadingConditionRule noSiren = new LeadingConditionRule(new NoSiren());
			npcLoot.Add(noSiren.OnSuccess(new CommonDrop(Mod.Find<ModItem>("LeviathanTrophy").Type, 10)));
			npcLoot.Add(noSiren.OnSuccess(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<LeviathanBag>(),
					1,
					5, 5)));
			noSiren.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<LeviathanBag>()));
			noSiren.OnSuccess(new CommonDrop(Mod.Find<ModItem>("LeviathanTrophy").Type, 10));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("EnchantedPearl").Type, 10)); //done
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.HotlineFishingHook, 10));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.BottomlessBucket, 10));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SuperAbsorbantSponge, 10));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.CratePotion, 5, 5, 9)); noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.FishingPotion, 5, 5, 9));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SonarPotion, 5, 5, 9));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("IOU").Type, 1));
			noSiren.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("LeviathanMask").Type, 7)); 
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Atlantis").Type, 4)));
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("BrackishFlask").Type, 4)));
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Leviatitan").Type, 4)));
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("LureofEnthrallment").Type, 4)));
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("SirensSong").Type, 4)));
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Greentide").Type, 4)));
			noSiren.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Atlantis").Type, 4)));
			npcLoot.Add(notExpert);
			npcLoot.Add(noSiren);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Wet, 240, true);
		}

		public override bool CheckActive()
		{
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Texture2D texture2 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/LeviathanTexTwo").Value;
			Texture2D texture3 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/LeviathanAltTexOne").Value;
			Texture2D texture4 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/LeviathanAltTexTwo").Value;
			if (NPC.IsABestiaryIconDummy)
			{
				Vector2 position = NPC.Center - screenPos + new Vector2(100f, 0f);
				Vector2 origin = new Vector2((TextureAssets.Npc[NPC.type].Value.Width / 2), (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
				SpriteEffects effects = (NPC.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
				float scale = 0.3f;
				Main.EntitySpriteDraw(texture, position, (NPC.frame), Color.White, NPC.rotation, origin, scale, effects, 0f);
				return false;
			}
			if (NPC.ai[0] == 1f)
			{
				if (NPC.localAI[0] == 0f)
				{
					CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture, 0, NPC, drawColor);
				}
				else
				{
					CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture2, 0, NPC, drawColor);
				}
			}
			else
			{
				if (NPC.localAI[0] == 0f)
				{
					CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture3, 0, NPC, drawColor);
				}
				else
				{
					CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture4, 0, NPC, drawColor);
				}
			}
			return false;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 3)
			{
				NPC.frame.Y = 0;
				NPC.localAI[0] += 1f;
			}
			if (NPC.localAI[0] > 1f)
			{
				NPC.localAI[0] = 0f;
				NPC.netUpdate = true;
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
	}
}