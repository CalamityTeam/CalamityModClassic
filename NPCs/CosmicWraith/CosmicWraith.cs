using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items.Accessories;
using CalamityModClassic1Point2.Items.DevourerMunsters;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.CosmicWraith
{
	[AutoloadBossHead]
	public class CosmicWraith : ModNPC
	{
		public const int CosmicProjectiles = 3;
		public const float CosmicAngleSpread = 170;
		public int CosmicCountdown = 0;
		public float phaseSwitch = 0f;
		public float chargeSwitch = 0f;
		public int dustTimer = 3;
		public int spawn = 15;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Signus, Envoy of the Devourer");
			Main.npcFrameCount[NPC.type] = 15;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 15f;
			NPC.damage = 250;
			NPC.width = 100;
			NPC.height = 130;
			NPC.defense = 70;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 275000 : 250000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.value = Item.buyPrice(0, 15, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			Music = MusicID.Boss4;
			NPC.HitSound = SoundID.NPCHit49;
			NPC.DeathSound = SoundID.NPCDeath51;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("An ethereal cosmic spectral wraith assassin from The Void.")

            });
        }

        public override void AI()
		{
			bool cosmicDust = (double)NPC.life <= (double)NPC.lifeMax * 0.75;
			bool speedBoost = (double)NPC.life <= (double)NPC.lifeMax * 0.65;
			bool cosmicRain = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			bool cosmicSpeed = (double)NPC.life <= (double)NPC.lifeMax * 0.35;
			bool cosmicTeleport = (double)NPC.life <= (double)NPC.lifeMax * 0.2;
			Player player = Main.player[NPC.target];
			bool revenge = CalamityWorld1Point2.revenge;
			bool expertMode = Main.expertMode;
			bool isHell = player.ZoneUnderworldHeight;
			NPC.dontTakeDamage = isHell ? false : true;
			NPC.TargetClosest(true);
			Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vectorCenter = NPC.Center;
			float num1243 = player.Center.X - vector142.X;
			float num1244 = player.Center.Y - vector142.Y;
			float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1000 = cosmicSpeed ? 12f : 15f; //should be lower
			float num1001 = 5f;
			float scaleFactor4 = 0.75f; //should be 0.75
			int num1002 = 0; //should be 0
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1005 = cosmicSpeed ? 12f : 15f; //should be lower
			float num1006 = 0.333333343f;
			float num1007 = 10f; //yes
			num1006 *= num1005;
			int num1009 = (NPC.ai[0] == 2f) ? 2 : 1;
			int num1010 = (NPC.ai[0] == 2f) ? 50 : 35;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, DustID.ShadowbeamStaff, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (Vector2.Distance(player.Center, vectorCenter) > 5600f)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (cosmicRain && CosmicCountdown == 0)
			{
				if (Main.rand.NextBool(100))
				{
					CosmicCountdown = 60;
				}
			}
			if (CosmicCountdown > 0)
			{
				CosmicCountdown--;
				if (CosmicCountdown == 0)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						for (int playerIndex = 0; playerIndex < 255; playerIndex++)
						{
							if (Main.player[playerIndex].active)
							{
								Player player2 = Main.player[playerIndex];
								int speed2 = revenge ? 15 : 10;
								float spawnX = Main.rand.Next(1000) - 500 + player2.Center.X;
								float spawnY = -1000 + player2.Center.Y;
								Vector2 baseSpawn = new Vector2(spawnX, spawnY);
								Vector2 baseVelocity = player2.Center - baseSpawn;
								baseVelocity.Normalize();
								baseVelocity = baseVelocity * speed2;
								int damage = expertMode ? 90 : 100;
								for (int i = 0; i < CosmicProjectiles; i++)
								{
									Vector2 spawn2 = baseSpawn;
									spawn2.X = spawn2.X + i * 30 - (CosmicProjectiles * 15);
									Vector2 velocity = baseVelocity;
									velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-CosmicAngleSpread / 2 + (CosmicAngleSpread * i / (float)CosmicProjectiles)));
									velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
									int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn2.X, spawn2.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("CosmicFlameBurst").Type, damage, 10f, Main.myPlayer, 0f, 0f);
									Main.projectile[projectile].tileCollide = false;
								}
							}
						}
					}
				}
			}
			float speed = expertMode ? 6f : 5.5f;
			if (speedBoost)
			{
				speed = expertMode ? 7.5f : 7f;
			}
			if (NPC.ai[0] <= 2f)
			{
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				if (num1245 < speed)
				{
					NPC.velocity.X = num1243;
					NPC.velocity.Y = num1244;
				}
				else
				{
					num1245 = speed / num1245;
					NPC.velocity.X = num1243 * num1245;
					NPC.velocity.Y = num1244 * num1245;
				}
			}
			if (NPC.ai[0] == 0f) 
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += 1f;
					if (NPC.justHit)
					{
						NPC.localAI[1] += 1f;
					}
					if (cosmicDust)
					{
						NPC.localAI[1] += 1f;
					}
					if (cosmicRain)
					{
						NPC.localAI[1] += 2f;
					}
					if (cosmicTeleport)
					{
						NPC.localAI[1] += 3f;
					}
					if (revenge)
					{
						NPC.localAI[1] += 3f;
					}
					if (NPC.localAI[1] >= (float)(240 + Main.rand.Next(200)))
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						int num1249 = 0;
						int num1250;
						int num1251;
						while (true)
						{
							num1249++;
							num1250 = (int)player.Center.X / 16;
							num1251 = (int)player.Center.Y / 16;
							num1250 += Main.rand.Next(-40, 41);
							num1251 += Main.rand.Next(-40, 41);
							if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, player.position, player.width, player.height))
							{
								break;
							}
							if (num1249 > 100)
							{
								return;
							}
						}
						NPC.ai[0] = 1f;
						NPC.ai[1] = (float)num1250;
						NPC.ai[2] = (float)num1251;
						NPC.netUpdate = true;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 1f) 
			{
				NPC.alpha += 5;
				if (NPC.alpha >= 255)
				{
					NPC.alpha = 255;
					NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
					NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
					NPC.ai[0] = 2f;
					return;
				}
			}
			else if (NPC.ai[0] == 2f) 
			{
				NPC.alpha -= 5;
				if (NPC.alpha <= 0)
				{
					NPC.ai[3] += 1f;
					NPC.alpha = 0;
					if (NPC.ai[3] >= 3f) 
					{
						NPC.ai[0] = 3f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
					} 
					else
					{
						NPC.ai[0] = 0f;
					}
					return;
				}
			}
			else if (NPC.ai[0] == 3f) 
			{
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				float num1065 = 6f; //changed from 6 to 7.5 modifies speed while firing projectiles
				float num1066 = 0.075f; //changed from 0.075 to 0.09375 modifies speed while firing projectiles
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector122 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1067 = player.position.X + (float)(player.width / 2) - vector122.X;
				float num1068 = player.position.Y + (float)(player.height / 2) - 300f - vector122.Y;
				float num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					if (NPC.ai[1] % 15f == 14f)
					{
						flag104 = true;
					}
				}
				else if (NPC.life < NPC.lifeMax / 3)
				{
					if (NPC.ai[1] % 25f == 24f)
					{
						flag104 = true;
					}
				}
				else if (NPC.life < NPC.lifeMax / 2)
				{
					if (NPC.ai[1] % 30f == 29f)
					{
						flag104 = true;
					}
				}
				else if (NPC.ai[1] % 35f == 34f)
				{
					flag104 = true;
				}
				if (flag104 && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(vector121, 1, 1, player.position, player.width, player.height))
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						float num1070 = 13f; //changed from 10
						if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer1Point2>().stressLevel400)
						{
							num1070 += 1f;
						}
						if (cosmicRain)
						{
							num1070 += 3f; //changed from 3 not a prob
						}
						if (cosmicSpeed)
						{
							num1070 += 2f;
						}
						if (revenge)
						{
							num1070 += 1f;
						}
						float num1071 = player.position.X + (float)player.width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = player.position.Y + (float)player.height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = expertMode ? 90 : 100; //projectile damage
						int num1075 = Mod.Find<ModProjectile>("CosmicFlameBurst").Type; //projectile type
						int num1076 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num1076].timeLeft = 240;
					}
				}
				if (!Collision.CanHit(new Vector2(vector121.X, vector121.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					num1065 = 14f; //changed from 14 not a prob
					num1066 = 0.1f; //changed from 0.1 not a prob
					vector122 = vector121;
					num1067 = player.position.X + (float)(player.width / 2) - vector122.X;
					num1068 = player.position.Y + (float)(player.height / 2) - vector122.Y;
					num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
					num1069 = num1065 / num1069;
					if (NPC.velocity.X < num1067)
					{
						NPC.velocity.X = NPC.velocity.X + num1066;
						if (NPC.velocity.X < 0f && num1067 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1066;
						}
					}
					else if (NPC.velocity.X > num1067)
					{
						NPC.velocity.X = NPC.velocity.X - num1066;
						if (NPC.velocity.X > 0f && num1067 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1066;
						}
					}
					if (NPC.velocity.Y < num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1066;
						if (NPC.velocity.Y < 0f && num1068 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1066;
						}
					}
					else if (NPC.velocity.Y > num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1066;
						if (NPC.velocity.Y > 0f && num1068 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1066;
						}
					}
				}
				else if (num1069 > 100f)
				{
					NPC.TargetClosest(true);
					NPC.spriteDirection = NPC.direction;
					num1069 = num1065 / num1069;
					if (NPC.velocity.X < num1067)
					{
						NPC.velocity.X = NPC.velocity.X + num1066;
						if (NPC.velocity.X < 0f && num1067 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1066 * 2f;
						}
					}
					else if (NPC.velocity.X > num1067)
					{
						NPC.velocity.X = NPC.velocity.X - num1066;
						if (NPC.velocity.X > 0f && num1067 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1066 * 2f;
						}
					}
					if (NPC.velocity.Y < num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1066;
						if (NPC.velocity.Y < 0f && num1068 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1066 * 2f;
						}
					}
					else if (NPC.velocity.Y > num1068)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1066;
						if (NPC.velocity.Y > 0f && num1068 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1066 * 2f;
						}
					}
				}
				if (NPC.ai[1] > 600f)
				{
					NPC.ai[0] = 4f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f) 
			{
				NPC.TargetClosest(false);
				NPC.rotation = NPC.velocity.ToRotation();
				if (Math.Sign(NPC.velocity.X) != 0) 
				{
					NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
				}
				if (NPC.rotation < -1.57079637f) 
				{
					NPC.rotation += 3.14159274f;
				}
				if (NPC.rotation > 1.57079637f) 
				{
					NPC.rotation -= 3.14159274f;
				}
				NPC.spriteDirection = Math.Sign(NPC.velocity.X);
				phaseSwitch += 1f;
				if (chargeSwitch == 0f) 
				{
					float scaleFactor6 = num998;
					Vector2 center4 = NPC.Center;
					Vector2 center5 = player.Center;
					Vector2 vector126 = center5 - center4;
					Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
					float num1013 = vector126.Length();
					vector126 = Vector2.Normalize(vector126) * scaleFactor6;
					vector127 = Vector2.Normalize(vector127) * scaleFactor6;
					bool flag64 = Collision.CanHit(NPC.Center, 1, 1, player.Center, 1, 1);
					if (NPC.ai[3] >= 120f) 
					{
						flag64 = true;
					}
					float num1014 = 8f;
					flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
					if (num1013 > num999 || !flag64) 
					{
						NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
						NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
						if (!flag64) 
						{
							NPC.ai[3] += 1f;
							if (NPC.ai[3] == 120f) 
							{
								NPC.netUpdate = true;
							}
						} 
						else
						{
							NPC.ai[3] = 0f;
						}
					} 
					else 
					{
						chargeSwitch = 1f;
						NPC.ai[2] = vector126.X;
						NPC.ai[3] = vector126.Y;
						NPC.netUpdate = true;
					}
				} 
				else if (chargeSwitch == 1f) 
				{
					NPC.velocity *= scaleFactor4;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] >= num1001) 
					{
						chargeSwitch = 2f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
						Vector2 velocity = new Vector2(NPC.ai[2], NPC.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
						velocity.Normalize();
						velocity *= scaleFactor5;
						NPC.velocity = velocity;
					}
				} 
				else if (chargeSwitch == 2f) 
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						dustTimer--;
						if (cosmicDust && dustTimer <= 0)
						{
							SoundEngine.PlaySound(SoundID.Item73, NPC.position);
							int damage = expertMode ? 90 : 100;
							Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("EssenceDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
							Main.projectile[projectile].timeLeft = 120;
							Main.projectile[projectile].velocity.X = 0f;
					        Main.projectile[projectile].velocity.Y = 0f;
				    	    dustTimer = 3;
						}
					}
					float num1016 = num1003;
					NPC.ai[1] += 1f;
					bool flag65 = Vector2.Distance(NPC.Center, player.Center) > num1004 && NPC.Center.Y > player.Center.Y;
					if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007) 
					{
						chargeSwitch = 0f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.velocity /= 2f;
						NPC.netUpdate = true;
						NPC.ai[1] = 45f;
						chargeSwitch = 4f;
					} 
					else 
					{
						Vector2 center6 = NPC.Center;
						Vector2 center7 = player.Center;
						Vector2 vec2 = center7 - center6;
						vec2.Normalize();
						if (vec2.HasNaNs()) 
						{
							vec2 = new Vector2((float)NPC.direction, 0f);
						}
						NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / num1005;
					}
				} 
				else if (chargeSwitch == 4f) 
				{
					NPC.ai[1] -= 3f;
					if (NPC.ai[1] <= 0f) 
					{
						chargeSwitch = 0f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
					}
					NPC.velocity *= 0.95f;
				}
				if (phaseSwitch > 400f)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					chargeSwitch = 0f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void FindFrame(int frameHeight) //9 total frames
		{
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] <= 2f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y >= frameHeight * 6)
				{
					NPC.frame.Y = 0;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 6)
				{
					NPC.frame.Y = frameHeight * 6;
				}
				if (NPC.frame.Y >= frameHeight * 12)
				{
					NPC.frame.Y = frameHeight * 6;
				}
			}
			else
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 12)
				{
					NPC.frame.Y = frameHeight * 12;
				}
				if (NPC.frame.Y >= frameHeight * 15)
				{
					NPC.frame.Y = frameHeight * 12;
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<TwistingNether>(), 1, 2, 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<SignusTrophy>(), 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Items.Weapons.CosmicKunai>(), 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Items.Weapons.Cosmilamp>(), 3));
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.CountNPCS(Mod.Find<ModNPC>("CosmicLantern").Type) < 5)
			{
				spawn--;
				if (NPC.life > 0 && Main.netMode != NetmodeID.MultiplayerClient && spawn <= 0)
				{
					spawn = 35;
					int num660 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("CosmicLantern").Type, 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == NetmodeID.Server && num660 < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
					}
					NPC.netUpdate = true;
				}
			}
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 200;
				NPC.height = 150;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 60; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
		}
	}
}