﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2;
using Terraria.ModLoader.Utilities;
using Terraria.WorldBuilding;
using CalamityModClassic1Point2.Items.Providence;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.UngodlySerpent
{
	public class UngodlySerpentHead : ModNPC
	{
		public bool flies = false;
		public bool directional = true;
		public float speed = 20f;
		public float turnSpeed = 0.2f;
		bool TailSpawned = false;
		
		public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Scale = 0.8f,
                PortraitScale = 0.8f,
                CustomTexturePath = "CalamityModClassic1Point2/NPCs/UngodlySerpent/Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 40
            };
            value.Position.X += 50;
            value.Position.Y += 35;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            //DisplayName.SetDefault("Ungodly Serpent");
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 210; //150
			NPC.npcSlots = 5f;
			NPC.width = 32; //324
			NPC.height = 100; //216
			NPC.defense = 10;
			NPC.lifeMax = 55225; //250000
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit8;
			NPC.DeathSound = SoundID.NPCDeath8;
			NPC.netAlways = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                new FlavorTextBestiaryInfoElement("Its days are numbered.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !NPC.downedMoonlord)
			{
				return 0f;
			}
			if (SpawnCondition.Underworld.Chance > 0f)
			{
				return SpawnCondition.Underworld.Chance / 8f;
			}
			return SpawnCondition.OverworldHallow.Chance / 8f;
		}
		
		public override void AI()
		{
			bool isHoly = Main.player[NPC.target].ZoneHallow;
			bool isHell = Main.player[NPC.target].ZoneUnderworldHeight;
			if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			if (NPC.life < (NPC.lifeMax * 0.8f) && NPC.life >= (NPC.lifeMax * 0.6f))
			{
				speed = 21f;
				turnSpeed = 0.21f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.6f) && NPC.life >= (NPC.lifeMax * 0.4f))
			{
				speed = 23f;
				turnSpeed = 0.23f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.4f) && NPC.life >= (NPC.lifeMax * 0.2f))
			{
				speed = 26f;
				turnSpeed = 0.26f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.2f))
			{
				speed = 30f;
				turnSpeed = 0.3f;
			}
			NPC.alpha -= 42;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (!TailSpawned)
            {
                int Previous = NPC.whoAmI;
                for (int num36 = 0; num36 < 31; num36++)
                {
                    int lol = 0;
                    if (num36 >= 0 && num36 < 29)
                    {
                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("UngodlySerpentBody").Type, NPC.whoAmI);
                    }
                    else if (num36 == 29)
					{
						lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("UngodlySerpentBody2").Type, NPC.whoAmI);
					}
                    else if (num36 == 30)
                    {
                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("UngodlySerpentTail").Type, NPC.whoAmI);
                    }
                    Main.npc[lol].realLife = NPC.whoAmI;
                    Main.npc[lol].ai[2] = (float)NPC.whoAmI;
                    Main.npc[lol].ai[1] = (float)Previous;
                    Main.npc[Previous].ai[0] = (float)lol;
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, lol, 0f, 0f, 0f, 0);
                    Previous = lol;
                }
                TailSpawned = true;
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
			bool flag94 = flies;
			if (!flag94)
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
								flag94 = true;
								break;
							}
						}
					}
				}
			}
			if (!flag94)
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
						flag94 = true;
					}
				}
			}
			if (directional)
			{
				if (NPC.velocity.X < 0f)
				{
					NPC.spriteDirection = 1;
				}
				else if (NPC.velocity.X > 0f)
				{
					NPC.spriteDirection = -1;
				}
			}
			if (Main.player[NPC.target].dead || !isHell || !isHoly)
			{
				flag94 = false;
				NPC.velocity.Y = NPC.velocity.Y + 1f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 1f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int num957 = 0; num957 < 200; num957++)
					{
						if (Main.npc[num957].aiStyle == NPC.aiStyle)
						{
							Main.npc[num957].active = false;
						}
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
				if (directional)
				{
					if (num191 < 0f)
					{
						NPC.spriteDirection = 1;
					}
					if (num191 > 0f)
					{
						NPC.spriteDirection = -1;
					}
				}
			}
			else
			{
				if (!flag94)
				{
					NPC.TargetClosest(true);
					NPC.velocity.Y = NPC.velocity.Y + (turnSpeed * 0.75f);
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
					bool flag21 = false;
					if (!flag21)
					{
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
				if (flag94)
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
					return;
				}
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 30; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 3, 5));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EnergyStaff>(), 10));
        }
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
	}
}