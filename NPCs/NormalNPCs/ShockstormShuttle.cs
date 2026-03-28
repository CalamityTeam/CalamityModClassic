using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class ShockstormShuttle : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shockstorm Shuttle");
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
				new FlavorTextBestiaryInfoElement("A small scouter saucer from a distant civilization, yet still very aggressive. Hopefully the rest of them aren't like that!")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 3f;
			NPC.damage = 30;
			NPC.width = 64; //324
			NPC.height = 38; //216
			NPC.defense = 20;
			NPC.lifeMax = 150;
			NPC.aiStyle = -1; //new multiplayer desync fix
            AIType = -1; //new multiplayer desync fix
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ShockstormShuttleBanner").Type;
		}
		
		public override void AI()
		{
			if (Main.netMode != 1)
			{
				NPC.localAI[0] += (float)Main.rand.Next(4); //adds to localAI to fire projectiles at random times
				if (NPC.localAI[0] >= (float)Main.rand.Next(100, 120)) //rate at which projectiles are shot
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num179 = 12f; //speed of projectile
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
						float num181 = Math.Abs(num180) * 0.1f;
						float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
						float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						NPC.netUpdate = true;
						num183 = num179 / num183;
						num180 *= num183;
						num182 *= num183;
						int num184 = 30; //projectile damage
						if (Main.expertMode)
						{
							num184 = 22;
						}
						int num185 = 435; //projectile ID
						if (Main.rand.Next(8) == 0)
						{
							num185 = 449; //more powerful projectile ID
						}
						value9.X += num180;
						value9.Y += num182;
						for (int num186 = 0; num186 < 2; num186++) //shoots two projectiles by looping twice
						{
							num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
							num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = 12f / num183;
							num180 += (float)Main.rand.Next(-20, 21); //projectile spreadX
							num182 += (float)Main.rand.Next(-20, 21); //projectile spreadY
							num180 *= num183;
							num182 *= num183;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
			}
			if (NPC.localAI[3] == 0f && Main.netMode != 1)
			{
				NPC.localAI[3] = 1f;
			}
			Vector2 center16 = NPC.Center;
			Player player8 = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || player8.dead || !player8.active)
			{
				NPC.TargetClosest(true);
				player8 = Main.player[NPC.target];
				NPC.netUpdate = true;
			}
			if ((player8.dead || Vector2.Distance(player8.Center, center16) > 3200f) && NPC.ai[0] != 1f)
			{
				if (NPC.ai[0] == 0f)
				{
					NPC.ai[0] = -1f;
				}
				if (NPC.ai[0] == 2f)
				{
					NPC.ai[0] = -2f;
				}
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] == -1f || NPC.ai[0] == -2f)
			{
				NPC.velocity.Y = NPC.velocity.Y - 0.4f;
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
				if (!player8.dead)
				{
					NPC.timeLeft = 300;
					if (NPC.ai[0] == -2f)
					{
						NPC.ai[0] = 2f;
					}
					if (NPC.ai[0] == 0f)
					{
						NPC.ai[0] = 0f;
					}
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 0f)
			{
				int num1580 = 0;
				if (NPC.ai[3] >= 580f)
				{
					num1580 = 0;
				}
				else if (NPC.ai[3] >= 440f)
				{
					num1580 = 5;
				}
				else if (NPC.ai[3] >= 420f)
				{
					num1580 = 4;
				}
				else if (NPC.ai[3] >= 280f)
				{
					num1580 = 3;
				}
				else if (NPC.ai[3] >= 260f)
				{
					num1580 = 2;
				}
				else if (NPC.ai[3] >= 20f)
				{
					num1580 = 1;
				}
				NPC.ai[3] += 1f;
				if (NPC.ai[3] >= 600f)
				{
					NPC.ai[3] = 0f;
				}
				int num1581 = num1580;
				if (NPC.ai[3] >= 580f)
				{
					num1580 = 0;
				}
				else if (NPC.ai[3] >= 440f)
				{
					num1580 = 5;
				}
				else if (NPC.ai[3] >= 420f)
				{
					num1580 = 4;
				}
				else if (NPC.ai[3] >= 280f)
				{
					num1580 = 3;
				}
				else if (NPC.ai[3] >= 260f)
				{
					num1580 = 2;
				}
				else if (NPC.ai[3] >= 20f)
				{
					num1580 = 1;
				}
				if (num1580 != num1581)
				{
					if (num1580 == 0)
					{
						NPC.ai[2] = 0f;
					}
					if (num1580 == 1)
					{
						NPC.ai[2] = (float)((Math.Sign((player8.Center - center16).X) == 1) ? 1 : -1);
					}
					if (num1580 == 2)
					{
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
				}
				if (num1580 == 0)
				{
					if (NPC.ai[2] == 0f)
					{
						NPC.ai[2] = (float)(-600 * Math.Sign((center16 - player8.Center).X));
					}
					Vector2 vector196 = player8.Center + new Vector2(NPC.ai[2], -250f) - center16;
					if (vector196.Length() < 50f)
					{
						NPC.ai[3] = 19f;
					}
					else
					{
						vector196.Normalize();
						NPC.velocity = Vector2.Lerp(NPC.velocity, vector196 * 16f, 0.1f);
					}
				}
				if (num1580 == 1)
				{
					int num1582 = (int)NPC.Center.X / 16;
					int num1583 = (int)(NPC.position.Y + (float)NPC.height) / 16;
					int num1584 = 0;
					bool flag149 = Main.tile[num1582, num1583].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1582, num1583].TileType] && !Main.tileSolidTop[(int)Main.tile[num1582, num1583].TileType];
					if (flag149)
					{
						num1584 = 1;
					}
					else
					{
						while (num1584 < 150 && num1583 + num1584 < Main.maxTilesY)
						{
							int num1585 = num1583 + num1584;
							bool flag150 = Main.tile[num1582, num1585].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1582, num1585].TileType] && !Main.tileSolidTop[(int)Main.tile[num1582, num1585].TileType];
							if (flag150)
							{
								num1584--;
								break;
							}
							num1584++;
						}
					}
					float num1586 = (float)(num1584 * 16);
					float num1587 = 250f;
					if (num1586 < num1587)
					{
						float num1588 = -4f;
						if (-num1588 > num1586)
						{
							num1588 = -num1586;
						}
						NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, num1588, 0.05f);
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.95f;
					}
					NPC.velocity.X = 3.5f * NPC.ai[2];
				}
				if (num1580 == 2)
				{
					if (NPC.ai[2] == 0f)
					{
						NPC.ai[2] = (float)(300 * Math.Sign((center16 - player8.Center).X));
					}
					Vector2 vector197 = player8.Center + new Vector2(NPC.ai[2], -170f) - center16;
					int num1589 = (int)NPC.Center.X / 16;
					int num1590 = (int)(NPC.position.Y + (float)NPC.height) / 16;
					int num1591 = 0;
					bool flag151 = Main.tile[num1589, num1590].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1589, num1590].TileType] && !Main.tileSolidTop[(int)Main.tile[num1589, num1590].TileType];
					if (flag151)
					{
						num1591 = 1;
					}
					else
					{
						while (num1591 < 150 && num1590 + num1591 < Main.maxTilesY)
						{
							int num1592 = num1590 + num1591;
							bool flag152 = Main.tile[num1589, num1592].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1589, num1592].TileType] && !Main.tileSolidTop[(int)Main.tile[num1589, num1592].TileType];
							if (flag152)
							{
								num1591--;
								break;
							}
							num1591++;
						}
					}
					float num1593 = (float)(num1591 * 16);
					float num1594 = 170f;
					if (num1593 < num1594)
					{
						vector197.Y -= num1594 - num1593;
					}
					if (vector197.Length() < 70f)
					{
						NPC.ai[3] = 279f;
					}
					else
					{
						vector197.Normalize();
						NPC.velocity = Vector2.Lerp(NPC.velocity, vector197 * 20f, 0.1f);
					}
				}
				else if (num1580 == 3)
				{
					float num1595 = 0.85f;
					int num1596 = (int)NPC.Center.X / 16;
					int num1597 = (int)(NPC.position.Y + (float)NPC.height) / 16;
					int num1598 = 0;
					bool flag153 = Main.tile[num1596, num1597].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1596, num1597].TileType] && !Main.tileSolidTop[(int)Main.tile[num1596, num1597].TileType];
					if (flag153)
					{
						num1598 = 1;
					}
					else
					{
						while (num1598 < 150 && num1597 + num1598 < Main.maxTilesY)
						{
							int num1599 = num1597 + num1598;
							bool flag154 = Main.tile[num1596, num1599].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1596, num1599].TileType] && !Main.tileSolidTop[(int)Main.tile[num1596, num1599].TileType];
							if (flag154)
							{
								num1598--;
								break;
							}
							num1598++;
						}
					}
					float num1600 = (float)(num1598 * 16);
					float num1601 = 170f;
					if (num1600 < num1601)
					{
						float num1602 = -4f;
						if (-num1602 > num1600)
						{
							num1602 = -num1600;
						}
						NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, num1602, 0.05f);
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y * num1595;
					}
					NPC.velocity.X = NPC.velocity.X * num1595;
				}
				if (num1580 == 4)
				{
					Vector2 vector198 = player8.Center + new Vector2(0f, -250f) - center16;
					if (vector198.Length() < 50f)
					{
						NPC.ai[3] = 439f;
						return;
					}
					vector198.Normalize();
					NPC.velocity = Vector2.Lerp(NPC.velocity, vector198 * 16f, 0.1f);
					return;
				}
				else if (num1580 == 5)
				{
					NPC.velocity *= 0.85f;
					return;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.velocity *= 0.96f;
				float num1603 = 150f;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= num1603)
				{
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					NPC.rotation = 0f;
					NPC.netUpdate = true;
					return;
				}
				if (NPC.ai[1] < 40f)
				{
					NPC.rotation = Vector2.UnitY.RotatedBy((double)(NPC.ai[1] / 40f * 6.28318548f), default(Vector2)).Y * 0.2f;
					return;
				}
				if (NPC.ai[1] < 80f)
				{
					NPC.rotation = Vector2.UnitY.RotatedBy((double)(NPC.ai[1] / 20f * 6.28318548f), default(Vector2)).Y * 0.3f;
					return;
				}
				if (NPC.ai[1] < 120f)
				{
					NPC.rotation = Vector2.UnitY.RotatedBy((double)(NPC.ai[1] / 10f * 6.28318548f), default(Vector2)).Y * 0.4f;
					return;
				}
					NPC.rotation = (NPC.ai[1] - 120f) / 30f * 6.28318548f;
					return;
			}
			else if (NPC.ai[0] == 2f)
			{
				float num1605 = 3600f;
				float num1606 = 120f;
				float num1607 = 60f;
				int num1608 = 0;
				if (NPC.ai[3] % num1606 >= num1607)
				{
					num1608 = 1;
				}
				int num1609 = num1608;
				num1608 = 0;
				NPC.ai[3] += 1f;
				if (NPC.ai[3] % num1606 >= num1607)
				{
					num1608 = 1;
				}
				if (num1608 != num1609)
				{
					if (num1608 == 1)
					{
						NPC.ai[2] = (float)((Math.Sign((player8.Center - center16).X) == 1) ? 1 : -1);
						if (Main.netMode != 1) //second projectile being shot.  Didn't use this
						{
							NPC.localAI[0] += (float)Main.rand.Next(4);
							if (NPC.localAI[0] >= (float)Main.rand.Next(50, 100))
							{
								NPC.localAI[0] = 0f;
								NPC.TargetClosest(true);
								if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
								{
									float num179 = 11f;
									Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
									float num181 = Math.Abs(num180) * 0.1f;
									float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
									float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									NPC.netUpdate = true;
									num183 = num179 / num183;
									num180 *= num183;
									num182 *= num183;
									int num184 = 50;
									if (Main.expertMode)
									{
										num184 = 28;
									}
									if (Main.hardMode)
									{
										num184 = 40;
										if (NPC.downedMoonlord)
										{
											num184 = 80;
											if (Main.expertMode)
											{
												num184 = 45;
											}
										}
									}
									int num185 = 449;
									value9.X += num180;
									value9.Y += num182;
									for (int num186 = 0; num186 < 2; num186++)
									{
										num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
										num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
										num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
										num183 = 12f / num183;
										num180 += (float)Main.rand.Next(-20, 21);
										num182 += (float)Main.rand.Next(-20, 21);
										num180 *= num183;
										num182 *= num183;
										Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
									}
								}
							}
						}
						SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
					}
					NPC.netUpdate = true;
				}
				if (NPC.ai[3] >= num1605)
				{
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
				else if (num1608 == 0)
				{
					Vector2 vector199 = player8.Center + new Vector2(NPC.ai[2] * 350f, -250f) - center16;
					vector199.Normalize();
					NPC.velocity = Vector2.Lerp(NPC.velocity, vector199 * 16f, 0.1f);
				}
				else
				{
					int num1610 = (int)NPC.Center.X / 16;
					int num1611 = (int)(NPC.position.Y + (float)NPC.height) / 16;
					int num1612 = 0;
					bool flag155 = Main.tile[num1610, num1611].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1610, num1611].TileType] && !Main.tileSolidTop[(int)Main.tile[num1610, num1611].TileType];
					if (flag155)
					{
						num1612 = 1;
					}
					else
					{
						while (num1612 < 150 && num1611 + num1612 < Main.maxTilesY)
						{
							int num1613 = num1611 + num1612;
							bool flag156 = Main.tile[num1610, num1613].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num1610, num1613].TileType] && !Main.tileSolidTop[(int)Main.tile[num1610, num1613].TileType];
							if (flag156)
							{
								num1612--;
								break;
							}
							num1612++;
						}
					}
					float num1614 = (float)(num1612 * 16);
					float num1615 = 250f;
					if (num1614 < num1615)
					{
						float num1616 = -4f;
						if (-num1616 > num1614)
						{
							num1616 = -num1614;
						}
						NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, num1616, 0.05f);
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.95f;
					}
					NPC.velocity.X = 8f * NPC.ai[2];
				}
				NPC.rotation = 0f;
				return;
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 234, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 234, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !Main.hardMode)
			{
				return 0f;
			}
			return SpawnCondition.Sky.Chance * 0.1f;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Electrified, 120, true);
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.MartianConduitPlating, 1, 10, 30));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EssenceofCinder").Type, 3));
		}
	}
}