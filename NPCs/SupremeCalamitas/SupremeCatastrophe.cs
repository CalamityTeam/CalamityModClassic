using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.SupremeCalamitas
{
	[AutoloadBossHead]
	public class SupremeCatastrophe : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Catastrophe");
			Main.npcFrameCount[NPC.type] = 3;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 5f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 99999;
			AnimationType = 126;
			NPC.lifeMax = 500000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.boss = true;
			NPC.noTileCollide = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/TerrariaBoss2");
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Catastrophe.")

            });
        }
        public override void AI()
		{
			bool defenseBoost = false;
			int defenseAdd = 0;
			for (int nPC = 0; nPC < 200; nPC++)
			{
				if (Main.npc[nPC].active && Main.npc[nPC].type == (Mod.Find<ModNPC>("SupremeCalamitas").Type))
				{
					defenseBoost = true;
					defenseAdd++;
				}
			}
			NPC.defense += defenseAdd * 25;
			if (!defenseBoost)
			{
				NPC.defense = 0;
				NPC.takenDamageMultiplier = 100f;
			}
			bool expertMode = Main.expertMode;
			bool flag44 = false;
			if (!Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1)) 
			{
				NPC.noTileCollide = true;
				flag44 = true;
			} 
			else
			{
				NPC.noTileCollide = false;
			}
			if (CalamityGlobalNPC.supremeCalamitas < 0)
            {
                NPC.SimpleStrikeNPC(9999, 0, false, noPlayerInteraction: true);
                return;
			}
			NPC.TargetClosest(true);
			float num676 = 60f;
			float num677 = 1f;
			Vector2 vector83 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num678 = Main.player[NPC.target].Center.X - vector83.X - 550f;
			float num679 = Main.player[NPC.target].Center.Y - vector83.Y - 450f;
			float num680 = (float)Math.Sqrt((double)(num678 * num678 + num679 * num679));
			num680 = num676 / num680;
			num678 *= num680;
			num679 *= num680;
			if (NPC.velocity.X < num678) 
			{
				NPC.velocity.X = NPC.velocity.X + num677;
				if (NPC.velocity.X < 0f && num678 > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X + num677;
				}
			} 
			else if (NPC.velocity.X > num678) 
			{
				NPC.velocity.X = NPC.velocity.X - num677;
				if (NPC.velocity.X > 0f && num678 < 0f) 
				{
					NPC.velocity.X = NPC.velocity.X - num677;
				}
			}
			if (NPC.velocity.Y < num679) 
			{
				NPC.velocity.Y = NPC.velocity.Y + num677;
				if (NPC.velocity.Y < 0f && num679 > 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + num677;
				}
			} 
			else if (NPC.velocity.Y > num679)
			{
				NPC.velocity.Y = NPC.velocity.Y - num677;
				if (NPC.velocity.Y > 0f && num679 < 0f) 
				{
					NPC.velocity.Y = NPC.velocity.Y - num677;
				}
			}
			NPC.ai[1] += 1f;
			if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.3) 
			{
				NPC.ai[1] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.2) 
			{
				NPC.ai[1] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.15) 
			{
				NPC.ai[1] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.1) 
			{
				NPC.ai[1] += 1f;
			}
			int num681 = 360;
			if (NPC.ai[1] < 20f || NPC.ai[1] > (float)(num681 - 20)) 
			{
				NPC.localAI[0] = 1f;
			} 
			else
			{
				NPC.localAI[0] = 0f;
			}
			if (flag44) 
			{
				NPC.ai[1] = 20f;
			}
			if (NPC.ai[1] >= (float)num681) 
			{
				NPC.TargetClosest(true);
				NPC.ai[1] = 0f;
				Vector2 vector84 = new Vector2(NPC.Center.X, NPC.Center.Y - 10f);
				float num682 = 10f;
				int num683 = expertMode ? 120 : 140;
				int num684 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
				float num685 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector84.X;
				float num686 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector84.Y;
				float num687 = (float)Math.Sqrt((double)(num685 * num685 + num686 * num686));
				num687 = num682 / num687;
				num685 *= num687;
				num686 *= num687;
				if (Main.netMode != NetmodeID.MultiplayerClient) 
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), vector84.X, vector84.Y, 0f, 14f, num684, num683, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			NPC.ai[2] += 1f;
			if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 3) 
			{
				NPC.ai[2] += 1f;
			}
			if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 3.5) 
			{
				NPC.ai[2] += 1f;
			}
			if (Main.npc[CalamityGlobalNPC.supremeCalamitas].life < Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 4) 
			{
				NPC.ai[2] += 1f;
			}
			if (Main.npc[CalamityGlobalNPC.supremeCalamitas].life < Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 4.5) 
			{
				NPC.ai[2] += 1f;
			}
			if (Main.npc[CalamityGlobalNPC.supremeCalamitas].life < Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 5) 
			{
				NPC.ai[2] += 1f;
			}
			if (Main.npc[CalamityGlobalNPC.supremeCalamitas].life < Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 5.5) 
			{
				NPC.ai[2] += 1f;
			}
			if (Main.npc[CalamityGlobalNPC.supremeCalamitas].life < Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax / 6) 
			{
				NPC.ai[2] += 1f;
			}
			if (!Collision.CanHit(Main.npc[CalamityGlobalNPC.supremeCalamitas].Center, 1, 1, Main.player[NPC.target].Center, 1, 1)) 
			{
				NPC.ai[2] += 4f;
			}
			if (NPC.ai[2] > (float)(100 + Main.rand.Next(4800))) 
			{
				NPC.ai[2] = 0f;
				for (int num688 = 0; num688 < 2; num688++) 
				{
					Vector2 vector85 = new Vector2(NPC.Center.X, NPC.Center.Y - 50f);
					if (num688 == 0) 
					{
						vector85.X -= 14f;
					} 
					else if (num688 == 1)
					{
						vector85.X += 14f;
					}
					float num689 = 12f;
					int num690 = expertMode ? 120 : 140;
					int num691 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
					if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.3) 
					{
						num690++;
						num689 += 0.25f;
					}
					if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.25) 
					{
						num690++;
						num689 += 0.25f;
					}
					if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.2) 
					{
						num690++;
						num689 += 0.25f;
					}
					if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.15) 
					{
						num690++;
						num689 += 0.25f;
					}
					if ((double)Main.npc[CalamityGlobalNPC.supremeCalamitas].life < (double)Main.npc[CalamityGlobalNPC.supremeCalamitas].lifeMax * 0.1) 
					{
						num690++;
						num689 += 0.25f;
					}
					float num692 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector85.X;
					float num693 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector85.Y;
					float num694 = (float)Math.Sqrt((double)(num692 * num692 + num693 * num693));
					num694 = num689 / num694;
					num692 *= num694;
					num693 *= num694;
					vector85.X += num692 * 3f;
					vector85.Y += num693 * 3f;
					if (Main.netMode != NetmodeID.MultiplayerClient) 
					{
						int num695 = Projectile.NewProjectile(NPC.GetSource_FromThis(), vector85.X, vector85.Y, num692, num693, num691, num690, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num695].timeLeft = 300;
					}
				}
				return;
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage = 0;
		}
	}
}