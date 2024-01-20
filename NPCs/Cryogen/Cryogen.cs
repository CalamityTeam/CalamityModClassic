using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Cryogen;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Weapons.Cryogen;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Cryogen
{
	[AutoloadBossHead]
	public class Cryogen : ModNPC
	{
		public int time = 0;
		public int oneTime = 0;
		public float iceShard = 0f;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cryogen");
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 24f;
			NPC.damage = 65;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 16000 : 14000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/Cryogen");
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("CryogenBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("A large snowflake in the wind.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 1f, 1f);
			Player player = Main.player[NPC.target];
			bool isChill = player.ZoneSnow;
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld1Point2.revenge;
			NPC.TargetClosest(true);
			if (NPC.ai[2] == 0f && NPC.localAI[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				int num6 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CryogenIce").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.ai[2] = (float)(num6 + 1);
				NPC.localAI[1] = -1f;
				NPC.netUpdate = true;
				Main.npc[num6].ai[0] = (float)NPC.whoAmI;
				Main.npc[num6].netUpdate = true;
			}
			int num7 = (int)NPC.ai[2] - 1;
			if (num7 != -1 && Main.npc[num7].active && Main.npc[num7].type == Mod.Find<ModNPC>("CryogenIce").Type)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = isChill ? false : true;
				NPC.ai[2] = 0f;
				if (NPC.localAI[1] == -1f)
				{
					NPC.localAI[1] = revenge ? 420f : 600f;
				}
				if (NPC.localAI[1] > 0f)
				{
					NPC.localAI[1] -= 1f;
				}
			}
			if (oneTime == 0)
			{
				RainStart();
				oneTime++;
			}
			if (expertMode)
			{
				int damageBuff = (int)(20f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.damage = NPC.defDamage + damageBuff;
				int defenseBuff = (int)(20f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.defense = NPC.defDefense + defenseBuff;
			}
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, -10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft > 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient && expertMode)
			{
				time++;
				if (time >= 240)
				{
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/4f;
				    	double offsetAngle;
				    	int i;
				    	int num184 = 20 + (revenge ? 3 : 0);
						for (i = 0; i < 2; i++ )
				    	{
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("IceBomb").Type, num184, 0f, Main.myPlayer, 0f, 0f);
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("IceBomb").Type, num184, 0f, Main.myPlayer, 0f, 0f);
				    	}
					}
					time = 0;
				}
			}
			if (NPC.ai[0] == 0f)
			{
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int shoot = revenge ? 5 : 4;
						NPC.localAI[0] += (float)Main.rand.Next(shoot);
						if (NPC.localAI[0] >= (float)Main.rand.Next(200, 300))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float spread = 45f * 0.0174f;
						    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
						    	double deltaAngle = spread/8f;
						    	double offsetAngle;
						    	int num184 = expertMode ? 18 : 22;
						    	int i;
						    	for (i = 0; i < 8; i++ )
						    	{
						   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
						   			Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 6f ), (float)( Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
						        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 6f ), (float)( -Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
						    	}
							}
						}
					}
					Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num1243 = player.Center.X - vector142.X;
					float num1244 = player.Center.Y - vector142.Y;
					float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
					float num1246 = isChill ? 4f : 6f;
					if (num1245 < num1246)
					{
						NPC.velocity.X = num1243;
						NPC.velocity.Y = num1244;
					}
					else
					{
						num1245 = num1246 / num1245;
						NPC.velocity.X = num1243 * num1245;
						NPC.velocity.Y = num1244 * num1245;
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.74) 
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int shoot = revenge ? 5 : 4;
						NPC.localAI[0] += (float)Main.rand.Next(shoot);
						if (NPC.localAI[0] >= (float)Main.rand.Next(150, 250))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								float num179 = revenge ? 12f : 11f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = expertMode ? 18 : 22;
								int num185 = Mod.Find<ModProjectile>("IceBlast").Type;
								value9.X += num180;
								value9.Y += num182;
								for (int num186 = 0; num186 < 8; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 12f / num183;
									num180 += (float)Main.rand.Next(-90, 91);
									num182 += (float)Main.rand.Next(-90, 91);
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
					}
					float num1164 = isChill ? 6f : 8f;
					float num1165 = isChill ? 1.2f : 1.3f;
					Vector2 vector133 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num1166 = player.Center.X - vector133.X;
					float num1167 = player.Center.Y - vector133.Y - 400f;
					float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
					if (num1168 < 20f)
					{
						num1166 = NPC.velocity.X;
						num1167 = NPC.velocity.Y;
					}
					else
					{
						num1168 = num1164 / num1168;
						num1166 *= num1168;
						num1167 *= num1168;
					}
					if (NPC.velocity.X < num1166)
					{
						NPC.velocity.X = NPC.velocity.X + num1165;
						if (NPC.velocity.X < 0f && num1166 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1165 * 2f;
						}
					}
					else if (NPC.velocity.X > num1166)
					{
						NPC.velocity.X = NPC.velocity.X - num1165;
						if (NPC.velocity.X > 0f && num1166 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1165 * 2f;
						}
					}
					if (NPC.velocity.Y < num1167)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1165;
						if (NPC.velocity.Y < 0f && num1167 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1165 * 2f;
						}
					}
					else if (NPC.velocity.Y > num1167)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1165;
						if (NPC.velocity.Y > 0f && num1167 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1165 * 2f;
						}
					}
					if (NPC.position.X + (float)NPC.width > player.position.X && NPC.position.X < player.position.X + (float)player.width && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height) && Main.netMode != NetmodeID.MultiplayerClient)
					{
						iceShard += 4f;
						if (iceShard > 8f)
						{
							iceShard = 0f;
							int num1169 = (int)(NPC.position.X + 10f + (float)Main.rand.Next(NPC.width - 20));
							int num1170 = (int)(NPC.position.Y + (float)NPC.height + 4f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)num1169, (float)num1170, 0f, 5f, Mod.Find<ModProjectile>("IceRain").Type, 30, 0f, Main.myPlayer, 0f, 0f);
							return;
						}
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.52) 
				{
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int shoot = revenge ? 5 : 4;
						NPC.localAI[0] += (float)Main.rand.Next(shoot);
						if (NPC.localAI[0] >= (float)Main.rand.Next(200, 201))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								if (Main.rand.NextBool(2))
								{
									float num179 = revenge ? 12f : 11f;
									Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									float num181 = Math.Abs(num180) * 0.1f;
									float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
									float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									NPC.netUpdate = true;
									num183 = num179 / num183;
									num180 *= num183;
									num182 *= num183;
									int num184 = expertMode ? 19 : 23;
									int num185 = Mod.Find<ModProjectile>("IceBlast").Type;
									value9.X += num180;
									value9.Y += num182;
									for (int num186 = 0; num186 < 8; num186++)
									{
										num180 = player.position.X + (float)player.width * 0.5f - value9.X;
										num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
										num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
										num183 = 12f / num183;
										num180 += (float)Main.rand.Next(-360, 361);
										num182 += (float)Main.rand.Next(-360, 361);
										num180 *= num183;
										num182 *= num183;
										Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
									}
								}
								else
								{
									float num179 = revenge ? 9f : 7f;
									Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									float num181 = Math.Abs(num180) * 0.1f;
									float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
									float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									NPC.netUpdate = true;
									num183 = num179 / num183;
									num180 *= num183;
									num182 *= num183;
									int num184 = expertMode ? 20 : 24;
									int num185 = Mod.Find<ModProjectile>("IceRain").Type;
									value9.X += num180;
									value9.Y += num182;
									for (int num186 = 0; num186 < 15; num186++)
									{
										num180 = player.position.X + (float)player.width * 0.5f - value9.X;
										num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
										num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
										num183 = 12f / num183;
										num180 += (float)Main.rand.Next(-100, 101);
										num182 += (float)Main.rand.Next(-100, 101);
										num180 *= num183;
										num182 *= num183;
										int ice = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
										Main.projectile[ice].velocity.Y = -10f;
									}
								}
							}
						}
					}
					Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num1243 = player.Center.X - vector142.X;
					float num1244 = player.Center.Y - vector142.Y;
					float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
					float num1246 = isChill ? 6.5f : 8f;
					if (num1245 < num1246)
					{
						NPC.velocity.X = num1243;
						NPC.velocity.Y = num1244;
					}
					else
					{
						num1245 = num1246 / num1245;
						NPC.velocity.X = num1243 * num1245;
						NPC.velocity.Y = num1244 * num1245;
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.35) 
				{
					NPC.ai[0] = 3f;
					NPC.ai[1] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int shoot = revenge ? 5 : 4;
						NPC.localAI[0] += (float)Main.rand.Next(shoot);
						if (NPC.localAI[0] >= (float)Main.rand.Next(150, 151))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								float num179 = revenge ? 12f : 11f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = expertMode ? 20 : 24;
								int num185 = Mod.Find<ModProjectile>("IceBlast").Type;
								value9.X += num180;
								value9.Y += num182;
								for (int num186 = 0; num186 < 8; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 12f / num183;
									num180 += (float)Main.rand.Next(-90, 91);
									num182 += (float)Main.rand.Next(-90, 91);
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
			       	}
					Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num1243 = player.Center.X - vector142.X;
					float num1244 = player.Center.Y - vector142.Y;
					float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
					float num1246 = isChill ? 7f : 9f;
					if (num1245 < num1246)
					{
						NPC.velocity.X = num1243;
						NPC.velocity.Y = num1244;
					}
					else
					{
						num1245 = num1246 / num1245;
						NPC.velocity.X = num1243 * num1245;
						NPC.velocity.Y = num1244 * num1245;
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.21) 
				{
					NPC.ai[0] = 4f;
					NPC.ai[1] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int shoot = revenge ? 5 : 4;
						NPC.localAI[0] += (float)Main.rand.Next(shoot);
						if (NPC.localAI[0] >= (float)Main.rand.Next(100, 101))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								float num179 = revenge ? 12f : 11f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = expertMode ? 20 : 24;
								int num185 = Mod.Find<ModProjectile>("IceBlast").Type;
								value9.X += num180;
								value9.Y += num182;
								for (int num186 = 0; num186 < 6; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = 12f / num183;
									num180 += (float)Main.rand.Next(-90, 91);
									num182 += (float)Main.rand.Next(-90, 91);
									num180 *= num183;
									num182 *= num183;
									int randomTime = Main.rand.Next(200, 600);
									int ice = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[ice].timeLeft = randomTime;
								}
							}
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float spread = 45f * 0.0174f;
						    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
						    	double deltaAngle = spread/8f;
						    	double offsetAngle;
						    	int num184 = expertMode ? 21 : 25;
						    	int i;
						    	for (i = 0; i < 6; i++ )
						    	{
						    		int randomTime = Main.rand.Next(400, 700);
						   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
						        	int ice = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 7f ), (float)( Math.Cos(offsetAngle) * 7f ), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
						        	int ice2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 7f ), (float)( -Math.Cos(offsetAngle) * 7f ), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
						        	Main.projectile[ice].timeLeft = randomTime;
						        	Main.projectile[ice2].timeLeft = randomTime;
						    	}
							}
						}
			       	}
					Vector2 vector161 = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
					float num1334 = (float)Main.rand.Next(-1000, 1001);
					float num1335 = (float)Main.rand.Next(-1000, 1001);
					float num1336 = (float)Math.Sqrt((double)(num1334 * num1334 + num1335 * num1335));
					float num1337 = revenge ? 16f : 15f;
					NPC.velocity *= 0.95f;
					NPC.rotation += 0.15f;
					num1336 = num1337 / num1336;
					num1334 *= num1336;
					num1335 *= num1336;
					vector161.X += num1334 * 4f;
					vector161.Y += num1335 * 4f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						iceShard += 1f;
						int num1338 = 7;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.18)
						{
							num1338--;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.155)
						{
							num1338 -= 2;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.13)
						{
							num1338 -= 3;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.115)
						{
							num1338 -= 4;
						}
						if (iceShard > (float)num1338)
						{
							int num1339 = expertMode ? 15 : 18;
							iceShard = 0f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector161.X, vector161.Y, num1334, num1335, Mod.Find<ModProjectile>("IceRain").Type, num1339, 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.10) 
				{
					NPC.ai[0] = 5f;
					NPC.ai[1] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					string key = "Cryogen is derping out!";

                    Color messageColor = Color.Cyan;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
					return;
				}
			}
			else
			{
				if (NPC.ai[1] == 0f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int shoot = revenge ? 5 : 4;
						NPC.localAI[0] += (float)Main.rand.Next(shoot);
						if (NPC.localAI[0] >= (float)Main.rand.Next(100, 101))
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								if (Main.rand.NextBool(2))
								{
									float num179 = revenge ? 12f : 11f;
									Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									float num181 = Math.Abs(num180) * 0.1f;
									float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
									float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									NPC.netUpdate = true;
									num183 = num179 / num183;
									num180 *= num183;
									num182 *= num183;
									int num184 = expertMode ? 24 : 28;
									int num185 = Mod.Find<ModProjectile>("IceBlast").Type;
									value9.X += num180;
									value9.Y += num182;
									for (int num186 = 0; num186 < 2; num186++)
									{
										num180 = player.position.X + (float)player.width * 0.5f - value9.X;
										num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
										num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
										num183 = 12f / num183;
										num180 += (float)Main.rand.Next(-360, 361);
										num182 += (float)Main.rand.Next(-360, 361);
										num180 *= num183;
										num182 *= num183;
										Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
									}
								}
								else
								{
									float num179 = revenge ? 9f : 8f;
									Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									float num181 = Math.Abs(num180) * 0.1f;
									float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
									float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									NPC.netUpdate = true;
									num183 = num179 / num183;
									num180 *= num183;
									num182 *= num183;
									int num184 = expertMode ? 24 : 28;
									int num185 = Mod.Find<ModProjectile>("IceRain").Type;
									value9.X += num180;
									value9.Y += num182;
									for (int num186 = 0; num186 < 3; num186++)
									{
										num180 = player.position.X + (float)player.width * 0.5f - value9.X;
										num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
										num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
										num183 = 12f / num183;
										num180 += (float)Main.rand.Next(-100, 101);
										num182 += (float)Main.rand.Next(-100, 101);
										num180 *= num183;
										num182 *= num183;
										int ice = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
										Main.projectile[ice].velocity.Y = -10f;
									}
								}
							}
						}
			       	}
					float num1372 = isChill ? 16f : 20f;
					Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
					float num1373 = player.position.X + (float)player.width * 0.5f - vector167.X;
					float num1374 = player.Center.Y - vector167.Y;
					float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
					float num1376 = num1372 / num1375;
					num1373 *= num1376;
					num1374 *= num1376;
					iceShard -= 1f;
					if (NPC.life >= (NPC.lifeMax * 0.75f))
					{
						if (num1375 < 200f || iceShard > 0f)
						{
							if (num1375 < 200f)
							{
								iceShard = 20f;
							}
							if (NPC.velocity.X < 0f)
							{
								NPC.direction = -1;
							}
							else
							{
								NPC.direction = 1;
							}
							NPC.rotation += (float)NPC.direction * 0.3f;
							return;
						}
					}
					else if (NPC.life >= (NPC.lifeMax * 0.05f) && NPC.life < (NPC.lifeMax * 0.075f))
					{
						if (num1375 < 190f || iceShard > 0f)
						{
							if (num1375 < 190f)
							{
								iceShard = 19f;
							}
							if (NPC.velocity.X < 0f)
							{
								NPC.direction = -1;
							}
							else
							{
								NPC.direction = 1;
							}
							NPC.rotation += (float)NPC.direction * 0.35f;
							return;
						}
					}
					else if (NPC.life >= (NPC.lifeMax * 0.025f) && NPC.life < (NPC.lifeMax * 0.05f))
					{
						if (num1375 < 180f || iceShard > 0f)
						{
							if (num1375 < 180f)
							{
								iceShard = 18f;
							}
							if (NPC.velocity.X < 0f)
							{
								NPC.direction = -1;
							}
							else
							{
								NPC.direction = 1;
							}
							NPC.rotation += (float)NPC.direction * 0.4f;
							return;
						}
					}
					else
					{
						if (num1375 < 170f || iceShard > 0f)
						{
							if (num1375 < 170f)
							{
								iceShard = 17f;
							}
							if (NPC.velocity.X < 0f)
							{
								NPC.direction = -1;
							}
							else
							{
								NPC.direction = 1;
							}
							NPC.rotation += (float)NPC.direction * 0.5f;
							return;
						}
					}
					NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
					NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
					if (num1375 < 350f)
					{
						NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
						NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
					}
					if (num1375 < 300f)
					{
						NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
						NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
					}
					NPC.rotation = NPC.velocity.X * 0.15f;
					return;
				}
			}
			if (NPC.ai[3] == 0f && NPC.life > 0)
			{
				NPC.ai[3] = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.02);
					if ((float)(NPC.life + num660) < NPC.ai[3])
					{
						NPC.ai[3] = (float)NPC.life;
						int num661 = Main.rand.Next(1, 3);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int randomSpawn = Main.rand.Next(3);
							if (randomSpawn == 0)
							{
								randomSpawn = Mod.Find<ModNPC>("Cryocore").Type;
							}
							else if (randomSpawn == 1)
							{
								randomSpawn = Mod.Find<ModNPC>("IceMass").Type;
							}
							else
							{
								randomSpawn = Mod.Find<ModNPC>("Cryocore2").Type;
							}
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, randomSpawn, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(randomSpawn);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
							Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
							Main.npc[num664].ai[1] = 0f;
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
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 60;
				NPC.height = 60;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceRod, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceRod, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceRod, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		private void RainStart()
		{
			if (!Main.raining)
			{
				int num = 86400;
				int num2 = num / 24;
				Main.rainTime = Main.rand.Next(num2 * 8, num);
				if (Main.rand.NextBool(3))
				{
					Main.rainTime += Main.rand.Next(0, num2);
				}
				if (Main.rand.NextBool(4))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 2);
				}
				if (Main.rand.NextBool(5))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 2);
				}
				if (Main.rand.NextBool(6))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 3);
				}
				if (Main.rand.NextBool(7))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 4);
				}
				if (Main.rand.NextBool(8))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 5);
				}
				float num3 = 1f;
				if (Main.rand.NextBool(2))
				{
					num3 += 0.05f;
				}
				if (Main.rand.NextBool(3))
				{
					num3 += 0.1f;
				}
				if (Main.rand.NextBool(4))
				{
					num3 += 0.15f;
				}
				if (Main.rand.NextBool(5))
				{
					num3 += 0.2f;
				}
				Main.rainTime = (int)((float)Main.rainTime * num3);
				Main.raining = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CryogenTrophy>(), 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CryoStone>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<CryogenBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CryogenMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CryoBar>(), 1, 15, 25));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofEleum>(), 1, 3, 5));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.FrostCore, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BittercoldStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Permafrost>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GlacialCrusher>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Cryogen.Icebreaker>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SnowstormStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EffluviumBow>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<IceStar>(), 4, 100, 150));
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode && Main.rand.NextBool(3))
			{
				target.AddBuff(BuffID.Frostburn, 100, true);
			}
			else if (Main.rand.NextBool(5))
			{
				target.AddBuff(BuffID.Frostburn, 100, true);
			}
		}
	}
}