using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.ModLoader.Utilities;
using CalamityModClassic1Point2.Items.Accessories;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.ThiccWaifu
{
	public class ThiccWaifu : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cloud Elemental");
			Main.npcFrameCount[NPC.type] = 9;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 4f;
			NPC.damage = 45;
			NPC.width = 50; //324
			NPC.height = 100; //216
			NPC.defense = 40;
			NPC.lifeMax = 7000;
			NPC.knockBackResist = 0.05f;
			NPC.value = Item.buyPrice(0, 2, 0, 0);
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath39;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[44] = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.rarity = 2;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("It's best to stay indoors on rainy days.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.375f, 0.5f, 0.625f);
			bool flag111 = false;
			bool flag112 = false;
			bool flag113 = true;
			bool flag114 = false;
			int num1454 = 4;
			int num1455 = 3;
			int num1456 = 0;
			float num1457 = 0.2f;
			float num1458 = 2f;
			float num1459 = -0.2f;
			float num1460 = -4f;
			bool flag115 = true;
			float num1461 = 2f;
			float num1462 = 0.1f;
			float num1463 = 1f;
			float num1464 = 0.04f;
			bool flag116 = false;
			float scaleFactor26 = 0.96f;
			bool flag117 = true;
			if (NPC.type == Mod.Find<ModNPC>("ThiccWaifu").Type)
			{
				flag115 = false;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				num1456 = 3;
				num1459 = -0.1f;
				num1457 = 0.1f;
				float num1465 = (float)NPC.life / (float)NPC.lifeMax;
				num1461 += (1f - num1465) * 2f;
				num1462 += (1f - num1465) * 0.02f;
				if (num1465 < 0.5f) 
				{
					NPC.knockBackResist = 0f;
				}
				NPC.localAI[2] = 0f;
				if (NPC.ai[0] < 0f) 
				{
					NPC.ai[0] = MathHelper.Min(NPC.ai[0] + 1f, 0f);
				}
				if (NPC.ai[0] > 0f) 
				{
					flag117 = false;
					flag116 = true;
					NPC.ai[0] += 1f;
					if (NPC.ai[0] >= 135f) 
					{
						NPC.ai[0] = -300f;
						NPC.netUpdate = true;
					}
					Vector2 vector = NPC.Center;
					vector = Vector2.UnitX * (float)NPC.direction * 200f;
					Vector2 vector223 = NPC.Center + Vector2.UnitX * (float)NPC.direction * 50f - Vector2.UnitY * 6f;
					if (NPC.ai[0] == 54f && Main.netMode != NetmodeID.MultiplayerClient) 
					{
						List<Point> list4 = new List<Point>();
						Vector2 vec5 = Main.player[NPC.target].Center + new Vector2(Main.player[NPC.target].velocity.X * 30f, 0f);
						Point point14 = vec5.ToTileCoordinates();
						int num1468 = 0;
						while (num1468 < 1000 && list4.Count < 3) 
						{
							bool flag118 = false;
							int num1469 = Main.rand.Next(point14.X - 30, point14.X + 30 + 1);
							foreach (Point current in list4) 
							{
								if (Math.Abs(current.X - num1469) < 10) 
								{
									flag118 = true;
									break;
								}
							}
							if (!flag118) 
							{
								int startY = point14.Y - 20;
								int num1470;
								int num1471;
								Collision.ExpandVertically(num1469, startY, out num1470, out num1471, 1, 51);
								if (StrayMethods.CanSpawnSandstormHostile(new Vector2((float)num1469, (float)(num1471 - 15)) * 16f, 15, 15)) 
								{
									list4.Add(new Point(num1469, num1471 - 15));
								}
							}
							num1468++;
						}
						foreach (Point current2 in list4) 
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)(current2.X * 16), (float)(current2.Y * 16), 0f, 0f, Mod.Find<ModProjectile>("StormMarkHostile").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					new Vector2(0.9f, 2f);
					if (NPC.ai[0] < 114f && NPC.ai[0] > 0f) 
					{
						List<Vector2> list5 = new List<Vector2>();
						for (int num1472 = 0; num1472 < 1000; num1472++) 
						{
							Projectile projectile9 = Main.projectile[num1472];
							if (projectile9.active && projectile9.type == Mod.Find<ModProjectile>("StormMarkHostile").Type) 
							{
								list5.Add(projectile9.Center);
							}
						}
					}
				}
				if (NPC.ai[0] == 0f) 
				{
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
					flag116 = true;
				}
			}
			if (NPC.justHit) 
			{
				NPC.localAI[2] = 0f;
			}
			if (!flag112) 
			{
				if (NPC.localAI[2] >= 0f) 
				{
					float num1477 = 16f;
					bool flag119 = false;
					bool flag120 = false;
					if (NPC.position.X > NPC.localAI[0] - num1477 && NPC.position.X < NPC.localAI[0] + num1477) 
					{
						flag119 = true;
					} 
					else if ((NPC.velocity.X < 0f && NPC.direction > 0) || (NPC.velocity.X > 0f && NPC.direction < 0))
					{
						flag119 = true;
						num1477 += 24f;
					}
					if (NPC.position.Y > NPC.localAI[1] - num1477 && NPC.position.Y < NPC.localAI[1] + num1477) 
					{
						flag120 = true;
					}
					if (flag119 && flag120) 
					{
						NPC.localAI[2] += 1f;
						if (NPC.localAI[2] >= 30f && num1477 == 16f) 
						{
							flag111 = true;
						}
						if (NPC.localAI[2] >= 60f) 
						{
							NPC.localAI[2] = -180f;
							NPC.direction *= -1;
							NPC.velocity.X = NPC.velocity.X * -1f;
							NPC.collideX = false;
						}
					} 
					else 
					{
						NPC.localAI[0] = NPC.position.X;
						NPC.localAI[1] = NPC.position.Y;
						NPC.localAI[2] = 0f;
					}
					if (flag117) 
					{
						NPC.TargetClosest(true);
					}
				} 
				else 
				{
					NPC.localAI[2] += 1f;
					NPC.direction = ((Main.player[NPC.target].Center.X > NPC.Center.X) ? 1 : -1);
				}
			}
			int num1478 = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction * 2;
			int num1479 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
			int num1480 = (int)NPC.Bottom.Y / 16;
			int num1481 = (int)NPC.Bottom.X / 16;
			if (flag116) 
			{
				NPC.velocity *= scaleFactor26;
				return;
			}
			for (int num1482 = num1479; num1482 < num1479 + num1454; num1482++) 
			{
				if ((Main.tile[num1478, num1482].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1478, num1482].TileType]) || Main.tile[num1478, num1482].LiquidAmount > 0) 
				{
					if (num1482 <= num1479 + 1) 
					{
						flag114 = true;
					}
					flag113 = false;
					break;
				}
			}
			for (int num1483 = num1480; num1483 < num1480 + num1456; num1483++) 
			{
				if ((Main.tile[num1481, num1483].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1481, num1483].TileType]) || Main.tile[num1481, num1483].LiquidAmount > 0) 
				{
					flag114 = true;
					flag113 = false;
					break;
				}
			}
			if (flag115) 
			{
				for (int num1484 = num1479 - num1455; num1484 < num1479; num1484++) 
				{
					if ((Main.tile[num1478, num1484].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1478, num1484].TileType]) || Main.tile[num1478, num1484].LiquidAmount > 0) 
					{
						flag114 = false;
						flag111 = true;
						break;
					}
				}
			}
			if (flag111) 
			{
				flag114 = false;
				flag113 = true;
			}
			if (flag113) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num1457;
				if (NPC.velocity.Y > num1458) 
				{
					NPC.velocity.Y = num1458;
				}
			} 
			else 
			{
				if ((NPC.directionY < 0 && NPC.velocity.Y > 0f) || flag114) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num1459;
				}
				if (NPC.velocity.Y < num1460) 
				{
					NPC.velocity.Y = num1460;
				}
			}
			if (NPC.collideX) 
			{
				NPC.velocity.X = NPC.oldVelocity.X * -0.4f;
				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 1f) 
				{
					NPC.velocity.X = 1f;
				}
				if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -1f) 
				{
					NPC.velocity.X = -1f;
				}
			}
			if (NPC.collideY) 
			{
				NPC.velocity.Y = NPC.oldVelocity.Y * -0.25f;
				if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f) 
				{
					NPC.velocity.Y = 1f;
				}
				if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f) 
				{
					NPC.velocity.Y = -1f;
				}
			}
			if (NPC.direction == -1 && NPC.velocity.X > -num1461) 
			{
				NPC.velocity.X = NPC.velocity.X - num1462;
				if (NPC.velocity.X > num1461) 
				{
					NPC.velocity.X = NPC.velocity.X - num1462;
				} 
				else if (NPC.velocity.X > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + num1462 / 2f;
				}
				if (NPC.velocity.X < -num1461) 
				{
					NPC.velocity.X = -num1461;
				}
			} 
			else if (NPC.direction == 1 && NPC.velocity.X < num1461) 
			{
				NPC.velocity.X = NPC.velocity.X + num1462;
				if (NPC.velocity.X < -num1461) 
				{
					NPC.velocity.X = NPC.velocity.X + num1462;
				}
				else if (NPC.velocity.X < 0f) 
				{
					NPC.velocity.X = NPC.velocity.X - num1462 / 2f;
				}
				if (NPC.velocity.X > num1461) 
				{
					NPC.velocity.X = num1461;
				}
			}
			if (NPC.directionY == -1 && NPC.velocity.Y > -num1463) 
			{
				NPC.velocity.Y = NPC.velocity.Y - num1464;
				if (NPC.velocity.Y > num1463) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num1464 * 1.25f;
				} 
				else if (NPC.velocity.Y > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num1464 * 0.75f;
				}
				if (NPC.velocity.Y < -num1463) 
				{
					NPC.velocity.Y = -num1461;
					return;
				}
			} 
			else if (NPC.directionY == 1 && NPC.velocity.Y < num1463) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num1464;
				if (NPC.velocity.Y < -num1463) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num1464 * 1.25f;
				} 
				else if (NPC.velocity.Y < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y - num1464 * 0.75f;
				}
				if (NPC.velocity.Y > num1463) 
				{
					NPC.velocity.Y = num1463;
					return;
				}
			}
		}
		
		public override void FindFrame(int frameHeight)
		{
			if (NPC.ai[0] > 0f)
			{
				float num48 = NPC.ai[0];
				if (num48 < 6f)
				{
					NPC.frame.Y = frameHeight * 5;
				}
				else if (num48 < 105f)
				{
					NPC.frame.Y = frameHeight * (int)(num48 / 8f % 4f + 5f);
				}
				else if (num48 < 114f)
				{
					NPC.frame.Y = frameHeight * 5;
				}
				else if (num48 < 135f)
				{
					NPC.frame.Y = frameHeight * (int)((num48 - 99f - 15f) / 7f + 10f);
				}
				else
				{
					NPC.frame.Y = frameHeight;
				}
			}
			else
			{
				NPC.frameCounter = NPC.frameCounter + (double)(NPC.velocity.Length() * 0.1f) + 1.0;
				if (NPC.frameCounter >= 8.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y >= frameHeight * 5)
				{
					NPC.frame.Y = 0;
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !Main.hardMode || !Main.raining)
			{
				return 0f;
			}
			return SpawnCondition.Sky.Chance * 0.1f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<EyeoftheStorm>(), 4, 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<StormSaber>(), 5));
        }
	}
}