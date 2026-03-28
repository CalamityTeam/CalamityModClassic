using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Accessories.RareVariants;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Cryogen;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.Cryogen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Cryogen
{
	[AutoloadBossHead]
	public class Cryogen : ModNPC
	{
		private int time = 0;
		private bool oneTime = true;
		private float iceShard = 0f;
		private bool drawAltTexture = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryogen");
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				new FlavorTextBestiaryInfoElement("An icy being mostly seen only in the harshest blizzards.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 24f;
			NPC.damage = 60;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 10;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 26300 : 17900;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 39900;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 3000000 : 2700000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 12, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("MarkedforDeath").Type] = false;
			NPC.buffImmune[BuffID.OnFire] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = false;
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
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Cryogen");
		}

		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 1f, 1f);
			Player player = Main.player[NPC.target];
			bool isChill = player.ZoneSnow;
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			NPC.TargetClosest(true);
			if (NPC.ai[2] == 0f && NPC.localAI[1] == 0f && Main.netMode != 1 && NPC.ai[0] < 4f) //spawn shield for phase 0 1 2 3, not 4 5
			{
				int num6 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CryogenIce").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.ai[2] = (float)(num6 + 1);
				NPC.localAI[1] = -1f;
				NPC.netUpdate = true;
				Main.npc[num6].ai[0] = (float)NPC.whoAmI;
				Main.npc[num6].netUpdate = true;
			}
			int num7 = (int)NPC.ai[2] - 1;
			if (num7 != -1 && Main.npc[num7].active && Main.npc[num7].type == Mod.Find<ModNPC>("CryogenIce").Type)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
				NPC.ai[2] = 0f;
				if (NPC.localAI[1] == -1f)
				{
					NPC.localAI[1] = expertMode ? 720f : 1080f;
				}
				if (NPC.localAI[1] > 0f)
				{
					NPC.localAI[1] -= 1f;
				}
			}
			if (NPC.ai[0] == 0f || NPC.ai[0] == 2f || NPC.ai[0] == 4f)
			{
				NPC.rotation = NPC.velocity.X * 0.1f;
			}
			else if (NPC.ai[0] == 1f || NPC.ai[0] == 3f)
			{
				NPC.rotation = 0f;
			}
			if (oneTime)
			{
				RainStart();
				oneTime = false;
			}
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, -10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft < 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (Main.netMode != 1 && expertMode)
			{
				time++;
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					time++;
				}
				if (time >= 600)
				{
					Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
					double deltaAngle = spread / 4f;
					double offsetAngle;
					int i;
					int num184 = 22;
					for (i = 0; i < 2; i++)
					{
						offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBomb").Type, num184, 0f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBomb").Type, num184, 0f, Main.myPlayer, 0f, 0f);
					}
					time = 0;
				}
			}
			if (NPC.ai[0] == 0f)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 120f)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
							double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
							double deltaAngle = spread / 8f;
							double offsetAngle;
							int num184 = expertMode ? 20 : 23;
							int i;
							for (i = 0; i < 8; i++)
							{
								offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num1243 = player.Center.X - vector142.X;
				float num1244 = player.Center.Y - vector142.Y;
				float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
				float num1246 = isChill ? 3f : 5f;
				if (CalamityWorldPreTrailer.death)
				{
					num1246 = isChill ? 4f : 6f;
				}
				if (CalamityWorldPreTrailer.bossRushActive)
				{
					num1246 = 10f;
				}
				num1245 = num1246 / num1245;
				num1243 *= num1245;
				num1244 *= num1245;
				NPC.velocity.X = (NPC.velocity.X * 50f + num1243) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + num1244) / 51f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.83)
				{
					NPC.ai[0] = 1f;
					NPC.localAI[0] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 120f)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
							double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
							double deltaAngle = spread / 8f;
							double offsetAngle;
							int num184 = expertMode ? 20 : 23;
							int i;
							for (i = 0; i < 6; i++)
							{
								offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
								int ice = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
								int ice2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[ice].timeLeft = 300;
								Main.projectile[ice2].timeLeft = 300;
							}
						}
					}
				}
				float num1164 = isChill ? 4f : 6f;
				float num1165 = isChill ? 1f : 1.2f;
				if (CalamityWorldPreTrailer.death)
				{
					num1164 = isChill ? 5f : 7f;
				}
				if (CalamityWorldPreTrailer.bossRushActive)
				{
					num1164 = 12f;
				}
				Vector2 vector133 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num1166 = player.Center.X - vector133.X;
				float num1167 = player.Center.Y - vector133.Y - 400f;
				float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
				if (num1168 < 20f)
				{
					num1166 = NPC.velocity.X;
					num1167 = NPC.velocity.Y;
				}
				else
				{
					num1168 = num1164 / num1168;
					num1166 *= num1168;
					num1167 *= num1168;
				}
				if (NPC.velocity.X < num1166)
				{
					NPC.velocity.X = NPC.velocity.X + num1165;
					if (NPC.velocity.X < 0f && num1166 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num1165 * 2f;
					}
				}
				else if (NPC.velocity.X > num1166)
				{
					NPC.velocity.X = NPC.velocity.X - num1165;
					if (NPC.velocity.X > 0f && num1166 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num1165 * 2f;
					}
				}
				if (NPC.velocity.Y < num1167)
				{
					NPC.velocity.Y = NPC.velocity.Y + num1165;
					if (NPC.velocity.Y < 0f && num1167 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1165 * 2f;
					}
				}
				else if (NPC.velocity.Y > num1167)
				{
					NPC.velocity.Y = NPC.velocity.Y - num1165;
					if (NPC.velocity.Y > 0f && num1167 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1165 * 2f;
					}
				}
				if (NPC.position.X + (float)NPC.width > player.position.X && NPC.position.X < player.position.X + (float)player.width && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height) && Main.netMode != 1)
				{
					iceShard += 4f;
					if (iceShard > 8f)
					{
						iceShard = 0f;
						int num1169 = (int)(NPC.position.X + 10f + (float)Main.rand.Next(NPC.width - 20));
						int num1170 = (int)(NPC.position.Y + (float)NPC.height + 4f);
						int damage = expertMode ? 23 : 26;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (float)num1169, (float)num1170, 0f, 5f, Mod.Find<ModProjectile>("IceRain").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						return;
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.66)
				{
					NPC.ai[0] = 2f;
					NPC.localAI[0] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 120f)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							if (Main.rand.Next(2) == 0)
							{
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float spread = 45f * 0.0174f;
								double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
								double deltaAngle = spread / 8f;
								double offsetAngle;
								int num184 = expertMode ? 20 : 23;
								int i;
								for (i = 0; i < 6; i++)
								{
									offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
									int ice = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
									int ice2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[ice].timeLeft = 300;
									Main.projectile[ice2].timeLeft = 300;
								}
							}
							else
							{
								float num179 = revenge ? 9f : 7f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = expertMode ? 23 : 26;
								int num185 = Mod.Find<ModProjectile>("IceRain").Type;
								value9.X += num180;
								value9.Y += num182;
								for (int num186 = 0; num186 < 15; num186++)
								{
									num180 = player.position.X + (float)player.width * 0.5f - value9.X;
									num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
									num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
									num183 = num179 / num183;
									num180 += (float)Main.rand.Next(-100, 101);
									num182 += (float)Main.rand.Next(-100, 101);
									num180 *= num183;
									num182 *= num183;
									Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, -10f, num185, num184, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
					}
				}
				Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num1243 = player.Center.X - vector142.X;
				float num1244 = player.Center.Y - vector142.Y;
				float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
				float num1246 = isChill ? 5f : 7f;
				if (CalamityWorldPreTrailer.death)
				{
					num1246 = isChill ? 6f : 8f;
				}
				if (CalamityWorldPreTrailer.bossRushActive)
				{
					num1246 = 14f;
				}
				num1245 = num1246 / num1245;
				num1243 *= num1245;
				num1244 *= num1245;
				NPC.velocity.X = (NPC.velocity.X * 50f + num1243) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + num1244) / 51f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.49)
				{
					NPC.ai[0] = 3f;
					NPC.localAI[0] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 120f)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							float num179 = revenge ? 9f : 7f;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num184 = expertMode ? 23 : 26;
							int num185 = Mod.Find<ModProjectile>("IceRain").Type;
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 15; num186++)
							{
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = num179 / num183;
								num180 += (float)Main.rand.Next(-100, 101);
								num182 += (float)Main.rand.Next(-100, 101);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, -10f, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				float num1164 = isChill ? 4.5f : 6.5f;
				float num1165 = isChill ? 1.1f : 1.3f;
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					num1164 = isChill ? 5.5f : 7.5f;
				}
				Vector2 vector133 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num1166 = player.Center.X - vector133.X;
				float num1167 = player.Center.Y - vector133.Y - 300f;
				float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
				if (num1168 < 20f)
				{
					num1166 = NPC.velocity.X;
					num1167 = NPC.velocity.Y;
				}
				else
				{
					num1168 = num1164 / num1168;
					num1166 *= num1168;
					num1167 *= num1168;
				}
				if (NPC.velocity.X < num1166)
				{
					NPC.velocity.X = NPC.velocity.X + num1165;
					if (NPC.velocity.X < 0f && num1166 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num1165 * 2f;
					}
				}
				else if (NPC.velocity.X > num1166)
				{
					NPC.velocity.X = NPC.velocity.X - num1165;
					if (NPC.velocity.X > 0f && num1166 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num1165 * 2f;
					}
				}
				if (NPC.velocity.Y < num1167)
				{
					NPC.velocity.Y = NPC.velocity.Y + num1165;
					if (NPC.velocity.Y < 0f && num1167 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num1165 * 2f;
					}
				}
				else if (NPC.velocity.Y > num1167)
				{
					NPC.velocity.Y = NPC.velocity.Y - num1165;
					if (NPC.velocity.Y > 0f && num1167 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num1165 * 2f;
					}
				}
				if (NPC.position.X + (float)NPC.width > player.position.X && NPC.position.X < player.position.X + (float)player.width && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height) && Main.netMode != 1)
				{
					iceShard += 4f;
					if (iceShard > 8f)
					{
						iceShard = 0f;
						int num1169 = (int)(NPC.position.X + 10f + (float)Main.rand.Next(NPC.width - 20));
						int num1170 = (int)(NPC.position.Y + (float)NPC.height + 4f);
						int damage = expertMode ? 23 : 26;
						Projectile.NewProjectile(NPC.GetSource_FromThis(null), (float)num1169, (float)num1170, 0f, 5f, Mod.Find<ModProjectile>("IceRain").Type, damage, 0f, Main.myPlayer, 0f, 0f);
						return;
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.32)
				{
					NPC.ai[0] = 4f;
					NPC.localAI[0] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 60f && NPC.alpha == 0)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
							double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
							double deltaAngle = spread / 8f;
							double offsetAngle;
							int num184 = expertMode ? 20 : 23;
							int i;
							for (i = 0; i < 6; i++)
							{
								offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
								int ice = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 9f), (float)(Math.Cos(offsetAngle) * 9f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
								int ice2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 9f), (float)(-Math.Cos(offsetAngle) * 9f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[ice].timeLeft = 300;
								Main.projectile[ice2].timeLeft = 300;
							}
						}
					}
				}
				NPC.TargetClosest(true);
				Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num1243 = player.Center.X - vector142.X;
				float num1244 = player.Center.Y - vector142.Y;
				float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
				float speed = revenge ? 4.5f : 4f;
				if (CalamityWorldPreTrailer.bossRushActive)
				{
					speed = 9f;
				}
				num1245 = speed / num1245;
				num1243 *= num1245;
				num1244 *= num1245;
				NPC.velocity.X = (NPC.velocity.X * 50f + num1243) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + num1244) / 51f;
				if (NPC.ai[1] == 0f)
				{
					NPC.chaseable = true;
					NPC.dontTakeDamage = false;
					if (Main.netMode != 1)
					{
						NPC.localAI[2] += 1f;
						if (NPC.localAI[2] >= (float)(120 + Main.rand.Next(200)))
						{
							NPC.localAI[2] = 0f;
							NPC.TargetClosest(true);
							int num1249 = 0;
							int num1250;
							int num1251;
							while (true)
							{
								num1249++;
								num1250 = (int)player.Center.X / 16;
								num1251 = (int)player.Center.Y / 16;
								num1250 += Main.rand.Next(-50, 51);
								num1251 += Main.rand.Next(-50, 51);
								if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, player.position, player.width, player.height))
								{
									break;
								}
								if (num1249 > 100)
								{
									goto Block;
								}
							}
							NPC.ai[1] = 1f;
							NPC.localAI[3] = (float)num1250;
							iceShard = (float)num1251;
							NPC.netUpdate = true;
						Block:;
						}
					}
				}
				else if (NPC.ai[1] == 1f)
				{
					NPC.dontTakeDamage = true;
					NPC.chaseable = false;
					NPC.alpha += 4;
					if (NPC.alpha >= 255)
					{
						NPC.alpha = 255;
						NPC.position.X = NPC.localAI[3] * 16f - (float)(NPC.width / 2);
						NPC.position.Y = iceShard * 16f - (float)(NPC.height / 2);
						NPC.ai[1] = 2f;
						NPC.netUpdate = true;
					}
				}
				else if (NPC.ai[1] == 2f)
				{
					NPC.alpha -= 4;
					if (NPC.alpha <= 0)
					{
						NPC.dontTakeDamage = false;
						NPC.chaseable = true;
						NPC.alpha = 0;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
					}
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.15)
				{
					SoundEngine.PlaySound(SoundID.NPCDeath15, NPC.position);
					drawAltTexture = true;
					for (int num621 = 0; num621 < 40; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 70; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					if(Main.netMode != NetmodeID.Server)
					{
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore1").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore2").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore3").Type, 1f);
					}
					NPC.ai[0] = 5f;
					NPC.ai[1] = 0f;
					NPC.localAI[0] = 0f;
					NPC.localAI[2] = 0f;
					NPC.localAI[3] = 0f;
					iceShard = 0f;
					NPC.netUpdate = true;
					string key = "Cryogen is derping out!";
					Color messageColor = Color.Cyan;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
					return;
				}
			}
			else
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				float num1372 = isChill ? 16f : 22f;
				if (CalamityWorldPreTrailer.bossRushActive)
				{
					num1372 = 24f;
				}
				Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
				float num1373 = player.position.X + (float)player.width * 0.5f - vector167.X;
				float num1374 = player.Center.Y - vector167.Y;
				float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
				float num1376 = num1372 / num1375;
				num1373 *= num1376;
				num1374 *= num1376;
				iceShard -= 1f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.05 || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					if (num1375 < 170f || iceShard > 0f)
					{
						if (num1375 < 170f)
						{
							iceShard = 17f;
						}
						if (NPC.velocity.X < 0f)
						{
							NPC.direction = -1;
						}
						else
						{
							NPC.direction = 1;
						}
						NPC.rotation += (float)NPC.direction * 0.5f;
						return;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					if (num1375 < 190f || iceShard > 0f)
					{
						if (num1375 < 190f)
						{
							iceShard = 19f;
						}
						if (NPC.velocity.X < 0f)
						{
							NPC.direction = -1;
						}
						else
						{
							NPC.direction = 1;
						}
						NPC.rotation += (float)NPC.direction * 0.35f;
						return;
					}
				}
				else
				{
					if (num1375 < 200f || iceShard > 0f)
					{
						if (num1375 < 200f)
						{
							iceShard = 20f;
						}
						if (NPC.velocity.X < 0f)
						{
							NPC.direction = -1;
						}
						else
						{
							NPC.direction = 1;
						}
						NPC.rotation += (float)NPC.direction * 0.3f;
						return;
					}
				}
				NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
				NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
				if (num1375 < 350f)
				{
					NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
					NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
				}
				if (num1375 < 300f)
				{
					NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
					NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
				}
				NPC.rotation = NPC.velocity.X * 0.15f;
				return;
			}
			if (NPC.ai[3] == 0f && NPC.life > 0)
			{
				NPC.ai[3] = (float)NPC.lifeMax;
			}
			if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.05);
					if ((float)(NPC.life + num660) < NPC.ai[3])
					{
						NPC.ai[3] = (float)NPC.life;
						for (int num662 = 0; num662 < 2; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int randomSpawn = Main.rand.Next(3);
							if (randomSpawn == 0)
							{
								randomSpawn = Mod.Find<ModNPC>("Cryocore").Type;
							}
							else if (randomSpawn == 1)
							{
								randomSpawn = Mod.Find<ModNPC>("IceMass").Type;
							}
							else
							{
								randomSpawn = Mod.Find<ModNPC>("Cryocore2").Type;
							}
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(null), x, y, randomSpawn, 0, 0f, 0f, 0f, 0f, 255);
							if (Main.netMode == 2 && num664 < 200)
							{
								NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						return;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) //for alt textures
		{
			if (drawAltTexture)
			{
				Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
				Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Cryogen/Cryogen2").Value;
				CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture, 0, NPC, drawColor);
				return false;
			}
			return true;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return NPC.alpha == 0;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int num621 = 0; num621 < 40; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}

					for (int num623 = 0; num623 < 70; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							67, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67,
							0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}

					float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CryoGore8").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CryoGore9").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread,
						Mod.Find<ModGore>("CryoGore10").Type, 1f);
				}
			}
		}

		private void RainStart()
		{
			if (!Main.raining)
			{
				int num = 86400;
				int num2 = num / 24;
				Main.rainTime = Main.rand.Next(num2 * 8, num);
				if (Main.rand.Next(3) == 0)
				{
					Main.rainTime += Main.rand.Next(0, num2);
				}
				if (Main.rand.Next(4) == 0)
				{
					Main.rainTime += Main.rand.Next(0, num2 * 2);
				}
				if (Main.rand.Next(5) == 0)
				{
					Main.rainTime += Main.rand.Next(0, num2 * 2);
				}
				if (Main.rand.Next(6) == 0)
				{
					Main.rainTime += Main.rand.Next(0, num2 * 3);
				}
				if (Main.rand.Next(7) == 0)
				{
					Main.rainTime += Main.rand.Next(0, num2 * 4);
				}
				if (Main.rand.Next(8) == 0)
				{
					Main.rainTime += Main.rand.Next(0, num2 * 5);
				}
				float num3 = 1f;
				if (Main.rand.Next(2) == 0)
				{
					num3 += 0.05f;
				}
				if (Main.rand.Next(3) == 0)
				{
					num3 += 0.1f;
				}
				if (Main.rand.Next(4) == 0)
				{
					num3 += 0.15f;
				}
				if (Main.rand.Next(5) == 0)
				{
					num3 += 0.2f;
				}
				Main.rainTime = (int)((float)Main.rainTime * num3);
				Main.raining = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}

		public override void OnKill()
		{
			int permadongo = NPC.FindFirstNPC(Mod.Find<ModNPC>("DILF").Type);
			if (permadongo == -1 && Main.netMode != 1)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DILF").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<CryogenTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<CryogenBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CryogenBag>()));

			notExpert.OnSuccess(new CommonDrop(ItemID.FrozenKey, 5));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CryoStone>(), 10));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CryogenMask>(), 7));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Regenator>(), 40));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BittercoldStaff>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<EffluviumBow>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<GlacialCrusher>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Icebreaker>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<IceStar>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Avalanche>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<SnowstormStaff>(), 4));
			notExpert.OnSuccess(new CommonDrop(ItemID.SoulofMight, 1, 20, 41));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<CryoBar>(), 1, 15, 26));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<EssenceofEleum>(), 1, 4, 9));
			notExpert.OnSuccess(new CommonDrop(ItemID.FrostCore, 1));
			npcLoot.Add(notExpert);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Frostburn, 120, true);
			target.AddBuff(BuffID.Chilled, 90, true);
		}
	}
}