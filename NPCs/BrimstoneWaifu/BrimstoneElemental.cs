using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.BrimstoneWaifu;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.BrimstoneWaifu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.BrimstoneWaifu
{
	[AutoloadBossHead]
	public class BrimstoneElemental : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Elemental");
			Main.npcFrameCount[NPC.type] = 12;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.5f,
				PortraitScale = 0.64f
			};
			value.Position.Y -= 24f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("Once a great goddess, all she has within her heart now is hate. Hate for all that might pity her.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 64f;
			NPC.damage = 60;
			NPC.width = 100;
			NPC.height = 150;
			NPC.defense = 20;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 35708 : 26000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 54050;
			}
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.value = Item.buyPrice(0, 12, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("MarkedforDeath").Type] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath39;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/LeftAlone");
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.damage = 210;
				NPC.defense = 120;
				NPC.lifeMax = 300000;
				NPC.value = Item.buyPrice(0, 35, 0, 0);
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 2300000 : 2000000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			SpawnModBiomes = new int[] { ModContent.GetInstance<Crag>().Type };
		}

		public override void AI()
		{
			CalamityGlobalNPC.brimstoneElemental = NPC.whoAmI;
			Player player = Main.player[NPC.target];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			bool brimTeleport = (double)NPC.life <= (double)NPC.lifeMax * 0.2;
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool calamity = modPlayer.ZoneCalamity;
			NPC.TargetClosest(true);
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vectorCenter = NPC.Center;
			float xDistance = player.Center.X - center.X;
			float yDistance = player.Center.Y - center.Y;
			float totalDistance = (float)Math.Sqrt((double)(xDistance * xDistance + yDistance * yDistance));
			int dustAmt = (NPC.ai[0] == 2f) ? 2 : 1;
			int size = (NPC.ai[0] == 2f) ? 50 : 35;
			float speed = expertMode ? 5f : 4.5f;
			if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
			{
				speed = 5.5f;
			}
			for (int num1011 = 0; num1011 < 2; num1011++)
			{
				if (Main.rand.Next(3) < dustAmt)
				{
					int dust = Dust.NewDust(NPC.Center - new Vector2((float)size), size * 2, size * 2, 235, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.2f;
					Main.dust[dust].fadeIn = 1f;
				}
			}
			if (Vector2.Distance(player.Center, vectorCenter) > 5600f)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
			{
				speed = 11f;
			}
			else if (!calamity)
			{
				speed = 7f;
			}
			else if ((double)NPC.life <= (double)NPC.lifeMax * 0.65)
			{
				speed = expertMode ? 6f : 5f;
			}
			if (NPC.ai[0] <= 2f)
			{
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				totalDistance = speed / totalDistance;
				xDistance *= totalDistance;
				yDistance *= totalDistance;
				NPC.velocity.X = (NPC.velocity.X * 50f + xDistance) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + yDistance) / 51f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.defense = provy ? 120 : 20;
				NPC.chaseable = true;
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if (NPC.justHit)
					{
						NPC.localAI[1] += 1f;
					}
					if (NPC.localAI[1] >= (float)(200 + Main.rand.Next(100)))
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						int timer = 0;
						int playerPosX;
						int playerPosY;
						while (true)
						{
							timer++;
							playerPosX = (int)player.Center.X / 16;
							playerPosY = (int)player.Center.Y / 16;
							playerPosX += Main.rand.Next(-50, 51);
							playerPosY += Main.rand.Next(-50, 51);
							if (!WorldGen.SolidTile(playerPosX, playerPosY) && Collision.CanHit(new Vector2((float)(playerPosX * 16), (float)(playerPosY * 16)), 1, 1, player.position, player.width, player.height))
							{
								break;
							}
							if (timer > 100)
							{
								return;
							}
						}
						NPC.ai[0] = 1f;
						NPC.ai[1] = (float)playerPosX;
						NPC.ai[2] = (float)playerPosY;
						NPC.netUpdate = true;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.dontTakeDamage = true;
				NPC.defense = provy ? 120 : 20;
				NPC.chaseable = false;
				NPC.alpha += (brimTeleport ? 5 : 4);
				if (NPC.alpha >= 255)
				{
					if (Main.netMode != 1 && NPC.CountNPCS(Mod.Find<ModNPC>("Brimling").Type) < 2 && revenge)
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("Brimling").Type, 0, 0f, 0f, 0f, 0f, 255);
					}
					NPC.alpha = 255;
					NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
					NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
					NPC.ai[0] = 2f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.alpha -= (brimTeleport ? 5 : 4);
				if (NPC.alpha <= 0)
				{
					NPC.dontTakeDamage = false;
					NPC.defense = provy ? 120 : 20;
					NPC.chaseable = true;
					NPC.ai[3] += 1f;
					NPC.alpha = 0;
					if (NPC.ai[3] >= 2f)
					{
						NPC.ai[0] = 3f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
					}
					else
					{
						NPC.ai[0] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.defense = provy ? 120 : 20;
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				Vector2 shootFromVectorX = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				NPC.ai[1] += 1f;
				bool shootProjectile = false;
				if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					if (NPC.ai[1] % 10f == 9f)
					{
						shootProjectile = true;
					}
				}
				else if (CalamityWorldPreTrailer.bossRushActive)
				{
					if (NPC.ai[1] % 15f == 14f)
					{
						shootProjectile = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					if (NPC.ai[1] % 20f == 19f)
					{
						shootProjectile = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					if (NPC.ai[1] % 25f == 24f)
					{
						shootProjectile = true;
					}
				}
				else if (NPC.ai[1] % 30f == 29f)
				{
					shootProjectile = true;
				}
				if (shootProjectile && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(shootFromVectorX, 1, 1, player.position, player.width, player.height))
				{
					if (Main.netMode != 1)
					{
						float projectileSpeed = 6f; //changed from 10
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							projectileSpeed += 4f;
						}
						if (revenge)
						{
							projectileSpeed += 1f;
						}
						if ((double)NPC.life <= (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
						{
							projectileSpeed += 1f; //changed from 3 not a prob
						}
						if ((double)NPC.life <= (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
						{
							projectileSpeed += 1f;
						}
						if (!calamity)
						{
							projectileSpeed += 2f;
						}
						float relativeSpeedX = player.position.X + (float)player.width * 0.5f - shootFromVectorX.X + (float)Main.rand.Next(-80, 81);
						float relativeSpeedY = player.position.Y + (float)player.height * 0.5f - shootFromVectorX.Y + (float)Main.rand.Next(-40, 41);
						float totalRelativeSpeed = (float)Math.Sqrt((double)(relativeSpeedX * relativeSpeedX + relativeSpeedY * relativeSpeedY));
						totalRelativeSpeed = projectileSpeed / totalRelativeSpeed;
						relativeSpeedX *= totalRelativeSpeed;
						relativeSpeedY *= totalRelativeSpeed;
						int projectileDamage = expertMode ? 24 : 32; //projectile damage
						int projectileType = Mod.Find<ModProjectile>("BrimstoneHellfireball").Type; //projectile type
						int projectileShot = Projectile.NewProjectile(NPC.GetSource_FromThis(null),shootFromVectorX.X, shootFromVectorX.Y, relativeSpeedX, relativeSpeedY, projectileType, projectileDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[projectileShot].timeLeft = 240;
					}
				}
				if (NPC.position.Y > player.position.Y - 150f) //200
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.98f;
					}
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
					if (NPC.velocity.Y > 2f)
					{
						NPC.velocity.Y = 2f;
					}
				}
				else if (NPC.position.Y < player.position.Y - 400f) //500
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.98f;
					}
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
					if (NPC.velocity.Y < -2f)
					{
						NPC.velocity.Y = -2f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) > player.position.X + (float)(player.width / 2) + 150f) //100
				{
					if (NPC.velocity.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.985f;
					}
					NPC.velocity.X = NPC.velocity.X - 0.1f;
					if (NPC.velocity.X > 8f)
					{
						NPC.velocity.X = 8f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)(player.width / 2) - 150f) //100
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.985f;
					}
					NPC.velocity.X = NPC.velocity.X + 0.1f;
					if (NPC.velocity.X < -8f)
					{
						NPC.velocity.X = -8f;
					}
				}
				if (NPC.ai[1] > 300f)
				{
					NPC.ai[0] = 4f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				NPC.defense = 99999;
				NPC.dontTakeDamage = false;
				NPC.chaseable = false;
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += (float)Main.rand.Next(4);
					if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						NPC.localAI[0] += 3f;
					}
					if (CalamityWorldPreTrailer.death || !calamity)
					{
						NPC.localAI[0] += 2f;
					}
					if (NPC.localAI[0] >= 140f)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						float projectileSpeed = revenge ? 8f : 6f;
						Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num180 = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
						float num181 = Math.Abs(num180) * 0.1f;
						float num182 = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y - num181;
						float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						NPC.netUpdate = true;
						num183 = projectileSpeed / num183;
						num180 *= num183;
						num182 *= num183;
						int num184 = expertMode ? 22 : 30;
						int num185 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
						shootFromVector.X += num180;
						shootFromVector.Y += num182;
						for (int num186 = 0; num186 < 6; num186++)
						{
							num180 = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
							num182 = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y;
							num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = projectileSpeed / num183;
							num180 += (float)Main.rand.Next(-80, 81);
							num182 += (float)Main.rand.Next(-80, 81);
							num180 *= num183;
							num182 *= num183;
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, num180, num182, num185, num184 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[projectile].timeLeft = 300;
							Main.projectile[projectile].tileCollide = false;
						}
						float spread = 45f * 0.0174f;
						double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
						double deltaAngle = spread / 8f;
						double offsetAngle;
						int damage = expertMode ? 22 : 30;
						int i;
						for (i = 0; i < 6; i++)
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
							int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						}
					}
				}
				NPC.TargetClosest(true);
				NPC.ai[1] += 1f;
				NPC.velocity *= 0.95f;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				if (NPC.ai[1] > 300f)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return NPC.alpha == 0;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}

		public override void FindFrame(int frameHeight) //9 total frames
		{
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] <= 2f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y >= frameHeight * 4)
				{
					NPC.frame.Y = 0;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 4)
				{
					NPC.frame.Y = frameHeight * 4;
				}
				if (NPC.frame.Y >= frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 4;
				}
			}
			else
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 8;
				}
				if (NPC.frame.Y >= frameHeight * 12)
				{
					NPC.frame.Y = frameHeight * 8;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<BrimstoneElementalTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<BrimstoneWaifuBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BrimstoneWaifuBag>()));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Bloodstone>(), 1, 20, 31));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<RoseStone>(), 10));
			notExpert.OnSuccess(new CommonDrop(ItemID.SoulofFright, 1, 20, 41));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 4, 9));
			notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]
				{
					ModContent.ItemType<Abaddon>(),
					ModContent.ItemType<Brimlance>(),
					ModContent.ItemType<SeethingDischarge>(),
				}));
			npcLoot.Add(notExpert);
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 200;
					NPC.height = 150;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 40; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							235, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}

					for (int num623 = 0; num623 < 60; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							235, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235,
							0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}

					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("BrimstoneGore1").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("BrimstoneGore2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("BrimstoneGore3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("BrimstoneGore4").Type, 1f);
				}
			}
		}
	}
}