using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Weapons;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Leviathan;
using CalamityModClassic1Point1.Items.Placeables;
using CalamityModClassic1Point1.Items.Armor;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Leviathan
{
	[AutoloadBossHead]
	public class Siren : ModNPC
	{
		public static bool phase2 = false;
		public static bool phase3 = false;
		
		public override void SetDefaults()
		{
			//NPC.name = "Siren");
			//Tooltip.SetDefault("Siren Lure");
			NPC.damage = 75; //150
			NPC.npcSlots = 5f;
			NPC.width = 100; //324
			NPC.height = 150; //216
			NPC.defense = 35;
			NPC.lifeMax = 39000; //250000
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			Main.npcFrameCount[NPC.type] = 12;
			NPC.boss = true;
			NPC.timeLeft = NPC.activeTime * 30;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss3;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("An extension of the mighty Leviathan. The Leviathan is able to masterfully manipulate her lure as if it were its own creature.")

            });
        }
        public override void AI()
		{
			int currentLifeP2 = (int)(NPC.lifeMax * (2 / 3));
			int currentLifeP3 = (int)(NPC.lifeMax * (1 / 3));
			if (NPC.life <= currentLifeP2)
			{
				phase2 = true;
			}
			if (NPC.life <= currentLifeP3)
			{
				phase3 = true;
			}
			if (Main.rand.Next(300) == 0)
			{
				SoundEngine.PlaySound(SoundID.Zombie35, NPC.position);
			}
			Player player = Main.player[NPC.target];
			bool playerWet = player.wet;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 0.3f);
			int num1038 = 0;
			for (int num1039 = 0; num1039 < 255; num1039++)
			{
				if (Main.player[num1039].active && !Main.player[num1039].dead && (NPC.Center - Main.player[num1039].Center).Length() < 1000f)
				{
					num1038++;
				}
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			if (Main.player[NPC.target].dead)
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
			else if (NPC.ai[0] == -1f)
			{
				if (Main.netMode != 1)
				{
					float num1041 = NPC.ai[1];
					int num1042;
					do
					{
						num1042 = Main.rand.Next(3);
						if (num1042 == 1)
						{
							num1042 = 2;
						}
						else if (num1042 == 2)
						{
							num1042 = 3;
						}
					}
					while ((float)num1042 == num1041);
					NPC.ai[0] = (float)num1042;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					return;
				}
			}
			else if (NPC.ai[0] == 0f)
			{
				int num1043 = 2; //2 not a prob
				if (NPC.life < NPC.lifeMax / 2)
				{
					num1043++;
				}
				if (NPC.life < NPC.lifeMax / 3)
				{
					num1043++;
				}
				if (NPC.life < NPC.lifeMax / 5)
				{
					num1043++;
				}
				if (NPC.ai[1] > (float)(2 * num1043) && NPC.ai[1] % 2f == 0f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
				if (NPC.ai[1] % 2f == 0f)
				{
					NPC.TargetClosest(true);
					if (Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2))) < 20f)
					{
						NPC.localAI[0] = 1f;
						NPC.ai[1] += 1f;
						NPC.ai[2] = 0f;
						float num1044 = 16f; //16
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							num1044 += 2f; //2 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							num1044 += 2f; //2 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							num1044 += 2f; //2 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							num1044 += 2f; //2 not a prob
						}
						Vector2 vector117 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num1045 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector117.X;
						float num1046 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector117.Y;
						float num1047 = (float)Math.Sqrt((double)(num1045 * num1045 + num1046 * num1046));
						num1047 = num1044 / num1047;
						NPC.velocity.X = num1045 * num1047;
						NPC.velocity.Y = num1046 * num1047;
						NPC.spriteDirection = NPC.direction;
						SoundEngine.PlaySound(SoundID.Zombie34, NPC.position);
						return;
					}
					NPC.localAI[0] = 0f;
					float num1048 = 12f; //12 not a prob
					float num1049 = 0.15f; //0.15 not a prob
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
					{
						num1048 += 1f; //1 not a prob
						num1049 += 0.05f; //0.05 not a prob
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num1048 += 1f; //1 not a prob
						num1049 += 0.05f; //0.05 not a prob
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
					{
						num1048 += 2f; //2 not a prob
						num1049 += 0.05f; //0.05 not a prob
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
					{
						num1048 += 2f; //2 not a prob
						num1049 += 0.1f; //0.1 not a prob
					}
					if (NPC.position.Y + (float)(NPC.height / 2) < Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2))
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
					int num1050 = 600; //600 not a prob
					if (!playerWet)
					{
						num1050 = 350;
					}
					else
					{
						num1050 = 600;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							num1050 = 800; //300 not a prob
						}
						else if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							num1050 = 750; //450 not a prob
						}
						else if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							num1050 = 700; //500 not a prob
						}
						else if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							num1050 = 650; //550 not a prob
						}
					}
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
						NPC.localAI[0] = 1f;
						return;
					}
					NPC.TargetClosest(true);
					NPC.spriteDirection = NPC.direction;
					NPC.localAI[0] = 0f;
					NPC.velocity *= 0.9f;
					float num1052 = 0.1f; //0.1
					if (NPC.life < NPC.lifeMax / 2)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (NPC.life < NPC.lifeMax / 3)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (NPC.life < NPC.lifeMax / 5)
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
			else if (NPC.ai[0] == 2f)
			{
				NPC.TargetClosest(true);
				NPC.spriteDirection = NPC.direction;
				float num1053 = 12f; //12 found one!  dictates speed during bee spawn
				float num1054 = 0.1f; //0.1 found one!  dictates speed during bee spawn
				Vector2 vector118 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1055 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector118.X;
				float num1056 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 200f - vector118.Y;
				float num1057 = (float)Math.Sqrt((double)(num1055 * num1055 + num1056 * num1056));
				if (num1057 < 800f)
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
				NPC.localAI[0] = 0f;
				NPC.TargetClosest(true);
				Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
				float num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
				{
					NPC.ai[1] += 0.25f; //0.25 not a prob
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					NPC.ai[1] += 0.25f; //0.25 not a prob
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
				{
					NPC.ai[1] += 0.25f; //0.25 not a prob
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					NPC.ai[1] += 0.25f; //0.25 not a prob
				}
				bool flag103 = false;
				if (NPC.ai[1] > 40f) //changed from 40 not a prob
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.NPCHit25, NPC.position);
					if (Main.netMode != 1)
					{
						int num1061;
						if (Main.rand.Next(4) == 0)
						{
							num1061 = Mod.Find<ModNPC>("AquaticAberration").Type; //Aquatic entity spawns
						}
						else
						{
							num1061 = Mod.Find<ModNPC>("Parasea").Type;
						}
						int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.01f;
						Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.01f;
						Main.npc[num1062].localAI[0] = 60f;
						Main.npc[num1062].netUpdate = true;
					}
				}
				if (num1060 > 400f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					float num1063 = 14f; //changed from 14 not a prob
					float num1064 = 0.1f; //changed from 0.1 not a prob
					vector120 = vector119;
					num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
					num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
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
					NPC.ai[0] = -1f;
					NPC.ai[1] = 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				float num1065 = 6f; //changed from 6 to 7.5 modifies speed while firing projectiles
				float num1066 = 0.075f; //changed from 0.075 to 0.09375 modifies speed while firing projectiles
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector122 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1067 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector122.X;
				float num1068 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 300f - vector122.Y;
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
				if (flag104 && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y && Collision.CanHit(vector121, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					if (Main.netMode != 1)
					{
						float num1070 = 10f; //changed from 10
						if (!playerWet)
						{
							num1070 = 14f;
						}
						else
						{
							num1070 = 10f;
							if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
							{
								num1070 += 4f; //changed from 3 not a prob
							}
						}
						float num1071 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = 30; //projectile damage
						int num1075 = Mod.Find<ModProjectile>("WaterSpear").Type; //projectile type
						bool expertMode = Main.expertMode;
						if (NPC.life <= (NPC.lifeMax * 1f) && NPC.life >= (NPC.lifeMax * 0.75f))
						{
							if (Main.rand.Next(15) == 0)
							{
								num1074 = expertMode ? 27 : 50;
								if (playerWet)
								{
									num1074 = expertMode ? 22 : 40;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.Next(15) == 0)
							{
								num1074 = expertMode ? 21 : 38;
								if (playerWet)
								{
									num1074 = expertMode ? 16 : 28;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 19 : 35;
								if (playerWet)
								{
									num1074 = expertMode ? 14 : 25;
								}
								num1075 = Mod.Find<ModProjectile>("WaterSpear").Type;
							}
						}
						else if (NPC.life < (NPC.lifeMax * 0.75f) && NPC.life >= (NPC.lifeMax * 0.5f))
						{
							if (Main.rand.Next(12) == 0)
							{
								num1074 = expertMode ? 29 : 52;
								if (playerWet)
								{
									num1074 = expertMode ? 24 : 42;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.Next(12) == 0)
							{
								num1074 = expertMode ? 23 : 40;
								if (playerWet)
								{
									num1074 = expertMode ? 18 : 30;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 21 : 37;
								if (playerWet)
								{
									num1074 = expertMode ? 16 : 27;
								}
								num1075 = Mod.Find<ModProjectile>("WaterSpear").Type;
							}
						}
						else if (NPC.life < (NPC.lifeMax * 0.5f) && NPC.life >= (NPC.lifeMax * 0.25f))
						{
							if (Main.rand.Next(9) == 0)
							{
								num1074 = expertMode ? 30 : 54;
								if (playerWet)
								{
									num1074 = expertMode ? 25 : 44;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.Next(9) == 0)
							{
								num1074 = expertMode ? 25 : 42;
								if (playerWet)
								{
									num1074 = expertMode ? 20 : 32;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 22 : 39;
								if (playerWet)
								{
									num1074 = expertMode ? 17 : 29;
								}
								num1075 = Mod.Find<ModProjectile>("WaterSpear").Type;
							}
						}
						else if (NPC.life < (NPC.lifeMax * 0.25f))
						{
							if (Main.rand.Next(6) == 0)
							{
								num1074 = expertMode ? 31 : 57;
								if (playerWet)
								{
									num1074 = expertMode ? 26 : 47;
								}
								num1075 = Mod.Find<ModProjectile>("SirenSong").Type;
							}
							else if (Main.rand.Next(6) == 0)
							{
								num1074 = expertMode ? 27 : 45;
								if (playerWet)
								{
									num1074 = expertMode ? 22 : 35;
								}
								num1075 = Mod.Find<ModProjectile>("FrostMist").Type;
							}
							else
							{
								num1074 = expertMode ? 25 : 42;
								if (playerWet)
								{
									num1074 = expertMode ? 20 : 32;
								}
								num1075 = Mod.Find<ModProjectile>("WaterSpear").Type;
							}
						}
						int num1076 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num1076].timeLeft = 300;
					}
				}
				if (!Collision.CanHit(new Vector2(vector121.X, vector121.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					num1065 = 14f; //changed from 14 not a prob
					num1066 = 0.1f; //changed from 0.1 not a prob
					vector122 = vector121;
					num1067 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector122.X;
					num1068 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector122.Y;
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
					NPC.ai[1] = 3f;
					NPC.netUpdate = true;
					return;
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
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.localAI[0] == 1f)
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
			else
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
				if (NPC.frame.Y >= frameHeight * 12)
				{
					NPC.frame.Y = frameHeight * 4;
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "The Leviathan";
			potionType = ItemID.GreaterHealingPotion;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EnchantedPearl>(), 1));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<LeviathanTrophy>(), 10));
            LeadingConditionRule plantoreum = new LeadingConditionRule(new Conditions.DownedPlantera());
            plantoreum.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<LeviathanBag>()));
            plantoreum.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LeviathanMask>(), 7));
            plantoreum.OnSuccess(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Atlantis>(), ModContent.ItemType<BrackishFlask>(), ModContent.ItemType<Leviatitan>(), ModContent.ItemType<LureofEnthrallment>(), ModContent.ItemType<SirensSong>(), ModContent.ItemType<Greentide>() }));
            npcLoot.Add(plantoreum);
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
        	target.AddBuff(BuffID.Wet, 120, true);
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
	}
}