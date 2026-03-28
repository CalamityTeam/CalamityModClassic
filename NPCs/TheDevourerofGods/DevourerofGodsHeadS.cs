using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items.TheDevourerofGods;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;

namespace CalamityModClassicPreTrailer.NPCs.TheDevourerofGods
{
	[AutoloadBossHead]
	public class DevourerofGodsHeadS : ModNPC
	{
		private bool tail = false;
		private const int minLength = 120;
		private const int maxLength = 121;
		private bool halfLife = false;
		private int flameTimer = 900;
		private int laserShoot = 0;
		private float phaseSwitch = 0f;
		private float[] shotSpacing = new float[4] { 1050f, 1050f, 1050f, 1050f };
		private const float spacingVar = 105;
		private const int totalShots = 20;
		private int idleCounter = (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) ? 540 : 360;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Devourer of Gods");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.damage = 300; //150
			NPC.npcSlots = 5f;
			NPC.width = 100; //130
			NPC.height = 144; //150
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 1875000 : 1650000; //720000 672000
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 3060000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 10000000 : 9200000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.takenDamageMultiplier = 1.25f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.1f;
			NPC.boss = true;
			NPC.value = Item.buyPrice(1, 0, 0, 0);
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/UniversalCollapse");
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<DevourerofGodsBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DevourerofGodsBag>()));
		}


		public override void BossHeadRotation(ref float rotation)
		{
			rotation = NPC.rotation;
		}

		public override void AI()
		{
			CalamityGlobalNPC.DoGHead = NPC.whoAmI;
			Vector2 vector = NPC.Center;
			bool flies = NPC.ai[2] == 0f;
			bool expertMode = Main.expertMode;
			bool speedBoost2 = (double)NPC.life <= (double)NPC.lifeMax * 0.6 || (CalamityWorldPreTrailer.bossRushActive && (double)NPC.life <= (double)NPC.lifeMax * 0.9); //speed increase
			bool speedBoost4 = (double)NPC.life <= (double)NPC.lifeMax * 0.2 && !CalamityWorldPreTrailer.bossRushActive; //speed increase
			bool breathFireMore = (double)NPC.life <= (double)NPC.lifeMax * 0.1;
			if (speedBoost2 && !speedBoost4)
			{
				if (NPC.localAI[3] == 0f) //start laser wall phase
				{
					if (Main.netMode != 1)
					{
						NPC.localAI[2] += 1f;
						if (NPC.localAI[2] >= 720f)
						{
							NPC.localAI[2] = 0f;
							NPC.localAI[3] = 1f;
							NPC.netUpdate = true;
						}
					}
				}
				else if (NPC.localAI[3] == 1f) //turn invisible and fire laser walls
				{
					NPC.alpha += 4;
					if (NPC.alpha == 204) //255
					{
						laserShoot = 0;
					}
					if (NPC.alpha >= 204) //255
					{
						NPC.alpha = 204; //255
						idleCounter--;
						if (idleCounter <= 0)
						{
							NPC.localAI[3] = 2f;
							idleCounter = (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) ? 540 : 360;
							NPC.netUpdate = true;
						}
					}
				}
				else if (NPC.localAI[3] == 2f) //turn visible
				{
					NPC.alpha -= 1;
					if (NPC.alpha <= 0)
					{
						if (flameTimer < 270)
						{
							flameTimer = 270;
						}
						NPC.alpha = 0;
						NPC.localAI[3] = 0f;
						NPC.netUpdate = true;
					}
				}
			}
			else
			{
				NPC.alpha -= 6;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				if (NPC.localAI[3] > 0f)
				{
					NPC.localAI[3] = 0f;
					NPC.netUpdate = true;
				}
			}
			if (speedBoost4)
			{
				if (!halfLife)
				{
					string key = "A GOD DOES NOT FEAR DEATH!";
					Color messageColor = Color.Cyan;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					halfLife = true;
				}
			}
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
							segment = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsBodyS").Type, NPC.whoAmI);
						}
						else
						{
							segment = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsTailS").Type, NPC.whoAmI);
						}
						Main.npc[segment].realLife = NPC.whoAmI;
						Main.npc[segment].ai[2] = (float)NPC.whoAmI;
						Main.npc[segment].ai[1] = (float)Previous;
						Main.npc[Previous].ai[0] = (float)segment;
						NetMessage.SendData(23, -1, -1, null, segment, 0f, 0f, 0f, 0);
						Previous = segment;
					}
					tail = true;
				}
				int projectileDamage = expertMode ? 69 : 80;
				if (NPC.alpha <= 0 && (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive))
				{
					if (flameTimer <= 0)
					{
						flameTimer = 900;
					}
					else
					{
						flameTimer--;
						float num861 = 4f;
						int num863 = 1;
						if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
						{
							num863 = -1;
						}
						Vector2 vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num863 * 180) - vector86.X;
						float num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
						float num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
						num866 = num861 / num866;
						num864 *= num866;
						num865 *= num866;
						if (breathFireMore)
						{
							if (flameTimer <= 810 && flameTimer > 630)
							{
								if (NPC.soundDelay == 0)
								{
									NPC.soundDelay = 21;
									SoundEngine.PlaySound(SoundID.Item109, NPC.position);
								}
								if (NPC.soundDelay % 3 == 0)
								{
									float num867 = 1f;
									int num869 = Mod.Find<ModProjectile>("DoGFire").Type;
									vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector86.X;
									num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
									num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
									num866 = num867 / num866;
									num864 *= num866;
									num865 *= num866;
									num865 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num864 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num865 += NPC.velocity.Y * 0.75f;
									num864 += NPC.velocity.X * 0.75f;
									vector86.X -= num864 * 1f;
									vector86.Y -= num865 * 1f;
									int damage = expertMode ? 56 : 64;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector86.X, vector86.Y, num864, num865, num869, damage, 0f, Main.myPlayer, 0f, 1f);
								}
							}
							else if (flameTimer <= 630)
							{
								if (NPC.soundDelay == 0)
								{
									NPC.soundDelay = 21;
									SoundEngine.PlaySound(SoundID.Item109, NPC.position);
								}
								if (NPC.soundDelay % 3 == 0)
								{
									float num867 = 1f;
									int num869 = Mod.Find<ModProjectile>("DoGFire").Type;
									vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector86.X;
									num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
									num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
									num866 = num867 / num866;
									num864 *= num866;
									num865 *= num866;
									num865 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num864 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num865 += NPC.velocity.Y * 0.75f;
									num864 += NPC.velocity.X * 0.75f;
									vector86.X -= num864 * 1f;
									vector86.Y -= num865 * 1f;
									int damage = expertMode ? 56 : 64;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector86.X, vector86.Y, num864, num865, num869, damage, 0f, Main.myPlayer, 0f, 2f);
								}
							}
						}
						else
						{
							if (flameTimer <= 270 && flameTimer > 90)
							{
								if (NPC.soundDelay == 0)
								{
									NPC.soundDelay = 21;
									SoundEngine.PlaySound(SoundID.Item109, NPC.position);
								}
								if (NPC.soundDelay % 3 == 0)
								{
									float num867 = 1f;
									int num869 = Mod.Find<ModProjectile>("DoGFire").Type;
									vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector86.X;
									num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
									num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
									num866 = num867 / num866;
									num864 *= num866;
									num865 *= num866;
									num865 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num864 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num865 += NPC.velocity.Y * 0.75f;
									num864 += NPC.velocity.X * 0.75f;
									vector86.X -= num864 * 1f;
									vector86.Y -= num865 * 1f;
									int damage = expertMode ? 56 : 64;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector86.X, vector86.Y, num864, num865, num869, damage, 0f, Main.myPlayer, 0f, 1f);
								}
							}
							else if (flameTimer <= 90)
							{
								if (NPC.soundDelay == 0)
								{
									NPC.soundDelay = 21;
									SoundEngine.PlaySound(SoundID.Item109, NPC.position);
								}
								if (NPC.soundDelay % 3 == 0)
								{
									float num867 = 1f;
									int num869 = Mod.Find<ModProjectile>("DoGFire").Type;
									vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
									num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector86.X;
									num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
									num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
									num866 = num867 / num866;
									num864 *= num866;
									num865 *= num866;
									num865 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num864 += (float)Main.rand.Next(-10, 11) * 0.01f;
									num865 += NPC.velocity.Y * 0.75f;
									num864 += NPC.velocity.X * 0.75f;
									vector86.X -= num864 * 1f;
									vector86.Y -= num865 * 1f;
									int damage = expertMode ? 56 : 64;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector86.X, vector86.Y, num864, num865, num869, damage, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
					}
				}
				if (!speedBoost4 && (NPC.localAI[3] == 1f || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)))
				{
					laserShoot += 3;
					if (laserShoot >= 2400)
					{
						laserShoot = 0;
					}
					float speed = (CalamityWorldPreTrailer.bossRushActive ? 4.5f : 4f);
					if (laserShoot % (CalamityWorldPreTrailer.bossRushActive ? 210 : (CalamityWorldPreTrailer.death ? 240 : 300)) == 0) //300 600 900 1200 1500 1800 2100 2400
					{
						SoundEngine.PlaySound(SoundID.Item12, new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y));
						float targetPosY = Main.player[NPC.target].position.Y + (Main.rand.Next(2) == 0 ? 50f : 0f);
						int extraLasers = Main.rand.Next(2);
						for (int x = 0; x < totalShots; x++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X + 1000f, targetPosY + this.shotSpacing[0], -speed, 0f, Mod.Find<ModProjectile>("DoGDeath").Type, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X - 1000f, targetPosY + this.shotSpacing[0], speed, 0f, Mod.Find<ModProjectile>("DoGDeath").Type, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
							this.shotSpacing[0] -= spacingVar; //105
						}
						if (extraLasers == 1 && (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive))
						{
							for (int x = 0; x < 10; x++)
							{
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X + 1000f, targetPosY + this.shotSpacing[3], -speed, 0f, Mod.Find<ModProjectile>("DoGDeath").Type, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X - 1000f, targetPosY + this.shotSpacing[3], speed, 0f, Mod.Find<ModProjectile>("DoGDeath").Type, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
								this.shotSpacing[3] -= (Main.rand.Next(2) == 0 ? 180f : 200f);
							}
							this.shotSpacing[3] = 1050f;
						}
						this.shotSpacing[0] = 1050f;
					}
					if (laserShoot % (CalamityWorldPreTrailer.bossRushActive ? 300 : (CalamityWorldPreTrailer.death ? 360 : 450)) == 0) //480 960 1440 1920 2400
					{
						for (int x = 0; x < totalShots; x++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X + this.shotSpacing[1], Main.player[NPC.target].position.Y + 1000f, 0f, -speed, Mod.Find<ModProjectile>("DoGDeath").Type, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
							this.shotSpacing[1] -= spacingVar; //105
						}
						this.shotSpacing[1] = 1050f;
					}
					if (laserShoot % (CalamityWorldPreTrailer.bossRushActive ? 420 : (CalamityWorldPreTrailer.death ? 480 : 600)) == 0) //600 1200 1800 2400
					{
						for (int x = 0; x < totalShots; x++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), Main.player[NPC.target].position.X + this.shotSpacing[2], Main.player[NPC.target].position.Y - 1000f, 0f, speed, Mod.Find<ModProjectile>("DoGDeath").Type, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
							this.shotSpacing[2] -= spacingVar; //105
						}
						this.shotSpacing[2] = 1050f;
					}
				}
			}
			if (!NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsTailS").Type))
			{
				NPC.active = false;
			}
			float fallSpeed = 16f;
			if (Main.player[NPC.target].dead)
			{
				flies = false;
				NPC.velocity.Y = NPC.velocity.Y + 2f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 2f;
					fallSpeed = 32f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int a = 0; a < 200; a++)
					{
						if (Main.npc[a].type == Mod.Find<ModNPC>("DevourerofGodsHeadS").Type || Main.npc[a].type == Mod.Find<ModNPC>("DevourerofGodsBodyS").Type ||
							Main.npc[a].type == Mod.Find<ModNPC>("DevourerofGodsTailS").Type)
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
			if (NPC.velocity.X < 0f)
			{
				NPC.spriteDirection = -1;
			}
			else if (NPC.velocity.X > 0f)
			{
				NPC.spriteDirection = 1;
			}
			if (NPC.ai[2] == 0f)
			{
				if (Main.netMode != 2)
				{
					if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, vector) < 5600f)
					{
						Main.player[Main.myPlayer].AddBuff(Mod.Find<ModBuff>("Warped").Type, 2);
					}
				}
				phaseSwitch += 1f;
				NPC.localAI[1] = 0f;
				float speed = 15f;
				float turnSpeed = 0.4f;
				float homingSpeed = 24f;
				float homingTurnSpeed = 0.5f;
				if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 5600f) //RAGE
				{
					phaseSwitch += 9f;
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
					float num47 = (float)(num42 - 800);
					if (Main.player[NPC.target].position.Y > num47)
					{
						num192 = num47;
						if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 500f)
						{
							if (NPC.velocity.X > 0f)
							{
								num191 = Main.player[NPC.target].Center.X + 600f;
							}
							else
							{
								num191 = Main.player[NPC.target].Center.X - 600f;
							}
						}
					}
				}
				else
				{
					num188 = homingSpeed;
					num189 = homingTurnSpeed;
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
				if (num42 > 0)
				{
					for (int num51 = 0; num51 < 200; num51++)
					{
						if (Main.npc[num51].active && Main.npc[num51].type == NPC.type && num51 != NPC.whoAmI)
						{
							Vector2 vector3 = Main.npc[num51].Center - NPC.Center;
							if (vector3.Length() < 400f)
							{
								vector3.Normalize();
								vector3 *= 1000f;
								num191 -= vector3.X;
								num192 -= vector3.Y;
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
							if (vector4.Length() < 60f)
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
					if (NPC.velocity.Y > fallSpeed * 0.5f)
					{
						NPC.velocity.Y = fallSpeed * 0.5f;
					}
					if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)fallSpeed * 0.3)
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
					else if (NPC.velocity.Y == fallSpeed)
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
				if (phaseSwitch > ((CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) ? 600f : 900f))
				{
					NPC.ai[2] = 1f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[2] == 1f)
			{
				if (Main.netMode != 2)
				{
					if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, vector) < 5600f)
					{
						Main.player[Main.myPlayer].AddBuff(Mod.Find<ModBuff>("ExtremeGrav").Type, 2);
					}
				}
				phaseSwitch += 1f;
				float turnSpeed = 0.3f;
				bool increaseSpeed = Vector2.Distance(Main.player[NPC.target].Center, vector) > 3200f;
				if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 5600f) //RAGE
				{
					turnSpeed = 1.5f;
				}
				else if (increaseSpeed)
				{
					turnSpeed = 1f;
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
					int num954 = 1200;
					if ((double)NPC.life <= (double)NPC.lifeMax * 0.8 && (double)NPC.life > (double)NPC.lifeMax * 0.2)
					{
						num954 = 1400;
					}
					bool flag95 = true;
					if (NPC.position.Y > Main.player[NPC.target].position.Y)
					{
						for (int num955 = 0; num955 < 255; num955++)
						{
							if (Main.player[num955].active)
							{
								Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - 1000, (int)Main.player[num955].position.Y - 1000, 2000, num954);
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
				else
				{
					NPC.localAI[1] = 0f;
				}
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
					if (!flies)
					{
						NPC.TargetClosest(true);
						NPC.velocity.Y = NPC.velocity.Y + turnSpeed; //turnspeed * 0.5f
						if (NPC.velocity.Y > fallSpeed)
						{
							NPC.velocity.Y = fallSpeed;
						}
						if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)fallSpeed * 2.2) //max speed
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
						else if (NPC.velocity.Y == fallSpeed)
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
						double maximumSpeed1 = increaseSpeed ? 1.2 : 0.4;
						double maximumSpeed2 = increaseSpeed ? 3.0 : 1.0;
						num193 = (float)Math.Sqrt((double)(num191 * num191 + num192 * num192));
						float num25 = Math.Abs(num191);
						float num26 = Math.Abs(num192);
						float num27 = fallSpeed / num193;
						num191 *= num27;
						num192 *= num27;
						if (((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f)) && ((NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f)))
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + turnSpeed * 1.5f;
							}
							else if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - turnSpeed * 1.5f;
							}
							if (NPC.velocity.Y < num192)
							{
								NPC.velocity.Y = NPC.velocity.Y + turnSpeed * 1.5f;
							}
							else if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - turnSpeed * 1.5f;
							}
						}
						if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + turnSpeed;
							}
							else if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - turnSpeed;
							}
							if (NPC.velocity.Y < num192)
							{
								NPC.velocity.Y = NPC.velocity.Y + turnSpeed;
							}
							else if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - turnSpeed;
							}
							if ((double)Math.Abs(num192) < (double)fallSpeed * maximumSpeed1 /*0.2*/ && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
							{
								if (NPC.velocity.Y > 0f)
								{
									NPC.velocity.Y = NPC.velocity.Y + turnSpeed * 2f;
								}
								else
								{
									NPC.velocity.Y = NPC.velocity.Y - turnSpeed * 2f;
								}
							}
							if ((double)Math.Abs(num191) < (double)fallSpeed * maximumSpeed1 /*0.2*/ && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
							{
								if (NPC.velocity.X > 0f)
								{
									NPC.velocity.X = NPC.velocity.X + turnSpeed * 2f;
								}
								else
								{
									NPC.velocity.X = NPC.velocity.X - turnSpeed * 2f;
								}
							}
						}
						else if (num25 > num26)
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + turnSpeed * 1.1f;
							}
							else if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - turnSpeed * 1.1f;
							}
							if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)fallSpeed * maximumSpeed2) //0.5
							{
								if (NPC.velocity.Y > 0f)
								{
									NPC.velocity.Y = NPC.velocity.Y + turnSpeed;
								}
								else
								{
									NPC.velocity.Y = NPC.velocity.Y - turnSpeed;
								}
							}
						}
						else
						{
							if (NPC.velocity.Y < num192)
							{
								NPC.velocity.Y = NPC.velocity.Y + turnSpeed * 1.1f;
							}
							else if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - turnSpeed * 1.1f;
							}
							if ((double)(Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < (double)fallSpeed * maximumSpeed2) //0.5
							{
								if (NPC.velocity.X > 0f)
								{
									NPC.velocity.X = NPC.velocity.X + turnSpeed;
								}
								else
								{
									NPC.velocity.X = NPC.velocity.X - turnSpeed;
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
				if (phaseSwitch > ((CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) ? 600f : 900f))
				{
					NPC.ai[2] = 0f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
				if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
				{
					NPC.netUpdate = true;
					return;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = Mod.Find<ModItem>("CosmiliteBrick").Type;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.type == Mod.Find<ModProjectile>("SulphuricAcidMist2").Type || projectile.type == Mod.Find<ModProjectile>("EidolicWail").Type)
			{
				projectile.damage /= 4;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return NPC.alpha == 0;
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 2)
			{
				string key = "You think...you can butcher...ME!?";
				Color messageColor = Color.Cyan;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				modifiers.SetMaxDamage(0);
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 2f;
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
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("DoGS").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("DoGS2").Type, 1f);
				}
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

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 300, true);
			if ((CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) && NPC.alpha <= 0)
			{
				target.KillMe(PlayerDeathReason.ByOther(10), 1000.0, 0, false);
			}
			int num = Main.rand.Next(5);
			string key = "A fatal mistake!";
			if (num == 0)
			{
				key = "A fatal mistake!";
			}
			else if (num == 1)
			{
				key = "Good luck recovering from that!";
			}
			else if (num == 2)
			{
				key = "Delicious...";
			}
			else if (num == 3)
			{
				key = "Did that hurt?";
			}
			else if (num == 4)
			{
				key = "Nothing personal, kid.";
			}
			Color messageColor = Color.Cyan;
			if (Main.netMode == 0)
			{
				Main.NewText(Language.GetTextValue(key), messageColor);
			}
			else if (Main.netMode == 2)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
			}
			target.AddBuff(BuffID.Frostburn, 300, true);
			target.AddBuff(BuffID.Darkness, 300, true);
		}
	}
}