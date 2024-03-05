using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Polterghast;
using CalamityModClassic1Point2.Items.Weapons.Polterghast;
using Terraria.UI;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Polterghast
{
	[AutoloadBossHead]
	public class Polterghast : ModNPC
	{
		internal int dpsCap = CalamityWorld1Point2.downedPolterghast ? 77500 : 7750; //60
		private int damageTotal = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Polterghast");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 25f;
			NPC.damage = 100;
			NPC.width = 90;
			NPC.height = 120;
			NPC.defense = 150;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 500000 : 465000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.value = Item.buyPrice(5, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			Music = MusicLoader.GetMusicSlot("CalamityModClassic1Point2/Sounds/Music/RUIN");
			NPC.HitSound = SoundID.NPCHit7;
			NPC.DeathSound = SoundID.NPCDeath39;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("PolterghastBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("Spooky.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.5f, 1.25f, 1.25f);
			CalamityGlobalNPC1Point2.ghostBoss = NPC.whoAmI;
			bool revenge = CalamityWorld1Point2.revenge;
			bool expertMode = Main.expertMode;
			bool phase2 = NPC.life < NPC.lifeMax / 2;
			bool phase3 = revenge ? (NPC.life < NPC.lifeMax / 4) : (NPC.life < NPC.lifeMax / 5);
			Vector2 vector = NPC.Center;
			damageTotal -= dpsCap;
			if (damageTotal < 0)
			{
				damageTotal = 0;
			}
			bool speedBoost1 = false;
			bool speedBoost2 = false;
			NPC.TargetClosest(true);
			if (Main.player[NPC.target].dead) 
			{
				speedBoost2 = true;
				speedBoost1 = true;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				int distance = 6000;
				if (Math.Abs(vector.X - Main.player[NPC.target].Center.X) + Math.Abs(vector.Y - Main.player[NPC.target].Center.Y) > (float)distance) 
				{
					NPC.active = false;
					NPC.life = 0;
					if (Main.netMode == NetmodeID.Server) 
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			if (NPC.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient) 
			{
				NPC.localAI[0] = 1f;
				int num729 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				num729 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				num729 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PolterghastHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			}
			int[] array2 = new int[3];
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
					if (num732 > 2) 
					{
						break;
					}
				}
				num = num733;
			}
			num730 /= (float)num732;
			num731 /= (float)num732;
			float num734 = 2.5f;
			float num735 = 0.025f;
			if (phase2) 
			{
				num734 = 5f;
				num735 = 0.04f;
			}
			if (phase3) 
			{
				num734 = 14f;
				num735 = 0.07f;
			}
			if (!Main.player[NPC.target].ZoneDungeon) 
			{
				speedBoost1 = true;
				num734 += 8f;
				num735 = 0.15f;
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
			if (speedBoost2) 
			{
				num737 *= -1f;
				num736 *= -1f;
				num734 += 8f;
			}
			float num738 = (float)Math.Sqrt((double)(num736 * num736 + num737 * num737));
			int num739 = 500;
			if (speedBoost1) 
			{
				num739 += 350;
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
			Vector2 vector92 = new Vector2(vector.X, vector.Y);
			float num740 = Main.player[NPC.target].Center.X - vector92.X;
			float num741 = Main.player[NPC.target].Center.Y - vector92.Y;
			NPC.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
			if (!phase2 && !phase3) 
			{
				NPC.defense = 150;
				NPC.damage = (int)(100f * Main.GameModeInfo.EnemyDamageMultiplier);
				if (speedBoost1) 
				{
					NPC.defense *= 2;
					NPC.damage *= 2;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient) 
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
					if ((double)NPC.life < (double)NPC.lifeMax * 0.7) 
					{
						NPC.localAI[1] += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.6) 
					{
						NPC.localAI[1] += 1f;
					}
					if (speedBoost1) 
					{
						NPC.localAI[1] += 3f;
					}
					if (expertMode) 
					{
						NPC.localAI[1] += 1f;
					}
					if (expertMode && NPC.justHit && Main.rand.NextBool(2)) 
					{
						NPC.localAI[3] = 1f;
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
							float num742 = 14f;
							if (expertMode)
							{
								num742 = 16f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = 50;
							int num747 = Mod.Find<ModProjectile>("PhantomShot").Type;
							int maxValue2 = 4;
							int maxValue3 = 8;
							if (expertMode) 
							{
								maxValue2 = 2;
								maxValue3 = 6;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.8 && Main.rand.NextBool(maxValue2)) 
							{
								num746 = 60;
								NPC.localAI[1] = -30f;
								num747 = Mod.Find<ModProjectile>("PhantomBlast").Type;
							} 
							else if ((double)NPC.life < (double)NPC.lifeMax * 0.7 && Main.rand.NextBool(maxValue3))
							{
								num746 = 70;
								NPC.localAI[1] = -120f;
								if (NPC.CountNPCS(Mod.Find<ModNPC>("PhantomTurret").Type) < (revenge ? 3 : 2))
								{
									int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PhantomTurret").Type, 0, 0f, 0f, 0f, 0f, 255);
									Main.npc[spawn].netUpdate = true;
								}
							}
							if (speedBoost1) 
							{
								num746 *= 2;
							}
							if (expertMode) 
							{
								num746 = (int)((double)num746 * 0.9);
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = 300;
							return;
						}
						else
						{
							Vector2 vector93 = new Vector2(vector.X, vector.Y);
							float num742 = 17f;
							if (expertMode)
							{
								num742 = 19f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = 60;
							int num747 = Mod.Find<ModProjectile>("PhantomBlast").Type;
							if (speedBoost1) 
							{
								num746 *= 2;
							}
							if (expertMode) 
							{
								num746 = (int)((double)num746 * 0.9);
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
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
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 90;
					NPC.height = 90;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 10; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.RedTorch, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						Main.dust[num622].noGravity = true;
						if (Main.rand.NextBool(2))
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 60; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
				NPC.GivenName = "Necroghast";
				NPC.defense = 100;
				NPC.damage = (int)(120f * Main.GameModeInfo.EnemyDamageMultiplier);
				if (speedBoost1) 
				{
					NPC.defense *= 3;
					NPC.damage *= 3;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					NPC.localAI[1] += 1f;
					if (speedBoost1) 
					{
						NPC.localAI[1] += 3f;
					}
					if (expertMode) 
					{
						NPC.localAI[1] += 1f;
					}
					if (expertMode && NPC.justHit && Main.rand.NextBool(2)) 
					{
						NPC.localAI[3] = 1f;
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
							float num742 = 17f;
							if (expertMode)
							{
								num742 = 19f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = 60;
							int num747 = Mod.Find<ModProjectile>("PhantomShot2").Type;
							int maxValue2 = 4;
							int maxValue3 = 8;
							if (expertMode) 
							{
								maxValue2 = 2;
								maxValue3 = 6;
							}
							if (Main.rand.NextBool(maxValue2)) 
							{
								num746 = 65;
								NPC.localAI[1] = -30f;
								num747 = Mod.Find<ModProjectile>("PhantomBlast2").Type;
							} 
							else if (Main.rand.NextBool(maxValue3))
							{
								num746 = 75;
								NPC.localAI[1] = -120f;
								if (NPC.CountNPCS(Mod.Find<ModNPC>("PhantomTurret2").Type) < (revenge ? 3 : 2))
								{
									int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PhantomTurret2").Type, 0, 0f, 0f, 0f, 0f, 255);
									Main.npc[spawn].netUpdate = true;
								}
							}
							if (speedBoost1) 
							{
								num746 *= 2;
							}
							if (expertMode) 
							{
								num746 = (int)((double)num746 * 0.9);
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = 400;
							return;
						}
						else
						{
							Vector2 vector93 = new Vector2(vector.X, vector.Y);
							float num742 = 21f;
							if (expertMode)
							{
								num742 = 23f;
							}
							float num743 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector93.X;
							float num744 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector93.Y;
							float num745 = (float)Math.Sqrt((double)(num743 * num743 + num744 * num744));
							num745 = num742 / num745;
							num743 *= num745;
							num744 *= num745;
							int num746 = 65;
							int num747 = Mod.Find<ModProjectile>("PhantomBlast2").Type;
							if (speedBoost1) 
							{
								num746 *= 2;
							}
							if (expertMode) 
							{
								num746 = (int)((double)num746 * 0.9);
							}
							vector93.X += num743 * 3f;
							vector93.Y += num744 * 3f;
							int num748 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector93.X, vector93.Y, num743, num744, num747, num746, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num748].timeLeft = 240;
							return;
						}
					}
				}
			}
			else
			{
				NPC.HitSound = SoundID.NPCHit36;
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] += 1f;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						for (int I = 0; I < 3; I++) 
						{
							int Phantom = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.Center.X + (Math.Sin(I * 120) * 1000)), (int)(NPC.Center.Y + (Math.Cos(I * 120) * 1000)), Mod.Find<ModNPC>("PhantomFuckYou").Type, NPC.whoAmI, 0, 0, 0, -1);
							NPC Eye = Main.npc[Phantom];
							Eye.ai[0] = I * 120;
							Eye.ai[3] = I * 120;
						}
					}
					SoundEngine.PlaySound(SoundID.Item122, NPC.position);
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 90;
					NPC.height = 90;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 10; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.RedTorch, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						Main.dust[num622].noGravity = true;
						if (Main.rand.NextBool(2))
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 60; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
				NPC.GivenName = "Necroplasm";
				NPC.defense = 0;
				NPC.damage = (int)(220f * Main.GameModeInfo.EnemyDamageMultiplier);
				if (speedBoost1) 
				{
					NPC.defense *= 4;
					NPC.damage *= 4;
				}
				NPC.localAI[1] += 1f;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.3) 
				{
					NPC.localAI[1] += 1f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.2) 
				{
					NPC.localAI[1] += 1f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
				{
					NPC.localAI[1] += 1f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.05) 
				{
					NPC.localAI[1] += 1f;
				}
				if (NPC.localAI[1] >= 350f) 
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
					if (NPC.CountNPCS(Mod.Find<ModNPC>("PhantomSpirit").Type) < (revenge ? 5 : 3))
					{
						int num762 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PhantomSpirit").Type, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num762].velocity.X = num758;
						Main.npc[num762].velocity.Y = num760;
						Main.npc[num762].netUpdate = true;
					}
					if (NPC.CountNPCS(Mod.Find<ModNPC>("PhantomTurret2").Type) < (revenge ? 3 : 2))
					{
						int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("PhantomTurret2").Type, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[spawn].netUpdate = true;
					}
					NPC.localAI[1] = 0f;
					return;
				}
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PolterghastBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RuinousSoul>(), 1, 12, 18));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Polterghast.BansheeHook>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EtherealSubjugator>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Polterghast.DaemonsFlame>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Polterghast.FatesReveal>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Polterghast.GhastlyVisage>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Polterghast.GhoulishGouger>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TerrorBlade>(), 4));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}

		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Base);
		}

		public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[NPC.target];
			if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
			{
				modifiers.FinalDamage /= 2;
				modifiers.DisableCrit();
			}
			ModifyHit(ref modifiers.FinalDamage.Base);
		}
		
		public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		private void ModifyHit(ref float damage)
		{
			if (damage > NPC.lifeMax / 8)
			{
				damage = NPC.lifeMax / 8;
			}
		}
		
		private void OnHit(float damage)
		{
			damageTotal += (int)(damage * 60);
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				ModPacket netMessage = GetPacket(PolterghastMessageType.Damage);
				netMessage.Write(damage * 60);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					netMessage.Write(Main.myPlayer);
				}
				netMessage.Send();
			}
		}
		
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (damageTotal >= dpsCap * 60)
			{
				modifiers.FinalDamage *= 0;
			}
			bool flag = Main.netMode == NetmodeID.SinglePlayer;
			if (!NPC.active || NPC.life <= 0)
			{
				return;
			}
			double newDamage = modifiers.FinalDamage.Base;
			int newDefense = NPC.defense;
			if (NPC.ichor)
			{
				newDefense -= 20;
			}
			if (newDefense < 0)
			{
				newDefense = 0;
			}
			newDamage = newDamage - (double)newDefense * 0.25;
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			/*if (crit)
			{
				newDamage *= 2;
			}*/
			if (newDamage >= 1.0)
			{
				float protection = 0.25f +
					(NPC.life < NPC.lifeMax / 2 ? 0.25f : 0f) +
					(NPC.life < NPC.lifeMax / 4 ? 0.2f : 0f) +
					((NPC.life < NPC.lifeMax / 5 && CalamityWorld1Point2.revenge) ? 0.1f : 0f);
				newDamage = (double)((int)((double)(1f - (protection * (NPC.ichor ? 0.75f : 1f))) * newDamage));
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
				if (flag)
				{
					NPC.PlayerInteraction(Main.myPlayer);
				}
				NPC.justHit = true;
				if (!NPC.immortal)
				{
					if (NPC.realLife >= 0)
					{
						Main.npc[NPC.realLife].life -= (int)newDamage;
						NPC.life = Main.npc[NPC.realLife].life;
						NPC.lifeMax = Main.npc[NPC.realLife].lifeMax;
					}
					else
					{
						NPC.life -= (int)newDamage;
					}
				}
				NPC.HitEffect(modifiers.HitDirection, newDamage);
				if (NPC.HitSound != null)
				{
					SoundEngine.PlaySound(NPC.HitSound, NPC.position);
				}
				if (NPC.realLife >= 0)
				{
					Main.npc[NPC.realLife].checkDead();
				}
				else
				{
					NPC.checkDead();
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
		}
		
		public override void FindFrame(int frameHeight)
		{
			bool phase2 = CalamityWorld1Point2.revenge ? (NPC.life > NPC.lifeMax / 4) : (NPC.life > NPC.lifeMax / 5);
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if (NPC.life > NPC.lifeMax / 2)
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
			target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 2; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonSpirit, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.RedTorch, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 60; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.DungeonSpirit, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		private ModPacket GetPacket(PolterghastMessageType type)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)CalamityModClassic1Point2MessageType.Polterghast);
			packet.Write(NPC.whoAmI);
			packet.Write((byte)type);
			return packet;
		}
		
		public void HandlePacket(BinaryReader reader)
		{
			PolterghastMessageType type = (PolterghastMessageType)reader.ReadByte();
			if (type == PolterghastMessageType.Damage)
			{
				int damage = reader.ReadInt32();
				damageTotal += damage;
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket netMessage = GetPacket(PolterghastMessageType.Damage);
					int ignore = reader.ReadInt32();
					netMessage.Write(damage);
					netMessage.Send(-1, ignore);
				}
			}
		}
	}
	
	enum PolterghastMessageType : byte
	{
		Damage
	}
}