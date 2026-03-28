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
	public class ArmoredDiggerHead : ModNPC
	{
		bool TailSpawned = false;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Armored Digger");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/NormalNPCs/ArmoredDigger_Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 40
            };
            value.Position.Y += 40;
            value.Position.X += 48;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("A mechanical worm constructed by Draedon for the purposes of extracting resources... but it's been abandoned.")
            });
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 90;
			NPC.npcSlots = 10f;
			NPC.width = 54; //324
			NPC.height = 54; //216
			NPC.defense = 15;
			NPC.lifeMax = 20000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
            AIType = -1;
            AnimationType = 10;
			NPC.value = Item.buyPrice(0, 5, 0, 0);
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.netAlways = true;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ArmoredDiggerBanner").Type;
		}

        public override void AI()
        {
            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            if (Main.netMode != 1)
            {
                if (!TailSpawned)
                {
                    NPC.ai[3] = (float)NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    int num2 = NPC.whoAmI;
                    int num3 = 40;
                    for (int j = 0; j <= num3; j++)
                    {
                        int num4 = Mod.Find<ModNPC>("ArmoredDiggerBody").Type;
                        if (j == num3)
                        {
                            num4 = Mod.Find<ModNPC>("ArmoredDiggerTail").Type;
                        }
                        int num5 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), num4, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num5].ai[3] = (float)NPC.whoAmI;
                        Main.npc[num5].realLife = NPC.whoAmI;
                        Main.npc[num5].ai[1] = (float)num2;
                        Main.npc[num2].ai[0] = (float)num5;
                        NetMessage.SendData(23, -1, -1, null, num5, 0f, 0f, 0f, 0, 0, 0);
                        num2 = num5;
                    }
                    TailSpawned = true;
                }
            }
            int num12 = (int)(NPC.position.X / 16f) - 1;
            int num13 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num14 = (int)(NPC.position.Y / 16f) - 1;
            int num15 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num12 < 0)
            {
                num12 = 0;
            }
            if (num13 > Main.maxTilesX)
            {
                num13 = Main.maxTilesX;
            }
            if (num14 < 0)
            {
                num14 = 0;
            }
            if (num15 > Main.maxTilesY)
            {
                num15 = Main.maxTilesY;
            }
            bool flag2 = false;
            if (!flag2)
            {
                for (int k = num12; k < num13; k++)
                {
                    for (int l = num14; l < num15; l++)
                    {
                        if (Main.tile[k, l] != null && ((Main.tile[k, l].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[k, l].TileType] || (Main.tileSolidTop[(int)Main.tile[k, l].TileType] && Main.tile[k, l].TileFrameY == 0))) || Main.tile[k, l].LiquidAmount > 64))
                        {
                            Vector2 vector2;
                            vector2.X = (float)(k * 16);
                            vector2.Y = (float)(l * 16);
                            if (NPC.position.X + (float)NPC.width > vector2.X && NPC.position.X < vector2.X + 16f && NPC.position.Y + (float)NPC.height > vector2.Y && NPC.position.Y < vector2.Y + 16f)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flag2)
            {
                NPC.localAI[1] = 1f;
                Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int num16 = 1000;
                bool flag3 = true;
                if (NPC.position.Y > Main.player[NPC.target].position.Y)
                {
                    for (int m = 0; m < 255; m++)
                    {
                        if (Main.player[m].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)Main.player[m].position.X - num16, (int)Main.player[m].position.Y - num16, num16 * 2, num16 * 2);
                            if (rectangle.Intersects(rectangle2))
                            {
                                flag3 = false;
                                break;
                            }
                        }
                    }
                    if (flag3)
                    {
                        flag2 = true;
                    }
                }
            }
            else
            {
                NPC.localAI[1] = 0f;
            }
            float num17 = 16f;
            if (Main.player[NPC.target].dead || (double)Main.player[NPC.target].position.Y < Main.rockLayer * 16.0)
            {
                flag2 = false;
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if ((double)NPC.position.Y > (double)((Main.maxTilesY - 200) * 16))
                {
                    NPC.velocity.Y = NPC.velocity.Y + 1f;
                    num17 = 32f;
                }
                if ((double)NPC.position.Y > (double)((Main.maxTilesY - 200) * 16))
                {
                    for (int a = 0; a < 200; a++)
                    {
                        if (Main.npc[a].type == Mod.Find<ModNPC>("ArmoredDiggerHead").Type || Main.npc[a].type == Mod.Find<ModNPC>("ArmoredDiggerBody").Type ||
                            Main.npc[a].type == Mod.Find<ModNPC>("ArmoredDiggerTail").Type)
                        {
                            Main.npc[a].active = false;
                        }
                    }
                }
            }
            float num18 = 0.1f;
            float num19 = 0.15f;
            Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num20 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
            float num21 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
            num20 = (float)((int)(num20 / 16f) * 16);
            num21 = (float)((int)(num21 / 16f) * 16);
            vector3.X = (float)((int)(vector3.X / 16f) * 16);
            vector3.Y = (float)((int)(vector3.Y / 16f) * 16);
            num20 -= vector3.X;
            num21 -= vector3.Y;
            float num22 = (float)Math.Sqrt((double)(num20 * num20 + num21 * num21));
            if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num20 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector3.X;
                    num21 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector3.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)Math.Atan2((double)num21, (double)num20) + 1.57f;
                num22 = (float)Math.Sqrt((double)(num20 * num20 + num21 * num21));
                int num23 = (int)(44f * NPC.scale);
                num22 = (num22 - (float)num23) / num22;
                num20 *= num22;
                num21 *= num22;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num20;
                NPC.position.Y = NPC.position.Y + num21;
                return;
            }
            if (!flag2)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                if (NPC.velocity.Y > num17)
                {
                    NPC.velocity.Y = num17;
                }
                if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num17 * 0.4)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 1.1f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 1.1f;
                    }
                }
                else if (NPC.velocity.Y == num17)
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18;
                    }
                }
                else if (NPC.velocity.Y > 4f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 0.9f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 0.9f;
                    }
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num24 = num22 / 40f;
                    if (num24 < 10f)
                    {
                        num24 = 10f;
                    }
                    if (num24 > 20f)
                    {
                        num24 = 20f;
                    }
                    NPC.soundDelay = (int)num24;
                    SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                }
                num22 = (float)Math.Sqrt((double)(num20 * num20 + num21 * num21));
                float num25 = Math.Abs(num20);
                float num26 = Math.Abs(num21);
                float num27 = num17 / num22;
                num20 *= num27;
                num21 *= num27;
                if (((NPC.velocity.X > 0f && num20 > 0f) || (NPC.velocity.X < 0f && num20 < 0f)) && ((NPC.velocity.Y > 0f && num21 > 0f) || (NPC.velocity.Y < 0f && num21 < 0f)))
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num19;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num19;
                    }
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num19;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num19;
                    }
                }
                if ((NPC.velocity.X > 0f && num20 > 0f) || (NPC.velocity.X < 0f && num20 < 0f) || (NPC.velocity.Y > 0f && num21 > 0f) || (NPC.velocity.Y < 0f && num21 < 0f))
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18;
                    }
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num18;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num18;
                    }
                    if ((double)Math.Abs(num21) < (double)num17 * 0.2 && ((NPC.velocity.X > 0f && num20 < 0f) || (NPC.velocity.X < 0f && num20 > 0f)))
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num18 * 2f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num18 * 2f;
                        }
                    }
                    if ((double)Math.Abs(num20) < (double)num17 * 0.2 && ((NPC.velocity.Y > 0f && num21 < 0f) || (NPC.velocity.Y < 0f && num21 > 0f)))
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num18 * 2f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num18 * 2f;
                        }
                    }
                }
                else if (num25 > num26)
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 1.1f;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 1.1f;
                    }
                    if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num17 * 0.5)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num18;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num18;
                        }
                    }
                }
                else
                {
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num18 * 1.1f;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num18 * 1.1f;
                    }
                    if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)num17 * 0.5)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num18;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num18;
                        }
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
            if (flag2)
            {
                if (NPC.localAI[0] != 1f)
                {
                    NPC.netUpdate = true;
                }
                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0f)
                {
                    NPC.netUpdate = true;
                }
                NPC.localAI[0] = 0f;
            }
            if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
            {
                NPC.netUpdate = true;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool CheckActive()
        {
            return false;
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Chilled, 240, true);
			target.AddBuff(BuffID.Electrified, 180, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("LeadWizard").Type, 40));
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("DraedonsRemote").Type, 3));
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("DemonicBoneAsh").Type, 1, 2, 5));
        }
	}
}