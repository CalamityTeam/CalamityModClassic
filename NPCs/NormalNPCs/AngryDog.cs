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

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class AngryDog : ModNPC
	{
        public bool reset = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Angry Dog");
			Main.npcFrameCount[NPC.type] = 10;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("he still angy")
            });
        }
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 10;
			NPC.width = 46; //324
			NPC.height = 30; //216
			NPC.defense = 4;
			NPC.lifeMax = 50;
			if (CalamityWorldPreTrailer.downedCryogen)
			{
				NPC.damage = 84;
				NPC.defense = 20;
				NPC.lifeMax = 1000;
			}
			NPC.knockBackResist = 0.3f;
			AnimationType = 329;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 3, 0);
			NPC.HitSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AngryDogHit");
			NPC.DeathSound = SoundID.NPCDeath5;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("AngryDogBanner").Type;
		}

        public override void AI()
        {
            if (Main.rand.Next(900) == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/AngryDogGrowl"), NPC.position);
            }
            bool phase2 = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
            if (phase2)
            {
                if (!reset)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[3] = 0f;
                    reset = true;
                    NPC.netUpdate = true;
                }
                if (NPC.ai[1] < 7f)
                {
                    NPC.ai[1] += 1f;
                }
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
                    for (int k = 0; k < 5; k++)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, 33, 0f, -1f, 0, default(Color), 1f);
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
                    num7 = 5f;
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
                    int num11 = (int)((position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num10)) / 16f);
                    int num12 = (int)((position.Y + (float)NPC.height - 1f) / 16f);
                    if ((float)(num11 * 16) < position.X + (float)NPC.width && (float)(num11 * 16 + 16) > position.X && ((Main.tile[num11, num12].HasUnactuatedTile && !Main.tile[num11, num12].TopSlope && !Main.tile[num11, num12 - 1].TopSlope && Main.tileSolid[(int)Main.tile[num11, num12].TileType] && !Main.tileSolidTop[(int)Main.tile[num11, num12].TileType]) || (Main.tile[num11, num12 - 1].IsHalfBlock && Main.tile[num11, num12 - 1].HasUnactuatedTile)) && (!Main.tile[num11, num12 - 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num11, num12 - 1].TileType] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 1].TileType] || (Main.tile[num11, num12 - 1].IsHalfBlock && (!Main.tile[num11, num12 - 4].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num11, num12 - 4].TileType] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 4].TileType]))) && (!Main.tile[num11, num12 - 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num11, num12 - 2].TileType] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 2].TileType]) && (!Main.tile[num11, num12 - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num11, num12 - 3].TileType] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 3].TileType]) && (!Main.tile[num11 - num10, num12 - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num11 - num10, num12 - 3].TileType]))
                    {
                        float num13 = (float)(num12 * 16);
                        if (Main.tile[num11, num12].IsHalfBlock)
                        {
                            num13 += 8f;
                        }
                        if (Main.tile[num11, num12 - 1].IsHalfBlock)
                        {
                            num13 -= 8f;
                        }
                        if (num13 < position.Y + (float)NPC.height)
                        {
                            float num14 = position.Y + (float)NPC.height - num13;
                            if ((double)num14 <= 16.1)
                            {
                                NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num13;
                                NPC.position.Y = num13 - (float)NPC.height;
                                if (num14 < 9f)
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
                    int num15 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 2) * NPC.direction) + NPC.velocity.X * 5f) / 16f);
                    int num16 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                    int num17 = NPC.spriteDirection;
                    num17 *= -1;
                    if ((NPC.velocity.X < 0f && num17 == -1) || (NPC.velocity.X > 0f && num17 == 1))
                    {
                        float num18 = 3f;
                        if (Main.tile[num15, num16 - 2].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num15, num16 - 2].TileType])
                        {
                            if (Main.tile[num15, num16 - 3].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num15, num16 - 3].TileType])
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
                        else if (Main.tile[num15, num16 - 1].HasUnactuatedTile && !Main.tile[num15, num16 - 1].TopSlope && Main.tileSolid[(int)Main.tile[num15, num16 - 1].TileType])
                        {
                            NPC.velocity.Y = -7f;
                            NPC.netUpdate = true;
                        }
                        else if (NPC.position.Y + (float)NPC.height - (float)(num16 * 16) > 20f && Main.tile[num15, num16].HasUnactuatedTile && !Main.tile[num15, num16].TopSlope && Main.tileSolid[(int)Main.tile[num15, num16].TileType])
                        {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        }
                        else if ((NPC.directionY < 0 || Math.Abs(NPC.velocity.X) > num18) && (!Main.tile[num15, num16 + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num15, num16 + 1].TileType]) && (!Main.tile[num15, num16 + 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num15, num16 + 2].TileType]) && (!Main.tile[num15 + NPC.direction, num16 + 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num15 + NPC.direction, num16 + 3].TileType]))
                        {
                            NPC.velocity.Y = -8f;
                            NPC.netUpdate = true;
                        }
                    }
                }
                NPC.rotation += NPC.velocity.X * 0.05f;
                NPC.spriteDirection = -NPC.direction;
                return;
            }
            int num19 = 30;
            int num20 = 10;
            bool flag19= false;
            bool flag20 = false;
            bool flag30 = false;
            if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
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
            float num6 = 6f;
            float num70 = 0.07f;
            if (!flag19&& (NPC.velocity.Y == 0f || NPC.wet || (NPC.velocity.X <= 0f && NPC.direction < 0) || (NPC.velocity.X >= 0f && NPC.direction > 0)))
            {
                if (NPC.velocity.X < -num6 || NPC.velocity.X > num6)
                {
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity *= 0.8f;
                    }
                }
                else if (NPC.velocity.X < num6 && NPC.direction == 1)
                {
                    NPC.velocity.X = NPC.velocity.X + num70;
                    if (NPC.velocity.X > num6)
                    {
                        NPC.velocity.X = num6;
                    }
                }
                else if (NPC.velocity.X > -num6 && NPC.direction == -1)
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
            if (NPC.velocity.Y == 0f)
            {
                int num14 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 2) * NPC.direction) + NPC.velocity.X * 5f) / 16f);
                int num15 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                int num16 = NPC.spriteDirection;
                if ((NPC.velocity.X < 0f && num16 == -1) || (NPC.velocity.X > 0f && num16 == 1))
                {
                    float num17 = 3f;
                    if (Main.tile[num14, num15 - 2].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num14, num15 - 2].TileType])
                    {
                        if (Main.tile[num14, num15 - 3].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[num14, num15 - 3].TileType])
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
                    else if (Main.tile[num14, num15 - 1].HasUnactuatedTile && !Main.tile[num14, num15 - 1].TopSlope && Main.tileSolid[(int)Main.tile[num14, num15 - 1].TileType])
                    {
                        NPC.velocity.Y = -7f;
                        NPC.netUpdate = true;
                    }
                    else if (NPC.position.Y + (float)NPC.height - (float)(num15 * 16) > 20f && Main.tile[num14, num15].HasUnactuatedTile && !Main.tile[num14, num15].TopSlope && Main.tileSolid[(int)Main.tile[num14, num15].TileType])
                    {
                        NPC.velocity.Y = -6f;
                        NPC.netUpdate = true;
                    }
                    else if ((NPC.directionY < 0 || Math.Abs(NPC.velocity.X) > num17) && (!Main.tile[num14, num15 + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num14, num15 + 1].TileType]) && (!Main.tile[num14, num15 + 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num14, num15 + 2].TileType]) && (!Main.tile[num14 + NPC.direction, num15 + 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num14 + NPC.direction, num15 + 3].TileType]))
                    {
                        NPC.velocity.Y = -8f;
                        NPC.netUpdate = true;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[1] < 7f && NPC.ai[1] > 0f)
            {
                NPC.frame.Y = frameHeight * 8;
                NPC.frameCounter = 0.0;
                return;
            }
            if (NPC.ai[1] >= 7f)
            {
                NPC.frame.Y = frameHeight * 9;
                NPC.frameCounter = 0.0;
                return;
            }
            if (NPC.velocity.Y > 0f || NPC.velocity.Y < 0f)
            {
                NPC.frame.Y = frameHeight * 5;
                NPC.frameCounter = 0.0;
            }
            else
            {
                NPC.spriteDirection = NPC.direction;
                NPC.frameCounter += (double)(NPC.velocity.Length() / 8f);
                if (NPC.frameCounter > 12.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y >= frameHeight * 7)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneSnow && 
            	!spawnInfo.Player.ZoneTowerStardust &&
            	!spawnInfo.Player.ZoneTowerSolar &&
            	!spawnInfo.Player.ZoneTowerVortex &&
            	!spawnInfo.Player.ZoneTowerNebula &&
                !spawnInfo.Player.ZoneDungeon && 
				!spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea &&
				!spawnInfo.PlayerInTown && !spawnInfo.Player.ZoneOldOneArmy && !Main.snowMoon && !Main.pumpkinMoon ? 0.012f : 0f;
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Bleeding, 180, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ItemID.Leather, 2, 1, 3));
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Cryophobia").Type, 100));
        }
	}
}