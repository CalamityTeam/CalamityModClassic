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
using Terraria.GameContent.Generation;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Leviathan;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons.Leviathan;
using CalamityModClassic1Point2.Items.Armor;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Leviathan
{
	[AutoloadBossHead]
	public class Siren : ModNPC
	{
		public static bool phase2 = false;
		public static bool phase3 = false;
		public bool spawnedLevi = false;
		public bool secondClone = false;
		public float phaseSwitch = 0f;
		public float chargeSwitch = 0f;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Siren");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 60; //150
			NPC.npcSlots = 8f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 25;
			NPC.lifeMax = CalamityWorld.revenge ? 25000 : 21000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/SirenLure");
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("LeviathanBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("The trusted ally of The Leviathan. The Siren is able to attract unsuspecting prey then eat them.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 0.3f);
			Player player = Main.player[NPC.target];
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			bool playerWet = player.wet;
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1001 = 5f;
			float scaleFactor4 = 0.75f;
			int num1002 = 0;
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1006 = 0.333333343f;
			float num1007 = 10f;
			Vector2 vector = NPC.Center;
			Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
			bool isNotOcean = player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400));
			int npcType = Mod.Find<ModNPC>("Leviathan").Type;
			bool leviAlive = false;
			if (NPC.CountNPCS(npcType) > 0)
			{
				leviAlive = true;
			}
			float num1000 = leviAlive ? 14f : 18f;
			float num1005 = leviAlive ? 14f : 18f;
			num1006 *= num1005;
			if ((double)NPC.life <= (double)NPC.lifeMax * 0.5)
			{
				if (!spawnedLevi)
				{
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y - 200, Mod.Find<ModNPC>("SirenClone").Type);
					Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/LeviathanAndSiren");
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("Leviathan").Type);
					spawnedLevi = true;
				}
				phase2 = true;
			}
			int defenseMult = phase2 ? 2 : 3;
			int damageMult = phase2 ? 2 : 3;
			if (!leviAlive)
			{
				NPC.damage = NPC.defDamage * damageMult;
				NPC.defense = NPC.defDefense * defenseMult;
			}
			if ((double)NPC.life <= (double)NPC.lifeMax * 0.25)
			{
				if (!secondClone)
				{
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y - 200, Mod.Find<ModNPC>("SirenClone").Type);
					secondClone = true;
				}
				phase3 = true;
			}
			if (NPC.ai[3] == 0f && NPC.localAI[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				int num6 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("SirenIce").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.ai[3] = (float)(num6 + 1);
				NPC.localAI[1] = -1f;
				NPC.netUpdate = true;
				Main.npc[num6].ai[0] = (float)NPC.whoAmI;
				Main.npc[num6].netUpdate = true;
			}
			int num7 = (int)NPC.ai[3] - 1;
			if (num7 != -1 && Main.npc[num7].active && Main.npc[num7].type == Mod.Find<ModNPC>("SirenIce").Type)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = isNotOcean;
				NPC.ai[3] = 0f;
				if (NPC.localAI[1] == -1f)
				{
					NPC.localAI[1] = revenge ? 600f : 1200f;
				}
				if (NPC.localAI[1] > 0f)
				{
					NPC.localAI[1] -= 1f;
				}
			}
			if (isNotOcean)
			{
				NPC.alpha += 3;
				if (NPC.alpha >= 150)
				{
					NPC.alpha = 150;
				}
			}
			else
			{
				NPC.alpha -= 5;
				if (NPC.alpha <= 0)
				{
					NPC.alpha = 0;
				}
			}
			if (Main.rand.NextBool(300))
			{
				SoundEngine.PlaySound(SoundID.Zombie35, NPC.position);
			}
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
			else if (NPC.ai[0] == -1f)
			{
				int random = ((double)NPC.life <= (double)NPC.lifeMax * 0.5) ? 3 : 2;
				int num871 = Main.rand.Next(random);
				if (num871 == 0)
				{
					num871 = 0;
				}
				else if (num871 == 1)
				{
					num871 = 2;
				}
				else
				{
					num871 = 3;
				}
				NPC.ai[0] = (float)num871;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				return;
			}
			else if (NPC.ai[0] == 0f)
			{
				NPC.TargetClosest(true);
				NPC.rotation = NPC.velocity.X * 0.02f;
				NPC.spriteDirection = NPC.direction;
				float num1053 = leviAlive ? 5.5f : 7f;
				float num1054 = leviAlive ? 0.03f : 0.04f;
				Vector2 vector118 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1055 = player.position.X + (float)(player.width / 2) - vector118.X;
				float num1056 = player.position.Y + (float)(player.height / 2) - 200f - vector118.Y;
				float num1057 = (float)Math.Sqrt((double)(num1055 * num1055 + num1056 * num1056));
				if (num1057 < 400f)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					return;
				}
				num1057 = num1053 / num1057;
				if (NPC.velocity.X < num1055)
				{
					NPC.velocity.X = NPC.velocity.X + num1054;
					if (NPC.velocity.X < 0f && num1055 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num1054;
					}
				}
				else if (NPC.velocity.X > num1055)
				{
					NPC.velocity.X = NPC.velocity.X - num1054;
					if (NPC.velocity.X > 0f && num1055 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num1054;
					}
				}
				if (NPC.velocity.Y < num1056)
				{
					NPC.velocity.Y = NPC.velocity.Y + num1054;
					if (NPC.velocity.Y < 0f && num1056 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1054;
						return;
					}
				}
				else if (NPC.velocity.Y > num1056)
				{
					NPC.velocity.Y = NPC.velocity.Y - num1054;
					if (NPC.velocity.Y > 0f && num1056 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1054;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.rotation = NPC.velocity.X * 0.02f;
				NPC.localAI[0] = 0f;
				NPC.TargetClosest(true);
				Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1058 = player.position.X + (float)(player.width / 2) - vector120.X;
				float num1059 = player.position.Y + (float)(player.height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				if (revenge)
				{
					NPC.ai[1] += 1f;
				}
				if (!leviAlive)
				{
					NPC.ai[1] += 2f;
				}
				else
				{
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
					{
						NPC.ai[1] += 0.25f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						NPC.ai[1] += 0.25f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
					{
						NPC.ai[1] += 0.25f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
					{
						NPC.ai[1] += 0.25f;
					}
				}
				bool flag103 = false;
				if (NPC.ai[1] > 40f)
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, player.position, player.width, player.height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.Item85, NPC.position);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int num1061 = 371;
						int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.01f;
						Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.01f;
						Main.npc[num1062].localAI[0] = 60f;
						Main.npc[num1062].netUpdate = true;
						Main.npc[num1062].damage = leviAlive ? 60 : 70;
					}
				}
				if (num1060 > 400f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					float num1063 = leviAlive ? 9f : 10f;
					float num1064 = leviAlive ? 0.05f : 0.065f;
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
				if (NPC.ai[2] > 4f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.rotation = NPC.velocity.X * 0.02f;
				float num1065 = leviAlive ? 4f : 4.5f;
				float num1066 = leviAlive ? 0.05f : 0.065f;
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector122 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1067 = player.position.X + (float)(player.width / 2) - vector122.X;
				float num1068 = player.position.Y + (float)(player.height / 2) - 300f - vector122.Y;
				float num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if (!leviAlive)
				{
					if (NPC.ai[1] % 10f == 9f)
					{
						flag104 = true;
					}
				}
				else
				{
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
				}
				if (flag104 && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(vector121, 1, 1, player.position, player.width, player.height))
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						float num1070 = revenge ? 13f : 11f;
						if (isNotOcean || !leviAlive)
						{
							num1070 = revenge ? 19f : 18f;
						}
						else if (!playerWet)
						{
							num1070 = revenge ? 16f : 15f;
						}
						else
						{
							if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
							{
								num1070 += 4f;
							}
						}
						if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							num1070 += 1f;
						}
						float num1071 = player.position.X + (float)player.width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = player.position.Y + (float)player.height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = 30;
						int num1075 = Mod.Find<ModProjectile>("WaterSpear").Type;
						if (isNotOcean)
						{
							if (Main.rand.NextBool(6))
							{
								num1074 = 60;
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.NextBool(6))
							{
								num1074 = 55;
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = 50;
							}
						}
						else if (NPC.life >= (NPC.lifeMax * 0.75f))
						{
							if (Main.rand.NextBool(15))
							{
								num1074 = expertMode ? 25 : 30;
								if (playerWet)
								{
									num1074 = expertMode ? 22 : 26;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.NextBool(15))
							{
								num1074 = expertMode ? 23 : 26;
								if (playerWet)
								{
									num1074 = expertMode ? 20 : 22;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 21 : 26;
								if (playerWet)
								{
									num1074 = expertMode ? 18 : 21;
								}
							}
						}
						else if (NPC.life < (NPC.lifeMax * 0.75f) && NPC.life >= (NPC.lifeMax * 0.5f))
						{
							if (Main.rand.NextBool(12))
							{
								num1074 = expertMode ? 27 : 32;
								if (playerWet)
								{
									num1074 = expertMode ? 24 : 28;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.NextBool(12))
							{
								num1074 = expertMode ? 25 : 28;
								if (playerWet)
								{
									num1074 = expertMode ? 22 : 24;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 23 : 28;
								if (playerWet)
								{
									num1074 = expertMode ? 20 : 23;
								}
							}
						}
						else if (NPC.life < (NPC.lifeMax * 0.5f) && NPC.life >= (NPC.lifeMax * 0.25f))
						{
							if (Main.rand.NextBool(9))
							{
								num1074 = expertMode ? 29 : 34;
								if (playerWet)
								{
									num1074 = expertMode ? 26 : 30;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.NextBool(9))
							{
								num1074 = expertMode ? 27 : 30;
								if (playerWet)
								{
									num1074 = expertMode ? 24 : 26;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 25 : 30;
								if (playerWet)
								{
									num1074 = expertMode ? 22 : 25;
								}
							}
						}
						else if (NPC.life < (NPC.lifeMax * 0.25f))
						{
							if (Main.rand.NextBool(6))
							{
								num1074 = expertMode ? 31 : 36;
								if (playerWet)
								{
									num1074 = expertMode ? 28 : 32;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.NextBool(6))
							{
								num1074 = expertMode ? 29 : 32;
								if (playerWet)
								{
									num1074 = expertMode ? 26 : 28;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 27 : 32;
								if (playerWet)
								{
									num1074 = expertMode ? 24 : 27;
								}
							}
						}
						int num1076 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector121.X, vector121.Y, num1071, num1072, num1075, num1074 + (leviAlive ? 5 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num1076].timeLeft = 300;
					}
				}
				if (!Collision.CanHit(new Vector2(vector121.X, vector121.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					num1065 = leviAlive ? 7f : 10f;
					num1066 = leviAlive ? 0.05f : 0.065f;
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
				if (NPC.ai[1] > 800f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f) 
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
					NPC.damage = NPC.defDamage * 2;
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
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					chargeSwitch = 0f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			if (player.dead || Vector2.Distance(player.Center, vector) > 5600f)
			{
				if (NPC.localAI[3] < 120f) 
				{
					NPC.localAI[3] += 1f;
				}
				if (NPC.localAI[3] > 60f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + (NPC.localAI[3] - 60f) * 0.25f;
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
				return;
			}
			if (NPC.localAI[3] > 0f) 
			{
				NPC.localAI[3] -= 1f;
				return;
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void FindFrame(int frameHeight) //9 total frames
		{
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] == 2f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y >= frameHeight * 4)
				{
					NPC.frame.Y = 0;
				}
			}
			else if (NPC.ai[0] <= 1f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 4)
				{
					NPC.frame.Y = frameHeight * 4;
				}
				if (NPC.frame.Y >= frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 4;
				}
			}
			else
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 8;
				}
				if (NPC.frame.Y >= frameHeight * 12)
				{
					NPC.frame.Y = frameHeight * 8;
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			LeadingConditionRule noLevi = new LeadingConditionRule(new NoLevi());
            LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            noLevi.OnSuccess(new CommonDrop(ModContent.ItemType<EnchantedPearl>(), 1));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new NotDownedPlantera(), ModContent.ItemType<IOU>(), 1));
            noLevi.OnSuccess(new CommonDrop(ModContent.ItemType<LeviathanTrophy>(), 10));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<LeviathanMask>(), 7));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Atlantis>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Greentide>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Items.Weapons.Leviathan.BrackishFlask>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Items.Weapons.Leviathan.SirensSong>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<LureofEnthrallment>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Leviatitan>(), 4));
            npcLoot.Add(noLevi);
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
        	target.AddBuff(BuffID.Wet, 120, true);
        }
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
    }
    public class NoLevi : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Leviathan>());
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class NoSiren: IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Siren>());
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class NotDownedPlantera : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !NPC.downedPlantBoss;
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
}