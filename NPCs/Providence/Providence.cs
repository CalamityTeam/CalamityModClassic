using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.DevourerMunsters;
using CalamityModClassic1Point2.Items.Providence;
using CalamityModClassic1Point2.Items.Weapons.Providence;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Placeables;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Providence
{
	[AutoloadBossHead]
	public class Providence : ModNPC
	{
		public bool text = false;
		public float bossLife;
		internal int dpsCap = CalamityWorld.downedProvidence ? 72000 : 7200; //50
		private int damageTotal = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Providence, the Profaned God");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 20f;
			NPC.damage = 120;
			NPC.width = 150;
			NPC.height = 200;
			NPC.scale = 1.5f;
			NPC.defense = 120;
			NPC.lifeMax = CalamityWorld.revenge ? 300000 : 270000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.value = Item.buyPrice(3, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.chaseable = true;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/ProvidenceTheme");
			NPC.HitSound = SoundID.NPCHit44;
			NPC.DeathSound = SoundID.NPCDeath46;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("ProvidenceBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("A possessed statue with conscience formed from a war between demons and angels.")

            });
        }

        public override void AI()
		{
			CalamityGlobalNPC.holyBoss = NPC.whoAmI;
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			Player player = Main.player[NPC.target];
			Vector2 vector = NPC.Center;
			bool isHoly = player.ZoneHallow;
			bool isHell = player.ZoneUnderworldHeight;
			NPC.dontTakeDamage = (isHoly || isHell) ? false : true;
			damageTotal -= dpsCap;
			if (damageTotal < 0)
			{
				damageTotal = 0;
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnOffense").Type) > 0)
			{
				int damageBoost = revenge ? 240 : 120;
				NPC.damage = NPC.defDamage + damageBoost;
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnDefense").Type) > 0)
			{
				int defenseBoost = revenge ? 100 : 50;
				NPC.defense = NPC.defDefense + defenseBoost;
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("ProvSpawnHealer").Type) > 0)
			{
				float heal = revenge ? 90f : 120f;
				NPC.localAI[0] += 1f;
				if (NPC.localAI[0] >= heal)
				{
					NPC.localAI[0] = 0f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
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
			if (!Main.dayTime || Vector2.Distance(Main.player[NPC.target].Center, vector) > 6800f) 
			{
				if (NPC.timeLeft > 150)
				{
					NPC.timeLeft = 150;
				}
				if (NPC.velocity.X > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X + 0.25f;
				} 
				else 
				{
					NPC.velocity.X = NPC.velocity.X - 0.25f;
				}
				NPC.velocity.Y = NPC.velocity.Y - 0.1f;
			} 
			else if (NPC.timeLeft > 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.075);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
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
						return;
					}
				}
	       	}
			if (NPC.ai[0] == 0f) 
			{
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
				if (revenge)
				{
					num853 *= 1.15f;
					num854 *= 1.15f;
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
					if (NPC.ai[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient) 
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
						if (revenge)
						{
							num860 *= 1.15f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 65 : 70;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector112.X, vector112.Y, num857, num858, Mod.Find<ModProjectile>("HolyBlast").Type, holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				} 
				else if (NPC.ai[3] < 0f) 
				{
					NPC.ai[3] += 1f;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 600f && num852 < 600f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 1f) 
			{
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
				if (revenge)
				{
					num861 *= 1.15f;
					num862 *= 1.15f;
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
				if (Main.netMode != NetmodeID.MultiplayerClient) 
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
							int fireDamage = expertMode ? 50 : 54;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector113.X, vector113.Y, speedX2, num865, Mod.Find<ModProjectile>("HolyFire").Type, fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 600f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 2f) 
			{
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
				int num870 = expertMode ? 3 : 4;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
				{
					num870--; //2
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num870--; //1
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
				{
					num870 -= expertMode ? 2 : 1; //-1
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					num870 -= 2; //-3
				}
				if (NPC.ai[3] > (float)num870) 
				{
					NPC.ai[3] = 0f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						if (Main.rand.NextBool(4))
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector114.X, vector114.Y, num866, num867, Mod.Find<ModProjectile>("HolyLight").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
						else
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector114.X, vector114.Y, num866, num867, Mod.Find<ModProjectile>("HolyBurnOrb").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 750f && !text)
					{
						text = true;
						string key = "The air is burning...";
						Color messageColor = Color.Orange;
						if (Main.netMode == NetmodeID.SinglePlayer)
						{
							Main.NewText(key, messageColor);
						}
						else if (Main.netMode == NetmodeID.Server)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
						}
					}
					if (NPC.ai[1] > 900f) 
					{
						SoundEngine.PlaySound(SoundID.Item20, player.position);
						Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("ExtremeGravity").Type, 900, true);
						for (int num621 = 0; num621 < 40; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num622].velocity *= 3f;
							if (Main.rand.NextBool(2))
							{
								Main.dust[num622].scale = 0.5f;
								Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
							}
						}
						for (int num623 = 0; num623 < 60; num623++)
						{
							int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 3f);
							Main.dust[num624].noGravity = true;
							Main.dust[num624].velocity *= 5f;
							num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num624].velocity *= 2f;
						}
						text = false;
						NPC.ai[0] = -1f;
					}
				}
			}
			if (NPC.ai[0] == 3f) 
			{
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
				if (revenge)
				{
					num853 *= 1.15f;
					num854 *= 1.15f;
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
					if (NPC.ai[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient) 
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
						if (revenge)
						{
							num860 *= 1.15f;
						}
						num859 = num860 / num859;
						num857 *= num859;
						num858 *= num859;
						int holyDamage = expertMode ? 65 : 72;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector112.X, vector112.Y, num857, num858, Mod.Find<ModProjectile>("MoltenBlast").Type, holyDamage, 0f, Main.myPlayer, 0f, 0f);
					}
				} 
				else if (NPC.ai[3] < 0f) 
				{
					NPC.ai[3] += 1f;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 600f && num852 < 600f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			else if (NPC.ai[0] == 4f) 
			{
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
				if (revenge)
				{
					num861 *= 1.15f;
					num862 *= 1.15f;
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
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.ai[3] += 1f;
					int num864 = expertMode ? 74 : 78;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75) 
					{
						num864 = expertMode ? 70 : 74;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
					{
						num864 = expertMode ? 64 : 70;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25) 
					{
						num864 = expertMode ? 56 : 60;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						num864 = expertMode ? 46 : 50;
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
							int fireDamage = expertMode ? 55 : 61;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector113.X, vector113.Y, speedX2, num865, Mod.Find<ModProjectile>("HolyBomb").Type, fireDamage, 0f, Main.myPlayer, (float)Main.rand.Next(5), 0f);
						}
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.ai[1] += (float)Main.rand.Next(1, 4);
					if (NPC.ai[1] > 600f) 
					{
						NPC.ai[0] = -1f;
					}
				}
			}
			if (NPC.ai[0] == -1f) 
			{
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
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ProvidenceBag>()));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<ProvidenceTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProvidenceMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RuneofCos>(), 1));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<UnholyEssence>(), 1, 20, 29));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BlissfulBombardier>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HolyCollider>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Providence.MoltenAmputator>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PurgeGuzzler>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Providence.TelluricGlare>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Providence.SolarFlare>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new HallowProvi(), ModContent.ItemType<ElysianWings>(), 1)); // will these work? who knows! I'm too lazy to make new rules
            npcLoot.Add(ItemDropRule.ByCondition(new HellProvi(), ModContent.ItemType<ElysianAegis>(), 1));
        }

        public override void OnKill()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				int num52 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
				int num53 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
				int num54 = NPC.width / 2 / 16 + 1;
				for (int num55 = num52 - num54; num55 <= num52 + num54; num55++)
				{
					for (int num56 = num53 - num54; num56 <= num53 + num54; num56++)
					{
						if ((num55 == num52 - num54 || num55 == num52 + num54 || num56 == num53 - num54 || num56 == num53 + num54) && !Main.tile[num55, num56].HasTile)
						{
							if (Main.tile[num55, num56].HasTile)
                            {
                                Main.tile[num55, num56].TileType = 226;
                            }
							else
							{
								WorldGen.PlaceTile(num55, num56, 266);
							}
						}
						Main.tile[num55, num56].Get<LiquidData>().LiquidType = 0;
						Main.tile[num55, num56].LiquidAmount = 0;
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, num55, num56, 1, TileChangeType.None);
						}
						else
						{
							WorldGen.SquareTileFrame(num55, num56, true);
						}
					}
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Base);
		}

		public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[NPC.target];
			if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
			{
				modifiers.FinalDamage /= 2;
				modifiers.DisableCrit();
			}
			ModifyHit(ref modifiers.FinalDamage.Base);
		}
		
		public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
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
				ModPacket netMessage = GetPacket(ProvidenceMessageType.Damage);
				netMessage.Write(damage * 60);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					netMessage.Write(Main.myPlayer);
				}
				netMessage.Send();
			}
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
			double newDamage = modifiers.FinalDamage.Base;
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
			/*if (crit)
			{
				newDamage *= 2;
			}*/
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - ((NPC.ichor ? 0.25f : 0.33f) + (NPC.ai[0] == 2f ? 0.66f : 0f))) * newDamage));
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
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
			Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Providence/ProvidenceAlt").Value;
			CalamityModClassic1Point2.DrawTexture(spriteBatch, (NPC.ai[0] == 2f ? texture : TextureAssets.Npc[NPC.type].Value), 0, NPC, drawColor);
			return false;
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
				if (NPC.frame.Y > frameHeight * 5)
				{
					NPC.frame.Y = frameHeight * 5;
				}
				else if (NPC.frame.Y < frameHeight * 0)
				{
					NPC.frame.Y = frameHeight * 0;
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
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 2f;
			return null;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, true);
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				float randomSpread = (float)(Main.rand.Next(-50, 50) / 100);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence2").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence3").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence4").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("Providence5").Type, 1f);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 400;
				NPC.height = 350;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 90; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		private ModPacket GetPacket(ProvidenceMessageType type)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)CalamityModClassic1Point2MessageType.Providence);
			packet.Write(NPC.whoAmI);
			packet.Write((byte)type);
			return packet;
		}
		
		public void HandlePacket(BinaryReader reader)
		{
			ProvidenceMessageType type = (ProvidenceMessageType)reader.ReadByte();
			if (type == ProvidenceMessageType.Damage)
			{
				int damage = reader.ReadInt32();
				damageTotal += damage;
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket netMessage = GetPacket(ProvidenceMessageType.Damage);
					int ignore = reader.ReadInt32();
					netMessage.Write(damage);
					netMessage.Send(-1, ignore);
				}
			}
		}
	}
	
	enum ProvidenceMessageType : byte
	{
		Damage
    }
    public class HallowProvi : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.LocalPlayer.ZoneHallow && Main.expertMode;
        }
        public bool CanShowItemDropInUI()
        {
            return Main.expertMode;
        }

        public string GetConditionDescription()
        {
            return "While in the Hallow";
        }
    }
    public class HellProvi : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.LocalPlayer.ZoneUnderworldHeight && Main.expertMode;
        }
        public bool CanShowItemDropInUI()
        {
            return Main.expertMode;
        }

        public string GetConditionDescription()
        {
            return "While in the Underworld";
        }
    }
}