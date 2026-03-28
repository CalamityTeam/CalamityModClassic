using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items.Accessories.RareVariants;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.DevourerMunsters;
using CalamityModClassicPreTrailer.Items.Providence;
using CalamityModClassicPreTrailer.Items.Weapons.Providence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Providence
{
	[AutoloadBossHead]
	public class Providence : ModNPC
	{
		private bool text = false;
		private float bossLife;
		private float flightPath = 0f;
		private int phaseChange = 0;
		private int immuneTimer = 300;
		private int frameUsed = 0;
		private int healTimer = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Providence, the Profaned Goddess");
			Main.npcFrameCount[NPC.type] = 3;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.2f,
				PortraitScale = 0.32f,
				PortraitPositionYOverride = 16f
			};
			value.Position.Y += 6f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("Born from dark and light, Providence seeks to cleanse all from desire with the might of her Holy Flame.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 36f;
			NPC.damage = 100;
			NPC.width = 600;
			NPC.height = 450;
			NPC.defense = 50;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 500000 : 440000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 715000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 15000000 : 12500000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.value = Item.buyPrice(0, 50, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.chaseable = true;
			NPC.canGhostHeal = false;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/ProvidenceTheme");
			NPC.HitSound = SoundID.NPCHit44;
			NPC.DeathSound = SoundID.NPCDeath46;
		}

		public override void AI()
		{
			NPC.damage = 0;
			CalamityGlobalNPC.holyBoss = NPC.whoAmI;
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			Player player = Main.player[NPC.target];
			Vector2 vector = NPC.Center;
			bool isHoly = player.ZoneHallow;
			bool isHell = player.ZoneUnderworldHeight;
			bool canAttack = true;
			bool attackMore = (double)NPC.life <= (double)NPC.lifeMax * 0.15;
			bool phase2 = (double)NPC.life <= (double)NPC.lifeMax * 0.75;
			bool phase3 = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			if (!isHoly && !isHell && !CalamityWorldPreTrailer.bossRushActive)
			{
				if (immuneTimer > 0)
					immuneTimer--;
			}
			else
			{
				immuneTimer = 300;
			}
			NPC.dontTakeDamage = (immuneTimer <= 0);
			NPC.rotation = NPC.velocity.X * 0.004f;
			if (Main.raining)
			{
				Main.raining = false;
				if (Main.netMode == 2)
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			int guardianAmt = NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnOffense").Type) + NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnDefense").Type) + NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnHealer").Type);
			if (NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnHealer").Type) > 0)
			{
				float heal = revenge ? 90f : 120f;
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					heal = 30f;
				}
				switch (guardianAmt)
				{
					case 1:
						heal *= 2f;
						break;
					case 2:
						break;
					case 3:
						heal *= 0.5f;
						break;
				}
				healTimer++;
				if (healTimer >= heal)
				{
					healTimer = 0;
					if (Main.netMode != 1)
					{
						int healAmt = NPC.lifeMax / 200;
						if (healAmt > NPC.lifeMax - NPC.life)
						{
							healAmt = NPC.lifeMax - NPC.life;
						}
						if (healAmt > 0)
						{
							NPC.life += healAmt;
							NPC.HealEffect(healAmt, true);
							NPC.netUpdate = true;
						}
					}
				}
			}
			bool tooFarAway = Vector2.Distance(Main.player[NPC.target].Center, vector) > 2800f;
			if ((!Main.dayTime && NPC.ai[0] != 2f && NPC.ai[0] != 5f && NPC.ai[0] != 7f) || player.dead)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
				if (NPC.velocity.X > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + 0.25f;
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X - 0.25f;
				}
				NPC.velocity.Y = NPC.velocity.Y - 0.25f;
			}
			else if (NPC.timeLeft < 3600)
			{
				NPC.timeLeft = 3600;
			}
			if (tooFarAway)
			{
				if (!Main.player[NPC.target].dead && Main.player[NPC.target].active)
					Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("HolyInferno").Type, 2);
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
			if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.66);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
						int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
						int spawn1 = Mod.Find<ModNPC>("ProvSpawnDefense").Type;
						int spawn2 = Mod.Find<ModNPC>("ProvSpawnHealer").Type;
						int spawn3 = Mod.Find<ModNPC>("ProvSpawnOffense").Type;
						int spawnNPC1 = NPC.NewNPC(NPC.GetSource_FromThis(null), x - 100, y - 100, spawn1, 0, 0f, 0f, 0f, 0f, 255);
						int spawnNPC2 = NPC.NewNPC(NPC.GetSource_FromThis(null), x + 100, y - 100, spawn2, 0, 0f, 0f, 0f, 0f, 255);
						int spawnNPC3 = NPC.NewNPC(NPC.GetSource_FromThis(null), x, y + 100, spawn3, 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, spawnNPC1, 0f, 0f, 0f, 0, 0, 0);
							NetMessage.SendData(23, -1, -1, null, spawnNPC2, 0f, 0f, 0f, 0, 0, 0);
							NetMessage.SendData(23, -1, -1, null, spawnNPC3, 0f, 0f, 0f, 0, 0, 0);
						}
						return;
					}
				}
			}
			if (guardianAmt > 0)
			{
				canAttack = attackMore;
			}
			NPC.chaseable = canAttack && NPC.ai[0] != 2f && NPC.ai[0] != 5f && NPC.ai[0] != 7f;
			if (NPC.ai[0] != 2f && NPC.ai[0] != 5f)
			{
				bool firingLaser = NPC.ai[0] == 7f;
				if (flightPath == 0f)
				{
					NPC.TargetClosest(true);
					if (NPC.Center.X < player.Center.X)
						flightPath = 1f;
					else
						flightPath = -1f;
				}
				NPC.TargetClosest(true);
				int num851 = 800;
				float num852 = Math.Abs(NPC.Center.X - player.Center.X);
				if (NPC.Center.X < player.Center.X && flightPath < 0f && num852 > (float)num851)
				{
					flightPath = 0f;
				}
				if (NPC.Center.X > player.Center.X && flightPath > 0f && num852 > (float)num851)
				{
					flightPath = 0f;
				}
				float num853 = expertMode ? 1.1f : 1.05f;
				float num854 = expertMode ? 16f : 15f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
				{
					num853 = expertMode ? 1.15f : 1.1f;
					num854 = expertMode ? 17f : 16f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					num853 = expertMode ? 1.2f : 1.15f;
					num854 = expertMode ? 18f : 17f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
				{
					num853 = expertMode ? 1.25f : 1.2f;
					num854 = expertMode ? 19f : 18f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
				{
					num853 = expertMode ? 1.3f : 1.25f;
					num854 = expertMode ? 20f : 19f;
				}
				if (firingLaser)
				{
					num854 *= (canAttack ? 0.5f : 0.25f);
					num853 *= (canAttack ? 0.5f : 0.25f);
				}
				NPC.velocity.X = NPC.velocity.X + flightPath * num853;
				if (NPC.velocity.X > num854)
				{
					NPC.velocity.X = num854;
				}
				if (NPC.velocity.X < -num854)
				{
					NPC.velocity.X = -num854;
				}
				float num855 = player.position.Y - (NPC.position.Y + (float)NPC.height);
				if (num855 < (firingLaser ? 150f : 200f)) //150
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.2f;
				}
				if (num855 > (firingLaser ? 200f : 250f)) //200
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.2f;
				}
				float speedVariance = (canAttack ? 3f : 1.5f);
				if (NPC.velocity.Y > (firingLaser ? speedVariance : 6f)) //8
				{
					NPC.velocity.Y = (firingLaser ? speedVariance : 6f);
				}
				if (NPC.velocity.Y < (firingLaser ? -speedVariance : -6f)) //8
				{
					NPC.velocity.Y = (firingLaser ? -speedVariance : -6f);
				}
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				float num852 = Math.Abs(NPC.Center.X - player.Center.X);
				if ((num852 < 500f || NPC.ai[3] < 0f) && NPC.position.Y < player.position.Y)
				{
					NPC.ai[3] += 1f;
					int num856 = expertMode ? 25 : 26;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num856 = expertMode ? 23 : 24;
					}
					if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						num856 = expertMode ? 21 : 22;
					}
					if (!canAttack)
					{
						num856 = expertMode ? 40 : 46;
					}
					num856++;
					if (NPC.ai[3] > (float)num856)
					{
						NPC.ai[3] = (float)(-(float)num856);
					}
					if (NPC.ai[3] == 0f && Main.netMode != 1)
					{
						Vector2 vector112 = new Vector2(NPC.Center.X, NPC.Center.Y);
						vector112.X += NPC.velocity.X * 7f;
						float num857 = player.position.X + (float)player.width * 0.5f - vector112.X;
						float num858 = player.Center.Y - vector112.Y;
						float num859 = (float)Math.Sqrt((double)(num857 * num857 + num858 * num858));
						float num860 = expertMode ? 10.25f : 9f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							num860 = expertMode ? 11.5f : 10f;
						}
						if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							num860 = expertMode ? 12.75f : 11f;
						}
						if (revenge)
						{
							num860 *= 1.15f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 46 : 63;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector112.X, vector112.Y, num857, num858, Mod.Find<ModProjectile>("HolyBlast").Type, holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				else if (NPC.ai[3] < 0f)
				{
					NPC.ai[3] += 1f;
				}
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 300f)
						NPC.ai[0] = -1f;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				if (Main.netMode != 1)
				{
					NPC.ai[3] += 1f;
					int num864 = expertMode ? 33 : 36;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num864 = expertMode ? 30 : 33;
					}
					if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
					{
						num864 = expertMode ? 26 : 29;
					}
					if (!canAttack)
					{
						num864 = expertMode ? 45 : 52;
					}
					num864 += 3;
					if (NPC.ai[3] >= (float)num864)
					{
						NPC.ai[3] = 0f;
						Vector2 vector113 = new Vector2(NPC.Center.X, NPC.position.Y + (float)NPC.height - 14f);
						float num865 = NPC.velocity.Y;
						if (num865 < 0f)
						{
							num865 = 0f;
						}
						num865 += expertMode ? 4f : 3f;
						float speedX2 = NPC.velocity.X * 0.25f;
						int fireDamage = expertMode ? 40 : 59; //260 200
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector113.X, vector113.Y, speedX2, num865, Mod.Find<ModProjectile>("HolyFire").Type, fireDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 300f)
						NPC.ai[0] = -1f;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.TargetClosest(true);
				Vector2 vector114 = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
				float num866 = (float)Main.rand.Next(-1000, 1001);
				float num867 = (float)Main.rand.Next(-1000, 1001);
				float num868 = (float)Math.Sqrt((double)(num866 * num866 + num867 * num867));
				float num869 = 3f;
				NPC.velocity *= 0.95f;
				num868 = num869 / num868;
				num866 *= num868;
				num867 *= num868;
				vector114.X += num866 * 4f;
				vector114.Y += num867 * 4f;
				NPC.ai[3] += 1f;
				int num870 = expertMode ? 3 : 4;
				if (!canAttack)
				{
					num870 = expertMode ? 12 : 16;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
				{
					num870 -= 2;
				}
				if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
				{
					num870 -= 2;
				}
				if (NPC.ai[3] > (float)num870)
				{
					NPC.ai[3] = 0f;
					if (Main.netMode != 1)
					{
						if (Main.rand.Next(4) == 0 && !CalamityWorldPreTrailer.death && !CalamityWorldPreTrailer.bossRushActive)
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector114.X, vector114.Y, num866, num867, Mod.Find<ModProjectile>("HolyLight").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						else
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector114.X, vector114.Y, num866, num867, Mod.Find<ModProjectile>("HolyBurnOrb").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				NPC.ai[1] += 1f;
				if (NPC.ai[1] > 450f && !text)
				{
					text = true;
					string key = "The air is burning...";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
						Main.NewText(Language.GetTextValue(key), messageColor);
					else if (Main.netMode == 2)
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				if (NPC.ai[1] > 600f)
				{
					if (Main.netMode != 2)
					{
						Player player2 = Main.player[Main.myPlayer];
						if (!player2.dead && player2.active && Vector2.Distance(player2.Center, vector) < 2800f)
						{
							SoundEngine.PlaySound(SoundID.Item20, player2.position);
							player2.AddBuff(Mod.Find<ModBuff>("ExtremeGravity").Type, 3000, true);
							for (int num621 = 0; num621 < 40; num621++)
							{
								int num622 = Dust.NewDust(new Vector2(player2.position.X, player2.position.Y),
									player2.width, player2.height, 244, 0f, 0f, 100, default(Color), 2f);
								Main.dust[num622].velocity *= 3f;
								if (Main.rand.Next(2) == 0)
								{
									Main.dust[num622].scale = 0.5f;
									Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
								}
							}
							for (int num623 = 0; num623 < 60; num623++)
							{
								int num624 = Dust.NewDust(new Vector2(player2.position.X, player2.position.Y),
									player2.width, player2.height, 244, 0f, 0f, 100, default(Color), 3f);
								Main.dust[num624].noGravity = true;
								Main.dust[num624].velocity *= 5f;
								num624 = Dust.NewDust(new Vector2(player2.position.X, player2.position.Y),
									player2.width, player2.height, 244, 0f, 0f, 100, default(Color), 2f);
								Main.dust[num624].velocity *= 2f;
							}
						}
					}
					text = false;
					NPC.ai[0] = -1f;
				}
			}
			if (NPC.ai[0] == 3f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				float num852 = Math.Abs(NPC.Center.X - player.Center.X);
				if ((num852 < 500f || NPC.ai[3] < 0f) && NPC.position.Y < player.position.Y)
				{
					NPC.ai[3] += 1f;
					int num856 = expertMode ? 10 : 11;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num856 = expertMode ? 9 : 10;
					}
					if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
					{
						num856 = expertMode ? 8 : 9;
					}
					if (!canAttack)
					{
						num856 = expertMode ? 30 : 35;
					}
					num856++;
					if (NPC.ai[3] > (float)num856)
					{
						NPC.ai[3] = (float)(-(float)num856);
					}
					if (NPC.ai[3] == 0f && Main.netMode != 1)
					{
						Vector2 vector112 = new Vector2(NPC.Center.X, NPC.Center.Y);
						vector112.X += NPC.velocity.X * 7f;
						float num857 = player.position.X + (float)player.width * 0.5f - vector112.X;
						float num858 = player.Center.Y - vector112.Y;
						float num859 = (float)Math.Sqrt((double)(num857 * num857 + num858 * num858));
						float num860 = expertMode ? 10.25f : 9f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							num860 = expertMode ? 11.5f : 10f;
						}
						if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
						{
							num860 = expertMode ? 12.75f : 11f;
						}
						if (revenge)
						{
							num860 *= 1.15f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 39 : 55; //280 210
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector112.X, vector112.Y, num857 * 0.1f, num858, Mod.Find<ModProjectile>("MoltenBlast").Type, holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				else if (NPC.ai[3] < 0f)
				{
					NPC.ai[3] += 1f;
				}
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 300f)
						NPC.ai[0] = -1f;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				if (Main.netMode != 1)
				{
					NPC.ai[3] += 1f;
					int num864 = expertMode ? 70 : 74;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num864 = expertMode ? 64 : 70;
					}
					if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						num864 = expertMode ? 56 : 64;
					}
					if (!canAttack)
					{
						num864 = expertMode ? 148 : 156;
					}
					num864 += 3;
					if (NPC.ai[3] >= (float)num864)
					{
						NPC.ai[3] = 0f;
						Vector2 vector113 = new Vector2(NPC.Center.X, NPC.position.Y + (float)NPC.height - 14f);
						int i2 = (int)(vector113.X / 16f);
						int j2 = (int)(vector113.Y / 16f);
						if (!WorldGen.SolidTile(i2, j2))
						{
							float num865 = NPC.velocity.Y;
							if (num865 < 0f)
							{
								num865 = 0f;
							}
							num865 += expertMode ? 4f : 3f;
							float speedX2 = NPC.velocity.X * 0.25f;
							int fireDamage = expertMode ? 44 : 60; //260 100
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector113.X, vector113.Y, speedX2, num865, Mod.Find<ModProjectile>("HolyBomb").Type, fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 300f)
						NPC.ai[0] = -1f;
				}
			}
			else if (NPC.ai[0] == 5f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				if (Main.netMode != 1)
				{
					NPC.ai[2] += ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 2f : 1f);
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.ai[2] += 1f;
					}
					if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.ai[2] += 1f;
					}
					if (NPC.ai[2] > (canAttack ? 24f : 60f))
					{
						NPC.ai[2] = 0f;
						Vector2 vector93 = new Vector2(vector.X, vector.Y);
						float num742 = 10f;
						if (expertMode)
						{
							num742 = 12f;
						}
						float num743 = player.position.X + (float)player.width * 0.5f - vector93.X;
						float num744 = player.position.Y + (float)player.height * 0.5f - vector93.Y;
						float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
						num745 = num742 / num745;
						num743 *= num745;
						num744 *= num745;
						int num746 = expertMode ? 48 : 65; //288 220
						int num747 = Mod.Find<ModProjectile>("HolyShot").Type;
						vector93.X += num743 * 3f;
						vector93.Y += num744 * 3f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.position.X + (float)Main.rand.Next(-1000, 1000), player.position.Y + (float)Main.rand.Next(-100, 100), 0f, 0f, Mod.Find<ModProjectile>("HolySpear").Type, num746, 0f, Main.myPlayer, (float)Main.rand.Next(2), 0f);
						return;
					}
				}
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 300f)
						NPC.ai[0] = -1f;
				}
			}
			else if (NPC.ai[0] == 6f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.TargetClosest(true);
				NPC.velocity *= 0.95f;
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 60f)
					{
						int damage = expertMode ? 52 : 70; //288 220
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), player.Center.X, player.Center.Y - 360f, 0f, 0f, Mod.Find<ModProjectile>("ProvidenceCrystal").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 7f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				Vector2 value19 = new Vector2(27f, 59f);
				NPC.ai[2] += 1f;
				if (NPC.ai[2] < 180f)
				{
					NPC.localAI[1] -= 0.05f;
					if (NPC.localAI[1] < 0f)
					{
						NPC.localAI[1] = 0f;
					}
					if (NPC.ai[2] >= 60f)
					{
						Vector2 center20 = NPC.Center;
						int num1220 = 0;
						if (NPC.ai[2] >= 120f)
						{
							num1220 = 1;
						}
						int num;
						for (int num1221 = 0; num1221 < 1 + num1220; num1221 = num + 1)
						{
							int num1222 = 244;
							float num1223 = 1.2f;
							if (num1221 % 2 == 1)
							{
								num1223 = 2.8f;
							}
							Vector2 vector199 = center20 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * value19 / 2f;
							int num1224 = Dust.NewDust(vector199 - Vector2.One * 8f, 16, 16, num1222, NPC.velocity.X / 2f, NPC.velocity.Y / 2f, 0, default(Color), 1f);
							Main.dust[num1224].velocity = Vector2.Normalize(center20 - vector199) * 3.5f * (10f - (float)num1220 * 2f) / 10f;
							Main.dust[num1224].noGravity = true;
							Main.dust[num1224].scale = num1223;
							num = num1221;
						}
					}
				}
				else if (NPC.ai[2] < 360f)
				{
					if (NPC.ai[2] == 180f)
					{
						if (Main.player[Main.myPlayer].active && !Main.player[Main.myPlayer].dead && Vector2.Distance(Main.player[Main.myPlayer].Center, vector) < 2800f)
						{
							SoundEngine.PlaySound(SoundID.Zombie104, new Vector2(Main.player[Main.myPlayer].position.X, Main.player[Main.myPlayer].position.Y));
						}
						if (Main.netMode != 1)
						{
							NPC.TargetClosest(false);
							Vector2 vector200 = player.Center - NPC.Center;
							vector200.Normalize();
							float num1225 = -1f;
							if (vector200.X < 0f)
							{
								num1225 = 1f;
							}
							vector200 = vector200.RotatedBy((double)(-(double)num1225 * 6.28318548f / 6f), default(Vector2));
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y - 16f, vector200.X, vector200.Y, Mod.Find<ModProjectile>("ProvidenceHolyRay").Type, 100, 0f, Main.myPlayer, num1225 * 6.28318548f / 450f, (float)NPC.whoAmI);
							NPC.ai[3] = (vector200.ToRotation() + 9.424778f) * num1225; //3.14159265f
							NPC.netUpdate = true;
						}
					}
					NPC.localAI[1] += 0.05f;
					if (NPC.localAI[1] > 1f)
					{
						NPC.localAI[1] = 1f;
					}
					float num1226 = (float)(NPC.ai[3] >= 0f).ToDirectionInt();
					float num1227 = NPC.ai[3];
					if (num1227 < 0f)
					{
						num1227 *= -1f;
					}
					num1227 += -9.424778f;
					num1227 += num1226 * 6.28318548f / 540f;
					NPC.localAI[0] = num1227;
				}
				else
				{
					NPC.localAI[1] -= 0.07f; //15
					if (NPC.localAI[1] < 0f)
						NPC.localAI[1] = 0f;
				}
				if (Main.netMode != 1)
				{
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 375f)
						NPC.ai[0] = -1f;
				}
			}
			if (NPC.ai[0] == -1f)
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				phaseChange++;
				if (phaseChange > 14)
				{
					phaseChange = 0;
				}
				int phase = 0; //0 = blasts 1 = holy fire 2 = shell heal 3 = molten blobs 4 = holy bombs 5 = shell spears 6 = crystal 7 = laser
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					switch (phaseChange)
					{
						case 0: phase = 4; break; //1575 or 1500
						case 1: phase = 5; break; //1875 or 1800
						case 2: phase = 0; break; //2175 or 2100
						case 3: phase = phase2 ? 6 : 1; break;
						case 4: phase = 2; break; //600
						case 5: phase = 4; break; //900
						case 6: phase = 1; break; //1200
						case 7: phase = 5; break; //1500
						case 8:
							phase = phase3 ? 7 : 3; //1875 or 1800
							if (phase3)
							{
								NPC.TargetClosest(false);
								Vector2 v3 = player.Center - NPC.Center - new Vector2(0f, -22f);
								float num1219 = v3.Length() / 500f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								num1219 = 1f - num1219;
								num1219 *= 2f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								NPC.localAI[0] = v3.ToRotation();
								NPC.localAI[1] = num1219;
							}
							break;
						case 9: phase = 3; break; //2175 or 2100
						case 10: phase = phase2 ? 6 : 2; break;
						case 11: phase = 4; break; //300
						case 12:
							phase = phase3 ? 7 : 4; //675 or 600
							if (phase3)
							{
								NPC.TargetClosest(false);
								Vector2 v3 = player.Center - NPC.Center - new Vector2(0f, -22f);
								float num1219 = v3.Length() / 500f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								num1219 = 1f - num1219;
								num1219 *= 2f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								NPC.localAI[0] = v3.ToRotation();
								NPC.localAI[1] = num1219;
							}
							break;
						case 13: phase = 5; break; //975 or 900
						case 14: phase = 0; break; //1275 or 1200
					}
				}
				else
				{
					switch (phaseChange)
					{
						case 0: phase = 0; break; //3375 or 3300
						case 1:
							phase = phase3 ? 7 : 1; //3750 or 3600
							if (phase3)
							{
								NPC.TargetClosest(false);
								Vector2 v3 = player.Center - NPC.Center - new Vector2(0f, -22f);
								float num1219 = v3.Length() / 500f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								num1219 = 1f - num1219;
								num1219 *= 2f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								NPC.localAI[0] = v3.ToRotation();
								NPC.localAI[1] = num1219;
							}
							break;
						case 2: phase = 3; break; //4050 or 3900
						case 3: phase = 4; break; //4350 or 4200
						case 4: phase = 5; break; //4650 or 4500
						case 5: phase = phase2 ? 6 : 4; break;
						case 6: phase = 3; break; //300
						case 7: phase = 1; break; //600
						case 8: phase = 0; break; //900
						case 9: phase = 2; break; //1500
						case 10: phase = 4; break; //1800
						case 11:
							phase = phase3 ? 7 : 0; //2175 or 2100
							if (phase3)
							{
								NPC.TargetClosest(false);
								Vector2 v3 = player.Center - NPC.Center - new Vector2(0f, -22f);
								float num1219 = v3.Length() / 500f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								num1219 = 1f - num1219;
								num1219 *= 2f;
								if (num1219 > 1f)
								{
									num1219 = 1f;
								}
								NPC.localAI[0] = v3.ToRotation();
								NPC.localAI[1] = num1219;
							}
							break;
						case 12: phase = 3; break; //2475 or 2400
						case 13: phase = 1; break; //2775 or 2700
						case 14: phase = 5; break; //3075 or 3000
					}
				}
				NPC.TargetClosest(true);
				if (Math.Abs(NPC.Center.X - player.Center.X) > 5600f)
				{
					phase = 0;
				}
				NPC.ai[0] = (float)phase;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				return;
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("ProvidenceTrophy").Type, 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<ProvidenceBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ProvidenceBag>()));
			npcLoot.Add(ItemDropRule.ByCondition(new HallowProvi(), ModContent.ItemType<ElysianWings>(), 1, 1, 1, 1));
			npcLoot.Add(ItemDropRule.ByCondition(new HellProvi(), ModContent.ItemType<ElysianAegis>(), 1, 1, 1, 1));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<UnholyEssence>(), 1, 20, 30));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DivineGeode>(), 1, 10, 16));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RuneofCos>(), 1));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProvidenceMask>(), 7));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SamuraiBadge>(), 40));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlissfulBombardier>(), 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HolyCollider>(), 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MoltenAmputator>(), 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PurgeGuzzler>(), 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SolarFlare>(), 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TelluricGlare>(), 4));
		}

		public override void OnKill()
		{
			if (Main.netMode != 1)
			{
				int num52 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
				int num53 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
				int num54 = NPC.width / 2 / 16 + 1;
				for (int num55 = num52 - num54; num55 <= num52 + num54; num55++)
				{
					for (int num56 = num53 - num54; num56 <= num53 + num54; num56++)
					{
						if ((num55 == num52 - num54 || num55 == num52 + num54 || num56 == num53 - num54 || num56 == num53 + num54) && !Main.tile[num55, num56].HasTile)
						{
							Main.tile[num55, num56].TileType = (ushort)Mod.Find<ModTile>("ProfanedRock").Type;
							Main.tile[num55, num56].Get<TileWallWireStateData>().HasTile = true;
						}
						
						Main.tile[num55, num56].LiquidAmount = 0;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num55, num56, 1, TileChangeType.None);
						}
						else
						{
							WorldGen.SquareTileFrame(num55, num56, true);
						}
					}
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			double newDamage = (modifiers.FinalDamage.Base + (int)((double)NPC.defense * 0.25));
			float protection = (((NPC.ichor || NPC.onFire2) ? 0.2f : 0.25f) +
					((NPC.ai[0] == 2f || NPC.ai[0] == 5f || NPC.ai[0] == 7f) ? 0.65f : 0f)); //0.85 or 0.9
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - protection) * newDamage));
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			if (NPC.ai[0] == 2f || NPC.ai[0] == 5f)
			{
				if (NPC.localAI[2] == 0f)
				{
					texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Providence/ProvidenceDefense").Value;
				}
				else
				{
					texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Providence/ProvidenceDefenseAlt").Value;
				}
			}
			else
			{
				if (frameUsed == 0)
				{
					texture = TextureAssets.Npc[NPC.type].Value;
				}
				else if (frameUsed == 1)
				{
					texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Providence/ProvidenceAlt").Value;
				}
				else if (frameUsed == 2)
				{
					texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Providence/ProvidenceAttack").Value;
				}
				else
				{
					texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Providence/ProvidenceAttackAlt").Value;
				}
			}
			if (NPC.IsABestiaryIconDummy)
			{
				Vector2 vector = new Vector2((TextureAssets.Npc[NPC.type].Value.Width / 2), (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
				SpriteEffects effects = SpriteEffects.None;
				if (NPC.spriteDirection == 1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				Vector2 position = NPC.Center - screenPos;
				Main.EntitySpriteDraw(texture, position, (NPC.frame), Color.White, NPC.rotation, vector, NPC.scale, effects, 0f);
				return false;
			}
			CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture, 0, NPC, drawColor);
			return false;
		}

		public override void FindFrame(int frameHeight) //9 total frames
		{
			if (NPC.ai[0] == 2f || NPC.ai[0] == 5f)
			{
				if (NPC.localAI[2] == 0f)
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 5.0)
					{
						NPC.frame.Y = NPC.frame.Y + frameHeight;
						NPC.frameCounter = 0.0;
					}
					if (NPC.frame.Y >= frameHeight * 3)
					{
						NPC.frame.Y = 0;
						NPC.localAI[2] = 1f;
						NPC.netUpdate = true;
					}
				}
				else
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 5.0)
					{
						NPC.frame.Y = NPC.frame.Y + frameHeight;
						NPC.frameCounter = 0.0;
					}
					if (NPC.frame.Y >= frameHeight * 2)
					{
						NPC.frame.Y = frameHeight * 2;
					}
				}
			}
			else
			{
				if (NPC.localAI[2] > 0f)
				{
					NPC.localAI[2] = 0f;
 					NPC.netUpdate = true;
				}
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 5.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y >= frameHeight * 3) //6
				{
					NPC.frame.Y = 0;
					frameUsed++;
				}
				if (frameUsed > 3)
				{
					frameUsed = 0;
					NPC.netUpdate = true;
				}
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 2f;
			return null;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				float randomSpread = (float)(Main.rand.Next(-50, 50) / 100);
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence4").Type, 1f);
				}
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 400;
				NPC.height = 350;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 90; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 244, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}