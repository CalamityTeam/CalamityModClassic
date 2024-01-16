using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.ThiccWaifu
{
	public class ThiccWaifu : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Cloud Elemental");
			//Tooltip.SetDefault("Cloud Elemental");
			NPC.npcSlots = 4f;
			NPC.damage = 45;
			NPC.width = 50; //324
			NPC.height = 100; //216
			NPC.defense = 40;
			NPC.lifeMax = 7000;
			NPC.knockBackResist = 0.05f;
			Main.npcFrameCount[NPC.type] = 9;
			NPC.value = Item.buyPrice(0, 2, 0, 0);
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath39;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[44] = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("And then along came Zeus.")

            });
        }
        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.375f, 0.5f, 0.625f);
			bool flag197 = false;
			bool flag198 = false;
			bool flag199 = true;
			bool flag200 = false;
			int num2034 = 4;
			int num2035 = 3;
			int num2036 = 0;
			float num2037 = 0.2f;
			float num2038 = 2f;
			float num2039 = -0.2f;
			float num2040 = -4f;
			bool flag201 = true;
			float num2041 = 2f;
			float num2042 = 0.1f;
			float num2043 = 1f;
			float num2044 = 0.04f;
			bool flag202 = false;
			float scaleFactor40 = 0.96f;
			bool flag203 = true;
			if (NPC.type == Mod.Find<ModNPC>("ThiccWaifu").Type)
			{
				flag201 = false;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				num2036 = 3;
				num2039 = -0.1f;
				num2037 = 0.1f;
				float num2045 = (float)NPC.life / (float)NPC.lifeMax;
				num2041 += (1f - num2045) * 2f;
				num2042 += (1f - num2045) * 0.02f;
				if (num2045 < 0.5f) 
				{
					NPC.knockBackResist = 0f;
				}
				Vector2 vector291 = NPC.BottomLeft + new Vector2(0f, -12f);
				Vector2 bottomRight = NPC.BottomRight;
				Vector2 value100 = new Vector2((float)(-(float)NPC.spriteDirection * 10), -4f);
				Color color = new Color(153, 204, 255) * 0.7f;
				float num2046 = -0.3f + MathHelper.Max(NPC.velocity.Y * 2f, 0f);
				for (int num2047 = 0; num2047 < 2; num2047++) 
				{
					if (Main.rand.Next(2) != 0) 
					{
						Dust dust32 = Main.dust[Dust.NewDust(NPC.Bottom, 0, 0, 16, 0f, 0f, 0, default(Color), 1f)];
						dust32.position = new Vector2(MathHelper.Lerp(vector291.X, bottomRight.X, Main.rand.NextFloat()), MathHelper.Lerp(vector291.Y, bottomRight.Y, Main.rand.NextFloat())) + value100;
						if (num2047 == 1) 
						{
							dust32.position = NPC.Bottom + Utils.RandomVector2(Main.rand, -6f, 6f);
						}
						dust32.color = color;
						dust32.scale = 0.8f;
						Dust expr_6A3C7_cp_0 = dust32;
						expr_6A3C7_cp_0.velocity.Y = expr_6A3C7_cp_0.velocity.Y + num2046;
						Dust expr_6A3E0_cp_0 = dust32;
						expr_6A3E0_cp_0.velocity.X = expr_6A3E0_cp_0.velocity.X + (float)NPC.spriteDirection * 0.2f;
					}
				}
				NPC.localAI[2] = 0f;
				if (NPC.ai[0] < 0f) 
				{
					NPC.ai[0] = MathHelper.Min(NPC.ai[0] + 1f, 0f);
				}
				if (NPC.ai[0] > 0f) 
				{
					flag203 = false;
					flag202 = true;
					NPC.ai[0] += 1f;
					if (NPC.ai[0] >= 135f) 
					{
						NPC.ai[0] = -300f;
						NPC.netUpdate = true;
					}
					NPC.Center += Vector2.UnitX * (float)NPC.direction * 200f;
					Vector2 vector292 = NPC.Center + Vector2.UnitX * (float)NPC.direction * 50f - Vector2.UnitY * 6f;
					if (NPC.ai[0] == 54f && Main.netMode != 1) 
					{
						List<Point> list13 = new List<Point>();
						Vector2 vec12 = Main.player[NPC.target].Center + new Vector2(Main.player[NPC.target].velocity.X * 30f, 0f);
						Point point18 = vec12.ToTileCoordinates();
						int num2048 = 0;
						while (num2048 < 1000 && list13.Count < 3) 
						{
							bool flag204 = false;
							int num2049 = Main.rand.Next(point18.X - 30, point18.X + 30 + 1);
							foreach (Point current7 in list13) 
							{
								if (Math.Abs(current7.X - num2049) < 10) 
								{
									flag204 = true;
									break;
								}
							}
							if (!flag204) 
							{
								int startY = point18.Y - 20;
								int num2050;
								int num2051;
								Collision.ExpandVertically(num2049, startY, out num2050, out num2051, 1, 51);
								if (StrayMethods.CanSpawnSandstormHostile(new Vector2((float)num2049, (float)(num2051 - 15)) * 16f, 15, 15)) 
								{
									list13.Add(new Point(num2049, num2051 - 15));
								}
							}
							num2048++;
						}
						foreach (Point current8 in list13) 
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)(current8.X * 16), (float)(current8.Y * 16), 0f, 0f, Mod.Find<ModProjectile>("StormMarkHostile").Type, 0, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					new Vector2(0.9f, 2f);
					if (NPC.ai[0] < 114f && NPC.ai[0] > 0f) 
					{
						List<Vector2> list14 = new List<Vector2>();
						for (int num2052 = 0; num2052 < 1000; num2052++) 
						{
							Projectile projectile9 = Main.projectile[num2052];
							if (projectile9.active && projectile9.type == Mod.Find<ModProjectile>("StormMarkHostile").Type) 
							{
								list14.Add(projectile9.Center);
							}
						}
						Vector2 value101 = new Vector2(0f, 1500f);
						float num2053 = (NPC.ai[0] - 54f) / 30f;
						if (num2053 < 0.95f && num2053 >= 0f) 
						{
							foreach (Vector2 current9 in list14) 
							{
								Vector2 value102 = Vector2.CatmullRom(vector292 + value101, vector292, current9, current9 + value101, num2053);
								Vector2 value103 = Vector2.CatmullRom(vector292 + value101, vector292, current9, current9 + value101, num2053 + 0.05f);
								float num2054 = num2053;
								if (num2054 > 0.5f) 
								{
									num2054 = 1f - num2054;
								}
								float num2055 = 2f;
								if (Vector2.Distance(value102, value103) > 5f) 
								{
									num2055 = 3f;
								}
								if (Vector2.Distance(value102, value103) > 10f) 
								{
									num2055 = 4f;
								}
								for (float num2056 = 0f; num2056 < num2055; num2056 += 1f) 
								{
									Dust dust33 = Main.dust[Dust.NewDust(vector292, 0, 0, 16, 0f, 0f, 0, default(Color), 1f)];
									dust33.position = Vector2.Lerp(value102, value103, num2056 / num2055) + Utils.RandomVector2(Main.rand, -2f, 2f);
									dust33.noLight = true;
									dust33.scale = 0.3f + num2053;
								}
							}
						}
					}
					float arg_6A9C0_0 = NPC.ai[0];
				}
				if (NPC.ai[0] == 0f) 
				{
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
					flag202 = true;
				}
			}
			if (NPC.justHit) 
			{
				NPC.localAI[2] = 0f;
			}
			if (!flag198) 
			{
				if (NPC.localAI[2] >= 0f) 
				{
					float num2057 = 16f;
					bool flag205 = false;
					bool flag206 = false;
					if (NPC.position.X > NPC.localAI[0] - num2057 && NPC.position.X < NPC.localAI[0] + num2057) 
					{
						flag205 = true;
					} 
					else if ((NPC.velocity.X < 0f && NPC.direction > 0) || (NPC.velocity.X > 0f && NPC.direction < 0))
					{
						flag205 = true;
						num2057 += 24f;
					}
					if (NPC.position.Y > NPC.localAI[1] - num2057 && NPC.position.Y < NPC.localAI[1] + num2057) 
					{
						flag206 = true;
					}
					if (flag205 && flag206) 
					{
						NPC.localAI[2] += 1f;
						if (NPC.localAI[2] >= 30f && num2057 == 16f) 
						{
							flag197 = true;
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
					if (flag203) 
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
			int num2058 = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction * 2;
			int num2059 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
			int num2060 = (int)NPC.Bottom.Y / 16;
			int num2061 = (int)NPC.Bottom.X / 16;
			if (flag202) 
			{
				NPC.velocity *= scaleFactor40;
				return;
			}
			for (int num2062 = num2059; num2062 < num2059 + num2034; num2062++) 
			{
				if ((Main.tile[num2058, num2062].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num2058, num2062].TileType]) || Main.tile[num2058, num2062].LiquidAmount > 0) 
				{
					if (num2062 <= num2059 + 1) 
					{
						flag200 = true;
					}
					flag199 = false;
					break;
				}
			}
			for (int num2063 = num2060; num2063 < num2060 + num2036; num2063++) 
			{
				if ((Main.tile[num2061, num2063].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num2061, num2063].TileType]) || Main.tile[num2061, num2063].LiquidAmount > 0) 
				{
					flag200 = true;
					flag199 = false;
					break;
				}
			}
			if (flag201) 
			{
				for (int num2064 = num2059 - num2035; num2064 < num2059; num2064++) 
				{
					if ((Main.tile[num2058, num2064].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num2058, num2064].TileType]) || Main.tile[num2058, num2064].LiquidAmount > 0) 
					{
						flag200 = false;
						flag197 = true;
						break;
					}
				}
			}
			if (flag197) 
			{
				flag200 = false;
				flag199 = true;
			}
			if (flag199) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num2037;
				if (NPC.velocity.Y > num2038) 
				{
					NPC.velocity.Y = num2038;
				}
			} 
			else 
			{
				if ((NPC.directionY < 0 && NPC.velocity.Y > 0f) || flag200) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num2039;
				}
				if (NPC.velocity.Y < num2040) 
				{
					NPC.velocity.Y = num2040;
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
			if (NPC.direction == -1 && NPC.velocity.X > -num2041) 
			{
				NPC.velocity.X = NPC.velocity.X - num2042;
				if (NPC.velocity.X > num2041) 
				{
					NPC.velocity.X = NPC.velocity.X - num2042;
				} 
				else if (NPC.velocity.X > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X + num2042 / 2f;
				}
				if (NPC.velocity.X < -num2041) 
				{
					NPC.velocity.X = -num2041;
				}
			} 
			else if (NPC.direction == 1 && NPC.velocity.X < num2041) 
			{
				NPC.velocity.X = NPC.velocity.X + num2042;
				if (NPC.velocity.X < -num2041) 
				{
					NPC.velocity.X = NPC.velocity.X + num2042;
				} 
				else if (NPC.velocity.X < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - num2042 / 2f;
				}
				if (NPC.velocity.X > num2041) 
				{
					NPC.velocity.X = num2041;
				}
			}
			if (NPC.directionY == -1 && NPC.velocity.Y > -num2043) 
			{
				NPC.velocity.Y = NPC.velocity.Y - num2044;
				if (NPC.velocity.Y > num2043) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num2044 * 1.25f;
				} 
				else if (NPC.velocity.Y > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num2044 * 0.75f;
				}
				if (NPC.velocity.Y < -num2043) 
				{
					NPC.velocity.Y = -num2041;
					return;
				}
			} 
			else if (NPC.directionY == 1 && NPC.velocity.Y < num2043) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num2044;
				if (NPC.velocity.Y < -num2043) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num2044 * 1.25f;
				} 
				else if (NPC.velocity.Y < 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num2044 * 0.75f;
				}
				if (NPC.velocity.Y > num2043) 
				{
					NPC.velocity.Y = num2043;
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
			int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            int tile = (int)Main.tile[x, y].TileType;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.125f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.001f));
            return oUnderworld && oRockLayer && Main.raining && Main.hardMode && !spawnInfo.PlayerInTown && Main.SceneMetrics.EvilTileCount < 80 && Main.SceneMetrics.SandTileCount < 80 && Main.SceneMetrics.DungeonTileCount < 80 ? 0.05f : 0f;
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 16, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 16, hit.HitDirection, -1f, 0, default(Color), 1f);
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