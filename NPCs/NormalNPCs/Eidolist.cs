using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class Eidolist : ModNPC
	{
		public bool hasBeenHit = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eidolist");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("A cultist that succumbed to their cult magicks, yet still, they seem to guard something...")
			});
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 60; //324
			NPC.height = 80; //216
			NPC.defense = 0;
			NPC.lifeMax = 10000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 13000;
			}
			NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.value = Item.buyPrice(0, 1, 0, 0);
			NPC.alpha = 50;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit13;
			NPC.DeathSound = SoundID.NPCDeath59;
			NPC.timeLeft = NPC.activeTime * 2;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("EidolistBanner").Type;
		}

		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.4f, 0.5f);
			if (NPC.justHit || CalamityGlobalNPC.AnyBossNPCS())
			{
				hasBeenHit = true;
			}
			NPC.chaseable = hasBeenHit;
			if (!hasBeenHit)
			{
				if (NPC.collideX)
				{
					NPC.velocity.X = NPC.velocity.X * -1f;
					NPC.direction *= -1;
					NPC.netUpdate = true;
				}
				if (NPC.collideY)
				{
					NPC.netUpdate = true;
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -1f;
						NPC.directionY = -1;
						NPC.localAI[2] = -1f;
					}
					else if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
						NPC.directionY = 1;
						NPC.localAI[2] = 1f;
					}
				}
				NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
				if (NPC.velocity.X < -2f || NPC.velocity.X > 2f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.95f;
				}
				if (NPC.localAI[2] == -1f)
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.01f;
					if ((double)NPC.velocity.Y < -0.3)
					{
						NPC.localAI[2] = 1f;
					}
				}
				else
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.01f;
					if ((double)NPC.velocity.Y > 0.3)
					{
						NPC.localAI[2] = -1f;
					}
				}
				if ((double)NPC.velocity.Y > 0.4 || (double)NPC.velocity.Y < -0.4)
				{
					NPC.velocity.Y = NPC.velocity.Y * 0.95f;
				}
				return;
			}
			NPC.noTileCollide = true;
			float num1446 = 7f;
			int num1447 = 480;
			float num244;
			if (NPC.localAI[1] == 1f)
			{
				NPC.localAI[1] = 0f;
				if (Main.rand.Next(4) == 0)
				{
					NPC.ai[0] = (float)num1447;
				}
			}
			NPC.TargetClosest(true);
			NPC.rotation = Math.Abs(NPC.velocity.X) * (float)NPC.direction * 0.1f;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			Vector2 value53 = NPC.Center + new Vector2((float)(NPC.direction * 20), 6f);
			Vector2 vector251 = Main.player[NPC.target].Center - value53;
			bool flag104 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
			NPC.localAI[0] += 1f;
			if (Main.netMode != 1 && NPC.localAI[0] >= 300f)
			{
				NPC.localAI[0] = 0f;
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					float speed = 5f;
					Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
					float num6 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X + (float)Main.rand.Next(-10, 11);
					float num7 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector.Y + (float)Main.rand.Next(-10, 11);
					float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
					num8 = speed / num8;
					num6 *= num8;
					num7 *= num8;
					int damage = 40;
					if (Main.expertMode)
					{
						damage = 30;
					}
					int random = Main.rand.Next(2);
					if (random == 0)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, num6, num7, 465, damage, 0f, Main.myPlayer, 0f, 0f);
					}
					else
					{
						Vector2 vec = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center);
						vec = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center + Main.player[NPC.target].velocity * 20f);
						if (vec.HasNaNs())
						{
							vec = new Vector2((float)NPC.direction, 0f);
						}
						for (int n = 0; n < 1; n++)
						{
							Vector2 vector4 = vec * 4f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, vector4.X, vector4.Y, 464, damage, 0f, Main.myPlayer, 0f, 1f);
						}
					}
				}
			}
			if (vector251.Length() > 400f || !flag104)
			{
				Vector2 value54 = vector251;
				if (value54.Length() > num1446)
				{
					value54.Normalize();
					value54 *= num1446;
				}
				int num1448 = 30;
				NPC.velocity = (NPC.velocity * (float)(num1448 - 1) + value54) / (float)num1448;
			}
			else
			{
				NPC.velocity *= 0.98f;
			}
			if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
			{
				SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
				int num;
				for (int num1449 = 0; num1449 < 20; num1449 = num + 1)
				{
					int num1450 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 20, 0f, 0f, 100, Color.Transparent, 1f);
					Dust dust = Main.dust[num1450];
					dust.velocity *= 3f;
					Main.dust[num1450].noGravity = true;
					Main.dust[num1450].scale = 2.5f;
					num = num1449;
				}
				NPC.Center = new Vector2(NPC.ai[2] * 16f, NPC.ai[3] * 16f);
				NPC.velocity = Vector2.Zero;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
				for (int num1451 = 0; num1451 < 20; num1451 = num + 1)
				{
					int num1452 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 20, 0f, 0f, 100, Color.Transparent, 1f);
					Dust dust = Main.dust[num1452];
					dust.velocity *= 3f;
					Main.dust[num1452].noGravity = true;
					Main.dust[num1452].scale = 2.5f;
					num = num1451;
				}
			}
			float[] var_9_48E3C_cp_0 = NPC.ai;
			int var_9_48E3C_cp_1 = 0;
			num244 = var_9_48E3C_cp_0[var_9_48E3C_cp_1];
			var_9_48E3C_cp_0[var_9_48E3C_cp_1] = num244 + 1f;
			if (NPC.ai[0] >= (float)num1447 && Main.netMode != 1)
			{
				NPC.ai[0] = 0f;
				Point point12 = NPC.Center.ToTileCoordinates();
				Point point13 = Main.player[NPC.target].Center.ToTileCoordinates();
				int num1453 = 20;
				int num1454 = 3;
				int num1455 = 10;
				int num1456 = 1;
				int num1457 = 0;
				bool flag106 = false;
				if (vector251.Length() > 2000f)
				{
					flag106 = true;
				}
				while (!flag106 && num1457 < 100)
				{
					num1457++;
					int num1458 = Main.rand.Next(point13.X - num1453, point13.X + num1453 + 1);
					int num1459 = Main.rand.Next(point13.Y - num1453, point13.Y + num1453 + 1);
					if ((num1459 < point13.Y - num1455 || num1459 > point13.Y + num1455 || num1458 < point13.X - num1455 || num1458 > point13.X + num1455) && (num1459 < point12.Y - num1454 || num1459 > point12.Y + num1454 || num1458 < point12.X - num1454 || num1458 > point12.X + num1454) && !Main.tile[num1458, num1459].HasUnactuatedTile)
					{
						bool flag107 = true;
						if (flag107 && (Main.tile[num1458, num1459].LiquidType == LiquidID.Lava))
						{
							flag107 = false;
						}
						if (flag107 && Collision.SolidTiles(num1458 - num1456, num1458 + num1456, num1459 - num1456, num1459 + num1456))
						{
							flag107 = false;
						}
						if (flag107)
						{
							NPC.ai[2] = (float)num1458;
							NPC.ai[3] = (float)num1459;
							break;
						}
					}
				}
				NPC.netUpdate = true;
			}
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return hasBeenHit;
			}
			return null;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (!Main.hardMode)
			{
				return 0f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer3 && spawnInfo.Water)
			{
				return 0.05f;
			}
			return spawnInfo.Player.ZoneDungeon ? 0.04f : 0f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new CelestialPillarsNotPresent(), Mod.Find<ModItem>("EidolonTablet").Type, 4));
			npcLoot.Add(new CommonDrop(ItemID.BlueLunaticHood, 4));
			npcLoot.Add(new CommonDrop(ItemID.BlueLunaticRobe, 4));
			npcLoot.Add(ItemDropRule.ByCondition(new DownedCalDoppelorPlantera(), ItemID.Ectoplasm, 1, 3, 6));
			npcLoot.Add(ItemDropRule.ByCondition(new DownedCalDoppelorPlantera(), Mod.Find<ModItem>("Lumenite").Type, 1, 8, 11));
			npcLoot.Add(ItemDropRule.ByCondition(new DownedCalDoppelorPlantera(), Mod.Find<ModItem>("Lumenite").Type, 2, 2, 5));
		}
	}
}