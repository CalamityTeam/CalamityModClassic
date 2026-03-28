using System;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Dusts;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.Astrageldon;
using CalamityModClassicPreTrailer.Items.Placeables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Astrageldon
{
	[AutoloadBossHead]
	public class Astrageldon : ModNPC
	{
		private float bossLife;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astrum Aureus");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.27f,
				PortraitScale = 0.45f,
			};
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("One of Draedon's machines, even it succumbed to the infection's spread.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 15f;
			NPC.damage = 90;
			NPC.width = 400;
			NPC.height = 280;
			NPC.defense = 120;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 102000 : 80000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 158000;
			}
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 15, 0, 0);
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
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.boss = true;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Astrageldon");
			if (NPC.downedMoonlord && CalamityWorldPreTrailer.revenge)
			{
				NPC.lifeMax = 400000;
				NPC.value = Item.buyPrice(0, 35, 0, 0);
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 1400000 : 1200000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

		public override void AI()
		{
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			int shootBuff = (int)(2f * (1f - (float)NPC.life / (float)NPC.lifeMax));
			float shootTimer = 1f + ((float)shootBuff);
			bool dayTime = Main.dayTime;
			Player player = Main.player[NPC.target];
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			if (!player.active || player.dead || dayTime)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.noTileCollide = true;
					NPC.velocity = new Vector2(0f, 10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else
			{
				if (NPC.timeLeft < 1800)
				{
					NPC.timeLeft = 1800;
				}
			}
			if (NPC.ai[0] != 1f) //emit light when not recharging
			{
				Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 2.55f, 1f, 0f);
			}
			if (NPC.ai[0] == 2f || NPC.ai[0] >= 5f || (NPC.ai[0] == 4f && NPC.velocity.Y > 0f) || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) //fire fast, latent homing, projectiles while walking.  lasers in a circle pattern while teleporting
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += ((NPC.ai[0] == 2f || (NPC.ai[0] == 4f && NPC.velocity.Y > 0f && expertMode)) ? 4f : shootTimer);
					if (NPC.localAI[0] >= 180f) //6 seconds 3 seconds 2 seconds
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						int laserDamage = expertMode ? 35 : 45; //180 120
						SoundEngine.PlaySound(SoundID.Item33, NPC.position);
						if (NPC.downedMoonlord && revenge && !CalamityWorldPreTrailer.bossRushActive)
						{
							laserDamage *= 3;
						}
						if ((NPC.ai[0] >= 5f && NPC.ai[0] != 7) || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) //teleporting
						{
							Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
							double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
							double deltaAngle = spread / 8f;
							double offsetAngle;
							int i;
							for (i = 0; i < 4; i++)
							{
								offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, (float)(Math.Sin(offsetAngle) * 7f),
									(float)(Math.Cos(offsetAngle) * 7f), Mod.Find<ModProjectile>("AstralFlame").Type, laserDamage, 0f, Main.myPlayer, 0f, 0f);
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, (float)(-Math.Sin(offsetAngle) * 7f),
									(float)(-Math.Cos(offsetAngle) * 7f), Mod.Find<ModProjectile>("AstralFlame").Type, laserDamage, 0f, Main.myPlayer, 0f, 0f);
							}
						}
						else if ((NPC.ai[0] == 4f && NPC.velocity.Y > 0f && expertMode) || NPC.ai[0] == 2f) //falling and walking
						{
							float num179 = 18.5f;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num185 = Mod.Find<ModProjectile>("AstralLaser").Type;
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 5; num186++)
							{
								num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
								num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = num179 / num183;
								num180 += (float)Main.rand.Next(-60, 61);
								num182 += (float)Main.rand.Next(-60, 61);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, num182, num185, laserDamage, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
			}
			if (NPC.ai[0] == 0f) //start up
			{
				NPC.ai[1] += 1f;
				if (NPC.justHit || NPC.ai[1] >= 60f)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 1f) //use idle frames and regain fuel, become vulnerable
			{
				NPC.defense = 0;
				NPC.velocity.X *= 0.98f;
				NPC.velocity.Y *= 0.98f;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= ((NPC.life < NPC.lifeMax / 4 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 90f : 150f))
				{
					NPC.defense = 120;
					NPC.noGravity = true;
					NPC.noTileCollide = true;
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 2f) //walk around and fire astral flames and lasers
			{
				float num823 = 4.5f;
				bool flag51 = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					num823 = 5.5f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
				{
					num823 = 7f;
				}
				if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 200f)
				{
					flag51 = true;
				}
				if (flag51)
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
					if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
					{
						NPC.velocity.X = 0f;
					}
				}
				else
				{
					float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
					NPC.direction = (playerLocation < 0 ? 1 : -1);
					if (NPC.direction > 0)
					{
						NPC.velocity.X = (NPC.velocity.X * 20f + num823) / 21f;
					}
					if (NPC.direction < 0)
					{
						NPC.velocity.X = (NPC.velocity.X * 20f - num823) / 21f;
					}
				}
				int num854 = 80;
				int num855 = 20;
				Vector2 position2 = new Vector2(NPC.Center.X - (float)(num854 / 2), NPC.position.Y + (float)NPC.height - (float)num855);
				bool flag52 = false;
				if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height - 16f)
				{
					flag52 = true;
				}
				if (flag52)
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.5f;
				}
				else if (Collision.SolidCollision(position2, num854, num855))
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y > -0.2)
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.025f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.2f;
					}
					if (NPC.velocity.Y < -4f)
					{
						NPC.velocity.Y = -4f;
					}
				}
				else
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y < 0.1)
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.025f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.5f;
					}
				}
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 360f)
				{
					NPC.noGravity = false;
					NPC.noTileCollide = false;
					NPC.ai[0] = 3f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
				if (NPC.velocity.Y > 10f)
				{
					NPC.velocity.Y = 10f;
				}
			}
			else if (NPC.ai[0] == 3f) //leap upwards
			{
				NPC.noTileCollide = false;
				if (NPC.velocity.Y == 0f)
				{
					NPC.velocity.X = NPC.velocity.X * 0.8f;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 0f)
					{
						NPC.ai[1] += 1f;
					}
					if (NPC.ai[1] >= 60f) //120
					{
						NPC.ai[1] = -20f;
					}
					else if (NPC.ai[1] == -1f)
					{
						NPC.TargetClosest(true);
						NPC.velocity.X = (float)(4 * NPC.direction); //4
						NPC.velocity.Y = -14.5f; //12.1
						NPC.ai[0] = 4f;
						NPC.ai[1] = 0f;
					}
				}
			}
			else if (NPC.ai[0] == 4f) //stomp
			{
				if (NPC.velocity.Y == 0f)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/LegStomp"), NPC.position);
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 2f)
					{
						NPC.ai[0] = ((NPC.life < NPC.lifeMax / 2 || revenge) ? 5f : 1f);
						NPC.ai[2] = 0f;
					}
					else
					{
						NPC.ai[0] = 3f;
					}
					for (int num622 = (int)NPC.position.X - 20; num622 < (int)NPC.position.X + NPC.width + 40; num622 += 20)
					{
						for (int num623 = 0; num623 < 4; num623++)
						{
							int num624 = Dust.NewDust(new Vector2(NPC.position.X - 20f, NPC.position.Y + (float)NPC.height), NPC.width + 20, 4, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[num624].velocity *= 0.2f;
						}
					}
				}
				else
				{
					NPC.TargetClosest(true);
					if (NPC.position.X < player.position.X && NPC.position.X + (float)NPC.width > player.position.X + (float)player.width)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
						NPC.velocity.Y = NPC.velocity.Y + 0.6f; //0.2
					}
					else
					{
						if (NPC.direction < 0)
						{
							NPC.velocity.X = NPC.velocity.X - 0.2f;
						}
						else if (NPC.direction > 0)
						{
							NPC.velocity.X = NPC.velocity.X + 0.2f;
						}
						float num626 = 8f; //4
						if (revenge)
						{
							num626 += 1f;
						}
						if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
						{
							num626 += 2f;
						}
						if (NPC.life < NPC.lifeMax / 2 || CalamityWorldPreTrailer.bossRushActive)
						{
							num626 += 1f;
						}
						if (NPC.life < NPC.lifeMax / 10 || CalamityWorldPreTrailer.bossRushActive)
						{
							num626 += 1f;
						}
						if (NPC.velocity.X < -num626)
						{
							NPC.velocity.X = -num626;
						}
						if (NPC.velocity.X > num626)
						{
							NPC.velocity.X = num626;
						}
					}
				}
			}
			else if (NPC.ai[0] == 5f) //start teleport and summon minions, jump a bit
			{
				NPC.velocity.X *= 0.95f;
				NPC.velocity.Y *= 0.95f;
				NPC.chaseable = true;
				NPC.dontTakeDamage = false;
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if (!Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						NPC.localAI[1] += 5f;
					}
					if (NPC.localAI[1] >= 240f)
					{
						bool spawnFlag = revenge;
						if (NPC.CountNPCS(Mod.Find<ModNPC>("AstrageldonSlime").Type) > 1)
						{
							spawnFlag = false;
						}
						if (spawnFlag && Main.netMode != 1)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y - 25, Mod.Find<ModNPC>("AstrageldonSlime").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						int num1249 = 0;
						int num1250;
						int num1251;
						while (true)
						{
							num1249++;
							num1250 = (int)Main.player[NPC.target].Center.X / 16;
							num1251 = (int)Main.player[NPC.target].Center.Y / 16;
							num1250 += Main.rand.Next(-30, 31);
							num1251 += Main.rand.Next(-30, 31);
							if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
							{
								break;
							}
							if (num1249 > 100)
							{
								goto Block;
							}
						}
						NPC.ai[0] = 6f;
						NPC.ai[3] = (float)num1250;
						NPC.localAI[2] = (float)num1251;
						NPC.netUpdate = true;
					Block:;
					}
				}
			}
			else if (NPC.ai[0] == 6f) //mid-teleport
			{
				NPC.chaseable = false;
				NPC.dontTakeDamage = true;
				NPC.alpha += 10;
				if (NPC.alpha >= 255)
				{
					NPC.alpha = 255;
					NPC.position.X = NPC.ai[3] * 16f - (float)(NPC.width / 2);
					NPC.position.Y = NPC.localAI[2] * 16f - (float)(NPC.height / 2);
					NPC.ai[0] = 7f;
					NPC.netUpdate = true;
				}
				if (NPC.soundDelay == 0)
				{
					NPC.soundDelay = 15;
					SoundEngine.PlaySound(SoundID.Item109, NPC.position);
				}
				int num;
				for (int num245 = 0; num245 < 10; num245 = num + 1)
				{
					int num244 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), NPC.velocity.X, NPC.velocity.Y, 255, default(Color), 2f);
					Main.dust[num244].noGravity = true;
					Main.dust[num244].velocity *= 0.5f;
					num = num245;
				}
			}
			else if (NPC.ai[0] == 7f) //end teleport
			{
				NPC.alpha -= 10;
				if (NPC.alpha <= 0)
				{
					bool spawnFlag = revenge;
					if (NPC.CountNPCS(Mod.Find<ModNPC>("AstrageldonSlime").Type) > 1)
					{
						spawnFlag = false;
					}
					if (spawnFlag && Main.netMode != 1)
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y - 25, Mod.Find<ModNPC>("AstrageldonSlime").Type, 0, 0f, 0f, 0f, 0f, 255);
					}
					NPC.chaseable = true;
					NPC.dontTakeDamage = false;
					NPC.alpha = 0;
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
				}
				if (NPC.soundDelay == 0)
				{
					NPC.soundDelay = 15;
					SoundEngine.PlaySound(SoundID.Item109, NPC.position);
				}
				int num;
				for (int num245 = 0; num245 < 10; num245 = num + 1)
				{
					int num244 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), NPC.velocity.X, NPC.velocity.Y, 255, default(Color), 2f);
					Main.dust[num244].noGravity = true;
					Main.dust[num244].velocity *= 0.5f;
					num = num245;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return (NPC.alpha == 0 && NPC.ai[0] > 1f);
		}

		public override void FindFrame(int frameHeight)
		{
			if (NPC.ai[0] == 3f || NPC.ai[0] == 4f)
			{
				if (NPC.velocity.Y == 0f && NPC.ai[1] >= 0f && NPC.ai[0] == 3f) //idle before jump
				{
					if (NPC.localAI[3] > 0f)
					{
						NPC.localAI[3] = 0f;
						NPC.netUpdate = true;
					}
					NPC.frameCounter += 1.0;
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
				else if (NPC.velocity.Y <= 0f || NPC.ai[1] < 0f) //prepare to jump and then jump
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 12.0)
					{
						NPC.frame.Y = NPC.frame.Y + frameHeight;
						NPC.frameCounter = 0.0;
					}
					if (NPC.frame.Y >= frameHeight * 5)
					{
						NPC.frame.Y = frameHeight * 5;
					}
				}
				else //stomping
				{
					if (NPC.localAI[3] == 0f)
					{
						NPC.localAI[3] = 1f;
						NPC.frameCounter = 0.0;
						NPC.frame.Y = 0;
						NPC.netUpdate = true;
					}
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 12.0)
					{
						NPC.frame.Y = NPC.frame.Y + frameHeight;
						NPC.frameCounter = 0.0;
					}
					if (NPC.frame.Y >= frameHeight * 5)
					{
						NPC.frame.Y = frameHeight * 5;
					}
				}
			}
			else if (NPC.ai[0] >= 5f)
			{
				if (NPC.localAI[3] > 0f)
				{
					NPC.localAI[3] = 0f;
					NPC.netUpdate = true;
				}
				if (NPC.velocity.Y == 0f) //idle before teleport
				{
					NPC.frameCounter += 1.0;
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
				else //in-air
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 12.0)
					{
						NPC.frame.Y = NPC.frame.Y + frameHeight;
						NPC.frameCounter = 0.0;
					}
					if (NPC.frame.Y >= frameHeight * 5)
					{
						NPC.frame.Y = frameHeight * 5;
					}
				}
			}
			else
			{
				if (NPC.localAI[3] > 0f)
				{
					NPC.localAI[3] = 0f;
					NPC.netUpdate = true;
				}
				NPC.frameCounter += 1.0;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D NPCTexture = TextureAssets.Npc[NPC.type].Value;
			Texture2D GlowMaskTexture = TextureAssets.Npc[NPC.type].Value;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}

			if (NPC.ai[0] == 0f)
			{
				NPCTexture = TextureAssets.Npc[NPC.type].Value;
				GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonGlow").Value;
			}
			else if (NPC.ai[0] == 1f) //nothing special done here
			{
				NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonRecharge").Value;
			}
			else if (NPC.ai[0] == 2f) //nothing special done here
			{
				NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonWalk").Value;
				GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonWalkGlow").Value;
			}
			else if (NPC.ai[0] == 3f || NPC.ai[0] == 4f) //needs to have an in-air frame
			{
				if (NPC.velocity.Y == 0f && NPC.ai[1] >= 0f && NPC.ai[0] == 3f) //idle before jump
				{
					NPCTexture = TextureAssets.Npc[NPC.type].Value; //idle frames
					GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonGlow").Value;
				}
				else if (NPC.velocity.Y <= 0f || NPC.ai[1] < 0f) //jump frames if flying upward or if about to jump
				{
					NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonJump").Value;
					GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonJumpGlow").Value;
				}
				else //stomping
				{
					NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonStomp").Value;
					GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonStompGlow").Value;
				}
			}
			else if (NPC.ai[0] >= 5f) //needs to have an in-air frame
			{
				if (NPC.velocity.Y == 0f) //idle before teleport
				{
					NPCTexture = TextureAssets.Npc[NPC.type].Value; //idle frames
					GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonGlow").Value;
				}
				else //in-air frames
				{
					NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonJump").Value;
					GlowMaskTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Astrageldon/AstrageldonJumpGlow").Value;
				}
			}

			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			int frameCount = Main.npcFrameCount[NPC.type];
			Microsoft.Xna.Framework.Rectangle frame = NPC.frame;
			float scale = NPC.scale;
			float rotation = NPC.rotation;
			float offsetY = NPC.gfxOffY;

			Main.spriteBatch.Draw(NPCTexture,
				new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[NPC.type].Value.Width * scale / 2f + vector11.X * scale,
				NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)TextureAssets.Npc[NPC.type].Value.Height * scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * scale + 0f + offsetY),
				new Microsoft.Xna.Framework.Rectangle?(frame),
				NPC.GetAlpha(drawColor),
				rotation,
				vector11,
				scale,
				spriteEffects,
				0f);

			if (NPC.ai[0] != 1) //draw only if not recharging
			{
				Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y - 30f); //30
				Vector2 vector = center - Main.screenPosition;
				vector -= new Vector2((float)GlowMaskTexture.Width, (float)(GlowMaskTexture.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
				vector += vector11 * 1f + new Vector2(0f, 0f + 4f + offsetY);
				Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Gold);

				Main.spriteBatch.Draw(GlowMaskTexture, vector,
					new Microsoft.Xna.Framework.Rectangle?(frame), color, rotation, vector11, 1f, spriteEffects, 0f);
			}
			return false;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}
 
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<AstrageldonBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AstrageldonBag>()));
			npcLoot.Add(new CommonDrop(ModContent.ItemType<AstrageldonTrophy>(), 10));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<AstralJelly>(), 1, 9, 13));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Stardust>(), 1, 20, 31));
			notExpert.OnSuccess(new CommonDrop(ItemID.FallenStar, 1, 25, 41));
			npcLoot.Add(notExpert);
			npcLoot.Add(new CommonDrop(ModContent.ItemType<AureusMask>(), 7));
			/*
			int amount = Main.rand.Next(25, 41) / 2;
			if (Main.expertMode)
			{
				amount = (int)((float)amount * 1.5f);
			}
			*/
			int min = (int)(25 * 0.5f);
			int max = (int)(41 * 0.5f) * Main.rand.Next(1, 4);
			npcLoot.Add(ItemDropRule.ByCondition(new MoonCondition(), 3459, 1, min, max));
			npcLoot.Add(ItemDropRule.ByCondition(new MoonCondition(), 3458, 1, min, max));
			npcLoot.Add(ItemDropRule.ByCondition(new MoonCondition(), 3457, 1, min, max));
			npcLoot.Add(ItemDropRule.ByCondition(new MoonCondition(), 3456, 1, min, max));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.soundDelay == 0)
			{
				NPC.soundDelay = 15;
				SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstrumAureusHit"), NPC.Center);
			}

			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 150;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 50; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 100; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
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
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 180, true);
		}
	}
}