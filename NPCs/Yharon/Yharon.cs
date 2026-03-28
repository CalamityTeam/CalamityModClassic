using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Patreon;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.RareVariants;
using CalamityModClassicPreTrailer.Items.Weapons.Yharon;
using CalamityModClassicPreTrailer.Items.Yharon;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Yharon
{
	[AutoloadBossHead]
	public class Yharon : ModNPC
	{
		private Rectangle safeBox = default(Rectangle);
		private bool protectionBoost = false;
		private bool moveCloser = false;
		private bool phaseOneLoot = false;
		private bool dropLoot = false;
		private bool useTornado = true;
		private int healCounter = 0;
		private int secondPhasePhase = 1;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jungle Dragon, Yharon");
			Main.npcFrameCount[NPC.type] = 7;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("The tyrant's loyal lifelong friend has set his sight on you.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 50f;
			NPC.damage = 330;
			NPC.width = 200; //200
			NPC.height = 200; //200
			NPC.defense = 200;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 2525000 : 2275000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 3025000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 4000000 : 3700000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.value = Item.buyPrice(1, 50, 0, 0);
			NPC.boss = true;
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/YHARON");
			if (CalamityWorldPreTrailer.downedBuffedMothron || CalamityWorldPreTrailer.bossRushActive)
			{
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/YHARONREBIRTH");
			}
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath60;
		}

		public override void AI()
		{
			#region StartupOrSwitchToAI2
			dropLoot = (double)NPC.life <= (double)NPC.lifeMax * 0.1;
			if (Main.raining)
			{
				Main.raining = false;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			if (NPC.localAI[2] == 1f)
			{
				if (!CalamityWorldPreTrailer.downedBuffedMothron && !CalamityWorldPreTrailer.bossRushActive)
				{
					phaseOneLoot = true;
					NPC.DeathSound = null;
					NPC.dontTakeDamage = true;
					NPC.chaseable = false;
					NPC.velocity.Y = NPC.velocity.Y - 0.4f;
					if (NPC.alpha < 255)
					{
						NPC.alpha += 5;
						if (NPC.alpha > 255)
						{
							NPC.alpha = 255;
						}
					}
					if (NPC.timeLeft > 55)
					{
						NPC.timeLeft = 55;
					}
					if (NPC.timeLeft < 5)
					{
						string key = "My dragon deems you an unworthy opponent. You must acquire the power of the dark sun to witness his true power.";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
						NPC.localAI[2] = 0f;
						NPC.boss = false;
						NPC.life = 0;
						if (dropLoot)
						{
							NPC.NPCLoot();
						}
						NPC.active = false;
						NPC.netUpdate = true;
					}
					return;
				}
				phaseOneLoot = false;
				Yharon_AI2();
				return;
			}
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = Main.expertMode;
			bool phase2Check = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.85 : 0.7); //Check for phase 2.  In phase 1 for 15% or 30%
			bool phase3Check = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.6 : 0.4); //Check for phase 3.  In phase 2 for 25% or 30%
			bool phase4Check = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.25 : 0.2); //Check for phase 4.  In phase 3 for 35% or 20%
			if (CalamityWorldPreTrailer.death && !CalamityWorldPreTrailer.bossRushActive)
			{
				phase2Check = (double)NPC.life <= (double)NPC.lifeMax * 0.9; //Check for phase 2.  In phase 1 for 10%
				phase3Check = (double)NPC.life <= (double)NPC.lifeMax * 0.8; //Check for phase 3.  In phase 2 for 10%
				phase4Check = (double)NPC.life <= (double)NPC.lifeMax * 0.3; //Check for phase 4.  In phase 3 for 50%
			}
			bool phase5Check = (double)NPC.life <= (double)NPC.lifeMax * 0.1; //Check for phase 5.  In phase 4 for 15% or 10% or 20%
			bool phase2Change = NPC.ai[0] > 5f; //Phase 2 stuff
			bool phase3Change = NPC.ai[0] > 12f; //Phase 3 stuff
			bool phase4Change = NPC.ai[0] > 20f; //Phase 4 stuff
			int flareCount = 3;
			bool isCharging = NPC.ai[3] < 20f; //10
			if (NPC.localAI[1] == 0f)
			{
				NPC.localAI[1] = 1f;
				Vector2 vectorPlayer = new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y);
				safeBox.X = (int)(vectorPlayer.X - (revenge ? 3000f : 3500f));
				safeBox.Y = (int)(vectorPlayer.Y - (revenge ? 9000f : 10500f));
				safeBox.Width = revenge ? 6000 : 7000;
				safeBox.Height = revenge ? 18000 : 21000;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X + (revenge ? 3000f : 3500f), Main.player[NPC.target].position.Y + 100f, 0f, 0f, Mod.Find<ModProjectile>("SkyFlareRevenge").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X - (revenge ? 3000f : 3500f), Main.player[NPC.target].position.Y + 100f, 0f, 0f, Mod.Find<ModProjectile>("SkyFlareRevenge").Type, 0, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			#endregion
			#region SpeedOrDamageChecks
			float teleportLocation = 0f;
			int teleChoice = Main.rand.Next(2);
			if (teleChoice == 0)
			{
				teleportLocation = revenge ? 500f : 600f;
			}
			else
			{
				teleportLocation = revenge ? -500f : -600f;
			}
			if (phase4Change)
			{
				NPC.defense = 140;
			}
			else if (phase3Change)
			{
				NPC.defense = 160;
			}
			else if (phase2Change)
			{
				NPC.defense = 180;
			}
			else
			{
				NPC.defense = 200;
			}
			int aiChangeRate = expertMode ? 36 : 38;
			float npcVelocity = expertMode ? 0.7f : 0.69f;
			float scaleFactor = expertMode ? 11f : 10.8f;
			if (phase4Change || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
			{
				npcVelocity = 0.95f;
				scaleFactor = 14f;
				aiChangeRate = 25;
			}
			else if (phase3Change)
			{
				npcVelocity = 0.9f;
				scaleFactor = 13f;
				aiChangeRate = 25;
			}
			else if (phase2Change && isCharging)
			{
				npcVelocity = (expertMode ? 0.8f : 0.78f);
				scaleFactor = (expertMode ? 12.2f : 12f);
				aiChangeRate = (expertMode ? 36 : 38);
			}
			else if (isCharging && !phase2Change && !phase3Change && !phase4Change)
			{
				aiChangeRate = 25;
			}
			int chargeTime = expertMode ? 38 : 40;
			int chargeTime2 = expertMode ? 34 : 36;
			float chargeSpeed = expertMode ? 22f : 20.5f; //17 and 16
			float chargeSpeed2 = expertMode ? 37f : 34f;
			if (phase4Change || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) //phase 4
			{
				chargeTime = 28;
				chargeSpeed = 31f; //27
			}
			else if (phase3Change) //phase 3
			{
				chargeTime = 32;
				chargeTime2 = 27;
				chargeSpeed = 25f; //27
				chargeSpeed2 = 41f;
			}
			else if (isCharging && phase2Change) //phase 2
			{
				chargeTime = expertMode ? 35 : 37;
				chargeTime2 = expertMode ? 31 : 33;
				if (expertMode)
				{
					chargeSpeed = 24f; //21
					chargeSpeed2 = 39f;
				}
			}
			#endregion
			#region VariablesForChargingEtc
			int xPos = (NPC.direction == 1 ? 25 : -25);
			int num1454 = 80;
			int num1455 = 4;
			float num1456 = 0.3f;
			float scaleFactor11 = 5f;
			int num1457 = 90;
			int num1458 = 180;
			int num1459 = 180;
			int num1460 = 30;
			int num1461 = 120;
			int num1462 = 4;
			float scaleFactor13 = 20f;
			float num1463 = 6.28318548f / (float)(num1461 / 2);
			int num1464 = 75;
			#endregion
			#region DespawnOrEnrage
			Vector2 vectorCenter = NPC.Center;
			Player player = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
				player = Main.player[NPC.target];
				NPC.netUpdate = true;
			}
			if (player.dead)
			{
				NPC.velocity.Y = NPC.velocity.Y - 0.4f;
				if (NPC.timeLeft > 150)
				{
					NPC.timeLeft = 150;
				}
				if (NPC.ai[0] > 12f)
				{
					NPC.ai[0] = 13f;
				}
				else if (NPC.ai[0] > 5f)
				{
					NPC.ai[0] = 6f;
				}
				else
				{
					NPC.ai[0] = 0f;
				}
				NPC.ai[2] = 0f;
			}
			else if (NPC.timeLeft < 3600)
			{
				NPC.timeLeft = 3600;
			}
			if (!Main.player[NPC.target].Hitbox.Intersects(safeBox))
			{
				aiChangeRate = 15;
				protectionBoost = true;
				NPC.damage = NPC.defDamage * 5;
				chargeSpeed += 25f;
			}
			else
			{
				NPC.damage = expertMode ? 528 : 330;
				protectionBoost = false;
			}
			#endregion
			#region Rotation
			if (NPC.localAI[0] == 0f)
			{
				NPC.localAI[0] = 1f;
				NPC.alpha = 255;
				NPC.rotation = 0f;
				if (Main.netMode != 1)
				{
					NPC.ai[0] = -1f;
					NPC.netUpdate = true;
				}
			}
			float npcRotation = (float)Math.Atan2((double)(player.Center.Y - vectorCenter.Y), (double)(player.Center.X - vectorCenter.X));
			if (NPC.spriteDirection == 1)
			{
				npcRotation += 3.14159274f;
			}
			if (npcRotation < 0f)
			{
				npcRotation += 6.28318548f;
			}
			if (npcRotation > 6.28318548f)
			{
				npcRotation -= 6.28318548f;
			}
			if (NPC.ai[0] == -1f) //spawn
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 3f) //tornado
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 4f) //enter new phase
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 9f) //tornado
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 10f) //enter new phase
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 16f) //tornado
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 20f) //tornado
			{
				npcRotation = 0f;
			}
			float npcRotationSpeed = 0.04f;
			if (NPC.ai[0] == 1f || NPC.ai[0] == 5f || NPC.ai[0] == 7f || NPC.ai[0] == 11f || NPC.ai[0] == 14f || NPC.ai[0] == 18f) //charge
			{
				npcRotationSpeed = 0f;
			}
			if (NPC.ai[0] == 8f || NPC.ai[0] == 12f || NPC.ai[0] == 15f || NPC.ai[0] == 19f) //circle
			{
				npcRotationSpeed = 0f;
			}
			if (NPC.ai[0] == 3f) //tornado
			{
				npcRotationSpeed = 0.01f;
			}
			if (NPC.ai[0] == 4f) //enter new phase
			{
				npcRotationSpeed = 0.01f;
			}
			if (NPC.ai[0] == 9f || NPC.ai[0] == 16f || NPC.ai[0] == 20f) //tornado
			{
				npcRotationSpeed = 0.01f;
			}
			if (NPC.rotation < npcRotation)
			{
				if ((double)(npcRotation - NPC.rotation) > 3.1415926535897931)
				{
					NPC.rotation -= npcRotationSpeed;
				}
				else
				{
					NPC.rotation += npcRotationSpeed;
				}
			}
			if (NPC.rotation > npcRotation)
			{
				if ((double)(NPC.rotation - npcRotation) > 3.1415926535897931)
				{
					NPC.rotation += npcRotationSpeed;
				}
				else
				{
					NPC.rotation -= npcRotationSpeed;
				}
			}
			if (NPC.rotation > npcRotation - npcRotationSpeed && NPC.rotation < npcRotation + npcRotationSpeed)
			{
				NPC.rotation = npcRotation;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.28318548f;
			}
			if (NPC.rotation > 6.28318548f)
			{
				NPC.rotation -= 6.28318548f;
			}
			if (NPC.rotation > npcRotation - npcRotationSpeed && NPC.rotation < npcRotation + npcRotationSpeed)
			{
				NPC.rotation = npcRotation;
			}
			#endregion
			#region AlphaAndInitialSpawnEffects
			if (NPC.ai[0] != -1f && NPC.ai[0] < 9f)
			{
				bool colliding = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
				if (colliding)
				{
					NPC.alpha += 15;
				}
				else
				{
					NPC.alpha -= 15;
				}
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				if (NPC.alpha > 150)
				{
					NPC.alpha = 150;
				}
			}
			if (NPC.ai[0] == -1f) //initial spawn effects
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.velocity *= 0.98f;
				int num1467 = Math.Sign(player.Center.X - vectorCenter.X);
				if (num1467 != 0)
				{
					NPC.direction = num1467;
					NPC.spriteDirection = -NPC.direction;
				}
				if (NPC.ai[2] > 20f)
				{
					NPC.velocity.Y = -2f;
					NPC.alpha -= 5;
					bool colliding = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
					if (colliding)
					{
						NPC.alpha += 15;
					}
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
					if (NPC.alpha > 150)
					{
						NPC.alpha = 150;
					}
				}
				if (NPC.ai[2] == (float)(num1457 - 30))
				{
					int num1468 = 72;
					for (int num1469 = 0; num1469 < num1468; num1469++)
					{
						Vector2 vector169 = Vector2.Normalize(NPC.velocity) * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f;
						vector169 = vector169.RotatedBy((double)((float)(num1469 - (num1468 / 2 - 1)) * 6.28318548f / (float)num1468), default(Vector2)) + NPC.Center;
						Vector2 value16 = vector169 - NPC.Center;
						int num1470 = Dust.NewDust(vector169 + value16, 0, 0, 244, value16.X * 2f, value16.Y * 2f, 100, default(Color), 1.4f);
						Main.dust[num1470].noGravity = true;
						Main.dust[num1470].noLight = true;
						Main.dust[num1470].velocity = Vector2.Normalize(value16) * 3f;
					}
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1464)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			#endregion
			#region Phase1
			else if (NPC.ai[0] == 0f && !player.dead)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
				}
				Vector2 value17 = player.Center + new Vector2(NPC.ai[1], -200f) - vectorCenter;
				Vector2 vector170 = Vector2.Normalize(value17 - NPC.velocity) * scaleFactor;
				if (NPC.velocity.X < vector170.X)
				{
					NPC.velocity.X = NPC.velocity.X + npcVelocity;
					if (NPC.velocity.X < 0f && vector170.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + npcVelocity;
					}
				}
				else if (NPC.velocity.X > vector170.X)
				{
					NPC.velocity.X = NPC.velocity.X - npcVelocity;
					if (NPC.velocity.X > 0f && vector170.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - npcVelocity;
					}
				}
				if (NPC.velocity.Y < vector170.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
					if (NPC.velocity.Y < 0f && vector170.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
					}
				}
				else if (NPC.velocity.Y > vector170.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
					if (NPC.velocity.Y > 0f && vector170.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
					}
				}
				int num1471 = Math.Sign(player.Center.X - vectorCenter.X);
				if (num1471 != 0)
				{
					if (NPC.ai[2] == 0f && num1471 != NPC.direction)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.direction = num1471;
					if (NPC.spriteDirection != -NPC.direction)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.spriteDirection = -NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate)
				{
					int aiState = 0;
					switch ((int)NPC.ai[3]) //0 2 4 6 1 3 5 7 repeat
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
							aiState = 1; //normal charges
							break;
						case 5:
							aiState = 5; //fast charge
							break;
						case 6:
							NPC.ai[3] = 1f;
							aiState = 2; //fireball attack
							break;
						case 7:
							NPC.ai[3] = 0f;
							aiState = 3; //tornadoes
							break;
					}
					if (phase2Check)
					{
						aiState = 4;
					}
					if (aiState == 1)
					{
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1471 != 0)
						{
							NPC.direction = num1471;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					else if (aiState == 2)
					{
						NPC.ai[0] = 2f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 3)
					{
						NPC.ai[0] = 3f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 4)
					{
						NPC.ai[0] = 4f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 5)
					{
						NPC.ai[0] = 5f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed2;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1471 != 0)
						{
							NPC.direction = num1471;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 1f) //charge attack
			{
				int num1473 = 7;
				for (int num1474 = 0; num1474 < num1473; num1474++)
				{
					Vector2 vector171 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + vectorCenter;
					Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1475 = Dust.NewDust(vector171 + value18, 0, 0, 244, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num1475].noGravity = true;
					Main.dust[num1475].noLight = true;
					Main.dust[num1475].velocity /= 4f;
					Main.dust[num1475].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f) //fireball attack
			{
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
				}
				Vector2 value19 = player.Center + new Vector2(NPC.ai[1], -200f) - vectorCenter;
				Vector2 vector172 = Vector2.Normalize(value19 - NPC.velocity) * scaleFactor11;
				if (NPC.velocity.X < vector172.X)
				{
					NPC.velocity.X = NPC.velocity.X + num1456;
					if (NPC.velocity.X < 0f && vector172.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num1456;
					}
				}
				else if (NPC.velocity.X > vector172.X)
				{
					NPC.velocity.X = NPC.velocity.X - num1456;
					if (NPC.velocity.X > 0f && vector172.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num1456;
					}
				}
				if (NPC.velocity.Y < vector172.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y + num1456;
					if (NPC.velocity.Y < 0f && vector172.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1456;
					}
				}
				else if (NPC.velocity.Y > vector172.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y - num1456;
					if (NPC.velocity.Y > 0f && vector172.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1456;
					}
				}
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				if (NPC.ai[2] % (float)num1455 == 0f) //fire flare bombs from mouth
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
					if (Main.netMode != 1)
					{
						Vector2 vector = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						if (NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare").Type) < flareCount)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, Mod.Find<ModNPC>("DetonatingFlare").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						int damage = expertMode ? 75 : 90; //700
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, 0f, 0f, Mod.Find<ModProjectile>("FlareBomb").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				int num1476 = Math.Sign(player.Center.X - vectorCenter.X);
				if (num1476 != 0)
				{
					NPC.direction = num1476;
					if (NPC.spriteDirection != -NPC.direction)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.spriteDirection = -NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1454)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f) //Fire small flares
			{
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30))
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, (float)(-(float)NPC.direction * 2), 8f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 0f, 0f); //changed
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1457)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f) //enter phase 2
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1458 - 60))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1458)
				{
					NPC.ai[0] = 6f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 5f)
			{
				int num1473 = 14;
				for (int num1474 = 0; num1474 < num1473; num1474++)
				{
					Vector2 vector171 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + vectorCenter;
					Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1475 = Dust.NewDust(vector171 + value18, 0, 0, 244, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1475].noGravity = true;
					Main.dust[num1475].noLight = true;
					Main.dust[num1475].velocity /= 4f;
					Main.dust[num1475].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime2)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			#endregion
			#region Phase2
			else if (NPC.ai[0] == 6f && !player.dead) //phase 2
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
				}
				Vector2 value20 = player.Center + new Vector2(NPC.ai[1], -200f) - vectorCenter;
				Vector2 vector175 = Vector2.Normalize(value20 - NPC.velocity) * scaleFactor;
				if (NPC.velocity.X < vector175.X)
				{
					NPC.velocity.X = NPC.velocity.X + npcVelocity;
					if (NPC.velocity.X < 0f && vector175.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + npcVelocity;
					}
				}
				else if (NPC.velocity.X > vector175.X)
				{
					NPC.velocity.X = NPC.velocity.X - npcVelocity;
					if (NPC.velocity.X > 0f && vector175.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - npcVelocity;
					}
				}
				if (NPC.velocity.Y < vector175.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
					if (NPC.velocity.Y < 0f && vector175.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
					}
				}
				else if (NPC.velocity.Y > vector175.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
					if (NPC.velocity.Y > 0f && vector175.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
					}
				}
				int num1477 = Math.Sign(player.Center.X - vectorCenter.X);
				if (num1477 != 0)
				{
					if (NPC.ai[2] == 0f && num1477 != NPC.direction)
					{
						NPC.rotation = 3.14159274f;
					}
					NPC.direction = num1477;
					if (NPC.spriteDirection != -NPC.direction)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.spriteDirection = -NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate)
				{
					int aiState = 0;
					switch ((int)NPC.ai[3]) //0 2 4 6 8 1 3 5 7 9 repeat
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
							aiState = 1;
							break;
						case 6:
							aiState = 5;
							break;
						case 7:
							aiState = 6;
							break;
						case 8:
							NPC.ai[3] = 1f;
							aiState = 2;
							break;
						case 9:
							NPC.ai[3] = 0f;
							aiState = 3;
							break;
					}
					if (phase3Check)
					{
						aiState = 4;
					}
					if (aiState == 1)
					{
						NPC.ai[0] = 7f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					else if (aiState == 2)
					{
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
						NPC.ai[0] = 8f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 3)
					{
						NPC.ai[0] = 9f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 4)
					{
						NPC.ai[0] = 10f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 5)
					{
						NPC.ai[0] = 11f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed2;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					else if (aiState == 6)
					{
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
						NPC.ai[0] = 12f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 7f) //charge
			{
				int num1479 = 7;
				for (int num1480 = 0; num1480 < num1479; num1480++)
				{
					Vector2 vector176 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime)
				{
					NPC.ai[0] = 6f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 8f)
			{
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				if (NPC.ai[2] % (float)num1462 == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
					if (Main.netMode != 1)
					{
						Vector2 vector = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						if (NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare2").Type) < flareCount)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, Mod.Find<ModNPC>("DetonatingFlare2").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, (float)Main.rand.Next(-400, 401) * 0.13f, (float)Main.rand.Next(-30, 31) * 0.13f, Mod.Find<ModProjectile>("FlareDust").Type, 0, 0f, Main.myPlayer, 0f, 0f); //changed
					}
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461)
				{
					NPC.ai[0] = 6f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 9f)
			{
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30))
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1457)
				{
					NPC.ai[0] = 6f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 10f) //start phase 3
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1459 - 60))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1459)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 11f)
			{
				int num1479 = 14;
				for (int num1480 = 0; num1480 < num1479; num1480++)
				{
					Vector2 vector176 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime2)
				{
					NPC.ai[0] = 6f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 12f)
			{
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				if (NPC.ai[2] % (float)num1462 == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
					if (Main.netMode != 1)
					{
						int damage = expertMode ? 75 : 90; //700
						Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						float speed = 0.01f;
						Vector2 vectorShoot = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f + 30f);
						float playerX = player.position.X + (float)player.width * 0.5f - vectorShoot.X;
						float playerY = player.position.Y - vectorShoot.Y;
						float playerXY = (float)Math.Sqrt((double)(playerX * playerX + playerY * playerY));
						playerXY = speed / playerXY;
						playerX *= playerXY;
						playerY *= playerXY;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector173.X + xPos, (int)vector173.Y - 15, playerX, playerY, Mod.Find<ModProjectile>("FlareDust2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461)
				{
					NPC.ai[0] = 6f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			#endregion
			#region Phase3
			else if (NPC.ai[0] == 13f && !player.dead)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
				}
				Vector2 value20 = player.Center + new Vector2(NPC.ai[1], -200f) - vectorCenter;
				Vector2 vector175 = Vector2.Normalize(value20 - NPC.velocity) * scaleFactor;
				if (NPC.velocity.X < vector175.X)
				{
					NPC.velocity.X = NPC.velocity.X + npcVelocity;
					if (NPC.velocity.X < 0f && vector175.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + npcVelocity;
					}
				}
				else if (NPC.velocity.X > vector175.X)
				{
					NPC.velocity.X = NPC.velocity.X - npcVelocity;
					if (NPC.velocity.X > 0f && vector175.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - npcVelocity;
					}
				}
				if (NPC.velocity.Y < vector175.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
					if (NPC.velocity.Y < 0f && vector175.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + npcVelocity;
					}
				}
				else if (NPC.velocity.Y > vector175.Y)
				{
					NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
					if (NPC.velocity.Y > 0f && vector175.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - npcVelocity;
					}
				}
				int num1477 = Math.Sign(player.Center.X - vectorCenter.X);
				if (num1477 != 0)
				{
					if (NPC.ai[2] == 0f && num1477 != NPC.direction)
					{
						NPC.rotation = 3.14159274f;
					}
					NPC.direction = num1477;
					if (NPC.spriteDirection != -NPC.direction)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.spriteDirection = -NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate)
				{
					int aiState = 0;
					switch ((int)NPC.ai[3]) //0 2 4 6 8 9 1 3 5 7 10 repeat
					{
						case 0:
						case 1:
						case 2:
						case 3:
							aiState = ((CalamityWorldPreTrailer.death && !CalamityWorldPreTrailer.bossRushActive) ? 5 : 1); //normal charges
							break;
						case 4:
						case 5:
						case 6:
							aiState = 5; //fast charges
							break;
						case 7:
							aiState = 3; //big tornado
							break;
						case 8:
							aiState = 6; //slow flare bombs
							break;
						case 9:
							NPC.ai[3] = 1f;
							aiState = 7; //small tornado
							break;
						case 10:
							NPC.ai[3] = 0f;
							aiState = 2; //flare circle
							break;
					}
					if (phase4Check)
					{
						aiState = 4;
					}
					if (aiState == 1)
					{
						NPC.ai[0] = 14f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					else if (aiState == 2)
					{
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
						NPC.ai[0] = 15f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 3)
					{
						NPC.ai[0] = 16f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 4)
					{
						NPC.ai[0] = 17f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 5)
					{
						NPC.ai[0] = 18f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed2;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					else if (aiState == 6)
					{
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0)
						{
							NPC.direction = num1477;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
						NPC.ai[0] = 19f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 7)
					{
						NPC.ai[0] = 20f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 14f) //charge
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				int num1479 = 7;
				for (int num1480 = 0; num1480 < num1479; num1480++)
				{
					Vector2 vector176 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 15f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				if (NPC.ai[2] % (float)num1462 == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
					if (Main.netMode != 1)
					{
						Vector2 vector = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						if (NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare2").Type) < flareCount && NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare").Type) < flareCount)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, (Main.rand.Next(2) == 0 ? Mod.Find<ModNPC>("DetonatingFlare").Type : Mod.Find<ModNPC>("DetonatingFlare2").Type), 0, 0f, 0f, 0f, 0f, 255);
						}
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, (float)Main.rand.Next(-401, 401) * 0.13f, (float)Main.rand.Next(-31, 31) * 0.13f, Mod.Find<ModProjectile>("FlareDust").Type, 0, 0f, Main.myPlayer, 0f, 0f); //changed
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector.X + xPos, (int)vector.Y - 15, (float)Main.rand.Next(-31, 31) * 0.13f, (float)Main.rand.Next(-151, 151) * 0.13f, Mod.Find<ModProjectile>("FlareDust").Type, 0, 0f, Main.myPlayer, 0f, 0f); //changed
					}
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 16f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30))
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1457)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 3f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 17f)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.ai[2] < (float)(num1459 - 90))
				{
					bool colliding = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
					if (colliding)
					{
						NPC.alpha += 15;
					}
					else
					{
						NPC.alpha -= 15;
					}
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
					if (NPC.alpha > 150)
					{
						NPC.alpha = 150;
					}
				}
				else if (NPC.alpha < 255)
				{
					NPC.alpha += 4;
					if (NPC.alpha > 255)
					{
						NPC.alpha = 255;
					}
				}
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1459 - 60))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1459)
				{
					NPC.ai[0] = 21f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 18f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				int num1479 = 14;
				for (int num1480 = 0; num1480 < num1479; num1480++)
				{
					Vector2 vector176 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime2)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 19f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				if (NPC.ai[2] % (float)num1462 == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
					if (Main.netMode != 1)
					{
						int damage = expertMode ? 75 : 90; //700
						Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						float speed = 0.01f;
						Vector2 vectorShoot = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f + 30f);
						float playerX = player.position.X + (float)player.width * 0.5f - vectorShoot.X;
						float playerY = player.position.Y - vectorShoot.Y;
						float playerXY = (float)Math.Sqrt((double)(playerX * playerX + playerY * playerY));
						playerXY = speed / playerXY;
						playerX *= playerXY;
						playerY *= playerXY;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector173.X + xPos, (int)vector173.Y - 15, playerX, playerY, Mod.Find<ModProjectile>("FlareDust2").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 20f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				if (CalamityWorldPreTrailer.death && !CalamityWorldPreTrailer.bossRushActive)
				{
					if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30))
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					}
				}
				else
				{
					if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30))
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1457)
				{
					NPC.ai[0] = 13f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			#endregion
			#region Phase4
			else if (NPC.ai[0] == 21f && !player.dead)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = false;
				if (NPC.alpha < 255)
				{
					NPC.alpha += 25;
					if (NPC.alpha > 255)
					{
						NPC.alpha = 255;
					}
				}
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] = (float)(360 * Math.Sign((vectorCenter - player.Center).X));
				}
				Vector2 value7 = player.Center + new Vector2(NPC.ai[1], teleportLocation) - vectorCenter; //teleport distance
				Vector2 desiredVelocity = Vector2.Normalize(value7 - NPC.velocity) * scaleFactor;
				NPC.SimpleFlyMovement(desiredVelocity, npcVelocity);
				int num32 = Math.Sign(player.Center.X - vectorCenter.X);
				if (num32 != 0)
				{
					if (NPC.ai[2] == 0f && num32 != NPC.direction)
					{
						NPC.rotation = 3.14159274f;
					}
					NPC.direction = num32;
					if (NPC.spriteDirection != -NPC.direction)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.spriteDirection = -NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate)
				{
					int aiState = 0;
					switch ((int)NPC.ai[3])
					{
						case 0: //skip 1
						case 2:
						case 3: //skip 4
						case 5:
						case 6:
						case 7: //skip 8
						case 9:
						case 10:
						case 11:
						case 12: //skip 13
						case 14:
						case 15:
						case 16:
						case 17:
						case 18: //skip 19
							aiState = 1;
							break;
						case 1: //+3
						case 4: //+4
						case 8: //+5
						case 13: //+6
						case 19:
							aiState = 2;
							break;
					}
					if (phase5Check)
					{
						aiState = 4;
					}
					if (aiState == 1)
					{
						NPC.ai[0] = 22f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num32 != 0)
						{
							NPC.direction = num32;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction;
						}
					}
					else if (aiState == 2)
					{
						NPC.ai[0] = 23f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 3)
					{
						NPC.ai[0] = 24f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (aiState == 4)
					{
						NPC.ai[0] = 25f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 22f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				NPC.alpha -= 25;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				int num34 = 7;
				for (int m = 0; m < num34; m++)
				{
					Vector2 vector11 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector11 = vector11.RotatedBy((double)(m - (num34 / 2 - 1)) * 3.1415926535897931 / (double)((float)num34), default(Vector2)) + vectorCenter;
					Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num35 = Dust.NewDust(vector11 + value8, 0, 0, 244, value8.X * 2f, value8.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num35].noGravity = true;
					Main.dust[num35].noLight = true;
					Main.dust[num35].velocity /= 4f;
					Main.dust[num35].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime)
				{
					NPC.ai[0] = 21f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 23f)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.alpha < 255)
				{
					NPC.alpha += 17;
					if (NPC.alpha > 255)
					{
						NPC.alpha = 255;
					}
				}
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1460 / 2))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1460 / 2))
				{
					if (NPC.ai[1] == 0f)
					{
						NPC.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
					}
					Vector2 center = player.Center + new Vector2(-NPC.ai[1], teleportLocation); //teleport distance
					vectorCenter = (NPC.Center = center);
					int num36 = Math.Sign(player.Center.X - vectorCenter.X);
					if (num36 != 0)
					{
						if (NPC.ai[2] == 0f && num36 != NPC.direction)
						{
							NPC.rotation += 3.14159274f;
						}
						NPC.direction = num36;
						if (NPC.spriteDirection != -NPC.direction)
						{
							NPC.rotation += 3.14159274f;
						}
						NPC.spriteDirection = -NPC.direction;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1460)
				{
					NPC.ai[0] = 21f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					if (NPC.ai[3] == 5f && Main.netMode != 1)
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					}
					if (NPC.ai[3] >= 20f) //14
					{
						NPC.ai[3] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 24f)
			{
				NPC.dontTakeDamage = phase5Check;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461)
				{
					NPC.ai[0] = 21f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 25f) //start phase 5
			{
				NPC.alpha = 0;
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1459 - 60))
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1459)
				{
					NPC.localAI[2] = 1f;
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			#endregion
		}

		#region AI2
		public void Yharon_AI2()
		{
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = Main.expertMode;
			bool phase2 = (double)NPC.life <= (double)NPC.lifeMax * 0.75;
			bool phase3 = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			bool phase4 = (double)NPC.life <= (double)NPC.lifeMax * 0.05;
			if (CalamityWorldPreTrailer.death)
			{
				phase2 = (double)NPC.life <= (double)NPC.lifeMax * 0.95;
				phase3 = (double)NPC.life <= (double)NPC.lifeMax * 0.7;
				phase4 = (double)NPC.life <= (double)NPC.lifeMax * 0.15;
			}
			else if (revenge)
			{
				phase2 = (double)NPC.life <= (double)NPC.lifeMax * 0.85;
				phase3 = (double)NPC.life <= (double)NPC.lifeMax * 0.6;
				phase4 = (double)NPC.life <= (double)NPC.lifeMax * 0.1;
			}
			float teleportLocation = 0f;
			int teleChoice = Main.rand.Next(2);
			if (teleChoice == 0)
			{
				teleportLocation = revenge ? 600f : 700f;
			}
			else
			{
				teleportLocation = revenge ? -600f : -700f;
			}
			NPC.defense = 100;
			if (NPC.ai[0] != 8f)
			{
				NPC.alpha -= 25;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
			}
			if (!moveCloser)
			{
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/DragonGod");
				moveCloser = true;
				string key = "The air is getting warmer around you.";
				Color messageColor = Color.Orange;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
			}
			if (NPC.localAI[3] < 900f)
			{
				phase2 = phase3 = phase4 = false;
				NPC.localAI[3] += 1f;
				int heal = 5; //900 / 5 = 180
				healCounter += 1;
				if (healCounter >= heal)
				{
					healCounter = 0;
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
			else
			{
				NPC.dontTakeDamage = NPC.ai[0] == 9f;
				NPC.chaseable = NPC.ai[0] < 8f;
			}
			NPCUtils.TargetClosestBetsy(NPC, false, null);
			NPCAimedTarget targetData = NPC.GetTargetData(true);
			if (!targetData.Hitbox.Intersects(safeBox))
			{
				protectionBoost = true;
				NPC.damage = NPC.defDamage * 5;
				if (NPC.timeLeft > 150)
				{
					NPC.timeLeft = 150;
				}
			}
			else
			{
				NPC.damage = expertMode ? 528 : 330;
				if (phase4)
				{
					NPC.damage = (int)((double)NPC.damage * 1.25);
				}
				protectionBoost = false;
				if (NPC.timeLeft < 3600)
				{
					NPC.timeLeft = 3600;
				}
			}
			int num = -1;
			float num2 = 1f;
			int num4 = expertMode ? 106 : 125;
			if (phase4)
			{
				num4 = (int)((double)num4 * 1.25);
			}
			float num6 = revenge ? 0.6f : 0.55f;
			float scaleFactor = revenge ? 10f : 9f;
			float chargeTime = 34f;
			float chargeTime2 = 30f;
			float chargeSpeed = revenge ? 26f : 25f;
			float chargeSpeed2 = revenge ? 40f : 38f;
			float num11 = 40f;
			float num12 = 80f;
			float num13 = num11 + num12;
			float num15 = 60f;
			float scaleFactor3 = 14f;
			float scaleFactor4 = revenge ? 16f : 15f; //12
			int num16 = 10;
			int num17 = 6 * num16;
			float num18 = 60f;
			float num19 = num15 + (float)num17 + num18;
			float num20 = 60f;
			float num21 = 1f;
			float num22 = 6.28318548f * (num21 / num20);
			float scaleFactor5 = revenge ? 38f : 36.5f; //32
			if (CalamityWorldPreTrailer.death)
			{
				scaleFactor = 10.5f;
				chargeSpeed = 27f;
				chargeSpeed2 = 41f;
				scaleFactor4 = 16.5f;
				scaleFactor5 = 39f;
			}
			if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
			{
				num6 = 0.65f;
				scaleFactor = 11f;
				chargeSpeed = 32f;
				chargeSpeed2 = 45f;
				scaleFactor4 = 18f;
				scaleFactor5 = 40f;
			}
			float num25 = 20f;
			float arg_F9_0 = NPC.ai[0];
			float num26;
			if (NPC.ai[0] == 0f)
			{
				float[] expr_115_cp_0 = NPC.ai;
				int expr_115_cp_1 = 1;
				num26 = expr_115_cp_0[expr_115_cp_1] + 1f;
				expr_115_cp_0[expr_115_cp_1] = num26;
				if (num26 >= 10f)
				{
					NPC.ai[1] = 0f;
					NPC.ai[0] = 1f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				if (NPC.ai[2] == 0f)
				{
					NPC.ai[2] = (float)((NPC.Center.X < targetData.Center.X) ? 1 : -1);
				}
				Vector2 destination = targetData.Center + new Vector2(-NPC.ai[2] * 300f, -200f);
				Vector2 desiredVelocity = NPC.DirectionTo(destination) * scaleFactor;
				NPC.SimpleFlyMovement(desiredVelocity, num6);
				int num27 = (NPC.Center.X < targetData.Center.X) ? 1 : -1;
				NPC.direction = (NPC.spriteDirection = num27);
				float[] expr_225_cp_0 = NPC.ai;
				int expr_225_cp_1 = 1;
				num26 = expr_225_cp_0[expr_225_cp_1] + 1f;
				expr_225_cp_0[expr_225_cp_1] = num26;
				if (num26 >= 30f)
				{
					int num28 = 1;
					if (phase4)
					{
						switch ((int)NPC.ai[3])
						{
							case 0:
								num28 = 8; //teleport
								break;
							case 1:
							case 2:
								num28 = 7; //fast charge
								break;
							case 3:
								num28 = 5; //fire circle + tornado (only once) + fireballs
								break;
						}
					}
					else if (phase3)
					{
						switch ((int)NPC.ai[3])
						{
							case 0:
								num28 = 6; //tornado
								break;
							case 1:
								num28 = 7; //fast charge
								break;
							case 2:
								num28 = 8; //teleport
								break;
							case 3:
								num28 = 7; //fast charge
								break;
							case 4:
								num28 = 5; //fire circle
								break;
							case 5:
								num28 = 4; //fireballs
								break;
							case 6:
								num28 = 7; //fast charge
								break;
							case 7:
								num28 = 8; //teleport
								break;
							case 8:
								num28 = 7; //fast charge
								break;
							case 9:
								num28 = 3; //fireballs
								break;
							case 10:
								num28 = 6; //tornado
								break;
							case 11:
								num28 = 7; //fast charge
								break;
							case 12:
								num28 = 8; //teleport
								break;
							case 13:
								num28 = 7; //fast charge
								break;
							case 14:
								num28 = 5; //fire circle
								break;
							case 15:
								num28 = 4; //fireballs
								break;
						}
					}
					else if (phase2)
					{
						switch ((int)NPC.ai[3])
						{
							case 0:
								num28 = 6; //tornado
								break;
							case 1:
								num28 = 7; //fast charge
								break;
							case 2:
								num28 = 2; //charge
								break;
							case 3:
								num28 = 5; //fire circle
								break;
							case 4:
								num28 = 4; //fireballs
								break;
							case 5:
								num28 = 7; //fast charge
								break;
							case 6:
								num28 = 2; //charge
								break;
							case 7:
								num28 = 3; //fireballs
								break;
							case 8:
								num28 = 7; //fast charge
								break;
							case 9:
								num28 = 2; //charge
								break;
							case 10:
								num28 = 5; //fire circle
								break;
						}
					}
					else
					{
						switch ((int)NPC.ai[3])
						{
							case 0:
								num28 = 6; //tornado
								break;
							case 1:
							case 2:
								num28 = 2; //charge
								break;
							case 3:
								num28 = 3; //fireballs
								break;
							case 4:
							case 5:
								num28 = 2; //charge
								break;
							case 6:
								num28 = 4; //fireballs
								break;
							case 7:
							case 8:
								num28 = 2; //charge
								break;
							case 9:
								num28 = 5; //fire circle
								break;
						}
					}
					NPC.ai[0] = (float)num28;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					switch (secondPhasePhase)
					{
						case 1:
							if (phase2)
							{
								secondPhasePhase = 2;
								NPC.ai[0] = 9f;
								NPC.ai[1] = 0f;
								NPC.ai[2] = 0f;
								NPC.ai[3] = 0f;
							}
							break;
						case 2:
							if (phase3)
							{
								secondPhasePhase = 3;
								NPC.ai[0] = 9f;
								NPC.ai[1] = 0f;
								NPC.ai[2] = 0f;
								NPC.ai[3] = 0f;
							}
							break;
						case 3:
							if (phase4)
							{
								secondPhasePhase = 4;
								NPC.ai[0] = 9f;
								NPC.ai[1] = 0f;
								NPC.ai[2] = 0f;
								NPC.ai[3] = 0f;
							}
							break;
					}
					NPC.netUpdate = true;
					float aiLimit = 10f;
					if (phase4)
					{
						aiLimit = 4f;
					}
					else if (phase3)
					{
						aiLimit = 16f;
					}
					else if (phase2)
					{
						aiLimit = 11f;
					}
					if (NPC.ai[3] >= aiLimit)
					{
						NPC.ai[3] = 0f;
					}
					switch (num28)
					{
						case 2: //charge
							{
								Vector2 vector = NPC.DirectionTo(targetData.Center);
								NPC.spriteDirection = ((vector.X > 0f) ? 1 : -1);
								NPC.rotation = vector.ToRotation();
								if (NPC.spriteDirection == -1)
								{
									NPC.rotation += 3.14159274f;
								}
								NPC.velocity = vector * chargeSpeed;
								break;
							}
						case 3: //fireballs
							{
								Vector2 vector2 = new Vector2((float)((targetData.Center.X > NPC.Center.X) ? 1 : -1), 0f);
								NPC.spriteDirection = ((vector2.X > 0f) ? 1 : -1);
								NPC.velocity = vector2 * -2f;
								break;
							}
						case 5: //spin move
							{
								Vector2 vector3 = NPC.DirectionTo(targetData.Center);
								NPC.spriteDirection = ((vector3.X > 0f) ? 1 : -1);
								NPC.rotation = vector3.ToRotation();
								if (NPC.spriteDirection == -1)
								{
									NPC.rotation += 3.14159274f;
								}
								NPC.velocity = vector3 * scaleFactor5;
								break;
							}
						case 7: //fast charge
							{
								Vector2 vector = NPC.DirectionTo(targetData.Center);
								NPC.spriteDirection = ((vector.X > 0f) ? 1 : -1);
								NPC.rotation = vector.ToRotation();
								if (NPC.spriteDirection == -1)
								{
									NPC.rotation += 3.14159274f;
								}
								NPC.velocity = vector * chargeSpeed2;
								break;
							}
					}
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				if (NPC.ai[1] == 1f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				int num1473 = 7;
				for (int num1474 = 0; num1474 < num1473; num1474++)
				{
					Vector2 vector171 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + NPC.Center;
					Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1475 = Dust.NewDust(vector171 + value18, 0, 0, 244, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num1475].noGravity = true;
					Main.dust[num1475].noLight = true;
					Main.dust[num1475].velocity /= 4f;
					Main.dust[num1475].velocity -= NPC.velocity;
				}
				float[] expr_498_cp_0 = NPC.ai;
				int expr_498_cp_1 = 1;
				num26 = expr_498_cp_0[expr_498_cp_1] + 1f;
				expr_498_cp_0[expr_498_cp_1] = num26;
				if (num26 >= chargeTime)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
				}
			}
			else if (NPC.ai[0] == 3f) //fireball spit
			{
				NPC.ai[1] += 1f;
				int num29 = (NPC.Center.X < targetData.Center.X) ? 1 : -1;
				NPC.ai[2] = (float)num29;
				if (NPC.ai[1] < num11)
				{
					Vector2 vector4 = targetData.Center + new Vector2((float)num29 * -600f, -250f);
					Vector2 value = NPC.DirectionTo(vector4) * 12f;
					if (NPC.Distance(vector4) < 12f)
					{
						NPC.Center = vector4;
					}
					else
					{
						NPC.position += value;
					}
					if (Vector2.Distance(vector4, NPC.Center) < 16f)
					{
						NPC.ai[1] = num11 - 1f;
					}
					num2 = 1.5f;
				}
				if (NPC.ai[1] == num11)
				{
					int num30 = (targetData.Center.X > NPC.Center.X) ? 1 : -1;
					NPC.velocity = new Vector2((float)num30, 0f) * 22f; //10f
					NPC.direction = (NPC.spriteDirection = num30);
				}
				if (NPC.ai[1] >= num11)
				{
					if (NPC.ai[1] % 8 == 0 && Main.netMode != 1)
					{
						float num33 = 30f;
						Vector2 position = NPC.Center + new Vector2((110f + num33) * (float)NPC.direction, -20f).RotatedBy((double)NPC.rotation,
							default(Vector2));
						float speed = 0.01f;
						Vector2 vectorShoot = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f + 30f);
						float playerX = targetData.Center.X - vectorShoot.X;
						float playerY = targetData.Center.Y - vectorShoot.Y;
						float playerXY = (float)Math.Sqrt((double)(playerX * playerX + playerY * playerY));
						playerXY = speed / playerXY;
						playerX *= playerXY;
						playerY *= playerXY;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), position.X, position.Y, playerX, playerY, Mod.Find<ModProjectile>("FlareDust2").Type, num4, 0f, Main.myPlayer, 1f, 0f);
					}
					num2 = 1.5f;
					if (Math.Abs(targetData.Center.X - NPC.Center.X) > 550f && Math.Abs(NPC.velocity.X) < 20f)
					{
						NPC.velocity.X = NPC.velocity.X + (float)Math.Sign(NPC.velocity.X) * 0.5f;
					}
				}
				if (NPC.ai[1] >= num13)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
				}
			}
			else if (NPC.ai[0] == 4f) //fireball spit
			{
				int num31 = (NPC.Center.X < targetData.Center.X) ? 1 : -1;
				NPC.ai[2] = (float)num31;
				if (NPC.ai[1] < num15)
				{
					Vector2 vector5 = targetData.Center + new Vector2((float)num31 * -1500f, -350f);
					Vector2 value2 = NPC.DirectionTo(vector5) * scaleFactor3;
					NPC.velocity = Vector2.Lerp(NPC.velocity, value2, 0.0333333351f);
					int num32 = (NPC.Center.X < targetData.Center.X) ? 1 : -1;
					NPC.direction = (NPC.spriteDirection = num32);
					if (Vector2.Distance(vector5, NPC.Center) < 16f)
					{
						NPC.ai[1] = num15 - 1f;
					}
					num2 = 1.5f;
				}
				else if (NPC.ai[1] == num15)
				{
					Vector2 vector6 = NPC.DirectionTo(targetData.Center);
					vector6.Y *= 0.25f;
					vector6 = vector6.SafeNormalize(Vector2.UnitX * (float)NPC.direction);
					NPC.spriteDirection = ((vector6.X > 0f) ? 1 : -1);
					NPC.rotation = vector6.ToRotation();
					if (NPC.spriteDirection == -1)
					{
						NPC.rotation += 3.14159274f;
					}
					NPC.velocity = vector6 * scaleFactor4;
				}
				else
				{
					NPC.position.X = NPC.position.X + NPC.DirectionTo(targetData.Center).X * 7f;
					NPC.position.Y = NPC.position.Y + NPC.DirectionTo(targetData.Center + new Vector2(0f, -400f)).Y * 6f;
					if (NPC.ai[1] <= num19 - num18)
					{
						num2 = 1.5f;
					}
					float num33 = 30f;
					Vector2 position = NPC.Center + new Vector2((110f + num33) * (float)NPC.direction, -20f).RotatedBy((double)NPC.rotation,
						default(Vector2));
					int num34 = (int)(NPC.ai[1] - num15 + 1f);
					if (num34 <= num17 && num34 % num16 == 0 && Main.netMode != 1)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), position, NPC.velocity, Mod.Find<ModProjectile>("YharonFireball").Type, num4, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (NPC.ai[1] > num19 - num18)
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
				}
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= num19)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
				}
			}
			else if (NPC.ai[0] == 5f) //spin 2 win
			{
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num22 * (float)NPC.direction), default(Vector2));
				NPC.position.Y = NPC.position.Y - 0.1f;
				NPC.position += NPC.DirectionTo(targetData.Center) * 10f;
				NPC.rotation -= num22 * (float)NPC.direction;
				num2 *= 0.7f;
				if (NPC.ai[1] == 1f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				float[] expr_B0F_cp_0 = NPC.ai;
				int expr_B0F_cp_1 = 1;
				num26 = expr_B0F_cp_0[expr_B0F_cp_1] + 1f;
				expr_B0F_cp_0[expr_B0F_cp_1] = num26;
				if (Main.netMode != 1)
				{
					if (NPC.ai[1] % 12 == 0)
					{
						float num33 = 30f;
						Vector2 position = NPC.Center + new Vector2((110f + num33) * (float)NPC.direction, -20f).RotatedBy((double)NPC.rotation,
							default(Vector2));
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), position, NPC.velocity, Mod.Find<ModProjectile>("YharonFireball").Type, num4, 0f, Main.myPlayer, 0f, 0f);
						if (phase4)
						{
							float speed = 0.01f;
							Vector2 vectorShoot = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f + 30f);
							float playerX = targetData.Center.X - vectorShoot.X;
							float playerY = targetData.Center.Y - vectorShoot.Y;
							float playerXY = (float)Math.Sqrt((double)(playerX * playerX + playerY * playerY));
							playerXY = speed / playerXY;
							playerX *= playerXY;
							playerY *= playerXY;
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), position.X, position.Y, playerX, playerY, Mod.Find<ModProjectile>("FlareDust2").Type, num4, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					if (NPC.ai[1] == 45f && phase4 && useTornado)
					{
						useTornado = false;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare2").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					}
				}
				if (num26 >= num20)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.velocity /= 2f;
				}
			}
			else if (NPC.ai[0] == 6f) //flare spawn
			{
				if (NPC.ai[1] == 0f)
				{
					Vector2 destination2 = targetData.Center + new Vector2(0f, -200f);
					Vector2 desiredVelocity2 = NPC.DirectionTo(destination2) * scaleFactor * 2f;
					NPC.SimpleFlyMovement(desiredVelocity2, num6 * 2f);
					int num35 = (NPC.Center.X < targetData.Center.X) ? 1 : -1;
					NPC.direction = (NPC.spriteDirection = num35);
					NPC.ai[2] += 1f;
					if (NPC.Distance(targetData.Center) < 1000f || NPC.ai[2] >= 180f) //450f
					{
						NPC.ai[1] = 1f;
						NPC.netUpdate = true;
					}
				}
				else
				{
					if (NPC.ai[1] == 1f)
					{
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
					}
					if (NPC.ai[1] < num25)
					{
						NPC.velocity *= 0.95f;
					}
					else
					{
						NPC.velocity *= 0.98f;
					}
					if (NPC.ai[1] == num25)
					{
						if (NPC.velocity.Y > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y / 3f;
						}
						NPC.velocity.Y = NPC.velocity.Y - 3f;
					}
					num2 *= 0.85f;
					bool flag3 = NPC.ai[1] == 20f || NPC.ai[1] == 45f || NPC.ai[1] == 70f;
					int flareCount = NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare").Type) + NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare2").Type);
					if (flareCount > 5)
					{
						flag3 = false;
					}
					if (flag3 && Main.netMode != 1)
					{
						Vector2 vector7 = NPC.Center + (6.28318548f * Main.rand.NextFloat()).ToRotationVector2() * new Vector2(2f, 1f) * 100f * (0.6f + Main.rand.NextFloat() * 0.4f);
						if (Vector2.Distance(vector7, targetData.Center) > 100f)
						{
							Point point2 = vector7.ToPoint();
							NPC.NewNPC(NPC.GetSource_FromThis(null), point2.X, point2.Y, Mod.Find<ModNPC>("DetonatingFlare").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
							NPC.NewNPC(NPC.GetSource_FromThis(null), point2.X, point2.Y, Mod.Find<ModNPC>("DetonatingFlare2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
						}
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X + (Main.rand.Next(2) == 0 ? 100 : -100), (int)NPC.Center.Y - 100, Mod.Find<ModNPC>("DetonatingFlare").Type, 0, 0f, 0f, 0f, 0f, 255);
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X + (Main.rand.Next(2) == 0 ? 100 : -100), (int)NPC.Center.Y - 100, Mod.Find<ModNPC>("DetonatingFlare2").Type, 0, 0f, 0f, 0f, 0f, 255);
					}
					NPC.ai[1] += 1f;
				}
				if (NPC.ai[1] >= 90f)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare2").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					Boom(600, num4);
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
				}
			}
			else if (NPC.ai[0] == 7f) //speedee chargee
			{
				if (NPC.ai[1] == 1f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				int num1473 = 7;
				for (int num1474 = 0; num1474 < num1473; num1474++)
				{
					Vector2 vector171 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + NPC.Center;
					Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1475 = Dust.NewDust(vector171 + value18, 0, 0, 244, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num1475].noGravity = true;
					Main.dust[num1475].noLight = true;
					Main.dust[num1475].velocity /= 4f;
					Main.dust[num1475].velocity -= NPC.velocity;
				}
				float[] expr_498_cp_0 = NPC.ai;
				int expr_498_cp_1 = 1;
				num26 = expr_498_cp_0[expr_498_cp_1] + 1f;
				expr_498_cp_0[expr_498_cp_1] = num26;
				if (num26 >= chargeTime2)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
				}
			}
			else if (NPC.ai[0] == 8f) //teleport
			{
				Vector2 npcCenter = NPC.Center;
				if (NPC.alpha < 255)
				{
					NPC.alpha += 17;
					if (NPC.alpha > 255)
					{
						NPC.alpha = 255;
					}
				}
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == 15f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort"), NPC.position);
				}
				if (Main.netMode != 1 && NPC.ai[2] == 15f)
				{
					if (NPC.ai[1] == 0f)
					{
						NPC.ai[1] = (float)(300 * Math.Sign((npcCenter - targetData.Center).X));
					}
					Vector2 center = targetData.Center + new Vector2(-NPC.ai[1], teleportLocation); //teleport distance
					npcCenter = (NPC.Center = center);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= 25f)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 9f) //enter next phase
			{
				NPC.chaseable = false;
				NPC.velocity *= 0.95f;
				Vector2 vector = NPC.DirectionTo(targetData.Center);
				NPC.spriteDirection = ((vector.X > 0f) ? 1 : -1);
				NPC.rotation = vector.ToRotation();
				if (NPC.spriteDirection == -1)
				{
					NPC.rotation += 3.14159274f;
				}
				if (NPC.ai[2] == 120f)
				{
					if (phase4)
					{
						int proj;
						for (int x = 0; x < 1000; x = proj + 1)
						{
							Projectile projectile = Main.projectile[x];
							if (projectile.active)
							{
								if (projectile.type == Mod.Find<ModProjectile>("Infernado2").Type)
								{
									if (projectile.ai[0] < 10f)
									{
										projectile.ai[0] = 10f;
										projectile.netUpdate = true;
									}
									if (projectile.timeLeft > 5)
										projectile.timeLeft = (int)(5f * projectile.ai[1]);
								}
								else if (projectile.type == Mod.Find<ModProjectile>("BigFlare2").Type)
									projectile.active = false;
							}
							proj = x;
						}
					}
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoar"), NPC.position);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= 180f)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
			}
			NPC.localAI[0] += num2;
			if (NPC.localAI[0] >= 36f)
			{
				NPC.localAI[0] = 0f;
			}
			if (num != -1)
			{
				NPC.localAI[0] = (float)(num * 4);
			}
			float[] expr_11FC_cp_0 = NPC.localAI;
			int expr_11FC_cp_1 = 1;
			num26 = expr_11FC_cp_0[expr_11FC_cp_1] + 1f;
			expr_11FC_cp_0[expr_11FC_cp_1] = num26;
			if (num26 >= 60f)
			{
				NPC.localAI[1] = 0f;
			}
			float num42 = NPC.DirectionTo(targetData.Center).ToRotation();
			float num43 = 0.04f;
			switch ((int)NPC.ai[0])
			{
				case 2:
				case 5:
				case 7:
				case 8:
				case 9:
					num43 = 0f;
					break;
				case 3:
					num43 = 0.01f;
					num42 = 0f;
					if (NPC.spriteDirection == -1)
					{
						num42 -= 3.14159274f;
					}
					if (NPC.ai[1] >= num11)
					{
						num42 += (float)NPC.spriteDirection * 3.14159274f / 12f;
						num43 = 0.05f;
					}
					break;
				case 4:
					num43 = 0.01f;
					num42 = 3.14159274f;
					if (NPC.spriteDirection == 1)
					{
						num42 += 3.14159274f;
					}
					break;
				case 6:
					num43 = 0.02f;
					num42 = 0f;
					if (NPC.spriteDirection == -1)
					{
						num42 -= 3.14159274f;
					}
					break;
			}
			if (NPC.spriteDirection == -1)
			{
				num42 += 3.14159274f;
			}
			if (num43 != 0f)
			{
				NPC.rotation = NPC.rotation.AngleTowards(num42, num43);
			}
		}
		#endregion

		#region Drawing
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Microsoft.Xna.Framework.Rectangle frame6 = NPC.frame;
			Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			int num156 = texture.Height / Main.npcFrameCount[NPC.type];
			int y3 = num156 * (int)NPC.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num157 = 8;
			int num158 = 2;
			int num159 = 1;
			float num160 = 0f;
			int num161 = num159;
			if (base.NPC.IsABestiaryIconDummy)
			{
				Vector2 vector = new Vector2((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[base.NPC.type] / 2));
				Vector2 position = base.NPC.Center - screenPos + new Vector2(-175f, 0f);
				float scale = 1f;
				Main.EntitySpriteDraw(texture, position, (NPC.frame), Color.White, base.NPC.rotation, vector, scale, spriteEffects, 0f);
				return false;
			}
			if (NPC.localAI[2] == 1f && !phaseOneLoot)
			{
				bool drawAfterImage2 = (NPC.ai[0] == 2f || NPC.ai[0] == 3f || NPC.ai[0] == 4f || NPC.ai[0] == 5f || NPC.ai[0] == 7f) && Lighting.NotRetro;
				SpriteEffects spriteEffects2 = spriteEffects ^ SpriteEffects.FlipHorizontally;
				if (NPC.localAI[3] < 900f)
				{
					color9 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
				}
				Microsoft.Xna.Framework.Color alpha16 = NPC.GetAlpha(color9);
				while (drawAfterImage2 && ((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)))
				{
					goto IL_6899;
				IL_6881:
					num161 += num158;
					continue;
				IL_6899:
					float num164 = (float)(num157 - num161);
					if (num158 < 0)
					{
						num164 = (float)(num159 - num161);
					}
					alpha16 *= num164 / ((float)NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f); //1.5
					Vector2 value4 = (NPC.oldPos[num161]);
					float num165 = NPC.rotation;
					Main.spriteBatch.Draw(texture, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha16, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects2.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, spriteEffects2, 0f);
					goto IL_6881;
				}
				Main.spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), NPC.GetAlpha(color9), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, spriteEffects2, 0);
				return false;
			}
			Microsoft.Xna.Framework.Color alpha15 = NPC.GetAlpha(color9);
			bool drawAfterImage = (NPC.ai[0] == 1f || NPC.ai[0] == 5f || NPC.ai[0] == 7f || NPC.ai[0] == 8f || NPC.ai[0] == 11f || NPC.ai[0] == 12f ||
				NPC.ai[0] == 14f || NPC.ai[0] == 15f || NPC.ai[0] == 18f || NPC.ai[0] == 19f || NPC.ai[0] == 22f) && Lighting.NotRetro;
			while (drawAfterImage && ((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)))
			{
				goto IL_6899;
			IL_6881:
				num161 += num158;
				continue;
			IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				alpha15 *= num164 / ((float)NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f); //1.5
				Vector2 value4 = (NPC.oldPos[num161]);
				float num165 = NPC.rotation;
				Main.spriteBatch.Draw(texture, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, spriteEffects, 0f);
				goto IL_6881;
			}
			Main.spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), NPC.GetAlpha(color9), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, spriteEffects, 0);
			return false;
		}
		#endregion
		
		#region Loot
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			/*
			if (!dropLoot)
			{
				return;
			}
			*/
			LeadingConditionRule darkSun = new LeadingConditionRule(new DarkSunCondition());
			darkSun.OnSuccess(new CommonDrop(ModContent.ItemType<BossRush>(), 1));
			darkSun.OnSuccess(new CommonDrop(ModContent.ItemType<YharonTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<YharonBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<YharonBag>()));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<HellcasterFragment>(), 1, 22, 29));
			darkSun.OnSuccess(new CommonDrop(ModContent.ItemType<VoidVortex>(), 40));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<ForgottenDragonEgg>(), 10));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HellcasterFragment>(), 1, 15, 23));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<YharonMask>(), 7));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AngryChickenStaff>(), 4));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PhoenixFlameBarrage>(), 4));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DragonsBreath>(), 4));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DragonRage>(), 4));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProfanedTrident>(), 4));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheBurningSky>(), 4));
			darkSun.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ChickenCannon>(), 4));
			npcLoot.Add(darkSun);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = Mod.Find<ModItem>("OmegaHealingPotion").Type;
		}
		#endregion

		#region DamageFormula
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 10)
			{
				modifiers.SetMaxDamage(0);
			}
			double newDamage = (modifiers.FinalDamage.Base + (int)((double)NPC.defense * 0.25));
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			if (newDamage >= 1.0)
			{
				if (NPC.localAI[2] == 1f)
				{
					float protection = (((NPC.ichor || NPC.onFire2) ? 0.17f : 0.22f) +
					(protectionBoost ? 0.68f : 0f)); //0.85 or 0.9
					newDamage = (double)((int)((double)(1f - protection) * newDamage));
				}
				else
				{
					float protection = (((NPC.ichor || NPC.onFire2) ? 0.12f : 0.17f) +
					(protectionBoost ? 0.73f : 0f)); //0.85 or 0.9
					newDamage = (double)((int)((double)(1f - protection) * newDamage));
				}
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
		}
		#endregion

		#region HPBarCooldownSlotandStats
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 2f;
			return null;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		#endregion

		#region FindFrame
		public override void FindFrame(int frameHeight)
		{
			if ((NPC.localAI[2] != 1f && (NPC.ai[0] == 0f || NPC.ai[0] == 6f || NPC.ai[0] == 13f || NPC.ai[0] == 21f)) ||
				(NPC.localAI[2] == 1f && (NPC.ai[0] == 5f || NPC.ai[0] < 2f))) //idle
			{
				int num84 = 4; //5
				if (NPC.localAI[2] != 1f && (NPC.ai[0] == 6f || NPC.ai[0] == 13f || NPC.ai[0] == 21f)) //Phase ai switch
				{
					num84 = 3; //4
				}
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > (double)num84)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y >= frameHeight * 5) //6
				{
					NPC.frame.Y = 0;
				}
			}
			if ((NPC.localAI[2] != 1f && (NPC.ai[0] == 1f || NPC.ai[0] == 5f || NPC.ai[0] == 7f || NPC.ai[0] == 11f || NPC.ai[0] == 14f || NPC.ai[0] == 18f || NPC.ai[0] == 22f)) ||
				(NPC.localAI[2] == 1f && (NPC.ai[0] == 6f || NPC.ai[0] == 2f || NPC.ai[0] == 7f))) //Charging or birb spawn
			{
				NPC.frame.Y = frameHeight * 5; //6
			}
			if ((NPC.localAI[2] != 1f && (NPC.ai[0] == 2f || NPC.ai[0] == 8f || NPC.ai[0] == 12f || NPC.ai[0] == 15f || NPC.ai[0] == 19f || NPC.ai[0] == 23f)) ||
				(NPC.localAI[2] == 1f && (NPC.ai[0] == 4f || NPC.ai[0] == 3f || NPC.ai[0] == 8f))) //Fireball spit, teleport, circle, flamethrower
			{
				NPC.frame.Y = frameHeight * 5; //6
			}
			if (NPC.localAI[2] != 1f && (NPC.ai[0] == 3f || NPC.ai[0] == 9f || NPC.ai[0] == -1f || NPC.ai[0] == 16f || NPC.ai[0] == 20f || NPC.ai[0] == 24f)) //Summon tornadoes
			{
				int num85 = 90;
				if (NPC.ai[2] < (float)(num85 - 30) || NPC.ai[2] > (float)(num85 - 10))
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 4.0) //5
					{
						NPC.frameCounter = 0.0;
						NPC.frame.Y = NPC.frame.Y + frameHeight;
					}
					if (NPC.frame.Y >= frameHeight * 5) //6
					{
						NPC.frame.Y = 0;
					}
				}
				else
				{
					NPC.frame.Y = frameHeight * 5; //6
					if (NPC.ai[2] > (float)(num85 - 20) && NPC.ai[2] < (float)(num85 - 15))
					{
						NPC.frame.Y = frameHeight * 6; //7
					}
				}
			}
			if ((NPC.localAI[2] != 1f && (NPC.ai[0] == 4f || NPC.ai[0] == 10f || NPC.ai[0] == 17f || NPC.ai[0] == 25f)) ||
				(NPC.localAI[2] == 1f && NPC.ai[0] == 9f)) //Enter new phase
			{
				int num86 = 180;
				if (NPC.ai[2] < (float)(num86 - 60) || NPC.ai[2] > (float)(num86 - 20))
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 4.0) //5
					{
						NPC.frameCounter = 0.0;
						NPC.frame.Y = NPC.frame.Y + frameHeight;
					}
					if (NPC.frame.Y >= frameHeight * 5) //6
					{
						NPC.frame.Y = 0;
					}
				}
				else
				{
					NPC.frame.Y = frameHeight * 5; //6
					if (NPC.ai[2] > (float)(num86 - 50) && NPC.ai[2] < (float)(num86 - 25))
					{
						NPC.frame.Y = frameHeight * 6; //7
					}
				}
			}
		}
		#endregion

		#region Boom
		public void Boom(int timeLeft, int damage)
		{
			if (Main.netMode != 1)
			{
				Vector2 valueBoom = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float spreadBoom = 15f * 0.0174f;
				double startAngleBoom = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spreadBoom / 2;
				double deltaAngleBoom = spreadBoom / 8f;
				double offsetAngleBoom;
				int iBoom;
				for (iBoom = 0; iBoom < 25; iBoom++)
				{
					offsetAngleBoom = (startAngleBoom + deltaAngleBoom * (iBoom + iBoom * iBoom) / 2f) + 32f * iBoom;
					int boom1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), valueBoom.X, valueBoom.Y, (float)(Math.Sin(offsetAngleBoom) * 5f), (float)(Math.Cos(offsetAngleBoom) * 5f), Mod.Find<ModProjectile>("FlareBomb").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					int boom2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), valueBoom.X, valueBoom.Y, (float)(-Math.Sin(offsetAngleBoom) * 5f), (float)(-Math.Cos(offsetAngleBoom) * 5f), Mod.Find<ModProjectile>("FlareBomb").Type, damage, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[boom1].timeLeft = timeLeft;
					Main.projectile[boom2].timeLeft = timeLeft;
				}
			}
		}
		#endregion

		#region HitEffect
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				Boom(150, 1000);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 300;
				NPC.height = 280;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 244, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		#endregion
	}
}