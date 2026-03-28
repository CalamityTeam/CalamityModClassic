using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Scavenger;
using CalamityModClassicPreTrailer.Items.Weapons.Ravager;
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

namespace CalamityModClassicPreTrailer.NPCs.Scavenger
{
	[AutoloadBossHead]
	public class ScavengerBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ravager");
			Main.npcFrameCount[NPC.type] = 7;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.5f,
				PortraitScale = 0.5f,
				CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/Scavenger/Scavenger_Bestiary",
				PortraitPositionYOverride = 60f
			};
			value.Position.Y += 35f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new FlavorTextBestiaryInfoElement("The innumerable corpses of the undead, forced to rise once more in a bid for victory... A great mistake that cost its creators their lives.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 20f;
			NPC.aiStyle = -1;
			NPC.damage = 100;
			NPC.width = 332; //324
			NPC.height = 214; //216
			NPC.defense = 80;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 53500 : 42700;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 90000;
			}
			NPC.knockBackResist = 0f;
			AIType = -1;
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
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.boss = true;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 25, 0, 0);
			NPC.HitSound = SoundID.NPCHit41;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Ravager");
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.defense = 180;
				NPC.lifeMax = 350000;
				NPC.value = Item.buyPrice(0, 35, 0, 0);
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 2300000 : 2100000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override void AI()
		{
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			Lighting.AddLight((int)(NPC.position.X - 100f) / 16, (int)(NPC.position.Y - 20f) / 16, 0f, 0.51f, 2f);
			Lighting.AddLight((int)(NPC.position.X + 100f) / 16, (int)(NPC.position.Y - 20f) / 16, 0f, 0.51f, 2f);
			CalamityGlobalNPC.scavenger = NPC.whoAmI;
			if (NPC.localAI[0] == 0f && Main.netMode != 1)
			{
				NPC.localAI[0] = 1f;
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X - 70, (int)NPC.Center.Y + 88, Mod.Find<ModNPC>("ScavengerLegLeft").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X + 70, (int)NPC.Center.Y + 88, Mod.Find<ModNPC>("ScavengerLegRight").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X - 120, (int)NPC.Center.Y + 50, Mod.Find<ModNPC>("ScavengerClawLeft").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X + 120, (int)NPC.Center.Y + 50, Mod.Find<ModNPC>("ScavengerClawRight").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X + 1, (int)NPC.Center.Y - 20, Mod.Find<ModNPC>("ScavengerHead").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
			if (NPC.target >= 0 && Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
				if (Main.player[NPC.target].dead)
				{
					NPC.noTileCollide = true;
				}
			}
			if (NPC.alpha > 0)
			{
				NPC.alpha -= 10;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				NPC.ai[1] = 0f;
			}
			bool leftLegActive = false;
			bool rightLegActive = false;
			bool headActive = false;
			bool rightClawActive = false;
			bool leftClawActive = false;
			for (int num619 = 0; num619 < 200; num619++)
			{
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerHead").Type)
				{
					headActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerClawRight").Type)
				{
					rightClawActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerClawLeft").Type)
				{
					leftClawActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerLegRight").Type)
				{
					rightLegActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerLegLeft").Type)
				{
					leftLegActive = true;
				}
			}
			bool enrage = false;
			if (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) > NPC.position.Y + (float)(NPC.height / 2) + 10f)
			{
				enrage = true;
			}
			if (headActive || rightClawActive || leftClawActive || rightLegActive || leftLegActive)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
				if (Main.netMode != 2)
				{
					if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
					{
						Main.player[Main.myPlayer].AddBuff(Mod.Find<ModBuff>("WeakPetrification").Type, 2);
					}
				}
			}
			if (!headActive)
			{
				int rightDust = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y - 30f), 8, 8, 5, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[rightDust].alpha += Main.rand.Next(100);
				Main.dust[rightDust].velocity *= 0.2f;
				Dust rightDustExpr = Main.dust[rightDust];
				rightDustExpr.velocity.Y = rightDustExpr.velocity.Y - (3f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.Next(10) == 0)
				{
					rightDust = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y - 30f), 8, 8, 6, 0f, 0f, 0, default(Color), 1.5f);
					if (Main.rand.Next(20) != 0)
					{
						Main.dust[rightDust].noGravity = true;
						Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust rightDustExpr2 = Main.dust[rightDust];
						rightDustExpr2.velocity.Y = rightDustExpr2.velocity.Y - 4f;
					}
				}
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += (enrage ? 6f : 1f);
					if (NPC.localAI[1] >= 600f)
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
							double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
							double deltaAngle = spread / 8f;
							double offsetAngle;
							int i;
							int laserDamage = expertMode ? 34 : 48;
							for (i = 0; i < 4; i++)
							{
								offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), 259, laserDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, (float)(-Math.Sin(offsetAngle) * 7f), (float)(-Math.Cos(offsetAngle) * 7f), 259, laserDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
			}
			if (!rightClawActive)
			{
				int rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 80f, NPC.Center.Y + 45f), 8, 8, 5, 0f, 0f, 100, default(Color), 3f);
				Main.dust[rightDust].alpha += Main.rand.Next(100);
				Main.dust[rightDust].velocity *= 0.2f;
				Dust rightDustExpr = Main.dust[rightDust];
				rightDustExpr.velocity.X = rightDustExpr.velocity.X + (3f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.Next(10) == 0)
				{
					rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 80f, NPC.Center.Y + 45f), 8, 8, 6, 0f, 0f, 0, default(Color), 2f);
					if (Main.rand.Next(20) != 0)
					{
						Main.dust[rightDust].noGravity = true;
						Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust rightDustExpr2 = Main.dust[rightDust];
						rightDustExpr2.velocity.X = rightDustExpr2.velocity.X + 4f;
					}
				}
				if (Main.netMode != 1)
				{
					NPC.localAI[2] += (enrage ? 2f : 1f);
					if (NPC.localAI[2] >= 480f)
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						NPC.localAI[2] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X + 80f, NPC.Center.Y + 45f);
						int damage = expertMode ? 28 : 40;
						int laser = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, 12f, 0f, 258, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (!leftClawActive)
			{
				int leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 80f, NPC.Center.Y + 45f), 8, 8, 5, 0f, 0f, 100, default(Color), 3f);
				Main.dust[leftDust].alpha += Main.rand.Next(100);
				Main.dust[leftDust].velocity *= 0.2f;
				Dust leftDustExpr = Main.dust[leftDust];
				leftDustExpr.velocity.X = leftDustExpr.velocity.X - (3f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[leftDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.Next(10) == 0)
				{
					leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 80f, NPC.Center.Y + 45f), 8, 8, 6, 0f, 0f, 0, default(Color), 2f);
					if (Main.rand.Next(20) != 0)
					{
						Main.dust[leftDust].noGravity = true;
						Main.dust[leftDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust leftDustExpr2 = Main.dust[leftDust];
						leftDustExpr2.velocity.X = leftDustExpr2.velocity.X - 4f;
					}
				}
				if (Main.netMode != 1)
				{
					NPC.localAI[3] += (enrage ? 2f : 1f);
					if (NPC.localAI[3] >= 480f)
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						NPC.localAI[3] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X - 80f, NPC.Center.Y + 45f);
						int damage = expertMode ? 28 : 40;
						int laser = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, -12f, 0f, 258, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (!rightLegActive)
			{
				int rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 60f, NPC.Center.Y + 60f), 8, 8, 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[rightDust].alpha += Main.rand.Next(100);
				Main.dust[rightDust].velocity *= 0.2f;
				Dust rightDustExpr = Main.dust[rightDust];
				rightDustExpr.velocity.Y = rightDustExpr.velocity.Y + (0.5f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.Next(10) == 0)
				{
					rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 60f, NPC.Center.Y + 60f), 8, 8, 6, 0f, 0f, 0, default(Color), 1.5f);
					if (Main.rand.Next(20) != 0)
					{
						Main.dust[rightDust].noGravity = true;
						Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust rightDustExpr2 = Main.dust[rightDust];
						rightDustExpr2.velocity.Y = rightDustExpr2.velocity.Y + 1f;
					}
				}
				if (Main.netMode != 1)
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 300f)
					{
						NPC.ai[2] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X + 60f, NPC.Center.Y + 60f);
						int damage = expertMode ? 28 : 40;
						int fire = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, 0f, 2f, 326 + Main.rand.Next(3), damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[fire].timeLeft = 180;
					}
				}
			}
			if (!leftLegActive)
			{
				int leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 60f, NPC.Center.Y + 60f), 8, 8, 5, 0f, 0f, 100, default(Color), 2f);
				Main.dust[leftDust].alpha += Main.rand.Next(100);
				Main.dust[leftDust].velocity *= 0.2f;
				Dust leftDustExpr = Main.dust[leftDust];
				leftDustExpr.velocity.Y = leftDustExpr.velocity.Y + (0.5f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[leftDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.Next(10) == 0)
				{
					leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 60f, NPC.Center.Y + 60f), 8, 8, 6, 0f, 0f, 0, default(Color), 1.5f);
					if (Main.rand.Next(20) != 0)
					{
						Main.dust[leftDust].noGravity = true;
						Main.dust[leftDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust leftDustExpr2 = Main.dust[leftDust];
						leftDustExpr2.velocity.Y = leftDustExpr2.velocity.Y + 1f;
					}
				}
				if (Main.netMode != 1)
				{
					NPC.ai[3] += 1f;
					if (NPC.ai[3] >= 300f)
					{
						NPC.ai[3] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X - 60f, NPC.Center.Y + 60f);
						int damage = expertMode ? 28 : 40;
						int fire = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, 0f, 2f, 326 + Main.rand.Next(3), damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[fire].timeLeft = 180;
					}
				}
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.noTileCollide = false;
				if (NPC.velocity.Y == 0f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.8f;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 0f)
					{
						if ((!rightClawActive && !leftClawActive) || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							NPC.ai[1] += 1f;
						}
						if (!headActive || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							NPC.ai[1] += 1f;
						}
						if ((!rightLegActive && !leftLegActive) || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							NPC.ai[1] += 1f;
						}
					}
					if (NPC.ai[1] >= 300f)
					{
						NPC.ai[1] = -20f;
					}
					else if (NPC.ai[1] == -1f)
					{
						NPC.TargetClosest(true);
						int speedXMult = ((enrage || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 8 : 4);
						NPC.velocity.X = (float)(speedXMult * NPC.direction);
						NPC.velocity.Y = -15.2f;
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
					}
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				if (NPC.velocity.Y == 0f)
				{
					SoundEngine.PlaySound(SoundID.Item14, NPC.position);
					NPC.ai[0] = 0f;
					if (Main.netMode != 1)
					{
						if (NPC.CountNPCS(Mod.Find<ModNPC>("RockPillar").Type) < 2)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X - 360, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("RockPillar").Type, 0, 0f, 0f, 0f, 0f, 255);
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X + 360, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("RockPillar").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						if (NPC.CountNPCS(Mod.Find<ModNPC>("FlamePillar").Type) < 2)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)Main.player[NPC.target].Center.X - 180, (int)Main.player[NPC.target].Center.Y - 10, Mod.Find<ModNPC>("FlamePillar").Type, 0, 0f, 0f, 0f, 0f, 255);
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)Main.player[NPC.target].Center.X + 180, (int)Main.player[NPC.target].Center.Y - 10, Mod.Find<ModNPC>("FlamePillar").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
					}
					for (int stompDustArea = (int)NPC.position.X - 30; stompDustArea < (int)NPC.position.X + NPC.width + 60; stompDustArea += 30)
					{
						for (int stompDustAmount = 0; stompDustAmount < 6; stompDustAmount++)
						{
							int stompDust = Dust.NewDust(new Vector2(NPC.position.X - 30f, NPC.position.Y + (float)NPC.height), NPC.width + 30, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[stompDust].velocity *= 0.2f;
						}
						int stompGore = Gore.NewGore(NPC.GetSource_FromThis(null), new Vector2((float)(stompDustArea - 30), NPC.position.Y + (float)NPC.height - 12f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[stompGore].velocity *= 0.4f;
					}
				}
				else
				{
					NPC.TargetClosest(true);
					if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
						NPC.velocity.Y = NPC.velocity.Y + 0.2f;
					}
					else
					{
						if (NPC.direction < 0)
						{
							NPC.velocity.X = NPC.velocity.X - 0.2f;
						}
						else if (NPC.direction > 0)
						{
							NPC.velocity.X = NPC.velocity.X + 0.2f;
						}
						float velocityX = 3f;
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							velocityX += 3f;
						}
						if (!rightClawActive)
						{
							velocityX += 1f;
						}
						if (!leftClawActive)
						{
							velocityX += 1f;
						}
						if (!headActive)
						{
							velocityX += 1f;
						}
						if (!rightLegActive)
						{
							velocityX += 1f;
						}
						if (!leftLegActive)
						{
							velocityX += 1f;
						}
						if (NPC.velocity.X < -velocityX)
						{
							NPC.velocity.X = -velocityX;
						}
						if (NPC.velocity.X > velocityX)
						{
							NPC.velocity.X = velocityX;
						}
					}
				}
			}
			if (NPC.target <= 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			int distanceFromTarget = 3000;
			if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget)
			{
				NPC.TargetClosest(true);
				if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget)
				{
					NPC.active = false;
					NPC.netUpdate = true;
					return;
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			Vector2 vector = center - Main.screenPosition;
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerBodyGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerBodyGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Blue);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerBodyGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
			Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerLegRight").Value, new Vector2(center.X - Main.screenPosition.X + 28f, center.Y - Main.screenPosition.Y + 20f), //72 
				new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerLegRight").Value.Width, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerLegRight").Value.Height)),
				color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerLegLeft").Value, new Vector2(center.X - Main.screenPosition.X - 112f, center.Y - Main.screenPosition.Y + 20f), //72
				new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerLegLeft").Value.Width, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerLegLeft").Value.Height)),
				color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			if (NPC.CountNPCS(Mod.Find<ModNPC>("ScavengerHead").Type) > 0)
			{
				Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerHead").Value, new Vector2(center.X - Main.screenPosition.X - 70f, center.Y - Main.screenPosition.Y - 75f),
					new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerHead").Value.Width, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Scavenger/ScavengerHead").Value.Height)),
					color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 2f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerBody").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerBody2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerBody3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerBody4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerBody5").Type, 1f);
				}
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 2f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "Ravager";
			potionType = ItemID.GreaterHealingPotion;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<RavagerBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RavagerBag>()));
			npcLoot.Add(new CommonDrop(ModContent.ItemType<RavagerTrophy>(), 10));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Bloodstone>(), 1, 50, 61));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<VerstaltiteBar>(), 1, 5, 11));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<DraedonBar>(), 1, 5, 11));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CruptixBar>(), 1, 5, 11));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofCinder>(), 1, 1, 4));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofEleum>(), 1, 1, 4));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofChaos>(), 1, 1, 4));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<BarofLife>(), 2));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofCalamity>(), 3));
			
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<VerstaltiteBar>(), 1, 1, 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<DraedonBar>(), 1, 1, 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CruptixBar>(), 1, 1, 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CoreofCinder>(), 1, 1, 3));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CoreofEleum>(), 1, 1, 3));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CoreofChaos>(), 1, 1, 3));
			
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BloodPact>(), 3));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<FleshTotem>(), 3));
			notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{
				ModContent.ItemType<Hematemesis>(),
				ModContent.ItemType<RealmRavager>(),
				ModContent.ItemType<SpikecragStaff>(),
				ModContent.ItemType<UltimusCleaver>(),
				ModContent.ItemType<CraniumSmasher>(),
			}));
			npcLoot.Add(notExpert);
		}
	}
}