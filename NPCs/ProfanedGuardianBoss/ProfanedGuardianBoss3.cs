using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.ProfanedGuardianBoss
{
	public class ProfanedGuardianBoss3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Profaned Guardian");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 3f;
			NPC.aiStyle = -1;
			NPC.damage = 110;
			NPC.width = 100; //324
			NPC.height = 80; //216
			NPC.defense = 88;
			NPC.lifeMax = 34000;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = -1;
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.value = Item.buyPrice(1, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("Devotion to devotion.")

            });
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool isHoly = Main.player[NPC.target].ZoneHallow;
			bool isHell = Main.player[NPC.target].ZoneUnderworldHeight;
			NPC.defense = (isHoly || isHell) ? 88 : 99999;
			Vector2 vectorCenter = NPC.Center;
			Player player = Main.player[NPC.target];
			NPC.TargetClosest(false);
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
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (Math.Sign(NPC.velocity.X) != 0) 
			{
				NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
			}
			NPC.spriteDirection = Math.Sign(NPC.velocity.X);
			int num1009 = (NPC.ai[0] == 0f) ? 1 : 2;
			int num1010 = (NPC.ai[0] == 0f) ? 60 : 80;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int dustType = Main.rand.Next(2);
					if (dustType == 0)
					{
						dustType = 244;
					}
					else
					{
						dustType = 107;
					}
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, dustType, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (CalamityGlobalNPC.doughnutBoss < 0) 
			{
				NPC.active = false;
				NPC.netUpdate = true;
				return;
			}
			if (NPC.ai[0] == 0f) 
			{
				Vector2 vector96 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num784 = Main.npc[CalamityGlobalNPC.doughnutBoss].Center.X - vector96.X;
				float num785 = Main.npc[CalamityGlobalNPC.doughnutBoss].Center.Y - vector96.Y;
				float num786 = (float)Math.Sqrt((double)(num784 * num784 + num785 * num785));
				if (num786 > 90f) 
				{
					num786 = 24f / num786; //8f
					num784 *= num786;
					num785 *= num786;
					NPC.velocity.X = (NPC.velocity.X * 15f + num784) / 16f;
					NPC.velocity.Y = (NPC.velocity.Y * 15f + num785) / 16f;
					return;
				}
				if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < 24f) //8f
				{
					NPC.velocity.Y = NPC.velocity.Y * 1.15f; //1.05f
					NPC.velocity.X = NPC.velocity.X * 1.15f; //1.05f
				}
				if (Main.netMode != NetmodeID.MultiplayerClient && ((expertMode && Main.rand.NextBool(50)) || Main.rand.NextBool(100))) 
				{
					NPC.TargetClosest(true);
					vector96 = new Vector2(NPC.Center.X, NPC.Center.Y);
					num784 = player.Center.X - vector96.X;
					num785 = player.Center.Y - vector96.Y;
					num786 = (float)Math.Sqrt((double)(num784 * num784 + num785 * num785));
					num786 = 24f / num786; //8f
					NPC.velocity.X = num784 * num786;
					NPC.velocity.Y = num785 * num786;
					NPC.ai[0] = 1f;
					NPC.netUpdate = true;
					return;
				}
			} 
			else 
			{
				Vector2 value4 = player.Center - NPC.Center;
				value4.Normalize();
				value4 *= 27f; //9f
				NPC.velocity = (NPC.velocity * 99f + value4) / 100f;
				Vector2 vector97 = new Vector2(NPC.Center.X, NPC.Center.Y);
				float num787 = Main.npc[CalamityGlobalNPC.doughnutBoss].Center.X - vector97.X;
				float num788 = Main.npc[CalamityGlobalNPC.doughnutBoss].Center.Y - vector97.Y;
				float num789 = (float)Math.Sqrt((double)(num787 * num787 + num788 * num788));
				if (num789 > 700f) 
				{
					NPC.ai[0] = 0f;
					return;
				}
			}
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				NPC.localAI[0] += expertMode ? 2f : 1f;
				if (NPC.localAI[0] >= 600f)
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/8f;
				    	double offsetAngle;
				    	int damage = 0;
				    	int projectileShot = Mod.Find<ModProjectile>("HealOrbProv").Type;
				    	int i;
				    	for (i = 0; i < 3; i++ )
				    	{
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
				    	}
					}
				}
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Profaned Guardian";
			potionType = ItemID.GreaterHealingPotion;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.OnFire, 600, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}