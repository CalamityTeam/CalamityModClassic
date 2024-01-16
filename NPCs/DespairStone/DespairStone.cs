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
using CalamityModClassic1Point1;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.DespairStone
{
	public class DespairStone : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "DespairStone");
			//Tooltip.SetDefault("Despair Stone");
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 70;
			NPC.width = 72; //324
			NPC.height = 72; //216
			NPC.defense = 38;
			NPC.lifeMax = 500;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 50, 0);
			NPC.HitSound = SoundID.NPCHit41;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.behindTiles = true;
			NPC.lavaImmune = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.BrimstoneCragsBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Do not be afraid.")

            });
        }
        public override void AI()
		{
			int num = 30;
			int num2 = 10;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
			{
				flag2 = true;
				NPC.ai[3] += 1f;
			}
			num2 = 4;
			bool flag4 = NPC.velocity.Y == 0f;
			for (int i = 0; i < 200; i++)
			{
				if (i != NPC.whoAmI && Main.npc[i].active && Main.npc[i].type == NPC.type && Math.Abs(NPC.position.X - Main.npc[i].position.X) + Math.Abs(NPC.position.Y - Main.npc[i].position.Y) < (float)NPC.width)
				{
					if (NPC.position.X < Main.npc[i].position.X)
					{
						NPC.velocity.X = NPC.velocity.X - 0.05f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X + 0.05f;
					}
					if (NPC.position.Y < Main.npc[i].position.Y)
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.05f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.05f;
					}
				}
			}
			if (flag4)
			{
				NPC.velocity.Y = 0f;
			}
			if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num || flag2)
			{
				NPC.ai[3] += 1f;
				flag3 = true;
			}
			else if (NPC.ai[3] > 0f)
			{
				NPC.ai[3] -= 1f;
			}
			if (NPC.ai[3] > (float)(num * num2))
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.justHit)
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.ai[3] == (float)num)
			{
				NPC.netUpdate = true;
			}
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num3 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X;
			float num4 = Main.player[NPC.target].position.Y - vector.Y;
			float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			if (num5 < 200f && !flag3)
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.velocity.Y == 0f && Math.Abs(NPC.velocity.X) > 3f && ((NPC.Center.X < Main.player[NPC.target].Center.X && NPC.velocity.X > 0f) || (NPC.Center.X > Main.player[NPC.target].Center.X && NPC.velocity.X < 0f)))
			{
				NPC.velocity.Y = NPC.velocity.Y - 4f;
				SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, 0f, -1f, 0, default(Color), 1f);
				}
			}
			if (NPC.ai[3] < (float)num)
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
							NPC.spriteDirection = NPC.direction;
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
			float num7 = 6f;
			float num8 = 0.07f;
			if (!flag && (NPC.velocity.Y == 0f || NPC.wet || (NPC.velocity.X <= 0f && NPC.direction < 0) || (NPC.velocity.X >= 0f && NPC.direction > 0)))
			{
				if (Math.Sign(NPC.velocity.X) != NPC.direction)
				{
					NPC.velocity.X = NPC.velocity.X * 0.92f;
				}
				float num9 = MathHelper.Lerp(0.6f, 1f, Math.Abs(Main.windSpeedCurrent)) * (float)Math.Sign(Main.windSpeedCurrent);
				if (!Main.player[NPC.target].ZoneSandstorm)
				{
					num9 = 0f;
				}
				num7 = 5f + num9 * (float)NPC.direction * 4f;
				num8 = 0.2f;
				if (NPC.velocity.X < -num7 || NPC.velocity.X > num7)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity *= 0.8f;
					}
				}
				else if (NPC.velocity.X < num7 && NPC.direction == 1)
				{
					NPC.velocity.X = NPC.velocity.X + num8;
					if (NPC.velocity.X > num7)
					{
						NPC.velocity.X = num7;
					}
				}
				else if (NPC.velocity.X > -num7 && NPC.direction == -1)
				{
					NPC.velocity.X = NPC.velocity.X - num8;
					if (NPC.velocity.X < -num7)
					{
						NPC.velocity.X = -num7;
					}
				}
			}
			if (NPC.velocity.Y >= 0f)
			{
				int num10 = 0;
				if (NPC.velocity.X < 0f)
				{
					num10 = -1;
				}
				if (NPC.velocity.X > 0f)
				{
					num10 = 1;
                }
                Vector2 position = NPC.position;
                position.X += NPC.velocity.X;
                int x = (int)((position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num10)) / 16f);
                int y = (int)((position.Y + (float)NPC.height - 1f) / 16f);
                // Fuck tile collision _ YuH
                Tile t_xy = Main.tile[x, y]; ;
                Tile t_xy1 = Main.tile[x, y - 1];
                Tile t_xy2 = Main.tile[x, y - 2];
                Tile t_xy3 = Main.tile[x, y - 3];
                Tile t_xOffY3 = Main.tile[x - num10, y - 3]; // 3 down, offset 1 in the direction of the NPC's movement
                Tile t_xy4 = Main.tile[x, y - 4];
                bool positionCheck = (float)(x * 16) < position.X + (float)NPC.width && (float)(x * 16 + 16) > position.X;
                bool tileSolidityCheck1 = t_xy.HasUnactuatedTile && !t_xy.TopSlope && !t_xy1.TopSlope && Main.tileSolid[t_xy.TileType] && !Main.tileSolidTop[t_xy.TileType];
                bool oneBelowIsSolidHalf = t_xy1.IsHalfBlock && t_xy1.HasUnactuatedTile;
                bool canFallThrough = !t_xy1.HasUnactuatedTile || !Main.tileSolid[t_xy1.TileType] || Main.tileSolidTop[t_xy1.TileType] || (t_xy1.IsHalfBlock && (!t_xy4.HasUnactuatedTile || !Main.tileSolid[t_xy4.TileType] || Main.tileSolidTop[t_xy4.TileType]));
                bool twoDownIsNonSolid = !t_xy2.HasUnactuatedTile || !Main.tileSolid[t_xy2.TileType] || Main.tileSolidTop[t_xy2.TileType];
                bool threeDownIsNonSolid = !t_xy3.HasUnactuatedTile || !Main.tileSolid[t_xy3.TileType] || Main.tileSolidTop[t_xy3.TileType];
                // Notice it doesn't check for platforms in ther offset position. This is why walking AIs twirl on 1-wide platforms in hellevators. They have to turn around before this check succeeds.
                bool threeDownOffsetIsNonSolid = !t_xOffY3.HasUnactuatedTile || !Main.tileSolid[t_xOffY3.TileType];
                if (positionCheck && (tileSolidityCheck1 || oneBelowIsSolidHalf) && canFallThrough && twoDownIsNonSolid && threeDownIsNonSolid && threeDownOffsetIsNonSolid)
                {
                    float tilePixelPosition = (float)(y * 16);
                    if (Main.tile[x, y].IsHalfBlock)
                    {
                        tilePixelPosition += 8f;
                    }
                    if (Main.tile[x, y - 1].IsHalfBlock)
                    {
                        tilePixelPosition -= 8f;
                    }
                    if (tilePixelPosition < position.Y + (float)NPC.height)
                    {
                        float percentageTileRisen = position.Y + (float)NPC.height - tilePixelPosition;
                        if ((double)percentageTileRisen <= 16.1)
                        {
                            NPC.gfxOffY += NPC.position.Y + (float)NPC.height - tilePixelPosition;
                            NPC.position.Y = tilePixelPosition - (float)NPC.height;
                            if (percentageTileRisen < 9f)
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
            if (NPC.velocity.Y == 0f)
            {
                int NPCTileX = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 2) * NPC.direction) + NPC.velocity.X * 5f) / 16f);
                int NPCTileY = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                int spriteDirection = NPC.spriteDirection;
                spriteDirection *= -1;
                if ((NPC.velocity.X < 0f && spriteDirection == -1) || (NPC.velocity.X > 0f && spriteDirection == 1))
                {
                    if (Main.tile[NPCTileX, NPCTileY - 2].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[NPCTileX, NPCTileY - 2].TileType])
                    {
                        if (Main.tile[NPCTileX, NPCTileY - 3].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[NPCTileX, NPCTileY - 3].TileType])
                        {
                            NPC.velocity.Y = -8.5f;
                            NPC.netUpdate = true;
                        }
                        else
                        {
                            NPC.velocity.Y = -7.5f;
                            NPC.netUpdate = true;
                        }
                    }
                    else if (Main.tile[NPCTileX, NPCTileY - 1].HasUnactuatedTile && !Main.tile[NPCTileX, NPCTileY - 1].TopSlope && Main.tileSolid[(int)Main.tile[NPCTileX, NPCTileY - 1].TileType])
                    {
                        NPC.velocity.Y = -7f;
                        NPC.netUpdate = true;
                    }
                    else if (NPC.position.Y + (float)NPC.height - (float)(NPCTileY * 16) > 20f && Main.tile[NPCTileX, NPCTileY].HasUnactuatedTile && !Main.tile[NPCTileX, NPCTileY].TopSlope && Main.tileSolid[(int)Main.tile[NPCTileX, NPCTileY].TileType])
                    {
                        NPC.velocity.Y = -6f;
                        NPC.netUpdate = true;
                    }
                    else if ((NPC.directionY < 0 || Math.Abs(NPC.velocity.X) > 3f) && (!Main.tile[NPCTileX, NPCTileY + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[NPCTileX, NPCTileY + 1].TileType]) && (!Main.tile[NPCTileX, NPCTileY + 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[NPCTileX, NPCTileY + 2].TileType]) && (!Main.tile[NPCTileX + NPC.direction, NPCTileY + 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[NPCTileX + NPC.direction, NPCTileY + 3].TileType]))
                    {
                        NPC.velocity.Y = -8f;
                        NPC.netUpdate = true;
                    }
                }
            }
            NPC.rotation += NPC.velocity.X * 0.05f;
            NPC.spriteDirection = -NPC.direction;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.GetModPlayer<CalamityPlayer>().ZoneCalamity ? 0.25f : 0f; 
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