using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Yharon;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Yharon;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Yharon
{
	[AutoloadBossHead]
	public class Yharon : ModNPC
	{
		public int flareTimer = 0; //projectile stuff
		public int flareProjectiles = 2;
		public int skyFlareProjectiles = 1;
		public const float skyFlareAngleSpread = 360;
		public int skyFlareCountdown = 0;
		internal int dpsCap = CalamityWorld1Point2.downedYharon ? 182000 : 20000; //60
		private int damageTotal = 0;
		public Rectangle safeBox = default(Rectangle);
		public bool protectionBoost = false;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Jungle Dragon, Yharon");
			Main.npcFrameCount[NPC.type] = 7;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 20f;
			NPC.damage = 370;
			NPC.width = 150;
			NPC.height = 100;
			NPC.defense = 260;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 1300000 : 1150000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.value = Item.buyPrice(10, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			Music = MusicLoader.GetMusicSlot("CalamityModClassic1Point2/Sounds/Music/YHARON");
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath60;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("YharonBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("Jungle dargon.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld1Point2.revenge;
			bool expertMode = Main.expertMode;
			float expertDamage = expertMode ? (0.55f * Main.GameModeInfo.EnemyDamageMultiplier) : 1f;
			bool skyFlareStart = (double)NPC.life <= (double)NPC.lifeMax * 0.9; //starts sky flare barrages
			bool gigaFlareStart = (double)NPC.life <= (double)NPC.lifeMax * 0.75; //starts giga flares and increases sky flare amount
			bool phase2Check = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.8 : 0.65); //check for phase 2  Also increases giga flares and sky flares
			bool phase3Check = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.55 : 0.35); //check for phase 3  Also increases giga flares, speed, and sky flares
			bool phase4Check = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.2 : 0.1); //check for phase 4
			if (NPC.localAI[1] == 0f) 
			{
				NPC.localAI[1] = 1f;
				Vector2 vectorPlayer = new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y);
				safeBox.X = (int)(vectorPlayer.X - 2500f);
				safeBox.Y = (int)(vectorPlayer.Y - 2500f);
				safeBox.Width = 5000;
				safeBox.Height = 5000;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[NPC.target].position.X + 2500, Main.player[NPC.target].position.Y, 0f, 5f, Mod.Find<ModProjectile>("SkyFlareRevenge").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[NPC.target].position.X - 2500, Main.player[NPC.target].position.Y, 0f, 5f, Mod.Find<ModProjectile>("SkyFlareRevenge").Type, 0, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			bool phase2Change = NPC.ai[0] > 4f; //phase 2 stuff
			bool phase3Change = NPC.ai[0] > 9f; //phase 3 stuff
			bool phase4Change = NPC.ai[0] > 14f; //phase 4 stuff
			int flareCount = 6;
			bool isCharging = NPC.ai[3] < 14f; //10
			float teleportLocation = 0f;
			int teleChoice = Main.rand.Next(2);
			damageTotal -= dpsCap;
			if (damageTotal < 0)
			{
				damageTotal = 0;
			}
			if (teleChoice == 0)
			{
				teleportLocation = revenge ? 500f : 600f;
			}
			else
			{
				teleportLocation = revenge ? -500f : -600f;
			}
			if (phase3Check)
			{
				flareProjectiles = 3;
			}
			else if (phase2Check)
			{
				flareProjectiles = 2;
			}
			else
			{
				flareProjectiles = 1;
			}
			if (gigaFlareStart && flareTimer == 0)
			{
				flareTimer = 900;
			}
			if (flareTimer > 0)
			{
				flareTimer--;
				if (flareTimer == 0)
				{
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
					    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
					    	double deltaAngle = spread/8f;
					    	double offsetAngle;
					    	int damage = expertMode ? 80 : 90;
					    	int j;
					    	for (j = 0; j < flareProjectiles; j++ )
					    	{
					   			offsetAngle = (startAngle + deltaAngle * ( j + j * j ) / 2f ) + 32f * j;
					   			Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)Main.rand.Next(-200, 201) * 0.125f, (float)Main.rand.Next(-200, 201) * 0.125f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)Main.rand.Next(-200, 201) * 0.125f, (float)Main.rand.Next(-200, 201) * 0.125f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					    	}
						}
					}
				}
			}
			if (phase4Check)
			{
				skyFlareProjectiles = 2;
			}
			else
			{
				skyFlareProjectiles = 1;
			}
			if (skyFlareStart && skyFlareCountdown == 0)
			{
				skyFlareCountdown = 600;
			}
			if (skyFlareCountdown > 0)
			{
				skyFlareCountdown--;
				if (skyFlareCountdown == 0)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						for (int playerIndex = 0; playerIndex < 255; playerIndex++)
						{
							if (Main.player[playerIndex].active)
							{
								Player player2 = Main.player[playerIndex];
								int speed = Main.rand.Next(3, 11);
								float spawnX = Main.rand.Next(1000) - 500 + player2.Center.X;
								float spawnY = -1000 + player2.Center.Y;
								Vector2 baseSpawn = new Vector2(spawnX, spawnY);
								Vector2 baseVelocity = player2.Center - baseSpawn;
								baseVelocity.Normalize();
								baseVelocity = baseVelocity * speed;
								int damage = expertMode ? 65 : 70;
								for (int k = 0; k < skyFlareProjectiles; k++)
								{
									int randomTime = Main.rand.Next(100, 300);
									Vector2 spawn = baseSpawn;
									spawn.X = spawn.X + k * 90 - (skyFlareProjectiles * 15);
									Vector2 velocity = baseVelocity;
									velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-skyFlareAngleSpread / 2 + (skyFlareAngleSpread * k / (float)skyFlareProjectiles)));
									velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
									int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn.X, spawn.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("SkyFlare").Type, damage, 10f, Main.myPlayer, 0f, 0f);
									Main.projectile[projectile].timeLeft = randomTime;
								}
							}
						}
					}
				}
			}
			if (phase4Change)
			{
				NPC.damage = (int)((float)NPC.defDamage * 1.05f * expertDamage);
				NPC.defense = 140;
			}
			else if (phase3Change)
			{
				NPC.damage = (int)((float)NPC.defDamage * 1.05f * expertDamage);
				NPC.defense = 180;
			} 
			else if (phase2Change)
			{
				NPC.damage = (int)((float)NPC.defDamage * 1.1f * expertDamage);
				NPC.defense = 220;
			} 
			else 
			{
				NPC.damage = NPC.defDamage;
				NPC.defense = 260;
			}
			int aiChangeRate = expertMode ? 36 : 38;
			float npcVelocity = expertMode ? 0.7f : 0.69f;
			float scaleFactor = expertMode ? 11f : 10.8f;
			if (phase4Change)
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
			float playerRunAcceleration = 1f; // Main.player[NPC.target].velocity.Y == 0f ? Math.Abs(Main.player[NPC.target].moveSpeed * 0.5f) : (Main.player[NPC.target].runAcceleration * 1f); // Broken as of the movement speed rework
			if (playerRunAcceleration <= 1f)
			{
				playerRunAcceleration = 1f;
			}
			int chargeTime = expertMode ? 26 : 27;
			float chargeSpeed = playerRunAcceleration * (expertMode ? 27f : 26.5f); //17 and 16
			if (phase4Change) //phase 4
			{
				chargeTime = 21;
				chargeSpeed = playerRunAcceleration * 35f; //27
			} 
			else if (phase3Change) //phase 3
			{
				chargeTime = 23;
				chargeSpeed = playerRunAcceleration * 30.5f; //27
			}
			else if (isCharging && phase2Change) //phase 2
			{
				chargeTime = (expertMode ? 25 : 26);
				if (expertMode)
				{
					chargeSpeed = playerRunAcceleration * 28f; //21
				}
			}
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
				if (NPC.ai[0] > 4f)
				{
					NPC.ai[0] = 5f;
				}
				else
				{
					NPC.ai[0] = 0f;
				}
				NPC.ai[2] = 0f;
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
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
				protectionBoost = false;
			}
			if (NPC.localAI[0] == 0f) 
			{
				NPC.localAI[0] = 1f;
				NPC.alpha = 255;
				NPC.rotation = 0f; //checked
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.ai[0] = -1f;
					NPC.netUpdate = true;
				}
			}
			float npcRotation = (float)Math.Atan2((double)(player.Center.Y - vectorCenter.Y), (double)(player.Center.X - vectorCenter.X)); 
			if (NPC.spriteDirection == 1) //changed
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
			if (NPC.ai[0] == -1f) 
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 3f) 
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 4f) 
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 8f)
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 9f)
			{
				npcRotation = 0f;
			}
			if (NPC.ai[0] == 13f)
			{
				npcRotation = 0f;
			}
			float npcRotationSpeed = 0.04f;
			if (NPC.ai[0] == 1f || NPC.ai[0] == 6f || NPC.ai[0] == 11f) 
			{
				npcRotationSpeed = 0f;
			}
			if (NPC.ai[0] == 7f || NPC.ai[0] == 12f) 
			{
				npcRotationSpeed = 0f;
			}
			if (NPC.ai[0] == 3f) 
			{
				npcRotationSpeed = 0.01f;
			}
			if (NPC.ai[0] == 4f) 
			{
				npcRotationSpeed = 0.01f;
			}
			if (NPC.ai[0] == 8f || NPC.ai[0] == 13f) 
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
				if (num1467 != 0) //perhaps issues?  probably not
				{
					NPC.direction = num1467;
					NPC.spriteDirection = NPC.direction; //end issues
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
					int num1468 = 36;
					for (int num1469 = 0; num1469 < num1468; num1469++) 
					{
						Vector2 vector169 = Vector2.Normalize(NPC.velocity) * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f;
						vector169 = vector169.RotatedBy((double)((float)(num1469 - (num1468 / 2 - 1)) * 6.28318548f / (float)num1468), default(Vector2)) + NPC.Center;
						Vector2 value16 = vector169 - NPC.Center;
						int num1470 = Dust.NewDust(vector169 + value16, 0, 0, DustID.CopperCoin, value16.X * 2f, value16.Y * 2f, 100, default(Color), 1.4f); //changed
						Main.dust[num1470].noGravity = true;
						Main.dust[num1470].noLight = true;
						Main.dust[num1470].velocity = Vector2.Normalize(value16) * 3f;
					}
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
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
						NPC.rotation = 3.14159274f;
					}
					NPC.direction = num1471;
					if (num1471 != 0) 
					{
						NPC.direction = num1471;
						NPC.rotation = 0f;
						NPC.spriteDirection = -NPC.direction;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate) 
				{
					int num1472 = 0;
					switch ((int)NPC.ai[3]) //switch for attack modes
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
							num1472 = 1;
							break;
						case 6:
							NPC.ai[3] = 1f;
							num1472 = 2;
							break;
						case 7:
							NPC.ai[3] = 0f;
							num1472 = 3;
							break;
					}
					if (phase2Check)
					{
						num1472 = 4;
					}
					if (num1472 == 1) 
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
					else if (num1472 == 2) 
					{
						NPC.ai[0] = 2f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					} 
					else if (num1472 == 3) 
					{
						NPC.ai[0] = 3f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					} 
					else if (num1472 == 4) 
					{
						NPC.ai[0] = 4f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 1f) //charge attack
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				int num1473 = 7;
				for (int num1474 = 0; num1474 < num1473; num1474++) 
				{
					Vector2 vector171 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + vectorCenter;
					Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1475 = Dust.NewDust(vector171 + value18, 0, 0, DustID.CopperCoin, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f); //changed
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
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
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
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				if (NPC.ai[2] % (float)num1455 == 0f) //fire flare bombs from mouth
				{
					SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center); //changed
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						if (NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare").Type) < flareCount)
						{
							Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
							float speedX = (float)Main.rand.Next(15, 23);
							int detFlare = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector6.X, (int)vector6.Y - 100, Mod.Find<ModNPC>("DetonatingFlare").Type, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[detFlare].localAI[1] = (float)Main.rand.Next(5, 9);
							Main.npc[detFlare].localAI[2] = speedX / 100;
						}
						int damage = expertMode ? 150 : 164;
						int randomTime = Main.rand.Next(500, 1001);
						Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y - 100, (float)Main.rand.Next(-200, 201) * 0.13f, (float)Main.rand.Next(-200, 201) * 0.13f, Mod.Find<ModProjectile>("FlareBomb").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = randomTime;
					}
				}
				int num1476 = Math.Sign(player.Center.X - vectorCenter.X); 
				Vector2 dir2 = NPC.position - Main.player[NPC.target].position;
				if (num1476 != 0) //perhaps issues?
				{
					NPC.direction = num1476;
					if (NPC.spriteDirection != -NPC.direction) 
					{
						NPC.rotation += 6.28318548f;
						if (NPC.rotation > 6.28318548f)
						{
							NPC.rotation = 0f;
							if(dir2.X < 0)
							{								
								NPC.direction = -1;
							}
							else 
							{
								NPC.direction = 1;
							}
						}
					}
					NPC.spriteDirection = -NPC.direction; //end issues
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
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30)) 
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == (float)(num1457 - 30)) 
				{
					int randomTime = Main.rand.Next(200, 400);
					int randomTime2 = Main.rand.Next(100, 300);
					Vector2 vector174 = NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)NPC.direction) * (float)(NPC.width + 20) / 2f + vectorCenter;
					int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
					int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector174.X, vector174.Y, (float)(-(float)NPC.direction * 2), 8f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 0f, 0f); //changed
					Main.projectile[projectile].timeLeft = randomTime;
					Main.projectile[projectile2].timeLeft = randomTime2;
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
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1458) 
				{
					NPC.ai[0] = 5f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 5f && !player.dead) //phase 2
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
				if (num1477 != 0) //perhaps an issue lies here
				{
					if (NPC.ai[2] == 0f && num1477 != NPC.direction)
					{
						NPC.rotation = 3.14159274f;
					}
					NPC.direction = num1477;
					if (num1477 != 0) 
					{
						NPC.direction = num1477;
						NPC.rotation = 0f;
						NPC.spriteDirection = -NPC.direction; //end issue
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate) 
				{
					int num1478 = 0;
					switch ((int)NPC.ai[3]) //switch between attack modes
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
							num1478 = 1;
							break;
						case 8:
							NPC.ai[3] = 1f;
							num1478 = 2;
							break;
						case 9:
							NPC.ai[3] = 0f;
							num1478 = 3;
							break;
					}
					if (phase3Check) //checks if can initiate phase 3
					{
						num1478 = 4;
					}
					if (num1478 == 1) 
					{
						NPC.ai[0] = 6f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0) 
						{
							NPC.direction = num1477; //perhaps an issue lies here
							if (NPC.spriteDirection == 1) 
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction; //end issue
						}
					} 
					else if (num1478 == 2) 
					{
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0) 
						{
							NPC.direction = num1477; //perhaps an issue lies here
							if (NPC.spriteDirection == 1) 
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction; //end issue
						}
						NPC.ai[0] = 7f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					} 
					else if (num1478 == 3) 
					{
						NPC.ai[0] = 8f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					} 
					else if (num1478 == 4) 
					{
						NPC.ai[0] = 9f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 6f) //charge
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				int num1479 = 7;
				for (int num1480 = 0; num1480 < num1479; num1480++) 
				{
					Vector2 vector176 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, DustID.CopperCoin, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime) 
				{
					NPC.ai[0] = 5f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 7f) //Flare summon
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.ai[2] == 0f) 
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				if (NPC.ai[2] % (float)num1462 == 0f) 
				{
					SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center); //changed
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						if (NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare2").Type) < flareCount)
						{
							Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
							int detFlare = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector6.X, (int)vector6.Y - 100, Mod.Find<ModNPC>("DetonatingFlare2").Type, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[detFlare].localAI[3] = (float)Main.rand.Next(3, 9);
						}
						int damage = expertMode ? 85 : 90;
						Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y - 100, (float)Main.rand.Next(-500, 501) * 0.13f, (float)Main.rand.Next(-30, 31) * 0.13f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = 600;
					}
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461) 
				{
					NPC.ai[0] = 5f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 8f) //stop and fire big flare
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30)) 
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == (float)(num1457 - 30)) 
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1457) 
				{
					NPC.ai[0] = 5f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 9f) //start phase 3
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1459 - 60)) 
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1459) 
				{
					NPC.ai[0] = 10f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 10f && !player.dead) //phase 3, new part of AI
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
					if (num1477 != 0) 
					{
						NPC.direction = num1477;
						NPC.rotation = 0f;
						NPC.spriteDirection = -NPC.direction;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate)
				{
					int num1478 = 0;
					switch ((int)NPC.ai[3]) 
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
						case 8:
						case 9:
							num1478 = 1;
							break;
						case 10:
							NPC.ai[3] = 1f;
							num1478 = 2;
							break;
						case 11:
							NPC.ai[3] = 0f;
							num1478 = 3;
							break;
					}
					if (phase4Check)
					{
						num1478 = 4;
					}
					if (num1478 == 1) 
					{
						NPC.ai[0] = 11f;
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
					else if (num1478 == 2) 
					{
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1477 != 0) 
						{
							NPC.direction = num1477; //perhaps an issue lies here
							if (NPC.spriteDirection == 1) 
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction; //end issue
						}
						NPC.ai[0] = 12f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					} 
					else if (num1478 == 3) 
					{
						NPC.ai[0] = 13f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (num1478 == 4) 
					{
						NPC.ai[0] = 14f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 11f) //charge
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				int num1479 = 7;
				for (int num1480 = 0; num1480 < num1479; num1480++) 
				{
					Vector2 vector176 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, DustID.CopperCoin, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime) 
				{
					NPC.ai[0] = 10f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 2f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else if (NPC.ai[0] == 12f) //flare circle of doom
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.ai[2] == 0f) 
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				if (NPC.ai[2] % (float)num1462 == 0f) 
				{
					SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center); //changed
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						if (NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare2").Type) < flareCount && NPC.CountNPCS(Mod.Find<ModNPC>("DetonatingFlare").Type) < flareCount)
						{
							int randomSpawn = Main.rand.Next(2);
							if (randomSpawn == 0)
							{
								randomSpawn = Mod.Find<ModNPC>("DetonatingFlare").Type;
							}
							else
							{
								randomSpawn = Mod.Find<ModNPC>("DetonatingFlare2").Type;
							}
							Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
							float speedX = (float)Main.rand.Next(10, 16);
							int detFlare = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector6.X, (int)vector6.Y - 100, randomSpawn, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[detFlare].localAI[1] = (float)Main.rand.Next(5, 11);
							Main.npc[detFlare].localAI[2] = speedX / 100;
							Main.npc[detFlare].localAI[3] = (float)Main.rand.Next(3, 11);
						}
						int damage = expertMode ? 90 : 100;
						Vector2 vector = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						int projectile1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y - 100, (float)Main.rand.Next(-501, 501) * 0.13f, (float)Main.rand.Next(-31, 31) * 0.13f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile1].timeLeft = 600;
				        int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y - 100, (float)Main.rand.Next(-31, 31) * 0.13f, (float)Main.rand.Next(-251, 251) * 0.13f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile2].timeLeft = 420;
					}
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461) 
				{
					NPC.ai[0] = 10f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 13f) //dual tornado blast
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num1457 - 30)) 
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter); //changed
				}
				if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == (float)(num1457 - 30)) 
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vectorCenter.X, vectorCenter.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
					int randomTime = Main.rand.Next(200, 400);
					int randomTime2 = Main.rand.Next(100, 300);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1457) 
				{
					NPC.ai[0] = 10f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 14f) //phase 4 would be ai 9
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
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1459)
				{
					NPC.ai[0] = 15f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 15f && !player.dead) //teleport above or below player would be ai 10
			{
				NPC.dontTakeDamage = false;
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
					if (num32 != 0) 
					{
						NPC.direction = num32;
						NPC.rotation = 0f;
						NPC.spriteDirection = -NPC.direction; //end issue
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)aiChangeRate)
				{
					int num33 = 0;
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
							num33 = 1;
							break;
						case 1:
						case 4:
						case 8:
						case 13:
							num33 = 2;
							break;
					}
					if (num33 == 1)
					{
						NPC.ai[0] = 16f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num32 != 0) 
						{
							NPC.direction = num32; //perhaps an issue lies here
							if (NPC.spriteDirection == 1) 
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction; //end issue
						}
					}
					else if (num33 == 2)
					{
						NPC.ai[0] = 17f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (num33 == 3)
					{
						NPC.ai[0] = 18f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 16f) //charge npc would be ai 11
			{
				NPC.dontTakeDamage = false;
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
					int num35 = Dust.NewDust(vector11 + value8, 0, 0, DustID.CopperCoin, value8.X * 2f, value8.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num35].noGravity = true;
					Main.dust[num35].noLight = true;
					Main.dust[num35].velocity /= 4f;
					Main.dust[num35].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)chargeTime)
				{
					NPC.ai[0] = 15f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 17f) //teleport npc would be ai 12
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
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter);
				}
				if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[2] == (float)(num1460 / 2))
				{
					if (NPC.ai[1] == 0f)
					{
						NPC.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
					}
					Vector2 center = player.Center + new Vector2(-NPC.ai[1], teleportLocation); //teleport distance
					vectorCenter = (NPC.Center = center);
					int num36 = Math.Sign(player.Center.X - vectorCenter.X);
					NPC.rotation -= num1463 * (float)NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1460)
				{
					NPC.ai[0] = 15f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					if (NPC.ai[3] >= 14f)
					{
						NPC.ai[3] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 18f)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vectorCenter);
				}
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num1463 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num1463 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1461)
				{
					NPC.ai[0] = 15f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			float num66 = 0f;
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Microsoft.Xna.Framework.Rectangle frame6 = NPC.frame;
			Microsoft.Xna.Framework.Color alpha15 = NPC.GetAlpha(color9);
			float num212 = 1f - (float)NPC.life / (float)NPC.lifeMax;
			num212 *= num212;
			alpha15.R = (byte)((float)alpha15.R * num212);
			alpha15.G = (byte)((float)alpha15.G * num212);
			alpha15.B = (byte)((float)alpha15.B * num212);
			alpha15.A = (byte)((float)alpha15.A * num212);
			for (int num213 = 0; num213 < 4; num213++) 
			{
				Vector2 position9 = NPC.position;
				float num214 = Math.Abs(NPC.Center.X - Main.player[Main.myPlayer].Center.X);
				float num215 = Math.Abs(NPC.Center.Y - Main.player[Main.myPlayer].Center.Y);
				if (num213 == 0 || num213 == 2) 
				{
					position9.X = Main.player[Main.myPlayer].Center.X + num214;
				} 
				else 
				{
					position9.X = Main.player[Main.myPlayer].Center.X - num214;
				}
				position9.X -= (float)(NPC.width / 2);
				if (num213 == 0 || num213 == 1) 
				{
					position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
				} 
				else
				{
					position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
				}
				position9.Y -= (float)(NPC.height / 2);
				Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Vector2(position9.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[NPC.type].Value.Width * NPC.scale / 2f + vector11.X * NPC.scale, position9.Y - Main.screenPosition.Y + (float)NPC.height - (float)TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
			}
			Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[NPC.type].Value.Width * NPC.scale / 2f + vector11.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), NPC.GetAlpha(color9), NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
			return false;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<YharonBag>()));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<YharonTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<YharonMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HellcasterFragment>(), 1, 5, 8));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AngryChickenStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DragonsBreath>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Yharon.DragonRage>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Yharon.ProfanedTrident>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PhoenixFlameBarrage>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheBurningSky>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ChickenCannon>(), 4));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = Mod.Find<ModItem>("SupremeHealingPotion").Type;
		}
		
		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Flat);
		}

		public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			OnHit(damageDone);
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Base);
		}
		
		public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			OnHit(damageDone);
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
				ModPacket netMessage = GetPacket(YharonMessageType.Damage);
				netMessage.Write(damage * 60);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					netMessage.Write(Main.myPlayer);
				}
				netMessage.Send();
			}
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);
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
			double newDamage = modifiers.FinalDamage.Flat;
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
			/*if (modifiers.NonCritDamage) // help how do you detect crits
			{
				newDamage *= 2;
			}*/
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - ((NPC.ichor ? 0.25f : 0.33f) + (protectionBoost ? 0.66f : 0f))) * newDamage));
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
			scale = 2f;
			return null;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void FindFrame(int frameHeight)
		{
			if (NPC.ai[0] == 0f || NPC.ai[0] == 5f || NPC.ai[0] == 10f || NPC.ai[0] == 15f)
			{
				int num84 = 4; //5
				if (NPC.ai[0] == 5f || NPC.ai[0] == 10f || NPC.ai[0] == 15f)
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
			if (NPC.ai[0] == 1f || NPC.ai[0] == 6f || NPC.ai[0] == 11f || NPC.ai[0] == 16f)
			{
				NPC.frame.Y = frameHeight * 5; //6
			}
			if (NPC.ai[0] == 2f || NPC.ai[0] == 7f || NPC.ai[0] == 12f || NPC.ai[0] == 17f)
			{
				NPC.frame.Y = frameHeight * 5; //6
			}
			if (NPC.ai[0] == 3f || NPC.ai[0] == 8f || NPC.ai[0] == -1f || NPC.ai[0] == 13f || NPC.ai[0] == 18f)
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
			if (NPC.ai[0] == 4f || NPC.ai[0] == 9f || NPC.ai[0] == 14f)
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
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				Vector2 valueBoom = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float spreadBoom = 15f * 0.0174f;
		    	double startAngleBoom = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spreadBoom/2;
		    	double deltaAngleBoom = spreadBoom/8f;
		    	double offsetAngleBoom;
		    	int iBoom;
		    	int randomTimeBoom = Main.rand.Next(30, 180);
		    	int damageBoom = 1000;
		    	for (iBoom = 0; iBoom < 25; iBoom++ )
		    	{
		   			offsetAngleBoom = (startAngleBoom + deltaAngleBoom * ( iBoom + iBoom * iBoom ) / 2f ) + 32f * iBoom;
		        	int boom1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), valueBoom.X, valueBoom.Y, (float)( Math.Sin(offsetAngleBoom) * 5f ), (float)( Math.Cos(offsetAngleBoom) * 5f ), Mod.Find<ModProjectile>("FlareBomb").Type, damageBoom, 0f, Main.myPlayer, 0f, 0f);
		        	int boom2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), valueBoom.X, valueBoom.Y, (float)( -Math.Sin(offsetAngleBoom) * 5f ), (float)( -Math.Cos(offsetAngleBoom) * 5f ), Mod.Find<ModProjectile>("FlareBomb").Type, damageBoom, 0f, Main.myPlayer, 0f, 0f);
		        	Main.projectile[boom1].timeLeft = randomTimeBoom;
		        	Main.projectile[boom2].timeLeft = randomTimeBoom;
		    	}
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 300;
				NPC.height = 280;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		private ModPacket GetPacket(YharonMessageType type)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)CalamityModClassic1Point2MessageType.Yharon);
			packet.Write(NPC.whoAmI);
			packet.Write((byte)type);
			return packet;
		}
		
		public void HandlePacket(BinaryReader reader)
		{
			YharonMessageType type = (YharonMessageType)reader.ReadByte();
			if (type == YharonMessageType.Damage)
			{
				int damage = reader.ReadInt32();
				damageTotal += damage;
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket netMessage = GetPacket(YharonMessageType.Damage);
					int ignore = reader.ReadInt32();
					netMessage.Write(damage);
					netMessage.Send(-1, ignore);
				}
			}
		}
	}
	
	enum YharonMessageType : byte
	{
		Damage
	}
}