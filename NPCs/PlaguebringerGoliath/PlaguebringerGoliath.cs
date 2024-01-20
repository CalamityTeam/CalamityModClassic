using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.PlaguebringerGoliath;
using CalamityModClassic1Point2.Items.Weapons.Plaguebringer;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.PlaguebringerGoliath
{
	[AutoloadBossHead]
	public class PlaguebringerGoliath : ModNPC
	{
		public const int MissileProjectiles = 30;
		public const float MissileAngleSpread = 170;
		public int MissileCountdown = 0;
		public int despawnTimer = 600;
		public int halfLife = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Plaguebringer Goliath");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 190; //150
			NPC.npcSlots = 16f;
			NPC.width = 66; //324
			NPC.height = 66; //216
			NPC.defense = 55;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 56000 : 48000;
			NPC.knockBackResist = 0f;
			NPC.scale = 1.5f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			AnimationType = NPCID.QueenBee;
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 25, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/EyeofCthulhu");
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("PlaguebringerGoliathBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("A plagued queen bee covered in Titanium-Chlorophyte plating.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld1Point2.revenge;
			bool expertMode = Main.expertMode;
			Player player = Main.player[NPC.target];
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.15f, 0.35f, 0.05f);
			if (halfLife == 0 && (NPC.life <= NPC.lifeMax * 0.5f))
			{
				string key = "PLAGUE NUKE BARRAGE ARMED, PREPARING FOR LAUNCH!!!";
				Color messageColor = Color.Lime;
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText((key), messageColor);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
				}
				halfLife++;
			}
			if (expertMode)
			{
				if (NPC.ai[3] == 0f && NPC.life > 0)
				{
					NPC.ai[3] = (float)NPC.lifeMax;
				}
	        	if (NPC.life > 0)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int num660 = (int)((double)NPC.lifeMax * 0.3);
						if ((float)(NPC.life + num660) < NPC.ai[3])
						{
							NPC.ai[3] = (float)NPC.life;
							int num661 = Main.rand.Next(1, 2);
							for (int num662 = 0; num662 < num661; num662++)
							{
								int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
								int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
								int num663 = Mod.Find<ModNPC>("PlaguebringerShade").Type;
								int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
								if (Main.netMode == NetmodeID.Server && num664 < 200)
								{
									NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
								}
							}
							return;
						}
					}
				}
			}
			if (NPC.life <= (NPC.lifeMax * 0.5f) && MissileCountdown == 0)
			{
				if (Main.rand.NextBool(2000))
				{
					string key = "MISSILES LAUNCHED, TARGETING ROUTINE INITIATED!!!";
					Color messageColor = Color.Lime;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
					MissileCountdown = 300;
				}
			}
			if (MissileCountdown > 0)
			{
				MissileCountdown--;
				if (MissileCountdown == 0)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						for (int playerIndex = 0; playerIndex < 255; playerIndex++)
						{
							if (Main.player[playerIndex].active)
							{
								Player player1 = Main.player[playerIndex];
								int speed = revenge ? 7 : 5;
								float spawnX = Main.rand.Next(1000) - 500 + player1.Center.X;
								float spawnY = -1000 + player1.Center.Y;
								Vector2 baseSpawn = new Vector2(spawnX, spawnY);
								Vector2 baseVelocity = player1.Center - baseSpawn;
								baseVelocity.Normalize();
								baseVelocity = baseVelocity * speed;
								int damage = expertMode ? 60 : 69;
								for (int i = 0; i < MissileProjectiles; i++)
								{
									Vector2 spawn = baseSpawn;
									spawn.X = spawn.X + i * 30 - (MissileProjectiles * 15);
									Vector2 velocity = baseVelocity;
									velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-MissileAngleSpread / 2 + (MissileAngleSpread * i / (float)MissileProjectiles)));
									velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
									int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn.X, spawn.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("HiveBombGoliath").Type, damage, 10f, Main.myPlayer, 0f, 0f);
									Main.projectile[projectile].hostile = true;
								}
							}
						}
					}
				}
			}
			bool flag113 = false;
			if (!player.ZoneJungle)
			{
				flag113 = true;
			}
			if (flag113)
			{
				despawnTimer--;
				if (despawnTimer <= 0)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						for (int playerIndex = 0; playerIndex < 255; playerIndex++)
						{
							if (Main.player[playerIndex].active)
							{
								Player player2 = Main.player[playerIndex];
								int speed = 25;
								float spawnX = Main.rand.Next(1000) - 500 + player2.Center.X;
								float spawnY = -1000 + player2.Center.Y;
								Vector2 baseSpawn = new Vector2(spawnX, spawnY);
								Vector2 baseVelocity = player2.Center - baseSpawn;
								baseVelocity.Normalize();
								baseVelocity = baseVelocity * speed;
								for (int i = 0; i < MissileProjectiles + 70; i++)
								{
									Vector2 spawn = baseSpawn;
									spawn.X = spawn.X + i * 30 - (MissileProjectiles * 15);
									Vector2 velocity = baseVelocity;
									velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-MissileAngleSpread / 2 + (MissileAngleSpread * i / (float)MissileProjectiles)));
									velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
									int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn.X, spawn.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("HiveBombGoliath").Type, 100000, 10f, Main.myPlayer, 0f, 0f);
									Main.projectile[projectile].hostile = true;
								}
							}
						}
					}
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
					}
				}
			}
			else
			{
				despawnTimer = 600;
			}
			int num1038 = 0;
			for (int num1039 = 0; num1039 < 255; num1039++)
			{
				if (Main.player[num1039].active && !Main.player[num1039].dead && (NPC.Center - Main.player[num1039].Center).Length() < 1000f)
				{
					num1038++;
				}
			}
			if (expertMode)
			{
				int num1041 = (int)(65f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.damage = NPC.defDamage - num1041;
				int num1040 = (int)(75f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.defense = NPC.defDefense + num1040;
			}
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
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
				int num1043 = 2; //2 not a prob
				if (revenge)
				{
					num1043++;
				}
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
					if (Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (player.position.Y + (float)(player.height / 2))) < 20f)
					{
						NPC.localAI[0] = 1f;
						NPC.ai[1] += 1f;
						NPC.ai[2] = 0f;
						float num1044 = revenge ? 28f : 26f; //16
						if (flag113)
						{
							num1044 += 6f;
						}
						else
						{
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
						}
						Vector2 vector117 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num1045 = player.position.X + (float)(player.width / 2) - vector117.X;
						float num1046 = player.position.Y + (float)(player.height / 2) - vector117.Y;
						float num1047 = (float)Math.Sqrt((double)(num1045 * num1045 + num1046 * num1046));
						num1047 = num1044 / num1047;
						NPC.velocity.X = num1045 * num1047;
						NPC.velocity.Y = num1046 * num1047;
						NPC.spriteDirection = NPC.direction;
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
						return;
					}
					NPC.localAI[0] = 0f;
					float num1048 = revenge ? 16f : 15f; //12 not a prob
					float num1049 = revenge ? 0.195f : 0.1875f; //0.15 not a prob
					if (flag113)
					{
						num1048 += 1.5f; //2 not a prob
						num1049 += 0.1f; //0.1 not a prob
					}
					else
					{
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							num1048 += 1.25f; //1 not a prob
							num1049 += 0.0625f; //0.05 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							num1048 += 1.25f; //1 not a prob
							num1049 += 0.0625f; //0.05 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							num1048 += 2.5f; //2 not a prob
							num1049 += 0.0625f; //0.05 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							num1048 += 2.5f; //2 not a prob
							num1049 += 0.125f; //0.1 not a prob
						}
					}
					if (NPC.position.Y + (float)(NPC.height / 2) < player.position.Y + (float)(player.height / 2))
					{
						NPC.velocity.Y = NPC.velocity.Y + num1049;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - num1049;
					}
					if (NPC.velocity.Y < -15f)
					{
						NPC.velocity.Y = -num1048;
					}
					if (NPC.velocity.Y > 15f)
					{
						NPC.velocity.Y = num1048;
					}
					if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (player.position.X + (float)(player.width / 2))) > 600f)
					{
						NPC.velocity.X = NPC.velocity.X + 0.15f * (float)NPC.direction;
					}
					else if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (player.position.X + (float)(player.width / 2))) < 300f)
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
					int num1050 = 500; //600 not a prob
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
					{
						num1050 = revenge ? 300 : 350; //300 not a prob
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
					{
						num1050 = revenge ? 350 : 375; //450 not a prob
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num1050 = 400; //500 not a prob
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
					{
						num1050 = 450; //550 not a prob
					}
					int num1051 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)(player.width / 2))
					{
						num1051 = -1;
					}
					if (NPC.direction == num1051 && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (player.position.X + (float)(player.width / 2))) > (float)num1050)
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
					float num1052 = revenge ? 0.13f : 0.115f; //0.1
					if (flag113)
					{
						num1052 += 0.1f;
					}
					else
					{
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
				float num1053 = 10.5f; //12 found one!  dictates speed during bee spawn
				float num1054 = 0.113f; //0.1 found one!  dictates speed during bee spawn
				if (flag113)
				{
					num1054 = 0.15f;
				}
				Vector2 vector118 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1055 = player.position.X + (float)(player.width / 2) - vector118.X;
				float num1056 = player.position.Y + (float)(player.height / 2) - 200f - vector118.Y;
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
				float num1058 = player.position.X + (float)(player.width / 2) - vector120.X;
				float num1059 = player.position.Y + (float)(player.height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				if (revenge)
				{
					NPC.ai[1] += 0.25f;
				}
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
					NPC.ai[1] += 0.3f; //0.25 not a prob
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					NPC.ai[1] += 0.3f; //0.25 not a prob
				}
				bool flag103 = false;
				if (NPC.ai[1] > 10f) //changed from 40 not a prob
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, player.position, player.width, player.height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.NPCHit8, NPC.position);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int randomAmt = expertMode ? 2 : 4;
						int num1061;
						if (Main.rand.NextBool(randomAmt))
						{
							num1061 = Mod.Find<ModNPC>("PlagueBeeLargeG").Type;
						}
						else
						{
							num1061 = Mod.Find<ModNPC>("PlagueBeeG").Type;
						}
						int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.02f;
						Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.02f;
						Main.npc[num1062].localAI[0] = 60f;
						Main.npc[num1062].netUpdate = true;
					}
				}
				if (num1060 > 400f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					float num1063 = 12f; //changed from 14 not a prob
					float num1064 = 0.0857f; //changed from 0.1 not a prob
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
					NPC.ai[0] = -1f;
					NPC.ai[1] = 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				float num1065 = 5.5f; //changed from 6 to 7.5 modifies speed while firing projectiles
				float num1066 = 0.0688f; //changed from 0.075 to 0.09375 modifies speed while firing projectiles
				if (flag113)
				{
					num1065 = 9f;
					num1066 = 0.1f;
				}
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector122 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1067 = player.position.X + (float)(player.width / 2) - vector122.X;
				float num1068 = player.position.Y + (float)(player.height / 2) - 300f - vector122.Y;
				float num1069 = (float)Math.Sqrt((double)(num1067 * num1067 + num1068 * num1068));
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.025)
				{
					if (NPC.ai[1] % 5f == 4f)
					{
						flag104 = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					if (NPC.ai[1] % 10f == 9f)
					{
						flag104 = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.3)
				{
					if (NPC.ai[1] % 15f == 14f)
					{
						flag104 = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					if (NPC.ai[1] % 20f == 19f)
					{
						flag104 = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
				{
					if (NPC.ai[1] % 25f == 24f)
					{
						flag104 = true;
					}
				}
				else if (NPC.ai[1] % 30f == 29f)
				{
					flag104 = true;
				}
				if (flag104 && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(vector121, 1, 1, player.position, player.width, player.height))
				{
					SoundEngine.PlaySound(SoundID.Item42, NPC.position);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						float num1070 = revenge ? 17f : 15f; //changed from 10
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							num1070 += 5f; //changed from 3 not a prob
						}
						if (flag113)
						{
							num1070 += 10f;
						}
						float num1071 = player.position.X + (float)player.width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = player.position.Y + (float)player.height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = 50; //projectile damage
						int num1075 = Mod.Find<ModProjectile>("PlagueStingerGoliath").Type; //projectile type
						if (expertMode)
						{
							num1075 = Mod.Find<ModProjectile>("PlagueStingerGoliathV2").Type;
							num1074 = 43;
							if (NPC.life >= (NPC.lifeMax * 0.75f))
							{
								if (Main.rand.NextBool(13))
								{
									num1074 = 48;
									num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
								}
							}
							else if (NPC.life < (NPC.lifeMax * 0.75f) && NPC.life >= (NPC.lifeMax * 0.5f))
							{
								if (Main.rand.NextBool(9))
								{
									num1074 = 50;
									num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
								}
								else
								{
									num1074 = 45;
								}
							}
							else if (NPC.life < (NPC.lifeMax * 0.5f) && NPC.life >= (NPC.lifeMax * 0.25f))
							{
								if (Main.rand.NextBool(5))
								{
									num1074 = 52;
									num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
								}
								else
								{
									num1074 = 47;
								}
							}
							else if (NPC.life < (NPC.lifeMax * 0.25f))
							{
								if (Main.rand.NextBool(3))
								{
									num1074 = 54;
									num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
								}
								else
								{
									num1074 = 49;
								}
							}
						}
						else
						{
							if (Main.rand.NextBool(13))
							{
								num1074 = 70;
								num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
							}
						}
						int num1076 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num1076].timeLeft = 300;
						Main.projectile[num1076].tileCollide = true;
					}
				}
				if (!Collision.CanHit(new Vector2(vector121.X, vector121.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					num1065 = 12f; //changed from 14 not a prob
					num1066 = 0.0857f; //changed from 0.1 not a prob
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
				if (NPC.ai[1] > 400f)
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
			for (int k = 0; k < 3; k++)
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
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<PlaguebringerGoliathTrophy>(), 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<BloomStone>(), 10));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PlaguebringerGoliathBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PlaguebringerGoliathMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PlagueCellCluster>(), 1, 10, 14));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MepheticSprayer>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Malevolence>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VirulentKatana>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Plaguebringer.DiseasedPike>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PestilentDefiler>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Plaguebringer.ThePlaguebringer>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TheHive>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PlagueStaff>(), 4));
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 180, true);
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}