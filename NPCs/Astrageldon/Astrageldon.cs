using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Astrageldon
{
	public class Astrageldon : ModNPC
	{
		public float bossLife;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Astrageldon Slime");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.width = 225;
			NPC.height = 138;
			NPC.scale = 2f;
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorld.revenge ? 65000 : 60000;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0f;
			AnimationType = 50;
			NPC.value = Item.buyPrice(0, 20, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.boss = true;
			NPC.alpha = 60;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.MartianMadness;
			NPC.timeLeft = NPC.activeTime * 30; 
			SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.AstralMeteorBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("A quiet tragedy of cosmic disgust.")

            });
        }
        public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			int damageBuff = (int)(200f * (1f - (float)NPC.life / (float)NPC.lifeMax));
			NPC.damage = NPC.defDamage + damageBuff;
			int defenseBuff = (int)(40f * (1f - (float)NPC.life / (float)NPC.lifeMax));
			NPC.defense = NPC.defDefense + defenseBuff;
			int shootBuff = (int)(14f * (1f - (float)NPC.life / (float)NPC.lifeMax));
			float shootTimer = 1f + ((float)shootBuff) + ((float)(Main.rand.NextBool(15)? 15 : 0));
			bool flag8 = false;
			bool flag9 = false;
			NPC.aiAction = 0;
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.localAI[0] += shootTimer;
				if (NPC.localAI[0] >= 1200f)
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/8f;
				    	double offsetAngle;
				    	int i;
				    	int laserDamage = expertMode ? 28 : 31;
				    	int shootType = Main.rand.Next(10);
				    	float speedX = 5f;
				    	float speedY = 5f;
				    	if (shootType == 0)
				    	{
				    		speedX *= 1.5f;
				    		speedY *= 1.5f;
				    	}
				    	else if (shootType == 1)
				    	{
				    		speedX *= 1.25f;
				    		speedY *= 1.25f;
				    	}
				    	else if (shootType == 2)
				    	{
				    		speedX *= 0.5f;
				    		speedY *= 0.5f;
				    	}
				    	else if (shootType == 3)
				    	{
				    		speedX *= 1.25f;
				    	}
				    	else if (shootType == 4)
				    	{
				    		speedY *= 1.25f;
				    	}
				    	else if (shootType == 5)
				    	{
				    		speedX *= 0.5f;
				    		speedY *= 1.5f;
				    	}
				    	else if (shootType == 6)
				    	{
				    		speedX *= 1.5f;
				    		speedY *= 0.5f;
				    	}
				    	else if (shootType == 7)
				    	{
				    		speedX *= 1.25f;
				    		speedY *= 0.25f;
				    	}
				    	else if (shootType == 8)
				    	{
				    		speedX *= 0.25f;
				    		speedY *= 1.5f;
				    	}
				    	else
				    	{
				    		speedY *= 2f;
				    	}
				    	for (i = 0; i < 4; i++ )
					    {
					   		offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					   		Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)( Math.Sin(offsetAngle) * speedX ), (float)( Math.Cos(offsetAngle) * speedY ), Mod.Find<ModProjectile>("AstrageldonShot").Type, laserDamage, 0f, Main.myPlayer, 0f, 0f);
					       	Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)( -Math.Sin(offsetAngle) * speedX ), (float)( -Math.Cos(offsetAngle) * speedY ), Mod.Find<ModProjectile>("AstrageldonShot").Type, laserDamage, 0f, Main.myPlayer, 0f, 0f);
					   	}
					}
				}
		   	}
			if (NPC.localAI[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient) 
			{
				NPC.ai[0] = -100f;
				NPC.localAI[3] = 1f;
				NPC.TargetClosest(true);
				NPC.netUpdate = true;
			}
			if (Main.player[NPC.target].dead) 
			{
				NPC.TargetClosest(true);
				if (Main.player[NPC.target].dead) 
				{
					NPC.timeLeft = 0;
					if (Main.player[NPC.target].Center.X < NPC.Center.X) 
					{
						NPC.direction = 1;
					} 
					else 
					{
						NPC.direction = -1;
					}
				}
			}
			NPC.dontTakeDamage = (NPC.hide = flag9);
			if (NPC.velocity.Y == 0f) 
			{
				NPC.velocity.X = NPC.velocity.X * 0.8f;
				if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
				{
					NPC.velocity.X = 0f;
				}
				if (!flag8) 
				{
					NPC.ai[0] += 2f;
					if ((double)NPC.life < (double)NPC.lifeMax * 0.8) 
					{
						NPC.ai[0] += 1f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.6) 
					{
						NPC.ai[0] += 2f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.4) 
					{
						NPC.ai[0] += 3f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.2) 
					{
						NPC.ai[0] += 5f;
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1) 
					{
						NPC.ai[0] += 7f;
					}
					if (NPC.ai[0] >= 0f) 
					{
						NPC.netUpdate = true;
						NPC.TargetClosest(true);
						if (NPC.ai[1] == 3f) 
						{
							NPC.velocity.Y = -13f;
							NPC.velocity.X = NPC.velocity.X + 3.5f * (float)NPC.direction;
							NPC.ai[0] = -200f;
							NPC.ai[1] = 0f;
						} 
						else if (NPC.ai[1] == 2f) 
						{
							NPC.velocity.Y = -6f;
							NPC.velocity.X = NPC.velocity.X + 4.5f * (float)NPC.direction;
							NPC.ai[0] = -120f;
							NPC.ai[1] += 1f;
						} 
						else
						{
							NPC.velocity.Y = -8f;
							NPC.velocity.X = NPC.velocity.X + 4f * (float)NPC.direction;
							NPC.ai[0] = -120f;
							NPC.ai[1] += 1f;
						}
					} 
					else if (NPC.ai[0] >= -30f)
					{
						NPC.aiAction = 1;
					}
				}
			} 
			else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f))) 
			{
				if ((NPC.direction == -1 && (double)NPC.velocity.X < 0.1) || (NPC.direction == 1 && (double)NPC.velocity.X > -0.1)) 
				{
					NPC.velocity.X = NPC.velocity.X + 0.2f * (float)NPC.direction;
				} 
				else 
				{
					NPC.velocity.X = NPC.velocity.X * 0.93f;
				}
			}
			if (bossLife == 0f && NPC.life > 0)
			{
				bossLife = (float)NPC.lifeMax;
			}
	       	float num644 = 1f;
	       	if (NPC.life > 0)
			{
				float num659 = (float)NPC.life / (float)NPC.lifeMax;
				num659 = num659 * 0.5f + 0.75f;
				num659 *= num644;
				if (num659 != NPC.scale)
				{
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)NPC.height;
					NPC.scale = num659 * 2f;
					NPC.width = (int)(150f * NPC.scale);
					NPC.height = (int)(92f * NPC.scale);
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)NPC.height;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.015);
					if ((float)(NPC.life + num660) < bossLife)
					{
						bossLife = (float)NPC.life;
						int num661 = Main.rand.Next(2, 5);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("AstralSlime").Type;
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(num663);
							Main.npc[num664].value = Item.buyPrice(0, 0, 0, 0);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
							Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
							Main.npc[num664].ai[1] = 0f;
							Main.npc[num664].localAI[3] = 1f;
							if (Main.netMode == NetmodeID.Server && num664 < 200)
							{
								NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						return;
					}
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Stardust>(), 1, 30, 40));
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ShadowbeamStaff, hit.HitDirection, -1f, 0, default(Color), 1f);
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 100; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.YellowTorch, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.YellowTorch, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 150, true);
		}
	}
}