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
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Yharon;
using CalamityModClassic1Point1.Items.Placeables;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.UI;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Yharon
{
	[AutoloadBossHead]
	public class Yharon : ModNPC
	{
		public int flareTimer = 0; //projectile stuff
		public int flareProjectiles = 2;
		public int skyFlareProjectiles = 5;
		public const float skyFlareAngleSpread = 360;
		public int skyFlareCountdown = 0;
		public int enrageTimer = 600;
		public int timer = 30;
		public bool meleeAggro = false;
		public bool rangedSpeed = false;
		public bool magicBoost = false;
		public bool summonerRage = false;
		
		public override void SetDefaults()
		{
			//NPC.name = "Jungle Dragon Yharon");
			//Tooltip.SetDefault("Jungle Dragon, Yharon");
			NPC.npcSlots = 10f;
			NPC.damage = 350;
			NPC.width = 150;
			NPC.height = 100;
			NPC.defense = 130;
			NPC.lifeMax = 800000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			Main.npcFrameCount[NPC.type] = 7;
			NPC.value = Item.buyPrice(10, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.timeLeft = NPC.activeTime * 30;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/YHARON");
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath60;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("The Tyrant's loyal dragon.")

            });
        }

        public override void AI()
		{
			bool bossBuff = CalamityGlobalNPC1Point1.bossBuff;
			bool superBossBuff = CalamityGlobalNPC1Point1.superBossBuff;
			bool expertMode = Main.expertMode; //ALL THESE PHASES THO
			float expertDamage = expertMode ? (0.6f * Main.GameModeInfo.EnemyDamageMultiplier) : 1f;
			bool speedBoost1 = (double)NPC.life <= (double)NPC.lifeMax * 0.8; //speed increase
			bool speedBoost2 = (double)NPC.life <= (double)NPC.lifeMax * 0.6; //speed increase
			bool speedBoost3 = (double)NPC.life <= (double)NPC.lifeMax * 0.4; //speed increase
			bool speedBoost4 = (double)NPC.life <= (double)NPC.lifeMax * 0.2; //speed increase
			bool skyFlareStart = (double)NPC.life <= (double)NPC.lifeMax * 0.9; //starts sky flare barrages
			bool gigaFlareStart = (double)NPC.life <= (double)NPC.lifeMax * 0.75; //starts giga flares and increases sky flare amount
			bool phase2Check = (double)NPC.life <= (double)NPC.lifeMax * 0.7; //check for phase 2  Also increases giga flares and sky flares
			bool phase3Check = (double)NPC.life <= (double)NPC.lifeMax * 0.4; //check for phase 3  Also increases giga flares, speed, and sky flares
			bool phase4Check = (double)NPC.life <= (double)NPC.lifeMax * 0.1; //check for phase 4
			bool phase2Change = NPC.ai[0] > 4f; //phase 2 stuff
			bool phase3Change = NPC.ai[0] > 9f; //phase 3 stuff
			bool phase4Change = NPC.ai[0] > 14f; //phase 4 stuff
			bool flag132 = NPC.ai[3] < 10f;
			Player player5 = Main.player[NPC.target];
			float teleportLocation = 0f;
			int teleChoice = Main.rand.Next(2);
			if (teleChoice == 0)
			{
				teleportLocation = 600f;
			}
			else
			{
				teleportLocation = -600f;
			}
			if (phase3Check)
			{
				flareProjectiles = magicBoost ? 5 : 4;
			}
			else if (phase2Check)
			{
				flareProjectiles = magicBoost ? 4 : 3;
			}
			else
			{
				flareProjectiles = magicBoost ? 3 : 2;
			}
			if (gigaFlareStart && flareTimer == 0)
			{
				flareTimer = summonerRage ? 720 : 900;
			}
			if (flareTimer > 0)
			{
				flareTimer--;
				if (flareTimer == 0)
				{
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/8f;
				    	double offsetAngle;
				    	int damage = expertMode ? 85 : 150;
				    	int j;
				    	for (j = 0; j < flareProjectiles; j++ )
				    	{
				    		int randomTime = Main.rand.Next(60, 120);
				   			offsetAngle = (startAngle + deltaAngle * ( j + j * j ) / 2f ) + 32f * j;
				   			int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, 0f, 0f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
				        	int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, 0f, 0f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
				        	Main.projectile[projectile].timeLeft = randomTime;
				        	Main.projectile[projectile2].timeLeft = randomTime;
				        	Main.projectile[projectile].velocity.X = (float)Main.rand.Next(-200, 201) * 0.125f;
				        	Main.projectile[projectile].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.125f;
							Main.projectile[projectile2].velocity.X = (float)Main.rand.Next(-200, 201) * 0.125f;
							Main.projectile[projectile2].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.125f;
				    	}
					}
				}
			}
			if (phase3Check)
			{
				skyFlareProjectiles = magicBoost ? 21 : 15;
			}
			else if (phase2Check)
			{
				skyFlareProjectiles = magicBoost ? 15 : 10;
			}
			else if (gigaFlareStart)
			{
				skyFlareProjectiles = magicBoost ? 10 : 6;
			}
			else
			{
				skyFlareProjectiles = magicBoost ? 6 : 3;
			}
			if (skyFlareStart && skyFlareCountdown == 0)
			{
				skyFlareCountdown = summonerRage ? 480 : 600;
			}
			if (skyFlareCountdown > 0)
			{
				skyFlareCountdown--;
				if (skyFlareCountdown == 0)
				{
					for (int playerIndex = 0; playerIndex < 255; playerIndex++)
					{
						if (Main.player[playerIndex].active)
						{
							Player player = Main.player[playerIndex];
							int speed = Main.rand.Next(3, 7);
							float spawnX = Main.rand.Next(1000) - 500 + player.Center.X;
							float spawnY = -1000 + player.Center.Y;
							Vector2 baseSpawn = new Vector2(spawnX, spawnY);
							Vector2 baseVelocity = player.Center - baseSpawn;
							baseVelocity.Normalize();
							baseVelocity = baseVelocity * speed;
							int damage = expertMode ? 60 : 110;
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
			if (player5.lifeRegen >= 5) //knocking bullshit life regen down a notch
			{
				player5.lifeRegen = 5;
			}
			if (player5.setNebula) //cockblocking nebula fags kappa
			{
				if (player5.lifeRegen >= 10)
				{
					player5.lifeRegen = 10;
				}
			}
			if (phase4Change) //upon phase changes adjust stats accordingly
			{
				NPC.damage = (int)((float)NPC.defDamage * 1.1f * expertDamage);
				NPC.defense = meleeAggro ? 60 : 90;
			}
			else if (phase3Change)
			{
				NPC.damage = (int)((float)NPC.defDamage * 1.1f * expertDamage);
				NPC.defense = meleeAggro ? 80 : 110;
			} 
			else if (phase2Change)
			{
				NPC.damage = (int)((float)NPC.defDamage * 1.2f * expertDamage);
				NPC.defense = meleeAggro ? 90 : 120;
			} 
			else 
			{
				NPC.damage = NPC.defDamage;
				NPC.defense = meleeAggro ? 100 : 130;
			}
			int aiChangeRate = expertMode ? 40 : 60;
			float num1451 = expertMode ? 0.55f : 0.45f;
			float scaleFactor10 = expertMode ? 8.5f : 7.5f;
			if (phase4Change) 
			{
				num1451 = 0.75f;
				scaleFactor10 = 13f;
				aiChangeRate = 30;
			}
			else if (phase3Change) 
			{
				num1451 = 0.7f;
				scaleFactor10 = 12f;
				aiChangeRate = 30;
			} 
			else if (phase2Change && flag132) 
			{
				num1451 = (expertMode ? 0.6f : 0.5f);
				scaleFactor10 = (expertMode ? 10f : 8f);
				aiChangeRate = (expertMode ? 40 : 20);
			} 
			else if (flag132 && !phase2Change && !phase3Change && !phase4Change) 
			{
				aiChangeRate = 30;
			}
			int num1452 = expertMode ? 28 : 30;
			float chargeSpeed = expertMode ? 20f : 19f; //17 and 16
			if (phase4Change) //phase 4
			{
				num1452 = 23;
				chargeSpeed = 27f; //27
			} 
			else if (phase3Change) //phase 3
			{
				num1452 = 25;
				chargeSpeed = 23f; //27
			}
			else if (flag132 && phase2Change) //phase 2
			{
				num1452 = (expertMode ? 27 : 30);
				if (expertMode) 
				{
					chargeSpeed = 21f; //21
				}
			}
			if (speedBoost4)
			{
				chargeSpeed += 1f;
			}
			if (speedBoost3)
			{
				chargeSpeed += 1f;
			}
			if (speedBoost2)
			{
				chargeSpeed += 1f;
			}
			if (speedBoost1)
			{
				chargeSpeed += 1f;
			}
			if (phase4Check)
			{
				chargeSpeed += 1f;
			}
			if (meleeAggro)
			{
				chargeSpeed += 2f;
			}
			if (rangedSpeed)
			{
				chargeSpeed += 1f;
			}
			float playerEndurance = player5.endurance;
			if (playerEndurance >= 0.5f)
			{
				chargeSpeed += 1f;
			}
			if (playerEndurance >= 0.8f)
			{
				chargeSpeed += 1f;
			}
			if (playerEndurance >= 1f)
			{
				chargeSpeed += 4f;
			}
			int playerLife = player5.statLifeMax2;
			if (playerLife >= 1000)
			{
				chargeSpeed += 1f;
			}
			if (playerLife >= 2000)
			{
				chargeSpeed += 4f;
			}
			int playerDefense = player5.statDefense;
			if (playerDefense >= 150)
			{
				chargeSpeed += 1f;
			}
			if (playerDefense >= 200)
			{
				chargeSpeed += 1f;
			}
			if (playerDefense >= 250)
			{
				chargeSpeed += 1f;
			}
			if (playerDefense >= 300)
			{
				chargeSpeed += 4f;
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
			Vector2 vector168 = NPC.Center;
			if (NPC.target < 0 || NPC.target == 255 || player5.dead || !player5.active) 
			{
				NPC.TargetClosest(true);
				player5 = Main.player[NPC.target];
				NPC.netUpdate = true;
			}
			if (player5.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 3f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 3f;
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
			if (phase3Change || phase4Change)
			{
				timer--;
				if (timer <= 0)
				{
					int damage = expertMode ? 50 : 80;
					Vector2 vector173 = Vector2.Normalize(player5.Center - vector168) * (float)(NPC.width + 20) / 2f + vector168;
					int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
					Main.projectile[projectile].timeLeft = 60;
					Main.projectile[projectile].velocity.X = 0f;
			        Main.projectile[projectile].velocity.Y = 0f;
			        timer = 60;
				}
			}
			if (bossBuff && superBossBuff)
			{
				chargeSpeed += 6f;
			}
			bool flag = Main.player[NPC.target].ZoneJungle;
			if (!flag)
			{
				enrageTimer--;
				if (enrageTimer <= 0)
				{
					aiChangeRate = superBossBuff ? 10 : 20;
					NPC.defense = NPC.defDefense * 50;
					chargeSpeed += superBossBuff ? 8f : 6f;
				}
			}
			else
			{
				enrageTimer = 600;
			}
			if (NPC.localAI[0] == 0f) 
			{
				NPC.localAI[0] = 1f;
				NPC.alpha = 255;
				NPC.rotation = 0f; //checked
				if (Main.netMode != 1) 
				{
					NPC.ai[0] = -1f;
					NPC.netUpdate = true;
				}
			}
			float num1465 = (float)Math.Atan2((double)(player5.Center.Y - vector168.Y), (double)(player5.Center.X - vector168.X)); 
			if (NPC.spriteDirection == 1) //changed
			{
				num1465 += 3.14159274f;
			}
			if (num1465 < 0f) 
			{
				num1465 += 6.28318548f;
			}
			if (num1465 > 6.28318548f) 
			{
				num1465 -= 6.28318548f;
			}
			if (NPC.ai[0] == -1f) 
			{
				num1465 = 0f;
			}
			if (NPC.ai[0] == 3f) 
			{
				num1465 = 0f;
			}
			if (NPC.ai[0] == 4f) 
			{
				num1465 = 0f;
			}
			if (NPC.ai[0] == 8f)
			{
				num1465 = 0f;
			}
			if (NPC.ai[0] == 9f)
			{
				num1465 = 0f;
			}
			if (NPC.ai[0] == 13f)
			{
				num1465 = 0f;
			}
			float num1466 = 0.04f;
			if (NPC.ai[0] == 1f || NPC.ai[0] == 6f || NPC.ai[0] == 11f) 
			{
				num1466 = 0f;
			}
			if (NPC.ai[0] == 7f || NPC.ai[0] == 12f) 
			{
				num1466 = 0f;
			}
			if (NPC.ai[0] == 3f) 
			{
				num1466 = 0.01f;
			}
			if (NPC.ai[0] == 4f) 
			{
				num1466 = 0.01f;
			}
			if (NPC.ai[0] == 8f || NPC.ai[0] == 13f) 
			{
				num1466 = 0.01f;
			}
			if (NPC.rotation < num1465) //start of rotation code by yoraizor. not changed yet.
			{
				if ((double)(num1465 - NPC.rotation) > 3.1415926535897931) 
				{
					NPC.rotation -= num1466;
				} 
				else
				{
					NPC.rotation += num1466;
				}
			}
			if (NPC.rotation > num1465) 
			{
				if ((double)(NPC.rotation - num1465) > 3.1415926535897931) 
				{
					NPC.rotation += num1466;
				} 
				else
				{
					NPC.rotation -= num1466;
				}
			}
			if (NPC.rotation > num1465 - num1466 && NPC.rotation < num1465 + num1466) 
			{
				NPC.rotation = num1465;
			}
			if (NPC.rotation < 0f) 
			{
				NPC.rotation += 6.28318548f;
			}
			if (NPC.rotation > 6.28318548f) 
			{
				NPC.rotation -= 6.28318548f;
			}
			if (NPC.rotation > num1465 - num1466 && NPC.rotation < num1465 + num1466) 
			{
				NPC.rotation = num1465;
			} //end of rotation code by yoraizor
			if (NPC.ai[0] != -1f && NPC.ai[0] < 9f) 
			{
				bool flag134 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
				if (flag134) 
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
				int num1467 = Math.Sign(player5.Center.X - vector168.X);
				if (num1467 != 0) //perhaps issues?  probably not
				{
					NPC.direction = num1467;
					NPC.spriteDirection = NPC.direction; //end issues
				}
				if (NPC.ai[2] > 20f) 
				{
					NPC.velocity.Y = -2f;
					NPC.alpha -= 5;
					bool flag135 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
					if (flag135) 
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
						int num1470 = Dust.NewDust(vector169 + value16, 0, 0, 244, value16.X * 2f, value16.Y * 2f, 100, default(Color), 1.4f); //changed
						Main.dust[num1470].noGravity = true;
						Main.dust[num1470].noLight = true;
						Main.dust[num1470].velocity = Vector2.Normalize(value16) * 3f;
					}
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
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
			else if (NPC.ai[0] == 0f && !player5.dead) 
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f) 
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vector168 - player5.Center).X));
				}
				Vector2 value17 = player5.Center + new Vector2(NPC.ai[1], -200f) - vector168;
				Vector2 vector170 = Vector2.Normalize(value17 - NPC.velocity) * scaleFactor10;
				if (NPC.velocity.X < vector170.X) 
				{
					NPC.velocity.X = NPC.velocity.X + num1451;
					if (NPC.velocity.X < 0f && vector170.X > 0f) 
					{
						NPC.velocity.X = NPC.velocity.X + num1451;
					}
				} 
				else if (NPC.velocity.X > vector170.X) 
				{
					NPC.velocity.X = NPC.velocity.X - num1451;
					if (NPC.velocity.X > 0f && vector170.X < 0f) 
					{
						NPC.velocity.X = NPC.velocity.X - num1451;
					}
				}
				if (NPC.velocity.Y < vector170.Y) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num1451;
					if (NPC.velocity.Y < 0f && vector170.Y > 0f) 
					{
						NPC.velocity.Y = NPC.velocity.Y + num1451;
					}
				} 
				else if (NPC.velocity.Y > vector170.Y) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num1451;
					if (NPC.velocity.Y > 0f && vector170.Y < 0f) 
					{
						NPC.velocity.Y = NPC.velocity.Y - num1451;
					}
				}
				int num1471 = Math.Sign(player5.Center.X - vector168.X);
				if (num1471 != 0) //perhpas issues?
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
						NPC.spriteDirection = -NPC.direction; //end issues
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
						case 6:
						case 7:
						case 8:
						case 9:
							num1472 = 1;
							break;
						case 10:
							NPC.ai[3] = 1f;
							num1472 = 2;
							break;
						case 11:
							NPC.ai[3] = 0f;
							num1472 = 3;
							break;
					}
					if (phase2Check) //checks if can initiate phase 2
					{
						num1472 = 4;
					}
					if (num1472 == 1) 
					{
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player5.Center - vector168) * chargeSpeed;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num1471 != 0) //charging stuff.  possible issues
						{
							NPC.direction = num1471;
							if (NPC.spriteDirection == 1)
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction; //end issues
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
					vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + vector168;
					Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1475 = Dust.NewDust(vector171 + value18, 0, 0, 244, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1475].noGravity = true;
					Main.dust[num1475].noLight = true;
					Main.dust[num1475].velocity /= 4f;
					Main.dust[num1475].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1452) 
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
					NPC.ai[1] = (float)(300 * Math.Sign((vector168 - player5.Center).X));
				}
				Vector2 value19 = player5.Center + new Vector2(NPC.ai[1], -200f) - vector168;
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
				}
				if (NPC.ai[2] % (float)num1455 == 0f) //fire flare bombs from mouth
				{
					SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center); //changed
					if (Main.netMode != 1) 
					{
						int damage = expertMode ? 110 : 200;
						int randomTime = Main.rand.Next(300, 800);
						Vector2 vector173 = Vector2.Normalize(player5.Center - vector168) * (float)(NPC.width + 20) / 2f + vector168;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y - 100, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareBomb").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = randomTime;
						Main.projectile[projectile].velocity.X = (float)Main.rand.Next(-200, 201) * 0.13f;
				        Main.projectile[projectile].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.13f;
					}
				}
				int num1476 = Math.Sign(player5.Center.X - vector168.X); 
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30)) 
				{
					int randomTime = Main.rand.Next(200, 400);
					int randomTime2 = Main.rand.Next(100, 300);
					Vector2 vector174 = NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)NPC.direction) * (float)(NPC.width + 20) / 2f + vector168;
					int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector168.X, vector168.Y, 0f, 0f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
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
			else if (NPC.ai[0] == 5f && !player5.dead) //phase 2
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f) 
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vector168 - player5.Center).X));
				}
				Vector2 value20 = player5.Center + new Vector2(NPC.ai[1], -200f) - vector168;
				Vector2 vector175 = Vector2.Normalize(value20 - NPC.velocity) * scaleFactor10;
				if (NPC.velocity.X < vector175.X) 
				{
					NPC.velocity.X = NPC.velocity.X + num1451;
					if (NPC.velocity.X < 0f && vector175.X > 0f) 
					{
						NPC.velocity.X = NPC.velocity.X + num1451;
					}
				} 
				else if (NPC.velocity.X > vector175.X) 
				{
					NPC.velocity.X = NPC.velocity.X - num1451;
					if (NPC.velocity.X > 0f && vector175.X < 0f) 
					{
						NPC.velocity.X = NPC.velocity.X - num1451;
					}
				}
				if (NPC.velocity.Y < vector175.Y) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num1451;
					if (NPC.velocity.Y < 0f && vector175.Y > 0f) 
					{
						NPC.velocity.Y = NPC.velocity.Y + num1451;
					}
				} 
				else if (NPC.velocity.Y > vector175.Y) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num1451;
					if (NPC.velocity.Y > 0f && vector175.Y < 0f) 
					{
						NPC.velocity.Y = NPC.velocity.Y - num1451;
					}
				}
				int num1477 = Math.Sign(player5.Center.X - vector168.X);
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
							num1478 = 1;
							break;
						case 6:
							NPC.ai[3] = 1f;
							num1478 = 2;
							break;
						case 7:
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
						NPC.velocity = Vector2.Normalize(player5.Center - vector168) * chargeSpeed;
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
						NPC.velocity = Vector2.Normalize(player5.Center - vector168) * scaleFactor13;
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
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vector168;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1452) 
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
				}
				if (NPC.ai[2] % (float)num1462 == 0f) 
				{
					SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center); //changed
					if (Main.netMode != 1) 
					{
						int damage = expertMode ? 30 : 50;
						Vector2 vector173 = Vector2.Normalize(player5.Center - vector168) * (float)(NPC.width + 20) / 2f + vector168;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y - 100, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = 600;
						Main.projectile[projectile].velocity.X = (float)Main.rand.Next(-500, 501) * 0.13f;
				        Main.projectile[projectile].velocity.Y = (float)Main.rand.Next(-30, 31) * 0.13f;
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30)) 
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vector168.X, vector168.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
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
			else if (NPC.ai[0] == 10f && !player5.dead) //phase 3, new part of AI
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[1] == 0f) 
				{
					NPC.ai[1] = (float)(300 * Math.Sign((vector168 - player5.Center).X));
				}
				Vector2 value20 = player5.Center + new Vector2(NPC.ai[1], -200f) - vector168;
				Vector2 vector175 = Vector2.Normalize(value20 - NPC.velocity) * scaleFactor10;
				if (NPC.velocity.X < vector175.X) 
				{
					NPC.velocity.X = NPC.velocity.X + num1451;
					if (NPC.velocity.X < 0f && vector175.X > 0f) 
					{
						NPC.velocity.X = NPC.velocity.X + num1451;
					}
				} 
				else if (NPC.velocity.X > vector175.X) 
				{
					NPC.velocity.X = NPC.velocity.X - num1451;
					if (NPC.velocity.X > 0f && vector175.X < 0f) 
					{
						NPC.velocity.X = NPC.velocity.X - num1451;
					}
				}
				if (NPC.velocity.Y < vector175.Y) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num1451;
					if (NPC.velocity.Y < 0f && vector175.Y > 0f) 
					{
						NPC.velocity.Y = NPC.velocity.Y + num1451;
					}
				} 
				else if (NPC.velocity.Y > vector175.Y) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num1451;
					if (NPC.velocity.Y > 0f && vector175.Y < 0f) 
					{
						NPC.velocity.Y = NPC.velocity.Y - num1451;
					}
				}
				int num1477 = Math.Sign(player5.Center.X - vector168.X);
				if (num1477 != 0) 
				{
					if (NPC.ai[2] == 0f && num1477 != NPC.direction) //perhaps an issue lies here
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
					switch ((int)NPC.ai[3]) 
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
							num1478 = 1;
							break;
						case 6:
							NPC.ai[3] = 1f;
							num1478 = 2;
							break;
						case 7:
							NPC.ai[3] = 0f;
							num1478 = 3;
							break;
					}
					if (phase4Check) //checks if can initiate phase 4
					{
						num1478 = 4;
					}
					if (num1478 == 1) 
					{
						NPC.ai[0] = 11f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player5.Center - vector168) * chargeSpeed;
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
						NPC.velocity = Vector2.Normalize(player5.Center - vector168) * scaleFactor13;
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
					vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vector168;
					Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num1481].noGravity = true;
					Main.dust[num1481].noLight = true;
					Main.dust[num1481].velocity /= 4f;
					Main.dust[num1481].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1452) 
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
				}
				if (NPC.ai[2] % (float)num1462 == 0f) 
				{
					SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center); //changed
					if (Main.netMode != 1) 
					{
						int damage = expertMode ? 30 : 50;
						Vector2 vector = Vector2.Normalize(player5.Center - vector168) * (float)(NPC.width + 20) / 2f + vector168;
						int projectile1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y - 100, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile1].timeLeft = 300;
						Main.projectile[projectile1].velocity.X = (float)Main.rand.Next(-501, 501) * 0.13f;
				        Main.projectile[projectile1].velocity.Y = (float)Main.rand.Next(-31, 31) * 0.13f;
				        int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y - 100, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile2].timeLeft = 150;
						Main.projectile[projectile2].velocity.X = (float)Main.rand.Next(-31, 31) * 0.13f;
				        Main.projectile[projectile2].velocity.Y = (float)Main.rand.Next(-251, 251) * 0.13f;
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168); //changed
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1457 - 30)) 
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vector168.X, vector168.Y, 0f, 0f, Mod.Find<ModProjectile>("BigFlare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
					int randomTime = Main.rand.Next(200, 400);
					int randomTime2 = Main.rand.Next(100, 300);
					Vector2 vector174 = NPC.rotation.ToRotationVector2() * (Vector2.UnitX * (float)NPC.direction) * (float)(NPC.width + 20) / 2f + vector168;
					int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector168.X, vector168.Y, 0f, 0f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1)); //changed
					int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector174.X, vector174.Y, (float)(-(float)NPC.direction * 2), 8f, Mod.Find<ModProjectile>("Flare").Type, 0, 0f, Main.myPlayer, 0f, 0f); //changed
					Main.projectile[projectile].timeLeft = randomTime;
					Main.projectile[projectile2].timeLeft = randomTime2;
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
					bool flag9 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
					if (flag9)
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168);
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
			else if (NPC.ai[0] == 15f && !player5.dead) //teleport above or below player would be ai 10
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
					NPC.ai[1] = (float)(360 * Math.Sign((vector168 - player5.Center).X));
				}
				Vector2 value7 = player5.Center + new Vector2(NPC.ai[1], teleportLocation) - vector168; //teleport distance
				Vector2 desiredVelocity = Vector2.Normalize(value7 - NPC.velocity) * scaleFactor10;
				NPC.SimpleFlyMovement(desiredVelocity, num1451);
				int num32 = Math.Sign(player5.Center.X - vector168.X);
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
					case 0:
					case 2:
					case 3:
					case 5:
					case 6:
					case 7:
						num33 = 1;
						break;
					case 1:
					case 4:
					case 8:
						num33 = 2;
						break;
					}
					if (num33 == 1)
					{
						NPC.ai[0] = 16f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player5.Center - vector168) * chargeSpeed;
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
					vector11 = vector11.RotatedBy((double)(m - (num34 / 2 - 1)) * 3.1415926535897931 / (double)((float)num34), default(Vector2)) + vector168;
					Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num35 = Dust.NewDust(vector11 + value8, 0, 0, 244, value8.X * 2f, value8.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[num35].noGravity = true;
					Main.dust[num35].noLight = true;
					Main.dust[num35].velocity /= 4f;
					Main.dust[num35].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1452)
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
					SoundEngine.PlaySound(SoundID.Zombie92, vector168);
				}
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num1460 / 2))
				{
					if (NPC.ai[1] == 0f)
					{
						NPC.ai[1] = (float)(300 * Math.Sign((vector168 - player5.Center).X));
					}
					Vector2 center = player5.Center + new Vector2(-NPC.ai[1], teleportLocation); //teleport distance
					vector168 = (NPC.Center = center);
					int num36 = Math.Sign(player5.Center.X - vector168.X);
					NPC.rotation -= num1463 * (float)NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num1460)
				{
					NPC.ai[0] = 15f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					if (NPC.ai[3] >= 9f)
					{
						NPC.ai[3] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 18f) //neutral npc would be ai 13
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f)
				{
					SoundEngine.PlaySound(SoundID.Zombie92, vector168);
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
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			
			if (projectile.CountsAsClass(DamageClass.Melee))
			{
				meleeAggro = true;
			}
			else
			{
				meleeAggro = false;
			}
			if (projectile.CountsAsClass(DamageClass.Ranged))
			{
				rangedSpeed = true;
			}
			else
			{
				rangedSpeed = false;
			}
			if (projectile.CountsAsClass(DamageClass.Magic))
			{
				magicBoost = true;
			}
			else
			{
				magicBoost = false;
			}
			if (projectile.minion)
			{
				summonerRage = true;
			}
			else
			{
				summonerRage = false;
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
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<YharonBag>()));
			npcLoot.Add(new CommonDrop(ModContent.ItemType<YharonTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<YharonMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HellcasterFragment>(), 1, 5, 8));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AngryChickenStaff>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DragonsBreath>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.DragonRage>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.ProfanedTrident>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PhoenixFlameBarrage>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheBurningSky>(), 6));
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
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
		    	int damageBoom = 500;
				if (Main.expertMode)
				{
					damageBoom = 270;
				}
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
	}
}