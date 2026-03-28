using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer;
using Terraria.GameContent.Bestiary;
using Terraria.WorldBuilding;

namespace CalamityModClassicPreTrailer.NPCs.TheDevourerofGods
{
	public class DevourerofGodsHead2 : ModNPC
	{
		public bool tail = false;
        public bool flies = false;
		public const int minLength = 15;
		public const int maxLength = 16;
        public int invinceTime = 180;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Guardian");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.75f,
                PortraitScale = 0.75f,
                CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/TheDevourerofGods/CosmicGuardian_Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 40
            };
            value.Position.X += 62f;
            value.Position.Y += 35f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("His right hand man, it will spring into action once the Devourer feels threatened.")
            });
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 180; //150
			NPC.npcSlots = 5f;
			NPC.width = 64; //324
			NPC.height = 76; //216
			NPC.defense = 0;
            NPC.lifeMax = 100000; //192000
            NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
		}

        public override void AI()
        {
			bool expertMode = Main.expertMode;
			float playerRunAcceleration = Main.player[NPC.target].velocity.Y == 0f ? Math.Abs(Main.player[NPC.target].moveSpeed * 0.3f) : (Main.player[NPC.target].runAcceleration * 0.8f);
            if (playerRunAcceleration <= 1f)
            {
                playerRunAcceleration = 1f;
            }
            if (invinceTime > 0)
            {
                invinceTime--;
                NPC.damage = 0;
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.damage = expertMode ? 360 : 180;
                NPC.dontTakeDamage = false;
            }
            Vector2 vector = NPC.Center;
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            if (NPC.alpha != 0)
            {
                int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num935].noGravity = true;
                Main.dust[num935].noLight = true;
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            if (Main.netMode != 1)
            {
                if (!tail && NPC.ai[0] == 0f)
                {
                    int Previous = NPC.whoAmI;
                    for (int segmentSpawn = 0; segmentSpawn < maxLength; segmentSpawn++)
                    {
                        int segment = 0;
                        if (segmentSpawn >= 0 && segmentSpawn < minLength)
                        {
                            segment = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsBody2").Type, NPC.whoAmI);
                        }
                        else
                        {
                            segment = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsTail2").Type, NPC.whoAmI);
                        }
                        Main.npc[segment].realLife = NPC.whoAmI;
                        Main.npc[segment].ai[2] = (float)NPC.whoAmI;
                        Main.npc[segment].ai[1] = (float)Previous;
                        Main.npc[Previous].ai[0] = (float)segment;
                        NPC.netUpdate = true;
                        Previous = segment;
                    }
                    tail = true;
                }
                if (!NPC.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (!Main.npc[CalamityGlobalNPC.DoGHead].active)
            {
                for (int num569 = 0; num569 < 200; num569++)
                {
                    if (Main.npc[num569].active && (Main.npc[num569].type == Mod.Find<ModNPC>("DevourerofGodsHead2").Type || Main.npc[num569].type == Mod.Find<ModNPC>("DevourerofGodsBody2").Type || Main.npc[num569].type == Mod.Find<ModNPC>("DevourerofGodsTail2").Type))
                    {
                        Main.npc[num569].active = false;
                    }
                }
            }
            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(false);
                flies = false;
                NPC.velocity.Y = NPC.velocity.Y + 2f;
                if ((double)NPC.position.Y > Main.worldSurface * 16.0)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 2f;
                }
                if ((double)NPC.position.Y > Main.rockLayer * 16.0)
                {
                    for (int a = 0; a < 200; a++)
                    {
                        if (Main.npc[a].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[a].active = false;
                        }
                    }
                }
            }
            int num180 = (int)(NPC.position.X / 16f) - 1;
            int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num182 = (int)(NPC.position.Y / 16f) - 1;
            int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            float speed = playerRunAcceleration * 16f;
            float turnSpeed = playerRunAcceleration * 0.23f;
            if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 3000f) //RAGE
            {
                speed = playerRunAcceleration * 60f;
                turnSpeed = playerRunAcceleration * 0.8f;
            }
            if (!flies)
            {
                for (int num952 = num180; num952 < num181; num952++)
                {
                    for (int num953 = num182; num953 < num183; num953++)
                    {
                        if (Main.tile[num952, num953] != null && ((Main.tile[num952, num953].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num952, num953].TileType] || (Main.tileSolidTop[(int)Main.tile[num952, num953].TileType] && Main.tile[num952, num953].TileFrameY == 0))) || Main.tile[num952, num953].LiquidAmount > 64))
                        {
                            Vector2 vector105;
                            vector105.X = (float)(num952 * 16);
                            vector105.Y = (float)(num953 * 16);
                            if (NPC.position.X + (float)NPC.width > vector105.X && NPC.position.X < vector105.X + 16f && NPC.position.Y + (float)NPC.height > vector105.Y && NPC.position.Y < vector105.Y + 16f)
                            {
                                flies = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flies)
            {
                NPC.localAI[1] = 1f;
                Rectangle rectangle12 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int num954 = 1000;
                bool flag95 = true;
                if (NPC.position.Y > Main.player[NPC.target].position.Y)
                {
                    for (int num955 = 0; num955 < 255; num955++)
                    {
                        if (Main.player[num955].active)
                        {
                            Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - num954, (int)Main.player[num955].position.Y - num954, num954 * 2, num954 * 2);
                            if (rectangle12.Intersects(rectangle13))
                            {
                                flag95 = false;
                                break;
                            }
                        }
                    }
                    if (flag95)
                    {
                        flies = true;
                    }
                }
            }
            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
            float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
                    num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                int num194 = NPC.width;
                num193 = (num193 - (float)num194) / num193;
                num191 *= num193;
                num192 *= num193;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num191;
                NPC.position.Y = NPC.position.Y + num192;
            }
            else
            {
                if (!flies)
                {
                    NPC.TargetClosest(true);
                    NPC.velocity.Y = NPC.velocity.Y + (turnSpeed * 0.5f);
                    if (NPC.velocity.Y > num188)
                    {
                        NPC.velocity.Y = num188;
                    }
                    if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.4)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
                        }
                    }
                    else if (NPC.velocity.Y == num188)
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189;
                        }
                        else if (NPC.velocity.X > num191)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189;
                        }
                    }
                    else if (NPC.velocity.Y > 4f)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 0.9f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
                    {
                        float num195 = num193 / 40f;
                        if (num195 < 10f)
                        {
                            num195 = 10f;
                        }
                        if (num195 > 20f)
                        {
                            num195 = 20f;
                        }
                        NPC.soundDelay = (int)num195;
                        SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                    }
                    num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                    float num196 = System.Math.Abs(num191);
                    float num197 = System.Math.Abs(num192);
                    float num198 = num188 / num193;
                    num191 *= num198;
                    num192 *= num198;
                    if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189;
                        }
                        else
                        {
                            if (NPC.velocity.X > num191)
                            {
                                NPC.velocity.X = NPC.velocity.X - num189;
                            }
                        }
                        if (NPC.velocity.Y < num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189;
                        }
                        else
                        {
                            if (NPC.velocity.Y > num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189;
                            }
                        }
                        if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
                            }
                        }
                        if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189 * 2f;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - num189 * 2f;
                            }
                        }
                    }
                    else
                    {
                        if (num196 > num197)
                        {
                            if (NPC.velocity.X < num191)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
                            }
                            else if (NPC.velocity.X > num191)
                            {
                                NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
                            }
                            if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num189;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189;
                                }
                            }
                        }
                        else
                        {
                            if (NPC.velocity.Y < num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
                            }
                            else if (NPC.velocity.Y > num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
                            }
                            if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num189;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - num189;
                                }
                            }
                        }
                    }
                }
            }
            NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
            if (flies)
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
			potionType = ItemID.None;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
                if (Main.netMode != NetmodeID.Server)
				    Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("DoT").Type, 1f);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 15; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 30; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 180, true);
			target.AddBuff(BuffID.Frostburn, 180, true);
			target.AddBuff(BuffID.Darkness, 180, true);
		}
	}
}