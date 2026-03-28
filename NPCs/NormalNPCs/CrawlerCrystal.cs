using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class CrawlerCrystal : ModNPC
	{
		private bool detected = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystal Crawler");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				new FlavorTextBestiaryInfoElement("A scaredy lizard from the underground covered in Crystals, when threatened it will do its best to run away! poor thing.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.3f;
			NPC.aiStyle = -1;
			NPC.damage = 35;
			NPC.width = 44; //324
			NPC.height = 34; //216
			NPC.defense = 18;
			NPC.lifeMax = 300;
			NPC.knockBackResist = 0.15f;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit33;
			NPC.DeathSound = SoundID.NPCDeath36;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CrystalCrawlerBanner").Type;
		}

		public override void FindFrame(int frameHeight)
		{
			if (!detected)
			{
				NPC.frame.Y = frameHeight * 4;
				NPC.frameCounter = 0.0;
				return;
			}
			NPC.spriteDirection = -NPC.direction;
			NPC.frameCounter += (double)(NPC.velocity.Length() / 8f);
			if (NPC.frameCounter > 2.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 3)
			{
				NPC.frame.Y = 0;
			}
		}

		public override void AI()
		{
			if (!detected)
				NPC.TargetClosest(true);
			if (((Main.player[NPC.target].Center - NPC.Center).Length() < 100f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position,
					Main.player[NPC.target].width, Main.player[NPC.target].height)) || NPC.justHit)
				detected = true;
			if (!detected)
				return;
			int num19 = 30;
			int num20 = 10;
			bool flag19 = false;
			bool flag20 = false;
			bool flag30 = false;
			if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction > 0) || (NPC.velocity.X < 0f && NPC.direction < 0)))
			{
				flag20 = true;
				NPC.ai[3] += 1f;
			}
			if ((NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num19) | flag20)
			{
				NPC.ai[3] += 1f;
				flag30 = true;
			}
			else if (NPC.ai[3] > 0f)
			{
				NPC.ai[3] -= 1f;
			}
			if (NPC.ai[3] > (float)(num19 * num20))
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.justHit)
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.ai[3] == (float)num19)
			{
				NPC.netUpdate = true;
			}
			Vector2 vector19 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float arg_31B_0 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector19.X;
			float num30 = Main.player[NPC.target].position.Y - vector19.Y;
			float num40 = (float)Math.Sqrt((double)(arg_31B_0 * arg_31B_0 + num30 * num30));
			if (num40 < 200f && !flag30)
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.ai[3] < (float)num19)
			{
				NPC.TargetClosest(true);
			}
			else
			{
				if (NPC.velocity.X == 0f)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.ai[0] += 1f;
						if (NPC.ai[0] >= 2f)
						{
							NPC.direction *= -1;
							NPC.spriteDirection = -NPC.direction;
							NPC.ai[0] = 0f;
						}
					}
				}
				else
				{
					NPC.ai[0] = 0f;
				}
				NPC.directionY = -1;
				if (NPC.direction == 0)
				{
					NPC.direction = 1;
				}
			}
			float num6 = 7f; //5
			float num70 = 0.07f; //0.05
			if (!flag19 && (NPC.velocity.Y == 0f || NPC.wet || (NPC.velocity.X <= 0f && NPC.direction > 0) || (NPC.velocity.X >= 0f && NPC.direction < 0)))
			{
				if (NPC.velocity.X < -num6 || NPC.velocity.X > num6)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity *= 0.8f;
					}
				}
				else if (NPC.velocity.X < num6 && NPC.direction == -1)
				{
					NPC.velocity.X = NPC.velocity.X + num70;
					if (NPC.velocity.X > num6)
					{
						NPC.velocity.X = num6;
					}
				}
				else if (NPC.velocity.X > -num6 && NPC.direction == 1)
				{
					NPC.velocity.X = NPC.velocity.X - num70;
					if (NPC.velocity.X < -num6)
					{
						NPC.velocity.X = -num6;
					}
				}
			}
			if (NPC.velocity.Y >= 0f)
			{
				int num9 = 0;
				if (NPC.velocity.X < 0f)
				{
					num9 = -1;
				}
				if (NPC.velocity.X > 0f)
				{
					num9 = 1;
				}
				Vector2 position = NPC.position;
				position.X += NPC.velocity.X;
				int num10 = (int)((position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num9)) / 16f);
				int num11 = (int)((position.Y + (float)NPC.height - 1f) / 16f);
				if ((float)(num10 * 16) < position.X + (float)NPC.width && (float)(num10 * 16 + 16) > position.X && ((Main.tile[num10, num11].HasUnactuatedTile && !Main.tile[num10, num11].TopSlope && !Main.tile[num10, num11 - 1].TopSlope && Main.tileSolid[(int)Main.tile[num10, num11].TileType] && !Main.tileSolidTop[(int)Main.tile[num10, num11].TileType]) || (Main.tile[num10, num11 - 1].IsHalfBlock && Main.tile[num10, num11 - 1].HasUnactuatedTile)) && (!Main.tile[num10, num11 - 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num10, num11 - 1].TileType] || Main.tileSolidTop[(int)Main.tile[num10, num11 - 1].TileType] || (Main.tile[num10, num11 - 1].IsHalfBlock && (!Main.tile[num10, num11 - 4].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num10, num11 - 4].TileType] || Main.tileSolidTop[(int)Main.tile[num10, num11 - 4].TileType]))) && (!Main.tile[num10, num11 - 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num10, num11 - 2].TileType] || Main.tileSolidTop[(int)Main.tile[num10, num11 - 2].TileType]) && (!Main.tile[num10, num11 - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num10, num11 - 3].TileType] || Main.tileSolidTop[(int)Main.tile[num10, num11 - 3].TileType]) && (!Main.tile[num10 - num9, num11 - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num10 - num9, num11 - 3].TileType]))
				{
					float num12 = (float)(num11 * 16);
					if (Main.tile[num10, num11].IsHalfBlock)
					{
						num12 += 8f;
					}
					if (Main.tile[num10, num11 - 1].IsHalfBlock)
					{
						num12 -= 8f;
					}
					if (num12 < position.Y + (float)NPC.height)
					{
						float num13 = position.Y + (float)NPC.height - num12;
						if ((double)num13 <= 16.1)
						{
							NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num12;
							NPC.position.Y = num12 - (float)NPC.height;
							if (num13 < 9f)
							{
								NPC.stepSpeed = 1f;
							}
							else
							{
								NPC.stepSpeed = 2f;
							}
						}
					}
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
			{
				return 0f;
			}
			return SpawnCondition.EnchantedSword.Chance;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(ItemID.CrystalShard, 1, 2, 5));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("CrystalBlade").Type, 5));
		}
	}
}