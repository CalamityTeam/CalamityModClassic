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

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class Cryon : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryon");
			Main.npcFrameCount[NPC.type] = 8;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("Ice constructs with runes inscribed within their heads... Who might their creator be?")
            });
        }
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
            AIType = -1;
			NPC.damage = 42;
			NPC.width = 50; //324
			NPC.height = 64; //216
			NPC.defense = 20;
			NPC.lifeMax = 300;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CryonBanner").Type;
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
                num8 = 0.1f;
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
                    bool flag6 = NPC.type == 410 || NPC.type == 423;
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
                    else if ((NPC.directionY < 0 || Math.Abs(NPC.velocity.X) > num18) && (!flag6 || !Main.tile[num15, num16 + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num15, num16 + 1].TileType]) && (!Main.tile[num15, num16 + 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num15, num16 + 2].TileType]) && (!Main.tile[num15 + NPC.direction, num16 + 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[num15 + NPC.direction, num16 + 3].TileType]))
                    {
                        NPC.velocity.Y = -8f;
                        NPC.netUpdate = true;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y > 0f || NPC.velocity.Y < 0f)
            {
                NPC.spriteDirection = NPC.direction;
                NPC.frame.Y = frameHeight * 7;
                NPC.frameCounter = 0.0;
            }
            else
            {
                NPC.spriteDirection = NPC.direction;
                NPC.frameCounter += (double)(NPC.velocity.Length() / 2f);
                if (NPC.frameCounter > 12.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y >= frameHeight * 6)
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
				Main.hardMode && !spawnInfo.PlayerInTown && !spawnInfo.Player.ZoneOldOneArmy && !Main.snowMoon && !Main.pumpkinMoon ? 0.015f : 0f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, 300, true);
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 30, true);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 92, hit.HitDirection, -1f, 0, default(Color), 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 92, hit.HitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EssenceofEleum").Type, 2));
        }
	}
}