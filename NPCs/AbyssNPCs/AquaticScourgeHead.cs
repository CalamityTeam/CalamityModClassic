using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items.AquaticScourge;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	[AutoloadBossHead]
	public class AquaticScourgeHead : ModNPC
	{
		public bool detectsPlayer = false;
		public bool flies = true;
		public const int minLength = 30;
		public const int maxLength = 31;
		public float speed = 5f; //10
		public float turnSpeed = 0.08f; //0.15
		bool TailSpawned = false;
		public bool despawning = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aquatic Scourge");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.6f,
				PortraitScale = 0.6f,
				CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/AbyssNPCs/AquaticScourge_Bestiary"
			};
			value.Position.X += 40f;
			value.Position.Y += 20f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("A distant relative of the Desert Scourge, its appearance has been changed heavily by the sulphurous waters; Due to the ample food supply and its adaptions to its environment, it's far less aggressive than its kin.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 16f;
			NPC.damage = 100;
			NPC.width = 100; //36
			NPC.height = 90; //20
			NPC.defense = 40;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 85000 : 73000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 100000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 4300000 : 4000000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 12, 0, 0);
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
			SpawnModBiomes = new int[] { ModContent.GetInstance<Sulphur>().Type };
			if (Main.expertMode)
			{
				NPC.scale = 1.15f;
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
				npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
					ModContent.ItemType<AquaticScourgeBag>(), 
					1, 
					5, 
					5));
				npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AquaticScourgeBag>()));
		}

		public override void AI()
		{
			if (NPC.justHit || (double)NPC.life <= (double)NPC.lifeMax * 0.99 || CalamityWorldPreTrailer.bossRushActive)
			{
				detectsPlayer = true;
				NPC.damage = Main.expertMode ? 250 : 80;
				NPC.boss = true;
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/AquaticScourge");
			}
			else
			{
				NPC.damage = 0;
			}
			NPC.chaseable = detectsPlayer;
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
				if (!TailSpawned && NPC.ai[0] == 0f)
				{
					int Previous = NPC.whoAmI;
					for (int num36 = 0; num36 < maxLength; num36++)
					{
						int lol = 0;
						if (num36 >= 0 && num36 < minLength)
						{
							if (num36 % 2 == 0)
							{
								lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("AquaticScourgeBody").Type, NPC.whoAmI);
							}
							else
							{
								lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("AquaticScourgeBodyAlt").Type, NPC.whoAmI);
							}
						}
						else
						{
							lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("AquaticScourgeTail").Type, NPC.whoAmI);
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
				if (detectsPlayer)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 120f : 180f))
					{
						int npcPoxX = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
						int npcPoxY = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
						if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) < 300f &&
							!Main.tile[npcPoxX, npcPoxY].HasTile)
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							NPC.netUpdate = true;
							int random = Main.rand.Next(3);
							SoundEngine.PlaySound(SoundID.NPCHit8, NPC.Center);
							Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
							if (random == 0 && NPC.CountNPCS(Mod.Find<ModNPC>("AquaticSeekerHead").Type) < 1)
							{
								NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("AquaticSeekerHead").Type);
							}
							else if (random == 1 && NPC.CountNPCS(Mod.Find<ModNPC>("AquaticUrchin").Type) < 3)
							{
								NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("AquaticUrchin").Type);
							}
							else if (NPC.CountNPCS(Mod.Find<ModNPC>("AquaticParasite").Type) < 8)
							{
								NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("AquaticParasite").Type);
							}
						}
					}
					NPC.localAI[1] += 1f;
					if (Main.player[NPC.target].gravDir == -1f)
					{
						NPC.localAI[1] += 2f;
					}
					if (NPC.localAI[1] >= ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 300f : 450f))
					{
						int npcPoxX = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
						int npcPoxY = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
						if (!Main.tile[npcPoxX, npcPoxY].HasTile && Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 300f)
						{
							NPC.localAI[1] = 0f;
							NPC.TargetClosest(true);
							NPC.netUpdate = true;
							BarfShitUp();
						}
					}
				}
			}
			bool notOcean = Main.player[NPC.target].position.Y < 800f ||
				(double)Main.player[NPC.target].position.Y > Main.worldSurface * 16.0 ||
				(Main.player[NPC.target].position.X > 6400f && Main.player[NPC.target].position.X < (float)(Main.maxTilesX * 16 - 6400));
			if (Main.player[NPC.target].dead)
			{
				despawning = true;
				NPC.TargetClosest(false);
				flies = false;
				NPC.velocity.Y = NPC.velocity.Y + 5f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 5f;
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
			else
			{
				despawning = false;
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
			if (NPC.velocity.X < 0f)
			{
				NPC.spriteDirection = -1;
			}
			else if (NPC.velocity.X > 0f)
			{
				NPC.spriteDirection = 1;
			}
			bool canFly = flies;
			if (Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(false);
			}
			NPC.alpha -= 42;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (!NPC.AnyNPCs(Mod.Find<ModNPC>("AquaticScourgeTail").Type))
			{
				NPC.active = false;
			}
			float num188 = speed;
			float num189 = turnSpeed;
			Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
			float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
			int num42 = -1;
			int num43 = (int)(Main.player[NPC.target].Center.X / 16f);
			int num44 = (int)(Main.player[NPC.target].Center.Y / 16f);
			for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
			{
				for (int num46 = num44; num46 <= num44 + 15; num46++)
				{
					if (WorldGen.SolidTile2(num45, num46))
					{
						num42 = num46;
						break;
					}
				}
				if (num42 > 0)
				{
					break;
				}
			}
			if (num42 > 0)
			{
				num42 *= 16;
				float num47 = (float)(num42 + (notOcean ? 800 : 400)); //800
				if (!detectsPlayer)
				{
					num192 = num47;
					if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < (notOcean ? 500f : 400f)) //500
					{
						if (NPC.velocity.X > 0f)
						{
							num191 = Main.player[NPC.target].Center.X + (notOcean ? 600f : 480f); //600
						}
						else
						{
							num191 = Main.player[NPC.target].Center.X - (notOcean ? 600f : 480f); //600
						}
					}
				}
			}
			if (detectsPlayer)
			{
				num188 = 8f;
				num189 = 0.12f;
				if (!Main.player[NPC.target].wet)
				{
					num188 = 13f;
					num189 = 0.16f;
				}
				if (notOcean)
				{
					num188 = 15f;
					num189 = 0.18f;
				}
				if (Main.player[NPC.target].gravDir == -1f)
				{
					num188 = 20f;
					num189 = 0.2f;
				}
			}
			float num48 = num188 * 1.3f;
			float num49 = num188 * 0.7f;
			float num50 = NPC.velocity.Length();
			if (num50 > 0f)
			{
				if (num50 > num48)
				{
					NPC.velocity.Normalize();
					NPC.velocity *= num48;
				}
				else if (num50 < num49)
				{
					NPC.velocity.Normalize();
					NPC.velocity *= num49;
				}
			}
			if (!detectsPlayer)
			{
				for (int num51 = 0; num51 < 200; num51++)
				{
					if (Main.npc[num51].active && Main.npc[num51].type == NPC.type && num51 != NPC.whoAmI)
					{
						Vector2 vector3 = Main.npc[num51].Center - NPC.Center;
						if (vector3.Length() < 400f) //400
						{
							vector3.Normalize();
							vector3 *= 1000f;
							num191 -= vector3.X; //-
							num192 -= vector3.Y; //-
						}
					}
				}
			}
			else
			{
				for (int num52 = 0; num52 < 200; num52++)
				{
					if (Main.npc[num52].active && Main.npc[num52].type == NPC.type && num52 != NPC.whoAmI)
					{
						Vector2 vector4 = Main.npc[num52].Center - NPC.Center;
						if (vector4.Length() < 60f) //60
						{
							vector4.Normalize();
							vector4 *= 200f;
							num191 -= vector4.X;
							num192 -= vector4.Y;
						}
					}
				}
			}
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
				if (num191 < 0f)
				{
					NPC.spriteDirection = -1;
				}
				else if (num191 > 0f)
				{
					NPC.spriteDirection = 1;
				}
			}
			else
			{
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
							NPC.velocity.X = NPC.velocity.X + num189 * 2f; //changed from 2
						}
						else
						{
							NPC.velocity.X = NPC.velocity.X - num189 * 2f; //changed from 2
						}
					}
				}
				else
				{
					if (num196 > num197)
					{
						if (NPC.velocity.X < num191)
						{
							NPC.velocity.X = NPC.velocity.X + num189 * 1.1f; //changed from 1.1
						}
						else if (NPC.velocity.X > num191)
						{
							NPC.velocity.X = NPC.velocity.X - num189 * 1.1f; //changed from 1.1
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
			NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
		}

		public void BarfShitUp()
		{
			SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
			if (Main.netMode != 1)
			{
				Vector2 valueBoom = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float spreadBoom = 15f * 0.0174f;
				double startAngleBoom = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spreadBoom / 2;
				double deltaAngleBoom = spreadBoom / 8f;
				double offsetAngleBoom;
				int iBoom;
				int damageBoom = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive) ? 28 : 33;
				for (iBoom = 0; iBoom < 15; iBoom++)
				{
					int projectileType = (Main.rand.Next(2) == 0 ? Mod.Find<ModProjectile>("SandTooth").Type : Mod.Find<ModProjectile>("SandBlast").Type);
					if (projectileType == Mod.Find<ModProjectile>("SandTooth").Type)
					{
						damageBoom = Main.expertMode ? 24 : 30;
					}
					offsetAngleBoom = (startAngleBoom + deltaAngleBoom * (iBoom + iBoom * iBoom) / 2f) + 32f * iBoom;
					int boom1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), valueBoom.X, valueBoom.Y, (float)(Math.Sin(offsetAngleBoom) * 6.5f), (float)(Math.Cos(offsetAngleBoom) * 6.5f), projectileType, damageBoom, 0f, Main.myPlayer, 0f, 0f);
					int boom2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), valueBoom.X, valueBoom.Y, (float)(-Math.Sin(offsetAngleBoom) * 6.5f), (float)(-Math.Cos(offsetAngleBoom) * 6.5f), projectileType, damageBoom, 0f, Main.myPlayer, 0f, 0f);
				}
				damageBoom = Main.expertMode ? 31 : 36;
				int num320 = Main.rand.Next(5, 9);
				int num3;
				for (int num321 = 0; num321 < num320; num321 = num3 + 1)
				{
					Vector2 vector15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					vector15.Normalize();
					vector15 *= (float)Main.rand.Next(50, 401) * 0.01f;
					Projectile.NewProjectile(NPC.GetSource_FromThis(null),NPC.Center.X, NPC.Center.Y, vector15.X, vector15.Y, Mod.Find<ModProjectile>("SandPoisonCloud").Type, damageBoom, 0f, Main.myPlayer, 0f, (float)Main.rand.Next(-45, 1));
					num3 = num321;
				}
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if ((projectile.type == ProjectileID.HallowStar || projectile.type == ProjectileID.CrystalShard) && projectile.CountsAsClass(DamageClass.Ranged))
			{
				projectile.damage /= 2;
			}
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return detectsPlayer;
			}
			return null;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe)
			{
				return 0f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur && spawnInfo.Water)
			{
				if (!NPC.AnyNPCs(Mod.Find<ModNPC>("AquaticScourgeHead").Type))
					return 0.01f;
			}
			return 0f;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = Mod.Find<ModItem>("SulphurousSand").Type;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 15; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ASHead").Type, 1f);
				}
			}
		}

		public override bool CheckActive()
		{
			if (detectsPlayer && !Main.player[NPC.target].dead && !despawning)
			{
				return false;
			}
			if (NPC.timeLeft <= 0 && Main.netMode != 1)
			{
				for (int k = (int)NPC.ai[0]; k > 0; k = (int)Main.npc[k].ai[0])
				{
					if (Main.npc[k].active)
					{
						Main.npc[k].active = false;
						if (Main.netMode == 2)
						{
							Main.npc[k].life = 0;
							Main.npc[k].netSkip = -1;
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			return true;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Bleeding, 360, true);
			target.AddBuff(BuffID.Venom, 360, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 180);
			}
		}
	}
}