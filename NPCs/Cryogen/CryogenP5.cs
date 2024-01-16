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
    public class CryogenP5 : ModNPC
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
			//NPC.name = "Cryogen");
			//Tooltip.SetDefault("Cryogen");
			NPC.npcSlots = 5f;
			NPC.damage = 90;
			NPC.width = 200; //324
			NPC.height = 200; //216
			NPC.scale = 0.8f;
			NPC.defense = 10;
			NPC.lifeMax = 3000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            Main.npcFrameCount[NPC.type] = 1; //new
            AnimationType = 10; //new
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
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
			Music = MusicID.FrostMoon;
		}
		
		public override void AI()
		{
			bool isChill = Main.player[NPC.target].ZoneSnow;
			bool expertMode = Main.expertMode;
			if (Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 3f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 3f;
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
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.01f, 0.45f, 0.45f);
			if (NPC.ai[3] == 0f && NPC.life > 0)
			{
				NPC.ai[3] = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != 1)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.15);
					if ((float)(NPC.life + num660) < NPC.ai[3])
					{
						NPC.ai[3] = (float)NPC.life;
						int num661 = Main.rand.Next(1, 3);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("Cryocore2").Type;
							if (Main.expertMode && Main.rand.Next(3) == 0)
							{
								num663 = Mod.Find<ModNPC>("IceMass").Type;
							}
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
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("Cryocore").Type;
							if (Main.expertMode && Main.rand.Next(3) == 0)
							{
								num663 = Mod.Find<ModNPC>("IceMass").Type;
							}
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
	       	if (Main.netMode != 1)
			{
				NPC.localAI[0] += (float)Main.rand.Next(4);
				if (NPC.localAI[0] >= (float)Main.rand.Next(100, 101))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num179 = 11f;
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
						float num181 = Math.Abs(num180) * 0.1f;
						float num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y - num181;
						float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						NPC.netUpdate = true;
						num183 = num179 / num183;
						num180 *= num183;
						num182 *= num183;
						int num184 = expertMode ? 20 : 35;
						int num185 = 128;
						value9.X += num180;
						value9.Y += num182;
						for (int num186 = 0; num186 < 6; num186++)
						{
							num180 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - value9.X;
							num182 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - value9.Y;
							num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = 12f / num183;
							num180 += (float)Main.rand.Next(-90, 91);
							num182 += (float)Main.rand.Next(-90, 91);
							num180 *= num183;
							num182 *= num183;
							int randomTime = Main.rand.Next(200, 600);
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[projectile].timeLeft = randomTime;
						}
					}
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/8f;
				    	double offsetAngle;
				    	int num184 = expertMode ? 20 : 35;
				    	int i;
				    	for (i = 0; i < 6; i++ )
				    	{
				    		int randomTime = Main.rand.Next(400, 700);
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), 128, num184, 0f, Main.myPlayer, 0f, 0f);
				        	int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), 128, num184, 0f, Main.myPlayer, 0f, 0f);
				        	Main.projectile[projectile].timeLeft = randomTime;
				        	Main.projectile[projectile2].timeLeft = randomTime;
				    	}
					}
				}
	       	}
			NPC.TargetClosest(true);
			Vector2 vector161 = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
			float num1334 = (float)Main.rand.Next(-1000, 1001);
			float num1335 = (float)Main.rand.Next(-1000, 1001);
			float num1336 = (float)Math.Sqrt((double)(num1334 * num1334 + num1335 * num1335));
			float num1337 = 15f;
			NPC.velocity *= 0.95f;
			num1336 = num1337 / num1336;
			num1334 *= num1336;
			num1335 *= num1336;
			NPC.rotation += 0.225f;
			vector161.X += num1334 * 4f;
			vector161.Y += num1335 * 4f;
			NPC.ai[2] += 1f;
			int num1338 = 7;
			if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
			{
				num1338--;
			}
			if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
			{
				num1338 -= 2;
			}
			if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
			{
				num1338 -= 3;
			}
			if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
			{
				num1338 -= 4;
			}
			if (NPC.ai[2] > (float)num1338)
			{
				int num1339 = expertMode ? 14 : 25;
				NPC.ai[2] = 0f;
				Projectile.NewProjectile(NPC.GetSource_FromThis(), vector161.X, vector161.Y, num1334, num1335, 349, num1339, 0f, Main.myPlayer, 0f, 0f);
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
				NPC.width = 160;
				NPC.height = 160;
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
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("CryogenP6").Type, 0, NPC.whoAmI, targetX, targetY);
			Main.NewText("Cryogen's core is all that remains!", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
			return true;
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