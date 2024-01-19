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
using CalamityModClassic1Point1.Items.Providence;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.DevourerMunsters;
using CalamityModClassic1Point1.Items.Weapons;
using CalamityModClassic1Point1.Items;
using Terraria.UI;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Providence
{
	[AutoloadBossHead]
	public class Providence : ModNPC
	{
		public int text = 0;
		public float bossLife;
		
		public override void SetDefaults()
		{
			//NPC.name = "Providence, the Profaned God");
			//Tooltip.SetDefault("Providence, the Profaned God");
			NPC.npcSlots = 10f;
			NPC.damage = 90;
			NPC.width = 150;
			NPC.height = 200;
			NPC.scale = 2f;
			NPC.defense = 100;
			NPC.lifeMax = 350000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			Main.npcFrameCount[NPC.type] = 9; //change
			NPC.value = Item.buyPrice(3, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.chaseable = true;
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ProvidenceTheme");
			NPC.HitSound = SoundID.NPCHit44;
			NPC.DeathSound = SoundID.NPCDeath46;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("The Profaned Goddess herself... or well, \"God\". It was meant to play on her theme of oxymorons like how she's simultaneously both holy and unholy, etc.")

            });
        }

        public override void AI()
		{
			CalamityGlobalNPC.holyBoss = NPC.whoAmI;
			bool bossBuff = CalamityGlobalNPC.bossBuff;
			bool superBossBuff = CalamityGlobalNPC.superBossBuff;
			bool expertMode = Main.expertMode;
			bool isHoly = Main.player[NPC.target].ZoneHallow;
			bool isHell = Main.player[NPC.target].ZoneUnderworldHeight;
			NPC.dontTakeDamage = (isHoly || isHell) ? false : true;
			Player player = Main.player[NPC.target];
			if (player.dead)
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
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.075);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						int guardianSpawn = 1;
						for (int num662 = 0; num662 < guardianSpawn; num662++)
						{
							int spawnType = Main.rand.Next(3);
							if (spawnType == 0)
							{
								spawnType = Mod.Find<ModProjectile>("ProvSpawnOffense").Type;
							}
							else if (spawnType == 1)
							{
								spawnType = Mod.Find<ModProjectile>("ProvSpawnDefense").Type;
							}
							else
							{
								spawnType = Mod.Find<ModProjectile>("ProvSpawnHealer").Type;
							}
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, spawnType, 0, 0f, Main.myPlayer, 0f, 0f);
						}
						return;
					}
				}
	       	}
			if (NPC.ai[0] == 0f) 
			{
				NPC.defense = 100;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f) 
				{
					NPC.TargetClosest(true);
					if (NPC.Center.X < player.Center.X) 
					{
						NPC.ai[2] = 1f;
					} 
					else 
					{
						NPC.ai[2] = -1f;
					}
				}
				NPC.TargetClosest(true);
				int num851 = 800;
				float num852 = Math.Abs(NPC.Center.X - player.Center.X);
				if (NPC.Center.X < player.Center.X && NPC.ai[2] < 0f && num852 > (float)num851) 
				{
					NPC.ai[2] = 0f;
				}
				if (NPC.Center.X > player.Center.X && NPC.ai[2] > 0f && num852 > (float)num851) 
				{
					NPC.ai[2] = 0f;
				}
				float num853 = expertMode ? 1.5f : 1.45f;
				float num854 = expertMode ? 20f : 19f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
				{
					num853 = expertMode ? 1.55f : 1.5f;
					num854 = expertMode ? 21f : 20f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num853 = expertMode ? 1.6f : 1.55f;
					num854 = expertMode ? 22f : 21f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
				{
					num853 = expertMode ? 1.7f : 1.6f;
					num854 = expertMode ? 24f : 23f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					num853 = expertMode ? 1.8f : 1.7f;
					num854 = expertMode ? 28f : 27f;
				}
				NPC.velocity.X = NPC.velocity.X + NPC.ai[2] * num853;
				if (NPC.velocity.X > num854) 
				{
					NPC.velocity.X = num854;
				}
				if (NPC.velocity.X < -num854) 
				{
					NPC.velocity.X = -num854;
				}
				float num855 = player.position.Y - (NPC.position.Y + (float)NPC.height);
				if (num855 < 200f) //150
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.2f;
				}
				if (num855 > 250f) //200
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.2f;
				}
				if (NPC.velocity.Y > 8f) 
				{
					NPC.velocity.Y = 8f;
				}
				if (NPC.velocity.Y < -8f) 
				{
					NPC.velocity.Y = -8f;
				}
				if ((num852 < 500f || NPC.ai[3] < 0f) && NPC.position.Y < player.position.Y) 
				{
					NPC.ai[3] += 1f;
					int num856 = expertMode ? 11 : 12;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
					{
						num856 = expertMode ? 10 : 11;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num856 = expertMode ? 9 : 10;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num856 = expertMode ? 8 : 9;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num856 = expertMode ? 6 : 8;
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
						float num860 = expertMode ? 9f : 8f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
						{
							num860 = expertMode ? 10.25f : 9f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
						{
							num860 = expertMode ? 11.5f : 10f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
						{
							num860 = expertMode ? 12.75f : 11f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
						{
							num860 = expertMode ? 14f : 12f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 70 : 90;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector112.X, vector112.Y, num857, num858, Mod.Find<ModProjectile>("HolyBlast").Type, holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				} 
				else if (NPC.ai[3] < 0f) 
				{
					NPC.ai[3] += 1f;
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 900f && num852 < 600f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 1f) 
			{
				NPC.defense = 100;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.chaseable = true;
				NPC.TargetClosest(true);
				float num861 = expertMode ? 0.35f : 0.32f;
				float num862 = expertMode ? 17f : 16f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
				{
					num861 = expertMode ? 0.36f : 0.33f;
					num862 = expertMode ? 18f : 17f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num861 = expertMode ? 0.38f : 0.35f;
					num862 = expertMode ? 19f : 18f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
				{
					num861 = expertMode ? 0.4f : 0.37f;
					num862 = expertMode ? 20f : 19f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					num861 = expertMode ? 0.45f : 0.4f;
					num862 = expertMode ? 22f : 20f;
				}
				num861 -= 0.05f;
				num862 -= 1f;
				if (NPC.Center.X < player.Center.X) 
				{
					NPC.velocity.X = NPC.velocity.X + num861;
					if (NPC.velocity.X < 0f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
				}
				if (NPC.Center.X > player.Center.X) 
				{
					NPC.velocity.X = NPC.velocity.X - num861;
					if (NPC.velocity.X > 0f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
				}
				if (NPC.velocity.X > num862 || NPC.velocity.X < -num862) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.95f;
				}
				float num863 = player.position.Y - (NPC.position.Y + (float)NPC.height);
				if (num863 < 280f) //180
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
				}
				if (num863 > 300f) //200
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
				}
				if (NPC.velocity.Y > 6f) 
				{
					NPC.velocity.Y = 6f;
				}
				if (NPC.velocity.Y < -6f) 
				{
					NPC.velocity.Y = -6f;
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[3] += 1f;
					int num864 = expertMode ? 28 : 30;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
					{
						num864 = expertMode ? 26 : 29;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num864 = expertMode ? 23 : 27;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num864 = expertMode ? 20 : 24;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num864 = expertMode ? 15 : 18;
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
							int fireDamage = expertMode ? 50 : 75;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector113.X, vector113.Y, speedX2, num865, Mod.Find<ModProjectile>("HolyFire").Type, fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 900f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 2f) 
			{
				NPC.defense = 99999;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.chaseable = false;
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
				int num870 = expertMode ? 12 : 15;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
				{
					num870--;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num870 -= 2;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
				{
					num870 -= expertMode ? 4 : 3;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					num870 -= expertMode ? 5 : 4;
				}
				if (NPC.ai[3] > (float)num870) 
				{
					NPC.ai[3] = 0f;
					if (Main.rand.Next(3) == 0)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector114.X, vector114.Y, num866, num867, Mod.Find<ModProjectile>("HolyLight").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					}
					else
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector114.X, vector114.Y, num866, num867, Mod.Find<ModProjectile>("HolyBurnOrb").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 750f && text == 0)
					{
						text++;
						Main.NewText("The air is burning...", Color.Orange.R, Color.Orange.G, Color.Orange.B);
					}
					if (NPC.ai[1] > 900f) 
					{
						SoundEngine.PlaySound(SoundID.Item20, player.position);
						Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("ExtremeGravity").Type, 480, true);
						for (int num621 = 0; num621 < 40; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num622].velocity *= 3f;
							if (Main.rand.Next(2) == 0)
							{
								Main.dust[num622].scale = 0.5f;
								Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
							}
						}
						for (int num623 = 0; num623 < 60; num623++)
						{
							int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 3f);
							Main.dust[num624].noGravity = true;
							Main.dust[num624].velocity *= 5f;
							num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 244, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num624].velocity *= 2f;
						}
						text = 0;
						NPC.ai[0] = -1f;
					}
				}
			}
			if (NPC.ai[0] == 3f) 
			{
				NPC.defense = 100;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.chaseable = true;
				if (NPC.ai[2] == 0f) 
				{
					NPC.TargetClosest(true);
					if (NPC.Center.X < player.Center.X) 
					{
						NPC.ai[2] = 1f;
					} 
					else 
					{
						NPC.ai[2] = -1f;
					}
				}
				NPC.TargetClosest(true);
				int num851 = 800;
				float num852 = Math.Abs(NPC.Center.X - player.Center.X);
				if (NPC.Center.X < player.Center.X && NPC.ai[2] < 0f && num852 > (float)num851) 
				{
					NPC.ai[2] = 0f;
				}
				if (NPC.Center.X > player.Center.X && NPC.ai[2] > 0f && num852 > (float)num851) 
				{
					NPC.ai[2] = 0f;
				}
				float num853 = expertMode ? 1.5f : 1.45f;
				float num854 = expertMode ? 20f : 19f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
				{
					num853 = expertMode ? 1.55f : 1.5f;
					num854 = expertMode ? 21f : 20f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num853 = expertMode ? 1.6f : 1.55f;
					num854 = expertMode ? 22f : 21f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
				{
					num853 = expertMode ? 1.7f : 1.6f;
					num854 = expertMode ? 24f : 23f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					num853 = expertMode ? 1.8f : 1.7f;
					num854 = expertMode ? 28f : 27f;
				}
				NPC.velocity.X = NPC.velocity.X + NPC.ai[2] * num853;
				if (NPC.velocity.X > num854) 
				{
					NPC.velocity.X = num854;
				}
				if (NPC.velocity.X < -num854) 
				{
					NPC.velocity.X = -num854;
				}
				float num855 = player.position.Y - (NPC.position.Y + (float)NPC.height);
				if (num855 < 200f) //150
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.2f;
				}
				if (num855 > 250f) //200
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.2f;
				}
				if (NPC.velocity.Y > 8f) 
				{
					NPC.velocity.Y = 8f;
				}
				if (NPC.velocity.Y < -8f) 
				{
					NPC.velocity.Y = -8f;
				}
				if ((num852 < 500f || NPC.ai[3] < 0f) && NPC.position.Y < player.position.Y) 
				{
					NPC.ai[3] += 1f;
					int num856 = expertMode ? 11 : 12;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
					{
						num856 = expertMode ? 10 : 11;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num856 = expertMode ? 9 : 10;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num856 = expertMode ? 8 : 9;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num856 = expertMode ? 6 : 8;
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
						float num860 = expertMode ? 9f : 8f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
						{
							num860 = expertMode ? 10.25f : 9f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
						{
							num860 = expertMode ? 11.5f : 10f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
						{
							num860 = expertMode ? 12.75f : 11f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
						{
							num860 = expertMode ? 14f : 12f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 65 : 95;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector112.X, vector112.Y, num857, num858, Mod.Find<ModProjectile>("MoltenBlast").Type, holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				} 
				else if (NPC.ai[3] < 0f) 
				{
					NPC.ai[3] += 1f;
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 900f && num852 < 600f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 4f) 
			{
				NPC.defense = 100;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.chaseable = true;
				NPC.TargetClosest(true);
				float num861 = expertMode ? 0.35f : 0.32f;
				float num862 = expertMode ? 17f : 16f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
				{
					num861 = expertMode ? 0.36f : 0.33f;
					num862 = expertMode ? 18f : 17f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num861 = expertMode ? 0.38f : 0.35f;
					num862 = expertMode ? 19f : 18f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
				{
					num861 = expertMode ? 0.4f : 0.37f;
					num862 = expertMode ? 20f : 19f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					num861 = expertMode ? 0.45f : 0.4f;
					num862 = expertMode ? 22f : 20f;
				}
				num861 -= 0.05f;
				num862 -= 1f;
				if (NPC.Center.X < player.Center.X) 
				{
					NPC.velocity.X = NPC.velocity.X + num861;
					if (NPC.velocity.X < 0f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
				}
				if (NPC.Center.X > player.Center.X) 
				{
					NPC.velocity.X = NPC.velocity.X - num861;
					if (NPC.velocity.X > 0f) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
				}
				if (NPC.velocity.X > num862 || NPC.velocity.X < -num862) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.95f;
				}
				float num863 = player.position.Y - (NPC.position.Y + (float)NPC.height);
				if (num863 < 280f) //180
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
				}
				if (num863 > 300f) //200
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
				}
				if (NPC.velocity.Y > 6f) 
				{
					NPC.velocity.Y = 6f;
				}
				if (NPC.velocity.Y < -6f) 
				{
					NPC.velocity.Y = -6f;
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[3] += 1f;
					int num864 = expertMode ? 76 : 80;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
					{
						num864 = expertMode ? 74 : 78;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num864 = expertMode ? 70 : 74;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num864 = expertMode ? 64 : 70;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num864 = expertMode ? 56 : 60;
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
							int fireDamage = expertMode ? 55 : 85;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector113.X, vector113.Y, speedX2, num865, Mod.Find<ModProjectile>("HolyBomb").Type, fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != 1) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 900f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			if (NPC.ai[0] == -1f) 
			{
				NPC.defense = 100;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.chaseable = true;
				int num871 = Main.rand.Next(5);
				NPC.TargetClosest(true);
				if (Math.Abs(NPC.Center.X - player.Center.X) > 1000f) 
				{
					num871 = 0;
				}
				NPC.ai[0] = (float)num871;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				return;
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, true);
		}
		
		public override void FindFrame(int frameHeight) //9 total frames
		{
			if (NPC.ai[0] == 2f)
			{
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 4.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y > frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 8;
				}
				else if (NPC.frame.Y < frameHeight * 6)
				{
					NPC.frame.Y = frameHeight * 6;
				}
				return;
			}
			int num84 = 5; //5
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > (double)num84)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if (NPC.frame.Y >= frameHeight * 6) //6
			{
				NPC.frame.Y = 0;
			}
			return;
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
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ProvidenceBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProvidenceMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RuneofCos>(), 1));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<UnholyEssence>(), 1, 20, 29));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlissfulBombardier>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HolyCollider>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.MoltenAmputator>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PurgeGuzzler>(), 6));
            npcLoot.Add(ItemDropRule.ByCondition(new HallowProvidence(), ModContent.ItemType<ElysianWings>(), 1));
            npcLoot.Add(ItemDropRule.ByCondition(new HellProvidence(), ModContent.ItemType<ElysianAegis>(), 1));
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
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