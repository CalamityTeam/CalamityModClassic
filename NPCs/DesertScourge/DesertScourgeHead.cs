﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Placeables;
using CalamityModClassic1Point1.Items.DesertScourge;
using CalamityModClassic1Point1.Items.Weapons;
using CalamityModClassic1Point1.Items.Armor;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.DesertScourge
{
	[AutoloadBossHead]
	public class DesertScourgeHead : ModNPC
	{
		public bool flies = false;
		public bool directional = false;
		public float speed = 15f;
		public float turnSpeed = 0.15f;
		bool TailSpawned = false;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "CalamityModClassic1Point1/NPCs/DesertScourge/Bestiary"
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public override void SetDefaults()
		{
			//NPC.name = "DesertScourgeHead");
			//Tooltip.SetDefault("Desert Scourge");
			NPC.damage = 40; //150
			NPC.npcSlots = 5f;
			NPC.width = 32; //324
			NPC.height = 80; //216
			NPC.defense = 0;
			NPC.lifeMax = 5225; //250000
			NPC.aiStyle = 6; //new
			Main.npcFrameCount[NPC.type] = 1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 5, 0, 0);
			NPC.alpha = 255;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
			Music = MusicID.Boss1;
			if (Main.expertMode)
			{
				NPC.scale = 1.15f;
			}
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                new FlavorTextBestiaryInfoElement("When the Jungle Tyrant requested the witch to burn the sea, this former sea serpent had to adapt to the desert instead of its usual diet of sharks and crabs.")

            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.ZoneDesert && NPC.downedBoss3 ? 0.00001f : 0f; 
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
			if (CalamityGlobalNPC.bossBuff && CalamityGlobalNPC.superBossBuff)
			{
				speed = 50f;
				turnSpeed = 0.5f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.8f) && NPC.life >= (NPC.lifeMax * 0.6f))
			{
				speed = 16f;
				turnSpeed = 0.16f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.6f) && NPC.life >= (NPC.lifeMax * 0.4f))
			{
				speed = 17f;
				turnSpeed = 0.17f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.4f) && NPC.life >= (NPC.lifeMax * 0.2f))
			{
				speed = 19f;
				turnSpeed = 0.19f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.2f))
			{
				speed = 21f;
				turnSpeed = 0.21f;
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
                    if (num36 >= 0 && num36 < 30)
                    {
                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DesertScourgeBody").Type, NPC.whoAmI);
                    }
                    else
                    {
                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DesertScourgeTail").Type, NPC.whoAmI);
                    }
                    Main.npc[lol].realLife = NPC.whoAmI;
                    Main.npc[lol].ai[2] = (float)NPC.whoAmI;
                    Main.npc[lol].ai[1] = (float)Previous;
                    Main.npc[Previous].ai[0] = (float)lol;
                    NetMessage.SendData(23, -1, -1, null, lol, 0f, 0f, 0f, 0);
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
			if (Main.player[NPC.target].dead)
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 30; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override bool CheckDead()
		{
			if (!CalamityWorld.stopAerialite)
			{
				Main.NewText("The underground is shimmering with cyan light!", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
			}
			return true;
		}


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<DesertScourgeTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<DesertScourgeBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VictoryShard>(), 1, 7, 14));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Coral, 1, 5, 9));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Starfish, 1, 5, 9));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Seashell, 1, 5, 9));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SeaboundStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AquaticDischarge>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Barinade>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<StormSpray>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertScourgeMask>(), 7));
        }
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Bleeding, 600, true);
			}
		}
	}
}