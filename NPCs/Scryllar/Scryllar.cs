﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Scryllar
{
	public class Scryllar : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Scryllar");
			//Tooltip.SetDefault("Scryllar");
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 85;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 18;
			NPC.lifeMax = 400;
			NPC.alpha = 100;
			NPC.knockBackResist = 0.7f;
			NPC.value = Item.buyPrice(0, 0, 50, 0);
			NPC.HitSound = SoundID.NPCHit49;
			NPC.DeathSound = SoundID.NPCDeath51;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.lavaImmune = true;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.BrimstoneCragsBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Lost souls that died in the crags. This is a common theme.")

            });
        }

        public override void AI()
		{
			if ((double)Math.Abs(NPC.velocity.X) > 0.2)
			{
				NPC.spriteDirection = -NPC.direction;
        	}
			bool flag19 = false;
			if (NPC.justHit) 
			{
				NPC.ai[2] = 0f;
			}
			if (NPC.ai[2] >= 0f) 
			{
				int num282 = 16;
				bool flag21 = false;
				bool flag22 = false;
				if (NPC.position.X > NPC.ai[0] - (float)num282 && NPC.position.X < NPC.ai[0] + (float)num282) 
				{
					flag21 = true;
				} 
				else if ((NPC.velocity.X < 0f && NPC.direction > 0) || (NPC.velocity.X > 0f && NPC.direction < 0))
				{
					flag21 = true;
				}
				num282 += 24;
				if (NPC.position.Y > NPC.ai[1] - (float)num282 && NPC.position.Y < NPC.ai[1] + (float)num282) 
				{
					flag22 = true;
				}
				if (flag21 && flag22) 
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 30f && num282 == 16) 
					{
						flag19 = true;
					}
					if (NPC.ai[2] >= 60f) 
					{
						NPC.ai[2] = -200f;
						NPC.direction *= -1;
						NPC.velocity.X = NPC.velocity.X * -1f;
						NPC.collideX = false;
					}
				} 
				else 
				{
					NPC.ai[0] = NPC.position.X;
					NPC.ai[1] = NPC.position.Y;
					NPC.ai[2] = 0f;
				}
				NPC.TargetClosest(true);
			} 
			else 
			{
				NPC.TargetClosest(true);
				NPC.ai[2] += 2f;
			}
			int num283 = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction * 2;
			int num284 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
			bool flag23 = true;
			int num285 = 3;
			for (int num308 = num284; num308 < num284 + num285; num308++) 
			{
				if ((Main.tile[num283, num308].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num283, num308].TileType]) || Main.tile[num283, num308].LiquidAmount > 0) 
				{
					flag23 = false;
					break;
				}
			}
			if (Main.player[NPC.target].npcTypeNoAggro[NPC.type]) 
			{
				bool flag25 = false;
				for (int num309 = num284; num309 < num284 + num285 - 2; num309++) 
				{
					if ((Main.tile[num283, num309].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num283, num309].TileType]) || Main.tile[num283, num309].LiquidAmount > 0) 
					{
						flag25 = true;
						break;
					}
				}
				NPC.directionY = (!flag25).ToDirectionInt();
			}
			if (flag19) 
			{
				flag23 = true;
			}
			if (flag23) 
			{
				NPC.velocity.Y = NPC.velocity.Y + 0.1f;
				if (NPC.velocity.Y > 3f) 
				{
					NPC.velocity.Y = 3f;
				}
			} 
			else 
			{
				if (NPC.directionY < 0 && NPC.velocity.Y > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
				}
				if (NPC.velocity.Y < -4f) 
				{
					NPC.velocity.Y = -4f;
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
			float num311 = 4f;
			if (NPC.direction == -1 && NPC.velocity.X > -num311) 
			{
				NPC.velocity.X = NPC.velocity.X - 0.1f;
				if (NPC.velocity.X > num311) 
				{
					NPC.velocity.X = NPC.velocity.X - 0.1f;
				} 
				else if (NPC.velocity.X > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X + 0.05f;
				}
				if (NPC.velocity.X < -num311) 
				{
					NPC.velocity.X = -num311;
				}
			} 
			else if (NPC.direction == 1 && NPC.velocity.X < num311) 
			{
				NPC.velocity.X = NPC.velocity.X + 0.1f;
				if (NPC.velocity.X < -num311) 
				{
					NPC.velocity.X = NPC.velocity.X + 0.1f;
				} 
				else if (NPC.velocity.X < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - 0.05f;
				}
				if (NPC.velocity.X > num311) 
				{
					NPC.velocity.X = num311;
				}
			}
			num311 = 1.5f;
			if (NPC.directionY == -1 && NPC.velocity.Y > -num311) 
			{
				NPC.velocity.Y = NPC.velocity.Y - 0.04f;
				if (NPC.velocity.Y > num311) 
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.05f;
				} 
				else if (NPC.velocity.Y > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.03f;
				}
				if (NPC.velocity.Y < -num311) 
				{
					NPC.velocity.Y = -num311;
				}
			} 
			else if (NPC.directionY == 1 && NPC.velocity.Y < num311) 
			{
				NPC.velocity.Y = NPC.velocity.Y + 0.04f;
				if (NPC.velocity.Y < -num311) 
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.05f;
				} 
				else if (NPC.velocity.Y < 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y - 0.03f;
				}
				if (NPC.velocity.Y > num311) 
				{
					NPC.velocity.Y = num311;
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.GetModPlayer<CalamityPlayer1Point1>().ZoneCalamity ? 0.25f : 0f; 
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 40; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}