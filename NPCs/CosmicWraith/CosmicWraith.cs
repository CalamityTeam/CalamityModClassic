using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using CalamityModClassicPreTrailer.NPCs;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.CosmicWraith
{
	[AutoloadBossHead]
	public class CosmicWraith : ModNPC
	{
		private const int CosmicProjectiles = 3;
		private const float CosmicAngleSpread = 170;
		private int CosmicCountdown = 0;
		private float phaseSwitch = 0f;
		private float chargeSwitch = 0f;
		private int dustTimer = 3;
		private float spawnX = 750f;
		private float spawnY = 120f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Signus, Envoy of the Devourer");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				PortraitPositionYOverride = 10f,
				Scale = 0.4f,
				PortraitScale = 0.5f,
			};
			value.Position.X += 6f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("A strange figure draped in dark robes and even darker history... Many rumours about him have been spread, though none know the truth.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 32f;
			NPC.damage = 175;
			NPC.width = 130;
			NPC.height = 130;
			NPC.defense = 70;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/ScourgeofTheUniverse");
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 109500 : 70000;
			if (CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0)
			{
				NPC.value = Item.buyPrice(0, 35, 0, 0);
				Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Signus");
				NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 445500 : 280000;
				if (CalamityWorldPreTrailer.death)
				{
					NPC.lifeMax = 722250;
				}
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 2400000 : 2200000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ExoFreeze").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.HitSound = SoundID.NPCHit49;
			NPC.DeathSound = SoundID.NPCDeath51;
		}

		public override void AI()
		{
			bool cosmicDust = (double)NPC.life <= (double)NPC.lifeMax * 0.85;
			bool speedBoost = (double)NPC.life <= (double)NPC.lifeMax * 0.75;
			bool cosmicRain = (double)NPC.life <= (double)NPC.lifeMax * 0.65;
			bool cosmicSpeed = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			bool cosmicTeleport = (double)NPC.life <= (double)NPC.lifeMax * 0.33;
			Player player = Main.player[NPC.target];
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			NPC.TargetClosest(true);
			Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vectorCenter = NPC.Center;
			float num1243 = player.Center.X - vector142.X;
			float num1244 = player.Center.Y - vector142.Y;
			float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1000 = cosmicSpeed ? 12f : 15f;
			float num1001 = 5f;
			float scaleFactor4 = 0.75f;
			int num1002 = 0;
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1005 = cosmicSpeed ? 12f : 15f;
			float num1006 = 0.333333343f;
			float num1007 = 10f;
			float chargeSpeedDivisor = cosmicSpeed ? 11.85f : 14.85f;
			num1006 *= num1005;
			for (int num1011 = 0; num1011 < 2; num1011++)
			{
				if (Main.rand.Next(3) < 1)
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2(70f), 70 * 2, 70 * 2, 173, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (Vector2.Distance(player.Center, vectorCenter) > 6400f)
			{
				CalamityWorldPreTrailer.DoGSecondStageCountdown = 0;
				if (Main.netMode == 2)
				{
					var netMessage = Mod.GetPacket();
					netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.DoGCountdownSync);
					netMessage.Write(CalamityWorldPreTrailer.DoGSecondStageCountdown);
					netMessage.Send();
				}
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			else if (NPC.timeLeft < 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (cosmicRain && CosmicCountdown == 0)
			{
				CosmicCountdown = 300;
			}
			if (CosmicCountdown > 0)
			{
				CosmicCountdown--;
				if (CosmicCountdown == 0)
				{
					if (Main.netMode != 1)
					{
						int speed2 = revenge ? 13 : 12;
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							speed2 += 3;
						}
						float spawnX = Main.rand.Next(1000) - 500 + player.Center.X;
						float spawnY = -1000 + player.Center.Y;
						Vector2 baseSpawn = new Vector2(spawnX, spawnY);
						Vector2 baseVelocity = player.Center - baseSpawn;
						baseVelocity.Normalize();
						baseVelocity = baseVelocity * speed2;
						int damage = expertMode ? 49 : 62; //360 300
						for (int i = 0; i < CosmicProjectiles; i++)
						{
							Vector2 spawn2 = baseSpawn;
							spawn2.X = spawn2.X + i * 30 - (CosmicProjectiles * 15);
							Vector2 velocity = baseVelocity;
							velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-CosmicAngleSpread / 2 + (CosmicAngleSpread * i / (float)CosmicProjectiles)));
							velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(null), spawn2.X, spawn2.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("CosmicFlameBurst").Type, damage, 10f, Main.myPlayer, 0f, 0f);
							Main.projectile[projectile].tileCollide = false;
						}
					}
				}
			}
			if (NPC.ai[0] <= 2f)
			{
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				NPC.knockBackResist = 0.05f;
				if (expertMode)
				{
					NPC.knockBackResist *= Main.GameModeInfo.KnockbackToEnemiesMultiplier;
				}
				if (cosmicSpeed)
				{
					NPC.knockBackResist = 0f;
				}
				float speed = expertMode ? 14f : 12f;
				if (speedBoost)
				{
					speed = expertMode ? 16f : 14f;
				}
				if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					speed += 3f;
				}
				Vector2 vector98 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num795 = player.Center.X - vector98.X;
				float num796 = player.Center.Y - vector98.Y;
				float num797 = (float)Math.Sqrt((double)(num795 * num795 + num796 * num796));
				num797 = speed / num797;
				num795 *= num797;
				num796 *= num797;
				NPC.velocity.X = (NPC.velocity.X * 50f + num795) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + num796) / 51f;
			}
			else
			{
				NPC.knockBackResist = 0f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.chaseable = true;
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if (NPC.localAI[1] >= 150f)
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						int num1249 = 0;
						int num1250;
						int num1251;
						while (true)
						{
							num1249++;
							num1250 = (int)player.Center.X / 16;
							num1251 = (int)player.Center.Y / 16;
							num1250 += Main.rand.Next(-40, 41);
							num1251 += Main.rand.Next(-40, 41);
							if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, player.position, player.width, player.height))
							{
								break;
							}
							if (num1249 > 100)
							{
								return;
							}
						}
						NPC.ai[0] = 1f;
						NPC.ai[1] = (float)num1250;
						NPC.ai[2] = (float)num1251;
						NPC.netUpdate = true;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				NPC.alpha += (cosmicTeleport ? 5 : 4);
				if (NPC.alpha >= 255)
				{
					NPC.alpha = 255;
					NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
					NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
					NPC.ai[0] = 2f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.alpha -= (cosmicTeleport ? 5 : 4);
				if (NPC.alpha <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item122, NPC.position);
					if (Main.netMode != 1 && revenge)
					{
						int num660 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(Main.player[NPC.target].position.X + 750f), (int)(Main.player[NPC.target].position.Y), Mod.Find<ModNPC>("SignusBomb").Type, 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
						}
						int num661 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(Main.player[NPC.target].position.X - 750f), (int)(Main.player[NPC.target].position.Y), Mod.Find<ModNPC>("SignusBomb").Type, 0, 0f, 0f, 0f, 0f, 255);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(23, -1, -1, null, num661, 0f, 0f, 0f, 0, 0, 0);
						}
						for (int num621 = 0; num621 < 5; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X + 750f, Main.player[NPC.target].position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num622].velocity *= 3f;
							Main.dust[num622].noGravity = true;
							if (Main.rand.Next(2) == 0)
							{
								Main.dust[num622].scale = 0.5f;
								Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
							}
							int num623 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X - 750f, Main.player[NPC.target].position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num623].velocity *= 3f;
							Main.dust[num623].noGravity = true;
							if (Main.rand.Next(2) == 0)
							{
								Main.dust[num623].scale = 0.5f;
								Main.dust[num623].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
							}
						}
						for (int num623 = 0; num623 < 20; num623++)
						{
							int num624 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X + 750f, Main.player[NPC.target].position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
							Main.dust[num624].noGravity = true;
							Main.dust[num624].velocity *= 5f;
							num624 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X + 750f, Main.player[NPC.target].position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num624].velocity *= 2f;
							int num625 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X - 750f, Main.player[NPC.target].position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
							Main.dust[num625].noGravity = true;
							Main.dust[num625].velocity *= 5f;
							num625 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X - 750f, Main.player[NPC.target].position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num625].velocity *= 2f;
						}
					}
					NPC.dontTakeDamage = false;
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
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if (NPC.life < NPC.lifeMax / 3 || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					if (NPC.ai[1] % 10f == 9f)
					{
						flag104 = true;
					}
				}
				else if (NPC.life < NPC.lifeMax / 2)
				{
					if (NPC.ai[1] % 15f == 14f)
					{
						flag104 = true;
					}
				}
				else if (NPC.ai[1] % 20f == 19f)
				{
					flag104 = true;
				}
				if (flag104 && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(vector121, 1, 1, player.position, player.width, player.height))
				{
					if (Main.netMode != 1)
					{
						float num1070 = 15f; //changed from 10
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							num1070 += 3f;
						}
						if (cosmicRain)
						{
							num1070 += 2f; //changed from 3 not a prob
						}
						if (cosmicSpeed)
						{
							num1070 += 2f;
						}
						if (revenge)
						{
							num1070 += 1f;
						}
						if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
						{
							num1070 += 1f;
						}
						float num1071 = player.position.X + (float)player.width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = player.position.Y + (float)player.height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = expertMode ? 49 : 62; //projectile damage
						int num1075 = Mod.Find<ModProjectile>("CosmicFlameBurst").Type; //projectile type
						int num1076 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num1076].timeLeft = 240;
					}
				}
				if (NPC.position.Y > player.position.Y - 150f) //200
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.975f;
					}
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
					if (NPC.velocity.Y > 3f)
					{
						NPC.velocity.Y = 3f;
					}
				}
				else if (NPC.position.Y < player.position.Y - 400f) //500
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.975f;
					}
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
					if (NPC.velocity.Y < -3f)
					{
						NPC.velocity.Y = -3f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) > player.position.X + (float)(player.width / 2) + 150f) //100
				{
					if (NPC.velocity.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
					NPC.velocity.X = NPC.velocity.X - 0.1f;
					if (NPC.velocity.X > 12f)
					{
						NPC.velocity.X = 12f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)(player.width / 2) - 150f) //100
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
					NPC.velocity.X = NPC.velocity.X + 0.1f;
					if (NPC.velocity.X < -12f)
					{
						NPC.velocity.X = -12f;
					}
				}
				if (NPC.ai[1] > 300f)
				{
					NPC.ai[0] = 4f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				if (Main.netMode != 1)
				{
					if (NPC.CountNPCS(Mod.Find<ModNPC>("CosmicLantern").Type) < 5)
					{
						for (int x = 0; x < 5; x++)
						{
							int num660 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(Main.player[NPC.target].position.X + spawnX), (int)(Main.player[NPC.target].position.Y + spawnY), Mod.Find<ModNPC>("CosmicLantern").Type, 0, 0f, 0f, 0f, 0f, 255);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
							}
							int num661 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(Main.player[NPC.target].position.X - spawnX), (int)(Main.player[NPC.target].position.Y + spawnY), Mod.Find<ModNPC>("CosmicLantern").Type, 0, 0f, 0f, 0f, 0f, 255);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, num661, 0f, 0f, 0f, 0, 0, 0);
							}
							spawnY -= 60f;
						}
						spawnY = 120f;
						NPC.netUpdate = true;
					}
				}
				NPC.TargetClosest(false);
				NPC.rotation = NPC.velocity.ToRotation();
				if (Math.Sign(NPC.velocity.X) != 0)
				{
					NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
				}
				if (NPC.rotation < -1.57079637f)
				{
					NPC.rotation += 3.14159274f;
				}
				if (NPC.rotation > 1.57079637f)
				{
					NPC.rotation -= 3.14159274f;
				}
				NPC.spriteDirection = Math.Sign(NPC.velocity.X);
				phaseSwitch += 1f;
				if (chargeSwitch == 0f) //line up the charge
				{
					float scaleFactor6 = num998;
					Vector2 center4 = NPC.Center;
					Vector2 center5 = player.Center;
					Vector2 vector126 = center5 - center4;
					Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
					float num1013 = vector126.Length();
					vector126 = Vector2.Normalize(vector126) * scaleFactor6;
					vector127 = Vector2.Normalize(vector127) * scaleFactor6;
					bool flag64 = Collision.CanHit(NPC.Center, 1, 1, player.Center, 1, 1);
					if (NPC.ai[3] >= 120f)
					{
						flag64 = true;
					}
					float num1014 = 8f;
					flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
					if (num1013 > num999 || !flag64)
					{
						NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / chargeSpeedDivisor;
						NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / chargeSpeedDivisor;
						if (!flag64)
						{
							NPC.ai[3] += 1f;
							if (NPC.ai[3] == 120f)
							{
								NPC.netUpdate = true;
							}
						}
						else
						{
							NPC.ai[3] = 0f;
						}
					}
					else
					{
						chargeSwitch = 1f;
						NPC.ai[2] = vector126.X;
						NPC.ai[3] = vector126.Y;
						NPC.netUpdate = true;
					}
				}
				else if (chargeSwitch == 1f) //pause before charge
				{
					NPC.velocity *= scaleFactor4;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] >= num1001)
					{
						chargeSwitch = 2f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
						Vector2 velocity = new Vector2(NPC.ai[2], NPC.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
						velocity.Normalize();
						velocity *= scaleFactor5;
						NPC.velocity = velocity;
					}
				}
				else if (chargeSwitch == 2f) //charging
				{
					if (Main.netMode != 1)
					{
						dustTimer--;
						if (cosmicDust && dustTimer <= 0)
						{
							SoundEngine.PlaySound(SoundID.Item73, NPC.position);
							int damage = expertMode ? 49 : 62;
							Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector173.X, (int)vector173.Y, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("EssenceDust").Type, damage, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[projectile].timeLeft = 60;
							Main.projectile[projectile].velocity.X = 0f;
							Main.projectile[projectile].velocity.Y = 0f;
							dustTimer = 3;
						}
					}
					float num1016 = num1003;
					NPC.ai[1] += 1f;
					bool flag65 = Vector2.Distance(NPC.Center, player.Center) > num1004 && NPC.Center.Y > player.Center.Y;
					if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007)
					{
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.velocity /= 2f;
						NPC.netUpdate = true;
						NPC.ai[1] = 45f;
						chargeSwitch = 3f;
					}
					else
					{
						Vector2 center6 = NPC.Center;
						Vector2 center7 = player.Center;
						Vector2 vec2 = center7 - center6;
						vec2.Normalize();
						if (vec2.HasNaNs())
						{
							vec2 = new Vector2((float)NPC.direction, 0f);
						}
						NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / chargeSpeedDivisor;
					}
				}
				else if (chargeSwitch == 3f) //slow down after charging and reset
				{
					NPC.ai[1] -= 2f;
					if (NPC.ai[1] <= 0f)
					{
						chargeSwitch = 0f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
					}
					NPC.velocity *= 0.97f;
				}
				if (phaseSwitch > 300f)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					chargeSwitch = 0f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return NPC.alpha == 0;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] == 4f)
			{
				if (NPC.frameCounter > 72.0) //12
				{
					NPC.frameCounter = 0.0;
				}
			}
			else
			{
				int frameY = 196;
				if (NPC.frameCounter > 72.0)
				{
					NPC.frameCounter = 0.0;
				}
				NPC.frame.Y = frameY * (int)(NPC.frameCounter / 12.0); //1 to 6
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
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			int frameCount = Main.npcFrameCount[NPC.type];
			float scale = NPC.scale;
			float rotation = NPC.rotation;
			float offsetY = NPC.gfxOffY;
			if (NPC.ai[0] == 4f)
			{
				NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/CosmicWraith/CosmicWraithAlt2").Value;
				int height = 564;
				int width = 176;
				Vector2 vector = new Vector2((float)(width / 2), (float)(height / frameCount / 2));
				Microsoft.Xna.Framework.Rectangle frame = new Rectangle(0, 0, width, height / frameCount);
				frame.Y = 94 * (int)(NPC.frameCounter / 12.0); //1 to 6
				if (frame.Y >= 94 * 6)
				{
					frame.Y = 0;
				}
				Main.spriteBatch.Draw(NPCTexture,
					new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)width * scale / 2f + vector.X * scale,
					NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)height * scale / (float)frameCount + 4f + vector.Y * scale + 0f + offsetY),
					new Microsoft.Xna.Framework.Rectangle?(frame),
					NPC.GetAlpha(drawColor),
					rotation,
					vector,
					scale,
					spriteEffects,
					0f);
				return false;
			}
			else if (NPC.ai[0] == 3f)
			{
				NPCTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/CosmicWraith/CosmicWraithAlt").Value;
			}
			else
			{
				NPCTexture = TextureAssets.Npc[NPC.type].Value;
			}
			Microsoft.Xna.Framework.Rectangle frame2 = NPC.frame;
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / frameCount / 2));
			Main.spriteBatch.Draw(NPCTexture,
				new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[NPC.type].Value.Width * scale / 2f + vector11.X * scale,
				NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)TextureAssets.Npc[NPC.type].Value.Height * scale / (float)frameCount + 4f + vector11.Y * scale + 0f + offsetY),
				new Microsoft.Xna.Framework.Rectangle?(frame2),
				NPC.GetAlpha(drawColor),
				rotation,
				vector11,
				scale,
				spriteEffects,
				0f);
			return false;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("TwistingNether").Type, 1, 2, 4));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("SignusTrophy").Type, 10));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("CosmicKunai").Type, 3));
			npcLoot.Add(ItemDropRule.ByCondition(new NotInDoGSentinelPhase(), Mod.Find<ModItem>("Cosmilamp").Type, 3));
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
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
							173, 0f, 0f, 100, default(Color), 2f);
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
							173, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173,
							0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}

					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Signus").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Signus2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Signus3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Signus4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("Signus5").Type, 1f);
				}
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}