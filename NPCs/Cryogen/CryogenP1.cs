using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;

namespace CalamityModClassic1Point1.NPCs.Cryogen
{
	[AutoloadBossHead]
	public class CryogenP1 : ModNPC
	{
		public int time = 0;

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
			//NPC.name = "Cryogen");
			//Tooltip.SetDefault("Cryogen");
			NPC.npcSlots = 5f;
			NPC.damage = 70;
			NPC.width = 200; //324
			NPC.height = 200; //216
			NPC.defense = 25;
			NPC.lifeMax = 7000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            Main.npcFrameCount[NPC.type] = 1; //new
            AnimationType = 10; //new
			NPC.scale = 1.2f;
			NPC.knockBackResist = 0f;
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = true;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.timeLeft = NPC.activeTime * 30;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
			Music = MusicID.FrostMoon;
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.ZoneSnow && NPC.downedPlantBoss ? 0.00001f : 0f; 
		}
		
		public override void AI()
		{
			bool isChill = Main.player[NPC.target].ZoneSnow;
			bool expertMode = Main.expertMode;
			if (expertMode && Main.netMode != 1)
			{
				time++;
				if (time >= 300)
				{
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/4f;
				    	double offsetAngle;
				    	int i;
				    	int num184 = 28;
						for (i = 0; i < 2; i++ )
				    	{
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("IceBomb").Type, num184, 0f, Main.myPlayer, 0f, 0f);
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("IceBomb").Type, num184, 0f, Main.myPlayer, 0f, 0f);
				    	}
					}
					time = 0;
				}
			}
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.01f, 0.85f, 0.85f);
			if (Main.netMode != 1)
			{
				NPC.localAI[0] += (float)Main.rand.Next(4);
				if (NPC.localAI[0] >= (float)Main.rand.Next(200, 300))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/8f;
				    	double offsetAngle;
				    	int num184 = expertMode ? 28 : 50;
				    	int i;
				    	for (i = 0; i < 8; i++ )
				    	{
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), 128, num184, 0f, Main.myPlayer, 0f, 0f);
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), 128, num184, 0f, Main.myPlayer, 0f, 0f);
				    	}
					}
				}
	       	}
			NPC.rotation += 0.125f;
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
						int num661 = Main.rand.Next(1, 3);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("Cryocore").Type;
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(num663);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
							Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
							Main.npc[num664].ai[1] = 0f;
							if (Main.netMode == 2 && num664 < 200)
							{
								NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						return;
					}
				}
			}
			NPC.TargetClosest(true);
			Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num1243 = Main.player[NPC.target].Center.X - vector142.X;
			float num1244 = Main.player[NPC.target].Center.Y - vector142.Y;
			float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
			float num1246 = isChill ? 1f : 3f;
			if (num1245 < num1246)
			{
				NPC.velocity.X = num1243;
				NPC.velocity.Y = num1244;
			}
			else
			{
				num1245 = num1246 / num1245;
				NPC.velocity.X = num1243 * num1245;
				NPC.velocity.Y = num1244 * num1245;
			}
			if (NPC.ai[0] == 0f)
			{
				if (Main.netMode != 1)
				{
					NPC.localAI[1] += 2f;
					if (NPC.localAI[1] >= (float)(120 + Main.rand.Next(150)))
					{
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
							num1250 += Main.rand.Next(-50, 51);
							num1251 += Main.rand.Next(-50, 51);
							if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
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
				NPC.alpha += 5;
				if (NPC.alpha >= 255)
				{
					NPC.alpha = 255;
					NPC.position.X = NPC.ai[1] * 16f - (float)(NPC.width / 2);
					NPC.position.Y = NPC.ai[2] * 16f - (float)(NPC.height / 2);
					NPC.ai[0] = 2f;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.alpha -= 5;
				if (NPC.alpha <= 0)
				{
					NPC.alpha = 0;
					NPC.ai[0] = 0f;
					return;
				}
			}
			if (Main.player[NPC.target].dead) 
			{
				if (NPC.localAI[3] < 120f) 
				{
					NPC.localAI[3] += 1f;
				}
				if (NPC.localAI[3] > 60f) 
				{
					NPC.velocity.Y = NPC.velocity.Y + (NPC.localAI[3] - 60f) * 0.25f;
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
				NPC.ai[0] = 2f;
				NPC.alpha = 10;
				return;
			}
			if (NPC.localAI[3] > 0f) 
			{
				NPC.localAI[3] -= 1f;
				return;
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 240;
				NPC.height = 240;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
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
			}
		}
		
		public override bool CheckDead()
		{
			float targetX = NPC.Center.X;
			float targetY = NPC.Center.Y;
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("CryogenP2").Type, 0, NPC.whoAmI, targetX, targetY);
			Main.NewText("Cryogen's magic is being released!", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
			RainStart();
			return true;
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
			}
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode && Main.rand.Next(4) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 300, true);
			}
			else if (Main.rand.Next(5) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 300, true);
			}
		}
	}
}