using System;
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
using Terraria.WorldBuilding;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.AstrumDeus
{
	[AutoloadBossHead]
	public class AstrumDeusHead : ModNPC
	{
		public bool flies = false;
		public const int minLength = 50;
		public const int maxLength = 51;
		public int addOrbiter = 0;
		public int addOrbiter2 = 0;
		public float speed = 0.15f;
		public float turnSpeed = 0.1f;
		bool TailSpawned = false;
		public bool secondStage = false;
		public bool thirdStage = false;
		
		public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Scale = 0.8f,
                PortraitScale = 0.8f,
                CustomTexturePath = "CalamityModClassic1Point2/NPCs/AstrumDeus/Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 40
            };
            value.Position.X += 50;
            value.Position.Y += 35;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            //DisplayName.SetDefault("Astrum Deus");
            SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.AstralMeteorBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("The god of the stars.")

            });
        }

        public override void SetDefaults()
		{
			NPC.damage = 110; //150
			NPC.npcSlots = 5f;
			NPC.width = 56; //324
			NPC.height = 56; //216
			NPC.defense = 60;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 165000 : 150000; //250000
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.2f;
			if (Main.expertMode)
			{
				NPC.scale = 1.35f;
			}
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			Music = MusicID.Boss3;
		}
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
			int defenseDown = (int)(30f * (1f - (float)NPC.life / (float)NPC.lifeMax));
			NPC.defense = NPC.defDefense - defenseDown;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if ((NPC.life <= NPC.lifeMax * 0.65f))
			{
				if (secondStage == false && Main.netMode != NetmodeID.MultiplayerClient) 
				{
					SoundEngine.PlaySound(SoundID.Item74, NPC.position);
					for (int I = 0; I < 3; I++) 
					{
						int FireEye = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.Center.X + (Math.Sin(I * 120) * 75)), (int)(NPC.Center.Y + (Math.Cos(I * 120) * 75)), Mod.Find<ModNPC>("AstrumDeusProbe").Type, NPC.whoAmI, 0, 0, 0, -1);
						NPC Eye = Main.npc[FireEye];
						Eye.ai[0] = I * 120;
						Eye.ai[3] = I * 120;
					}
					secondStage = true;
				}
			}
			if ((NPC.life <= NPC.lifeMax * 0.3f))
			{
				if (thirdStage == false && Main.netMode != NetmodeID.MultiplayerClient) 
				{
					SoundEngine.PlaySound(SoundID.Item74, NPC.position);
					for (int I = 0; I < 5; I++) 
					{
						int FireEye = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.Center.X + (Math.Sin(I * 72) * 150)), (int)(NPC.Center.Y + (Math.Cos(I * 72) * 150)), Mod.Find<ModNPC>("AstrumDeusProbe2").Type, NPC.whoAmI, 0, 0, 0, -1);
						NPC Eye = Main.npc[FireEye];
						Eye.ai[0] = I * 72;
						Eye.ai[3] = I * 72;
					}
					thirdStage = true;
				}
			}
			if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			float speedMult = expertMode ? 2f : 1.8f;
			float life = (float)NPC.life;
			float totalLife = (float)NPC.lifeMax;
			float newSpeed = speed * (speedMult - (life / totalLife));
			float newTurnSpeed = turnSpeed * (speedMult - (life / totalLife));
			if (NPC.alpha != 0)
			{
				for (int num934 = 0; num934 < 2; num934++)
				{
					int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TheDestroyer, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			NPC.alpha -= 42;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (!TailSpawned && NPC.ai[0] == 0f)
	            {
	                int Previous = NPC.whoAmI;
	                for (int num36 = 0; num36 < maxLength; num36++)
	                {
	                    int lol = 0;
	                    if (num36 >= 0 && num36 < minLength)
	                    {
	                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("AstrumDeusBody").Type, NPC.whoAmI);
	                    }
	                    else
	                    {
	                        lol = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("AstrumDeusTail").Type, NPC.whoAmI);
	                    }
	                    if (num36 % 2 == 0)
	                    {
	                    	Main.npc[lol].localAI[3] = 1f;
	                    }
	                    Main.npc[lol].realLife = NPC.whoAmI;
	                    Main.npc[lol].ai[2] = (float)NPC.whoAmI;
	                    Main.npc[lol].ai[1] = (float)Previous;
	                    Main.npc[Previous].ai[0] = (float)lol;
	                    Previous = lol;
	                }
	                TailSpawned = true;
	            }
				if (!NPC.active && Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
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
				Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
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
			else
			{
				NPC.localAI[1] = 0f;
			}
			float maxSpeed = 20f;
			if (Main.dayTime || Main.player[NPC.target].dead)
			{
				flag94 = false;
				NPC.velocity.Y = NPC.velocity.Y + 1f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 1f;
					maxSpeed = 40f;
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
			float num188 = newSpeed;
			float num189 = newTurnSpeed;
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
				if (!flag94)
				{
					NPC.TargetClosest(true);
					NPC.velocity.Y = NPC.velocity.Y + 0.15f;
					if (NPC.velocity.Y > maxSpeed)
					{
						NPC.velocity.Y = maxSpeed;
					}
					if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)maxSpeed * 0.4)
					{
						if (NPC.velocity.X < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num188 * 1.1f;
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X + num188 * 1.1f;
						}
					}
					else if (NPC.velocity.Y == maxSpeed)
					{
						if (NPC.velocity.X < num191)
						{
							NPC.velocity.X = NPC.velocity.X + num188;
						}
						else if (NPC.velocity.X > num191)
						{
							NPC.velocity.X = NPC.velocity.X - num188;
						}
					}
					else if (NPC.velocity.Y > 4f)
					{
						if (NPC.velocity.X < 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num188 * 0.9f;
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X - num188 * 0.9f;
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
					float num198 = maxSpeed / num193;
					num191 *= num198;
					num192 *= num198;
					if (((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f)) && ((NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f)))
					{
						if (NPC.velocity.X < num191)
						{
							NPC.velocity.X = NPC.velocity.X + num189;
						}
						else if (NPC.velocity.X > num191)
						{
							NPC.velocity.X = NPC.velocity.X - num189;
						}
						if (NPC.velocity.Y < num192)
						{
							NPC.velocity.Y = NPC.velocity.Y + num189;
						}
						else if (NPC.velocity.Y > num192)
						{
							NPC.velocity.Y = NPC.velocity.Y - num189;
						}
					}
					if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
					{
						if (NPC.velocity.X < num191)
						{
							NPC.velocity.X = NPC.velocity.X + num188;
						}
						else
						{
							if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - num188;
							}
						}
						if (NPC.velocity.Y < num192)
						{
							NPC.velocity.Y = NPC.velocity.Y + num188;
						}
						else
						{
							if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - num188;
							}
						}
						if ((double)System.Math.Abs(num192) < (double)maxSpeed * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
						{
							if (NPC.velocity.Y > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num188 * 2f;
							}
							else
							{
								NPC.velocity.Y = NPC.velocity.Y - num188 * 2f;
							}
						}
						if ((double)System.Math.Abs(num191) < (double)maxSpeed * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
						{
							if (NPC.velocity.X > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num188 * 2f;
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X - num188 * 2f;
							}
						}
					}
					else
					{
						if (num196 > num197)
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + num188 * 1.1f;
							}
							else if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - num188 * 1.1f;
							}
							if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)maxSpeed * 0.5)
							{
								if (NPC.velocity.Y > 0f)
								{
									NPC.velocity.Y = NPC.velocity.Y + num188;
								}
								else
								{
									NPC.velocity.Y = NPC.velocity.Y - num188;
								}
							}
						}
						else
						{
							if (NPC.velocity.Y < num192)
							{
								NPC.velocity.Y = NPC.velocity.Y + num188 * 1.1f;
							}
							else if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - num188 * 1.1f;
							}
							if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)maxSpeed * 0.5)
							{
								if (NPC.velocity.X > 0f)
								{
									NPC.velocity.X = NPC.velocity.X + num188;
								}
								else
								{
									NPC.velocity.X = NPC.velocity.X - num188;
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
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.penetrate == -1 && !projectile.minion)
			{
				modifiers.FinalDamage /= 5;
			}
			else if (projectile.penetrate > 1)
			{
				modifiers.FinalDamage /= projectile.penetrate;
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 10; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.None;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.downedStarGod)
			{
				target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 150, true);
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 1.5f);
		}
	}
}