using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Polterghast;
using CalamityModClassicPreTrailer.Items.Weapons.Polterghast;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Polterghast
{
	[AutoloadBossHead]
	public class Polterghast : ModNPC
	{
		private int despawnTimer = 600;
		private bool spawnGhost = false;
		private bool boostDR = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Polterghast");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				new FlavorTextBestiaryInfoElement("The culmination of anger and hatred, as the vengeful spirits have coalesced into a singular monster with one objective: to kill the man responsible for their endless suffering.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 50f;
			NPC.damage = 150;
			NPC.width = 90;
			NPC.height = 120;
			NPC.defense = 150;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 495000 : 412500;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 660000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 2800000 : 2500000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.value = Item.buyPrice(0, 60, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/RUIN");
			NPC.HitSound = SoundID.NPCHit7;
			NPC.DeathSound = SoundID.NPCDeath39;
		}

		public override void AI()
		{
			if (Main.raining)
			{
				Main.raining = false;
				if (Main.netMode == 2)
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.5f, 1.25f, 1.25f);
			NPC.TargetClosest(true);
			Vector2 vector = NPC.Center;
			if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 6000f)
			{
				NPC.active = false;
			}
			bool speedBoost1 = false;
			bool despawnBoost = false;
			if (NPC.timeLeft < 1500)
			{
				NPC.timeLeft = 1500;
			}
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool phase2 = (double)NPC.life <= (double)NPC.lifeMax * 0.75; //hooks detach and fire beams
			bool phase3 = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.5 : 0.33); //hooks die and begins charging with ghosts spinning around player
			bool phase4 = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.33 : 0.2); //starts spitting ghost dudes
			bool phase5 = (double)NPC.life <= (double)NPC.lifeMax * (revenge ? 0.1 : 0.05); //starts moving incredibly fast
			CalamityGlobalNPC.ghostBoss = NPC.whoAmI;
			if (NPC.localAI[0] == 0f && Main.netMode != 1)
			{
				NPC.localAI[0] = 1f;
				int num729 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				num729 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				num729 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				num729 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			}
			int[] array2 = new int[4];
			float num730 = 0f;
			float num731 = 0f;
			int num732 = 0;
			int num;
			for (int num733 = 0; num733 < 200; num733 = num + 1)
			{
				if (Main.npc[num733].active && Main.npc[num733].type == Mod.Find<ModNPC>("PolterghastHook").Type)
				{
					num730 += Main.npc[num733].Center.X;
					num731 += Main.npc[num733].Center.Y;
					array2[num732] = num733;
					num732++;
					if (num732 > 3)
						break;
				}
				num = num733;
			}
			num730 /= (float)num732;
			num731 /= (float)num732;
			float num734 = 2.5f;
			float num735 = 0.025f;
			if (!Main.player[NPC.target].ZoneDungeon && !CalamityWorldPreTrailer.bossRushActive)
			{
				despawnTimer--;
				if (despawnTimer <= 0)
					despawnBoost = true;
				speedBoost1 = true;
				num734 += 8f;
				num735 = 0.15f;
			}
			else
			{
				despawnTimer = 600;
			}
			if (phase2)
			{
				num734 = 3.5f;
				num735 = 0.035f;
			}
			if (phase3)
			{
				if (NPC.CountNPCS(Mod.Find<ModNPC>("PolterPhantom").Type) > 0)
				{
					boostDR = true;
					if (NPC.ai[2] >= 300f)
					{
						num734 = phase5 ? 18f : 12f;
						num735 = phase5 ? 0.12f : 0.08f;
					}
					else
					{
						if (phase5)
						{
							num734 = 5f;
							num735 = 0.05f;
						}
						else if (phase4)
						{
							num734 = 4.5f;
							num735 = 0.045f;
						}
						else
						{
							num734 = 4f;
							num735 = 0.04f;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 600f)
					{
						NPC.ai[2] = 0f;
						NPC.netUpdate = true;
					}
				}
				else
				{
					boostDR = false;
					num734 = phase5 ? 22f : 18f;
					num735 = phase5 ? 0.15f : 0.12f;
					NPC.localAI[2] += 1f;
					if (NPC.localAI[2] >= 600f)
					{
						NPC.localAI[2] = 0f;
						if (Main.netMode != 1)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterPhantom").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						SoundEngine.PlaySound(SoundID.Item122, NPC.position);
						for (int num621 = 0; num621 < 10; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num622].velocity *= 3f;
							Main.dust[num622].noGravity = true;
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
							num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num624].velocity *= 2f;
						}
						NPC.netUpdate = true;
					}
				}
			}
			if (expertMode)
			{
				num734 += revenge ? 1.5f : 1f;
				num734 *= revenge ? 1.25f : 1.1f;
				num735 += revenge ? 0.015f : 0.01f;
				num735 *= revenge ? 1.2f : 1.1f;
			}
			Vector2 vector91 = new Vector2(num730, num731);
			float num736 = Main.player[NPC.target].Center.X - vector91.X;
			float num737 = Main.player[NPC.target].Center.Y - vector91.Y;
			if (despawnBoost)
			{
				num737 *= -1f;
				num736 *= -1f;
				num734 += 8f;
			}
			float num738 = (float)Math.Sqrt((double)(num736 * num736 + num737 * num737));
			int num739 = 500;
			if (speedBoost1)
			{
				num739 += 500;
			}
			if (expertMode)
			{
				num739 += 150;
			}
			if (num738 >= (float)num739)
			{
				num738 = (float)num739 / num738;
				num736 *= num738;
				num737 *= num738;
			}
			num730 += num736;
			num731 += num737;
			vector91 = new Vector2(vector.X, vector.Y);
			num736 = num730 - vector91.X;
			num737 = num731 - vector91.Y;
			num738 = (float)Math.Sqrt((double)(num736 * num736 + num737 * num737));
			if (num738 < num734)
			{
				num736 = NPC.velocity.X;
				num737 = NPC.velocity.Y;
			}
			else
			{
				num738 = num734 / num738;
				num736 *= num738;
				num737 *= num738;
			}
			Vector2 vector92 = new Vector2(vector.X, vector.Y);
			float num740 = Main.player[NPC.target].Center.X - vector92.X;
			float num741 = Main.player[NPC.target].Center.Y - vector92.Y;
			NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
			if (NPC.velocity.X < num736)
			{
				NPC.velocity.X = NPC.velocity.X + num735;
				if (NPC.velocity.X < 0f && num736 > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + num735 * 2f;
				}
			}
			else if (NPC.velocity.X > num736)
			{
				NPC.velocity.X = NPC.velocity.X - num735;
				if (NPC.velocity.X > 0f && num736 < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - num735 * 2f;
				}
			}
			if (NPC.velocity.Y < num737)
			{
				NPC.velocity.Y = NPC.velocity.Y + num735;
				if (NPC.velocity.Y < 0f && num737 > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + num735 * 2f;
				}
			}
			else if (NPC.velocity.Y > num737)
			{
				NPC.velocity.Y = NPC.velocity.Y - num735;
				if (NPC.velocity.Y > 0f && num737 < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y - num735 * 2f;
				}
			}
			if (!phase2 && !phase3)
			{
				if (speedBoost1)
				{
					NPC.defense = 300;
					NPC.damage = (int)(200f * Main.GameModeInfo.EnemyDamageMultiplier);
				}
				else
				{
					NPC.damage = expertMode ? 240 : 150;
					NPC.defense = 150;
				}
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.9)
					{
						NPC.localAI[1] += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.8)
					{
						NPC.localAI[1] += 1f;
					}
					if (speedBoost1 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 6f;
					}
					if (expertMode)
					{
						NPC.localAI[1] += 1f;
					}
					if (NPC.localAI[1] > 120f)
					{
						NPC.localAI[1] = 0f;
						bool flag47 = Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height);
						if (NPC.localAI[3] > 0f)
						{
							flag47 = true;
							NPC.localAI[3] = 0f;
						}
						if (flag47)
						{
							Vector2 vector93 = new Vector2(vector.X, vector.Y);
							float num742 = 6f;
							if (expertMode)
							{
								num742 = (CalamityWorldPreTrailer.bossRushActive ? 13f : 7f);
							}
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num742 *= 2f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = expertMode ? 46 : 55;
							int num747 = Mod.Find<ModProjectile>("PhantomShot").Type;
							int maxValue2 = 2;
							if (expertMode)
							{
								maxValue2 = 4;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.8 && Main.rand.Next(maxValue2) == 0)
							{
								num746 = expertMode ? 51 : 60;
								NPC.localAI[1] = -30f;
								num747 = Mod.Find<ModProjectile>("PhantomBlast").Type;
							}
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num746 *= 2;
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = (num747 == Mod.Find<ModProjectile>("PhantomBlast").Type ? 300 : 1200);
							return;
						}
						else
						{
							Vector2 vector93 = new Vector2(vector.X, vector.Y);
							float num742 = 11f;
							if (expertMode)
							{
								num742 = (CalamityWorldPreTrailer.bossRushActive ? 18f : 12f);
							}
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num742 *= 2f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = expertMode ? 51 : 60;
							int num747 = Mod.Find<ModProjectile>("PhantomBlast").Type;
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num746 *= 2;
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = 180;
							return;
						}
					}
				}
			}
			else if (!phase3)
			{
				if (NPC.ai[0] == 0f)
				{
					NPC.ai[0] += 1f;
					SoundEngine.PlaySound(SoundID.Item122, NPC.position);
					if (Main.netMode != NetmodeID.Server)
					{
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt2").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt3").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt4").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt5").Type, 1f);
					}
					for (int num621 = 0; num621 < 10; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						Main.dust[num622].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 30; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
				NPC.GivenName = "Necroghast";
				if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					NPC.defense = 200;
					NPC.damage = (int)(300f * Main.GameModeInfo.EnemyDamageMultiplier);
				}
				else
				{
					NPC.damage = expertMode ? 288 : 180;
					NPC.defense = 100;
				}
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 1f;
					if (speedBoost1 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 8f;
					}
					if (expertMode)
					{
						NPC.localAI[1] += 1f;
					}
					if (NPC.localAI[1] > 80f)
					{
						NPC.localAI[1] = 0f;
						bool flag47 = Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height);
						if (NPC.localAI[3] > 0f)
						{
							flag47 = true;
							NPC.localAI[3] = 0f;
						}
						if (flag47)
						{
							Vector2 vector93 = new Vector2(vector.X, vector.Y);
							float num742 = 6f;
							if (expertMode)
							{
								num742 = (CalamityWorldPreTrailer.bossRushActive ? 13f : 7f);
							}
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num742 *= 2f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = expertMode ? 49 : 60;
							int num747 = Mod.Find<ModProjectile>("PhantomShot2").Type;
							int maxValue2 = 2;
							if (expertMode)
							{
								maxValue2 = 4;
							}
							if (Main.rand.Next(maxValue2) == 0)
							{
								num746 = expertMode ? 54 : 65;
								NPC.localAI[1] = -30f;
								num747 = Mod.Find<ModProjectile>("PhantomBlast2").Type;
							}
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num746 *= 2;
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = (num747 == Mod.Find<ModProjectile>("PhantomBlast2").Type ? 300 : 1200);
							return;
						}
						else
						{
							Vector2 vector93 = new Vector2(vector.X, vector.Y);
							float num742 = 11f;
							if (expertMode)
							{
								num742 = (CalamityWorldPreTrailer.bossRushActive ? 18f : 12f);
							}
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num742 *= 2f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = expertMode ? 54 : 65;
							int num747 = Mod.Find<ModProjectile>("PhantomBlast2").Type;
							if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
							{
								num746 *= 2;
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = 240;
							return;
						}
					}
				}
			}
			else
			{
				NPC.HitSound = SoundID.NPCHit36;
				if (!spawnGhost)
				{
					spawnGhost = true;
					if (Main.netMode != 1)
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterPhantom").Type, 0, 0f, 0f, 0f, 0f, 255);
						for (int I = 0; I < 3; I++)
						{
							int Phantom = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(Main.player[NPC.target].Center.X + (Math.Sin(I * 120) * 500)),
								(int)(Main.player[NPC.target].Center.Y + (Math.Cos(I * 120) * 500)), Mod.Find<ModNPC>("PhantomFuckYou").Type, 0, 0, 0, 0, -1);
							NPC Eye = Main.npc[Phantom];
							Eye.ai[0] = I * 120;
							Eye.ai[3] = I * 120;
						}
					}
					SoundEngine.PlaySound(SoundID.Item122, NPC.position);
					if (Main.netMode != NetmodeID.Server)
					{
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt2").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt3").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt4").Type, 1f);
						Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
							Mod.Find<ModGore>("Polt5").Type, 1f);
					}
					for (int num621 = 0; num621 < 10; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						Main.dust[num622].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 30; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
				NPC.GivenName = "Necroplasm";
				if (speedBoost1 || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					NPC.defense = 200;
					NPC.damage = (int)(400f * Main.GameModeInfo.EnemyDamageMultiplier);
				}
				else
				{
					NPC.damage = expertMode ? 336 : 210;
					NPC.defense = 0;
				}
				if (phase4)
				{
					NPC.localAI[1] += 1f;
					if (NPC.localAI[1] >= 150f)
					{
						float num757 = 8f;
						Vector2 vector94 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num758 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector94.X + (float)Main.rand.Next(-10, 11);
						float num759 = Math.Abs(num758 * 0.2f);
						float num760 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector94.Y + (float)Main.rand.Next(-10, 11);
						if (num760 > 0f)
						{
							num759 = 0f;
						}
						num760 -= num759;
						float num761 = (float)Math.Sqrt((double)(num758 * num758 + num760 * num760));
						num761 = num757 / num761;
						num758 *= num761;
						num760 *= num761;
						if (NPC.CountNPCS(Mod.Find<ModNPC>("PhantomSpiritL").Type) < (revenge ? 3 : 2) && Main.netMode != 1)
						{
							int num762 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PhantomSpiritL").Type, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num762].velocity.X = num758;
							Main.npc[num762].velocity.Y = num760;
							Main.npc[num762].netUpdate = true;
						}
						NPC.localAI[1] = 0f;
						return;
					}
				}
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<PolterghastBag>(),
				1,
				5, 5));
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<PolterghastTrophy>(), 10));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PolterghastBag>()));
			
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<RuinousSoul>(), 1, 5, 9));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BansheeHook>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<DaemonsFlame>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<EtherealSubjugator>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<FatesReveal>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<GhastlyVisage>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<GhoulishGouger>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<TerrorBlade>(), 4));
			npcLoot.Add(notExpert);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			double newDamage = (modifiers.FinalDamage.Base + (int)((double)NPC.defense * 0.25));
			float protection = 0.1f + //.1
					((double)NPC.life <= (double)NPC.lifeMax * 0.75 ? 0.05f : 0f) + //.15
					((double)NPC.life <= (double)NPC.lifeMax * (CalamityWorldPreTrailer.revenge ? 0.5 : 0.33) ? 0.05f : 0f) + //.2
					(boostDR ? 0.6f : 0f); //.8
			if (NPC.ichor)
			{
				protection *= 0.88f;
			}
			else if (NPC.onFire2)
			{
				protection *= 0.9f;
			}
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - protection) * newDamage));
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
		}

		public override void FindFrame(int frameHeight)
		{
			bool phase2 = (double)NPC.life > (double)NPC.lifeMax * (CalamityWorldPreTrailer.revenge ? 0.5 : 0.33);
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if ((double)NPC.life > (double)NPC.lifeMax * 0.75)
			{
				if (NPC.frame.Y > frameHeight * 3)
				{
					NPC.frame.Y = 0;
				}
			}
			else if (phase2)
			{
				if (NPC.frame.Y < frameHeight * 4)
				{
					NPC.frame.Y = frameHeight * 4;
				}
				if (NPC.frame.Y > frameHeight * 7)
				{
					NPC.frame.Y = frameHeight * 4;
				}
			}
			else
			{
				if (NPC.frame.Y < frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 8;
				}
				if (NPC.frame.Y > frameHeight * 11)
				{
					NPC.frame.Y = frameHeight * 8;
				}
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			Dust.NewDust(NPC.position, NPC.width, NPC.height, 180, hit.HitDirection, -1f, 0, default(Color), 1f);
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 90;
				NPC.height = 90;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 60, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 60; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}