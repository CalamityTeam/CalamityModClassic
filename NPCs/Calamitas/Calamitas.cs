using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;

namespace CalamityModClassic1Point1.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class Calamitas : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        public override void SetDefaults()
		{
			//NPC.name = "Calamitas");
			//Tooltip.SetDefault("Calamitas");
			NPC.damage = 50;
			NPC.npcSlots = 5f;
			NPC.width = 100; //324
			NPC.height = 110; //216
			NPC.defense = 25;
			AnimationType = 125;
			NPC.lifeMax = 14000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			Main.npcFrameCount[NPC.type] = 3;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.timeLeft = NPC.activeTime * 30;
			Music = MusicID.Boss2;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.SpawnTileY < Main.rockLayer && Main.hardMode && NPC.downedGolemBoss && (Main.bloodMoon || Main.eclipse) ? 0.000001f : 0f;
		}
		
		public override void AI()
		{
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			bool dead2 = Main.player[NPC.target].dead;
			float num801 = NPC.position.X + (float)(NPC.width / 2) - Main.player[NPC.target].position.X - (float)(Main.player[NPC.target].width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - Main.player[NPC.target].position.Y - (float)(Main.player[NPC.target].height / 2);
			float num803 = (float)Math.Atan2((double)num802, (double)num801) + 1.57f;
			if (num803 < 0f)
			{
				num803 += 6.283f;
			}
			else if ((double)num803 > 6.283)
			{
				num803 -= 6.283f;
			}
			float num804 = 0.1f;
			if (NPC.rotation < num803)
			{
				if ((double)(num803 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num804;
				}
				else
				{
					NPC.rotation += num804;
				}
			}
			else if (NPC.rotation > num803)
			{
				if ((double)(NPC.rotation - num803) > 3.1415)
				{
					NPC.rotation += num804;
				}
				else
				{
					NPC.rotation -= num804;
				}
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num805 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), 235, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_2F45E_cp_0 = Main.dust[num805];
				expr_2F45E_cp_0.velocity.X = expr_2F45E_cp_0.velocity.X * 0.5f;
				Dust expr_2F47E_cp_0 = Main.dust[num805];
				expr_2F47E_cp_0.velocity.Y = expr_2F47E_cp_0.velocity.Y * 0.1f;
			}
			if (Main.netMode != 1 && !dead2 && NPC.timeLeft < 10)
			{
				for (int num806 = 0; num806 < 200; num806++)
				{
					if (num806 != NPC.whoAmI && Main.npc[num806].active && Main.npc[num806].timeLeft - 1 > NPC.timeLeft)
					{
						NPC.timeLeft = Main.npc[num806].timeLeft - 1;
					}
				}
			}
			if (dead2)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 2f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 2f;
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
			else
			{
				if (NPC.ai[1] == 0f)
				{
					float num823 = 8.5f;
					float num824 = 0.175f;
					if (Main.expertMode)
					{
						num823 = 10f;
						num824 = 0.2f;
					}
					if (Main.dayTime)
					{
						num823 = 12f;
						num824 = 0.25f;
					}
					Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					float num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 300f - vector82.Y;
					float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
					num827 = num823 / num827;
					num825 *= num827;
					num826 *= num827;
					if (NPC.velocity.X < num825)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
						if (NPC.velocity.X < 0f && num825 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num824;
						}
					}
					else if (NPC.velocity.X > num825)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
						if (NPC.velocity.X > 0f && num825 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num824;
						}
					}
					if (NPC.velocity.Y < num826)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
						if (NPC.velocity.Y < 0f && num826 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num824;
						}
					}
					else if (NPC.velocity.Y > num826)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
						if (NPC.velocity.Y > 0f && num826 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num824;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 300f)
					{
						NPC.ai[1] = 1f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
					}
					vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num825 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector82.X;
					num826 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector82.Y;
					NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 1f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 2f;
						}
						if (CalamityGlobalNPC.bossBuff && CalamityGlobalNPC.superBossBuff)
						{
							NPC.localAI[1] += 4f;
						}
						if (NPC.localAI[1] > 180f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num828 = 8.5f;
							int num829 = 33;
							int num830 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
							if (Main.expertMode)
							{
								num828 = 12f;
								num829 = 22;
							}
							if (Main.dayTime)
							{
								num828 = 15f;
								num829 = 25;
							}
							num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
							num827 = num828 / num827;
							num825 *= num827;
							num826 *= num827;
							vector82.X += num825 * 15f;
							vector82.Y += num826 * 15f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector82.X, vector82.Y, num825, num826, num830, num829, 0f, Main.myPlayer, 0f, 0f);
							return;
						}
					}
				}
				else
				{
					int num831 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
					{
						num831 = -1;
					}
					float num832 = 8.5f;
					float num833 = 0.225f;
					if (Main.expertMode)
					{
						num832 = 10f;
						num833 = 0.3f;
					}
					if (Main.dayTime)
					{
						num832 = 11.5f;
						num833 = 0.375f;
					}
					Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num831 * 340) - vector83.X;
					float num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
					num836 = num832 / num836;
					num834 *= num836;
					num835 *= num836;
					if (NPC.velocity.X < num834)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
						if (NPC.velocity.X < 0f && num834 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num833;
						}
					}
					else if (NPC.velocity.X > num834)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
						if (NPC.velocity.X > 0f && num834 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num833;
						}
					}
					if (NPC.velocity.Y < num835)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
						if (NPC.velocity.Y < 0f && num835 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num833;
						}
					}
					else if (NPC.velocity.Y > num835)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
						if (NPC.velocity.Y > 0f && num835 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num833;
						}
					}
					vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					num834 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector83.X;
					num835 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector83.Y;
					NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
					if (Main.netMode != 1)
					{
						NPC.localAI[1] += 1f;
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							NPC.localAI[1] += 0.75f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
						{
							NPC.localAI[1] += 1.5f;
						}
						if (Main.expertMode)
						{
							NPC.localAI[1] += 1.5f;
						}
						if (CalamityGlobalNPC.bossBuff && CalamityGlobalNPC.superBossBuff)
						{
							NPC.localAI[1] += 3f;
						}
						if (NPC.localAI[1] > 60f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							float num837 = 9f;
							int num838 = 28;
							int num839 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
							if (Main.expertMode)
							{
								num838 = 16;
							}
							if (Main.dayTime)
							{
								num838 = 32;
							}
							num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
							num836 = num837 / num836;
							num834 *= num836;
							num835 *= num836;
							vector83.X += num834 * 15f;
							vector83.Y += num835 * 15f;
							Projectile.NewProjectile(NPC.GetSource_FromThis(), vector83.X, vector83.Y, num834, num835, num839, num838, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 180f)
					{
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						return;
					}
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("CalamitasRun3").Type);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("CalamitasRun").Type);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("CalamitasRun2").Type);
			}
		}
		
		public override bool CheckDead()
		{
			/*Projectile.NewProjectile(NPC.GetSource_FromThis(), npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("RedSpawn"), 0, 0f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("GraySpawn"), 0, 0f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("TrueBattleSpawn"), 0, 0f, Main.myPlayer, 0f, 0f);*/
			Main.NewText("You underestimate my power...", Color.Orange.R, Color.Orange.G, Color.Orange.B);
			Main.NewText("The brothers have awoken!", Color.Orange.R, Color.Orange.G, Color.Orange.B);
			return true;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 200, true);
			}
			else
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 150, true);
			}
		}
	}
}