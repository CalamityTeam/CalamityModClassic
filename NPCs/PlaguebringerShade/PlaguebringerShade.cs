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
using Terraria.WorldBuilding;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.PlaguebringerShade
{
	public class PlaguebringerShade : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Plaguebringer Shade");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 130; //150
			NPC.npcSlots = 1f;
			NPC.width = 66; //324
			NPC.height = 66; //216
			NPC.defense = 30;
			NPC.lifeMax = 8000; //250000
			NPC.knockBackResist = 0f;
			NPC.alpha = 50;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			AnimationType = NPCID.QueenBee;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("Through the Titanium on the goliath's armor, it is able to make shadow clones.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.05f, 0.15f, 0.025f);
			if (Main.expertMode)
			{
				int num1041 = (int)(30f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.damage = NPC.defDamage - num1041;
			}
			bool flag113 = false;
			if (!Main.player[NPC.target].ZoneJungle)
			{
				flag113 = true;
				if (NPC.timeLeft > 150)
				{
					NPC.timeLeft = 150;
				}
			}
			else
			{
				if (NPC.timeLeft > 750)
				{
					NPC.timeLeft = 750;
				}
			}
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
			bool dead4 = Main.player[NPC.target].dead;
			if (dead4 && Main.expertMode)
			{
				if ((double)NPC.position.Y < Main.worldSurface * 16.0 + 2000.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.04f;
				}
				if (NPC.position.X < (float)(Main.maxTilesX * 8))
				{
					NPC.velocity.X = NPC.velocity.X - 0.04f;
				}
				else
				{
					NPC.velocity.X = NPC.velocity.X + 0.04f;
				}
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
					return;
				}
			}
			else if (NPC.ai[0] == -1f)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
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
				int num1043 = 2; //2
				if (flag113)
				{
					num1043 += 1;
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
						float num1044 = 15f; //12
						if (flag113)
						{
							num1044 += 2f;
						}
						Vector2 vector117 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num1045 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector117.X;
						float num1046 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector117.Y;
						float num1047 = (float)Math.Sqrt((double)(num1045 * num1045 + num1046 * num1046));
						num1047 = num1044 / num1047;
						NPC.velocity.X = num1045 * num1047;
						NPC.velocity.Y = num1046 * num1047;
						NPC.spriteDirection = NPC.direction;
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
						return;
					}
					NPC.localAI[0] = 0f;
					float num1048 = 12.25f; //12
					float num1049 = 0.155f; //0.15
					if (flag113)
					{
						num1048 += 1f; //2
						num1049 += 0.075f; //0.1
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
					int num1050 = 500; //600
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
					float num1052 = 0.105f; //0.1
					if (flag113)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.075f;
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
				float num1053 = 12f; //12
				float num1054 = 0.1f; //0.07
				if (flag113)
				{
					num1054 = 0.12f;
				}
				Vector2 vector118 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1055 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector118.X;
				float num1056 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 200f - vector118.Y;
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
				NPC.localAI[0] = 0f;
				NPC.TargetClosest(true);
				Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
				float num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				bool flag103 = false;
				if (NPC.ai[1] > 10f)
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.NPCHit8, NPC.position);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int num1061;
						if (Main.rand.NextBool(4))
						{
							num1061 = Mod.Find<ModNPC>("PlagueBeeLargeG").Type;
						}
						else
						{
							num1061 = Mod.Find<ModNPC>("PlagueBeeG").Type;
						}
						int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.005f;
						Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.005f;
						Main.npc[num1062].localAI[0] = 60f;
						Main.npc[num1062].netUpdate = true;
					}
				}
				if (num1060 > 400f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					float num1063 = 14.5f; //changed from 14
					float num1064 = 0.105f; //changed from 0.1
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
					NPC.velocity *= 0.9f; //changed from 0.9
				}
				NPC.spriteDirection = NPC.direction;
				if (NPC.ai[2] > 2f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				float num1065 = 7f; //changed from 4
				float num1066 = 0.075f; //changed from 0.05
				if (flag113)
				{
					num1066 = 0.09f;
					num1065 = 8f;
				}
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector122 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1067 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector122.X;
				float num1068 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 300f - vector122.Y;
				float num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if (NPC.ai[1] % 35f == 34f)
				{
					flag104 = true;
				}
				if (flag104 && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y && Collision.CanHit(vector121, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					SoundEngine.PlaySound(SoundID.Item42, NPC.position);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						float num1070 = 10.5f; //changed from 8
						if (flag113)
						{
							num1070 += 2f;
						}
						float num1071 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = 20; //projectile damage
						int num1075 = Mod.Find<ModProjectile>("PlagueStingerGoliath").Type; //projectile type
						if (Main.rand.NextBool(15))
						{
							num1074 = 25;
							num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
						}
						int num1076 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num1076].timeLeft = 300;
					}
				}
				if (!Collision.CanHit(new Vector2(vector121.X, vector121.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					num1065 = 14.5f; //changed from 14
					num1066 = 0.105f; //changed from 0.1
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
				if (NPC.ai[1] > 600f)
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Poisoned, hit.HitDirection, -1f, 0, default(Color), 1f);
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Poisoned, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Poisoned, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Poisoned, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, true);
		}
	}
}