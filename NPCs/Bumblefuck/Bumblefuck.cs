using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items.Bumblefuck;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.Bumblebirb;
using CalamityModClassicPreTrailer.Items.Weapons.RareVariants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Bumblefuck
{
	[AutoloadBossHead]
	public class Bumblefuck : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bumblebirb");
			Main.npcFrameCount[NPC.type] = 10;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("Failed clones of the Tyrant's loyal dragon, they were released onto the wild on accident, and now govern the jungle completely unopposed.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 32f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 160;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 40;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 252500 : 227500;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 302500;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 1000000 : 900000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ExoFreeze").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.boss = true;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Murderswarm");
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			NPC.HitSound = SoundID.NPCHit51;
			NPC.DeathSound = SoundID.NPCDeath46;
		}

		public override void AI()
		{
			Player player = Main.player[NPC.target];
			bool revenge = CalamityWorldPreTrailer.revenge;
			Vector2 vector = NPC.Center;
			int num1305 = revenge ? 6 : 4;
			if (CalamityWorldPreTrailer.death)
			{
				num1305 = 8;
			}
			NPC.noTileCollide = false;
			NPC.noGravity = true;
			NPC.damage = NPC.defDamage;
			if (Vector2.Distance(player.Center, vector) > 5600f)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			Vector2 vector205 = player.Center - NPC.Center;
			if (NPC.ai[0] > 1f && vector205.Length() > 5200f)
			{
				NPC.ai[0] = 1f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.TargetClosest(true);
				if (NPC.Center.X < player.Center.X - 2f)
				{
					NPC.direction = 1;
				}
				if (NPC.Center.X > player.Center.X + 2f)
				{
					NPC.direction = -1;
				}
				NPC.spriteDirection = NPC.direction;
				NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.05f) / 10f;
				if (NPC.collideX)
				{
					NPC.velocity.X = NPC.velocity.X * (-NPC.oldVelocity.X * 0.5f);
					if (NPC.velocity.X > 26f) //4
					{
						NPC.velocity.X = 26f; //4
					}
					if (NPC.velocity.X < -26f) //4
					{
						NPC.velocity.X = -26f; //4
					}
				}
				if (NPC.collideY)
				{
					NPC.velocity.Y = NPC.velocity.Y * (-NPC.oldVelocity.Y * 0.5f);
					if (NPC.velocity.Y > 26f) //4
					{
						NPC.velocity.Y = 26f; //4
					}
					if (NPC.velocity.Y < -26f) //4
					{
						NPC.velocity.Y = -26f; //4
					}
				}
				Vector2 value51 = player.Center - NPC.Center;
				value51.Y -= 200f;
				if (value51.Length() > 800f) //800 Charge to get closer and spawn birbs
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
				}
				else if (value51.Length() > 80f)
				{
					float scaleFactor15 = 12f; //6
					float num1306 = 30f;
					value51.Normalize();
					value51 *= scaleFactor15;
					NPC.velocity = (NPC.velocity * (num1306 - 1f) + value51) / num1306;
				}
				else if (NPC.velocity.Length() > 18f) //8
				{
					NPC.velocity *= 0.95f;
				}
				else if (NPC.velocity.Length() < 9f) //4
				{
					NPC.velocity *= 1.05f;
				}
				NPC.ai[1] += 1f;
				if (NPC.justHit)
				{
					NPC.ai[1] += (float)Main.rand.Next(10, 30);
				}
				if (NPC.ai[1] >= 180f && Main.netMode != 1)
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					while (NPC.ai[0] == 0f)
					{
						int damage = Main.expertMode ? 45 : 60; //180 120
						int num1307 = Main.rand.Next(3);
						if (num1307 == 0 && Collision.CanHit(NPC.Center, 1, 1, player.Center, 1, 1))
						{
							NPC.ai[0] = 2f;
						}
						else if (num1307 == 1)
						{
							NPC.ai[0] = 3f;
						}
						else if (NPC.CountNPCS(Mod.Find<ModNPC>("Bumblefuck2").Type) < num1305)
						{
							NPC.ai[0] = 4f; //summon more birbs
							SoundEngine.PlaySound(SoundID.Item102, NPC.position);
							for (int num186 = 0; num186 < 6; num186++)
							{
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, (float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-5, -2), Mod.Find<ModProjectile>("RedLightningFeather").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
					return;
				}
			}
			else
			{
				if (NPC.ai[0] == 1f) //move closer and spawn birbs
				{
					NPC.collideX = false;
					NPC.collideY = false;
					NPC.noTileCollide = true;
					if (NPC.target < 0 || !player.active || player.dead)
					{
						NPC.TargetClosest(true);
					}
					if (NPC.velocity.X < 0f)
					{
						NPC.direction = -1;
					}
					else if (NPC.velocity.X > 0f)
					{
						NPC.direction = 1;
					}
					NPC.spriteDirection = NPC.direction;
					NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.04f) / 10f;
					Vector2 value52 = player.Center - NPC.Center;
					if (value52.Length() < 500f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) //500
					{
						NPC.ai[0] = 0f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
					}
					float scaleFactor16 = 13f + value52.Length() / 100f; //7
					float num1308 = 25f;
					value52.Normalize();
					value52 *= scaleFactor16;
					NPC.velocity = (NPC.velocity * (num1308 - 1f) + value52) / num1308;
					return;
				}
				if (NPC.ai[0] == 2f)
				{
					NPC.damage = (int)((double)NPC.defDamage * 0.9);
					if (NPC.target < 0 || !player.active || player.dead)
					{
						NPC.TargetClosest(true);
						NPC.ai[0] = 0f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
					}
					if (player.Center.X - 10f < NPC.Center.X)
					{
						NPC.direction = -1;
					}
					else if (player.Center.X + 10f > NPC.Center.X)
					{
						NPC.direction = 1;
					}
					NPC.spriteDirection = NPC.direction;
					NPC.rotation = (NPC.rotation * 4f + NPC.velocity.X * 0.05f) / 5f;
					if (NPC.collideX)
					{
						NPC.velocity.X = NPC.velocity.X * (-NPC.oldVelocity.X * 0.5f);
						if (NPC.velocity.X > 27f) //4
						{
							NPC.velocity.X = 27f; //4
						}
						if (NPC.velocity.X < -27f) //4
						{
							NPC.velocity.X = -27f; //4
						}
					}
					if (NPC.collideY)
					{
						NPC.velocity.Y = NPC.velocity.Y * (-NPC.oldVelocity.Y * 0.5f);
						if (NPC.velocity.Y > 27f) //4
						{
							NPC.velocity.Y = 27f; //4
						}
						if (NPC.velocity.Y < -27f) //4
						{
							NPC.velocity.Y = -27f; //4
						}
					}
					Vector2 value53 = player.Center - NPC.Center;
					value53.Y -= 20f;
					NPC.ai[2] += 0.0222222228f;
					if (Main.expertMode)
					{
						NPC.ai[2] += 0.0166666675f;
					}
					float scaleFactor17 = 12f + NPC.ai[2] + value53.Length() / 120f; //4
					float num1309 = 20f;
					value53.Normalize();
					value53 *= scaleFactor17;
					NPC.velocity = (NPC.velocity * (num1309 - 1f) + value53) / num1309;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 180f || !Collision.CanHit(NPC.Center, 1, 1, player.Center, 1, 1))
					{
						NPC.ai[0] = 0f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						return;
					}
				}
				else
				{
					if (NPC.ai[0] == 3f)
					{
						NPC.noTileCollide = true;
						if (NPC.velocity.X < 0f)
						{
							NPC.direction = -1;
						}
						else
						{
							NPC.direction = 1;
						}
						NPC.spriteDirection = NPC.direction;
						NPC.rotation = (NPC.rotation * 4f + NPC.velocity.X * 0.035f) / 5f;
						Vector2 value54 = player.Center - NPC.Center;
						value54.Y -= 12f;
						if (NPC.Center.X > player.Center.X)
						{
							value54.X += 400f;
						}
						else
						{
							value54.X -= 400f;
						}
						if (Math.Abs(NPC.Center.X - player.Center.X) > 350f && Math.Abs(NPC.Center.Y - player.Center.Y) < 20f)
						{
							NPC.ai[0] = 3.1f;
							NPC.ai[1] = 0f;
						}
						NPC.ai[1] += 0.0333333351f;
						float scaleFactor18 = 18f + NPC.ai[1]; //8
						float num1310 = 4f;
						value54.Normalize();
						value54 *= scaleFactor18;
						NPC.velocity = (NPC.velocity * (num1310 - 1f) + value54) / num1310;
						return;
					}
					if (NPC.ai[0] == 3.1f)
					{
						NPC.noTileCollide = true;
						NPC.rotation = (NPC.rotation * 4f + NPC.velocity.X * 0.035f) / 5f;
						Vector2 vector206 = player.Center - NPC.Center;
						vector206.Y -= 12f;
						float scaleFactor19 = 26f; //16
						float num1311 = 8f;
						vector206.Normalize();
						vector206 *= scaleFactor19;
						NPC.velocity = (NPC.velocity * (num1311 - 1f) + vector206) / num1311;
						if (NPC.velocity.X < 0f)
						{
							NPC.direction = -1;
						}
						else
						{
							NPC.direction = 1;
						}
						NPC.spriteDirection = NPC.direction;
						NPC.ai[1] += 1f;
						if (NPC.ai[1] > 10f)
						{
							NPC.velocity = vector206;
							if (NPC.velocity.X < 0f)
							{
								NPC.direction = -1;
							}
							else
							{
								NPC.direction = 1;
							}
							NPC.ai[0] = 3.2f;
							NPC.ai[1] = 0f;
							NPC.ai[1] = (float)NPC.direction;
							return;
						}
					}
					else
					{
						if (NPC.ai[0] == 3.2f)
						{
							NPC.damage = (int)((double)NPC.defDamage * 1.5);
							NPC.collideX = false;
							NPC.collideY = false;
							NPC.noTileCollide = true;
							NPC.ai[2] += 0.0333333351f;
							NPC.velocity.X = (18f + NPC.ai[2]) * NPC.ai[1]; //16
							if ((NPC.ai[1] > 0f && NPC.Center.X > player.Center.X + 400f) || (NPC.ai[1] < 0f && NPC.Center.X < player.Center.X - 400f))
							{
								if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
								{
									NPC.ai[0] = 0f;
									NPC.ai[1] = 0f;
									NPC.ai[2] = 0f;
									NPC.ai[3] = 0f;
								}
								else if (Math.Abs(NPC.Center.X - player.Center.X) > 800f) //800
								{
									NPC.ai[0] = 1f;
									NPC.ai[1] = 0f;
									NPC.ai[2] = 0f;
									NPC.ai[3] = 0f;
								}
							}
							NPC.rotation = (NPC.rotation * 4f + NPC.velocity.X * 0.035f) / 5f;
							return;
						}
						if (NPC.ai[0] == 4f)
						{
							NPC.ai[0] = 0f;
							NPC.TargetClosest(true);
							if (Main.netMode != 1)
							{
								NPC.ai[1] = -1f;
								NPC.ai[2] = -1f;
								for (int num1312 = 0; num1312 < 1000; num1312++)
								{
									int num1313 = (int)player.Center.X / 16;
									int num1314 = (int)player.Center.Y / 16;
									int num1315 = 30 + num1312 / 50;
									int num1316 = 20 + num1312 / 75;
									num1313 += Main.rand.Next(-num1315, num1315 + 1);
									num1314 += Main.rand.Next(-num1316, num1316 + 1);
									if (!WorldGen.SolidTile(num1313, num1314))
									{
										while (!WorldGen.SolidTile(num1313, num1314) && (double)num1314 < Main.worldSurface)
										{
											num1314++;
										}
										if ((new Vector2((float)(num1313 * 16 + 8), (float)(num1314 * 16 + 8)) - player.Center).Length() < 5600f) //600
										{
											NPC.ai[0] = 4.1f;
											NPC.ai[1] = (float)num1313;
											NPC.ai[2] = (float)num1314;
											break;
										}
									}
								}
							}
							NPC.netUpdate = true;
							return;
						}
						if (NPC.ai[0] == 4.1f)
						{
							if (NPC.velocity.X < -2f)
							{
								NPC.direction = -1;
							}
							else if (NPC.velocity.X > 2f)
							{
								NPC.direction = 1;
							}
							NPC.spriteDirection = NPC.direction;
							NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.05f) / 10f;
							NPC.noTileCollide = true;
							int num1317 = (int)NPC.ai[1];
							int num1318 = (int)NPC.ai[2];
							float x2 = (float)(num1317 * 16 + 8);
							float y2 = (float)(num1318 * 16 - 20);
							Vector2 vector207 = new Vector2(x2, y2);
							vector207 -= NPC.Center;
							float num1319 = 6f + vector207.Length() / 150f;
							if (num1319 > 10f)
							{
								num1319 = 10f;
							}
							float num1320 = 10f; //10
							if (vector207.Length() < 100f) //10
							{
								NPC.ai[0] = 4.2f;
							}
							vector207.Normalize();
							vector207 *= num1319;
							NPC.velocity = (NPC.velocity * (num1320 - 1f) + vector207) / num1320;
							return;
						}
						if (NPC.ai[0] == 4.2f)
						{
							NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.05f) / 10f;
							NPC.noTileCollide = true;
							int num1321 = (int)NPC.ai[1];
							int num1322 = (int)NPC.ai[2];
							float x3 = (float)(num1321 * 16 + 8);
							float y3 = (float)(num1322 * 16 - 20);
							Vector2 vector208 = new Vector2(x3, y3);
							vector208 -= NPC.Center;
							float num1323 = 40f; //4
							float num1324 = 2f; //2
							if (Main.netMode != 1 && vector208.Length() < 40f) //4
							{
								int num1325 = 10;
								if (Main.expertMode)
								{
									num1325 = (int)((double)num1325 * 0.75);
								}
								NPC.ai[3] += 1f;
								if (NPC.ai[3] == (float)num1325)
								{
									NPC.NewNPC(NPC.GetSource_FromThis(null), num1321 * 16 + 8, num1322 * 16, Mod.Find<ModNPC>("Bumblefuck2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
								}
								else if (NPC.ai[3] == (float)(num1325 * 2))
								{
									NPC.ai[0] = 0f;
									NPC.ai[1] = 0f;
									NPC.ai[2] = 0f;
									NPC.ai[3] = 0f;
									if (NPC.CountNPCS(Mod.Find<ModNPC>("Bumblefuck2").Type) < num1305 && Main.rand.Next(5) != 0)
									{
										NPC.ai[0] = 4f;
									}
									else if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
									{
										NPC.ai[0] = 1f;
									}
								}
							}
							if (vector208.Length() > num1323)
							{
								vector208.Normalize();
								vector208 *= num1323;
							}
							NPC.velocity = (NPC.velocity * (num1324 - 1f) + vector208) / num1324;
							return;
						}
					}
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += (double)(NPC.velocity.Length() / 4f);
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] < 4f)
			{
				if (NPC.frameCounter >= 6.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y / frameHeight > 4)
				{
					NPC.frame.Y = 0;
				}
			}
			else
			{
				if (NPC.frameCounter >= 6.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y / frameHeight > 9)
				{
					NPC.frame.Y = frameHeight * 5;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Bumblebirb";
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 50; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("BumbleHead").Type, 1f);
					for (int wing = 0; wing < 2; wing++)
					{
						randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
							Mod.Find<ModGore>("BumbleWing").Type, 1f);
					}

					for (int leg = 0; leg < 4; leg++)
					{
						randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
							Mod.Find<ModGore>("BumbleLeg").Type, 1f);
					}
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<BumblebirbTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<BumblebirbBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BumblebirbBag>()));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Swordsplosion>(), 40));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<EffulgentFeather>(), 1, 6, 12));
			notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
			{ 
				ModContent.ItemType<RougeSlash>(),
				ModContent.ItemType<GildedProboscis>(),
				ModContent.ItemType<GoldenEagle>(),
			}));
			npcLoot.Add(notExpert);
		}
	}
}