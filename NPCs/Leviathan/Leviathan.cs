using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Leviathan
{
	[AutoloadBossHead]
	public class Leviathan : ModNPC
	{
		public int oneTime = 0;

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
			//NPC.name = "Leviathan");
			//Tooltip.SetDefault("The Leviathan");
			NPC.npcSlots = 10f;
			NPC.damage = 130;
			NPC.width = 300; //324
			NPC.height = 280; //216
			NPC.scale = 2f;
			NPC.defense = 99999;
			NPC.lifeMax = 999999;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			AIType = -1;
			Main.npcFrameCount[NPC.type] = 8;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath60;
			NPC.behindTiles = true;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
			NPC.boss = true;
			NPC.netAlways = true;
			Music = MusicID.Boss3;
			NPC.timeLeft = NPC.activeTime * 30;
		}

        public override void AI()
		{
			int npcType = Mod.Find<ModNPC>("Siren").Type;
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if ((Main.npc[num569].active && Main.npc[num569].type == (npcType)))
				{
					flag100 = true;
				}
			}
			if (!flag100)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 300;
				NPC.height = 280;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
				NPC.life = 0;
				SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.position);
				float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("Leviathangib1").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("Leviathangib2").Type, 1f);
				for (int gib = 0; gib <= 3; gib++)
				{
					randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
					Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("Leviathangib3").Type, 1f);
				}
			}
			if (oneTime == 0)
			{
				RainStart();
				oneTime++;
			}
			int sounder = Main.rand.Next(3);
			SoundStyle soundChoice = Terraria.ID.SoundID.Zombie38;
			if (sounder == 0)
			{
				soundChoice = SoundID.Zombie38;
			}
			else if (sounder == 1)
			{
				soundChoice = SoundID.Zombie39;
			}
			else
			{
				soundChoice = SoundID.Zombie40;
			}
			float teleportLocation = 0f;
			int teleChoice = Main.rand.Next(2);
			if (teleChoice == 0)
			{
				teleportLocation = 1000f;
			}
			else
			{
				teleportLocation = -1000f;
			}
			bool expertMode = Main.expertMode;
			int num2 = expertMode ? 60 : 55;
			float num3 = expertMode ? 0.7f : 0.65f;
			float scaleFactor = expertMode ? 12f : 10f;
			int num4 = expertMode ? 25 : 28;
			float num5 = expertMode ? 12f : 11f;
			int num9 = 90;
			int num11 = 180;
			int num12 = 30;
			int num13 = 120;
			float num15 = 6.28318548f / (float)(num13 / 2);
			int num16 = 10;
			Vector2 vector = NPC.Center;
			Player player = Main.player[NPC.target];
			bool playerWet = player.wet;
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
				player = Main.player[NPC.target];
				NPC.netUpdate = true;
			}
			if (player.dead)
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
			bool flag6 = player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400));
			if (flag6)
			{
				num2 = 20;
				NPC.damage = NPC.defDamage * 2;
				NPC.defense = NPC.defDefense * 2;
				num5 += 6f;
			}
			else if (!playerWet)
			{
				num2 = expertMode ? 40 : 35;
				num5 += 3f;
			}
			if (expertMode)
			{
				num5 += 1f;
			}
			if (Siren.phase2)
			{
				num5 += 1f;
			}
			if (Siren.phase3)
			{
				num5 += 1f;
			}
			if (NPC.localAI[0] == 0f)
			{
				NPC.localAI[0] = 1f;
				NPC.alpha = 255;
				NPC.rotation = 0f;
				if (Main.netMode != 1)
				{
					NPC.ai[0] = -1f;
					NPC.netUpdate = true;
				}
			}
			float num17 = (float)Math.Atan2((double)(player.Center.Y - vector.Y), (double)(player.Center.X - vector.X));
			if (NPC.spriteDirection == 1)
			{
				num17 += 3.14159274f;
			}
			if (num17 < 0f)
			{
				num17 += 6.28318548f;
			}
			if (num17 > 6.28318548f)
			{
				num17 -= 6.28318548f;
			}
			if (NPC.ai[0] == -1f)
			{
				num17 = 0f;
			}
			if (NPC.ai[0] == 3f)
			{
				num17 = 0f;
			}
			if (NPC.ai[0] == 4f)
			{
				num17 = 0f;
			}
			float num18 = 0.04f;
			if (NPC.ai[0] == 1f)
			{
				num18 = 0f;
			}
			if (NPC.ai[0] == 3f)
			{
				num18 = 0.01f;
			}
			if (NPC.ai[0] == 4f)
			{
				num18 = 0.01f;
			}
			if (NPC.rotation < num17)
			{
				if ((double)(num17 - NPC.rotation) > 3.1415926535897931)
				{
					NPC.rotation -= num18;
				}
				else
				{
					NPC.rotation += num18;
				}
			}
			if (NPC.rotation > num17)
			{
				if ((double)(NPC.rotation - num17) > 3.1415926535897931)
				{
					NPC.rotation += num18;
				}
				else
				{
					NPC.rotation -= num18;
				}
			}
			if (NPC.rotation > num17 - num18 && NPC.rotation < num17 + num18)
			{
				NPC.rotation = num17;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.28318548f;
			}
			if (NPC.rotation > 6.28318548f)
			{
				NPC.rotation -= 6.28318548f;
			}
			if (NPC.rotation > num17 - num18 && NPC.rotation < num17 + num18)
			{
				NPC.rotation = num17;
			}
			if (NPC.ai[0] == -1f) //initial spawn effects
			{
				NPC.velocity *= 0.98f;
				NPC.spriteDirection = -NPC.direction;
				if (NPC.ai[2] > 20f) 
				{
					NPC.velocity.Y = -2f;
					NPC.alpha -= 5;
					bool flag135 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
					if (flag135) 
					{
						NPC.alpha += 15;
					}
					if (NPC.alpha < 0) 
					{
						NPC.alpha = 0;
					}
					if (NPC.alpha > 150) 
					{
						NPC.alpha = 150;
					}
				}
				if (NPC.ai[2] == (float)(num9 - 30)) 
				{
					int num1468 = 36;
					for (int num1469 = 0; num1469 < num1468; num1469++) 
					{
						Vector2 vector169 = Vector2.Normalize(NPC.velocity) * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f;
						vector169 = vector169.RotatedBy((double)((float)(num1469 - (num1468 / 2 - 1)) * 6.28318548f / (float)num1468), default(Vector2)) + NPC.Center;
						Vector2 value16 = vector169 - NPC.Center;
						int num1470 = Dust.NewDust(vector169 + value16, 0, 0, 33, value16.X * 2f, value16.Y * 2f, 100, default(Color), 1.4f); //changed
						Main.dust[num1470].noGravity = true;
						Main.dust[num1470].noLight = true;
						Main.dust[num1470].velocity = Vector2.Normalize(value16) * 3f;
					}
					SoundEngine.PlaySound(soundChoice, vector);
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num16) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 0f && !player.dead)
			{
				if (NPC.ai[2] < (float)(num11 - 90))
				{
					bool flag9 = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);
					if (flag9)
					{
						NPC.alpha += 15;
					}
					else
					{
						NPC.alpha -= 15;
					}
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
					if (NPC.alpha > 150)
					{
						NPC.alpha = 150;
					}
				}
				else if (NPC.alpha < 175)
				{
					NPC.alpha += 5;
					if (NPC.alpha > 175)
					{
						NPC.alpha = 175;
					}
				}
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num11 - 60))
                {
                    SoundEngine.PlaySound(soundChoice, vector);
                }
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num11)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 1f && !player.dead)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = false;
				if (NPC.alpha < 175)
				{
					NPC.alpha += 15;
					if (NPC.alpha > 175)
					{
						NPC.alpha = 175;
					}
				}
				if (NPC.ai[1] == 0f)
				{
					NPC.ai[1] = (float)(360 * Math.Sign((vector - player.Center).X));
				}
				Vector2 value7 = player.Center + new Vector2(NPC.ai[1], teleportLocation) - vector; //was -800
				Vector2 desiredVelocity = Vector2.Normalize(value7 - NPC.velocity) * scaleFactor;
				NPC.SimpleFlyMovement(desiredVelocity, num3);
				int num32 = Math.Sign(player.Center.X - vector.X);
				if (num32 != 0)
				{
					if (NPC.ai[2] == 0f && num32 != NPC.direction)
					{
						NPC.rotation = 3.14159274f;
					}
					NPC.direction = num32;
					if (num32 != 0) 
					{
						NPC.direction = num32;
						NPC.rotation = 0f;
						NPC.spriteDirection = -NPC.direction; //end issue
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num2)
				{
					int num33 = 0;
					switch ((int)NPC.ai[3])
					{
					case 0:
					case 2:
					case 3:
					case 5:
					case 6:
					case 7:
						num33 = 1;
						break;
					case 1:
					case 4:
					case 8:
						num33 = 2;
						break;
					}
					if (num33 == 1)
					{
						NPC.ai[0] = 2f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.velocity = Vector2.Normalize(player.Center - vector) * num5;
						NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
						if (num32 != 0) 
						{
							NPC.direction = num32; //perhaps an issue lies here
							if (NPC.spriteDirection == 1) 
							{
								NPC.rotation += 3.14159274f;
							}
							NPC.spriteDirection = -NPC.direction; //end issue
						}
					}
					else if (num33 == 2)
					{
						NPC.ai[0] = 3f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					else if (num33 == 3)
					{
						NPC.ai[0] = 4f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				NPC.alpha -= 15;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				int num34 = 7;
				for (int m = 0; m < num34; m++)
				{
					Vector2 vector11 = Vector2.Normalize(NPC.velocity) * new Vector2((float)(NPC.width + 50) / 2f, (float)NPC.height) * 0.75f;
					vector11 = vector11.RotatedBy((double)(m - (num34 / 2 - 1)) * 3.1415926535897931 / (double)((float)num34), default(Vector2)) + vector;
					Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num35 = Dust.NewDust(vector11 + value8, 0, 0, 33, value8.X * 2f, value8.Y * 2f, 100, default(Color), 1.4f); //changed
					Main.dust[num35].noGravity = true;
					Main.dust[num35].noLight = true;
					Main.dust[num35].velocity /= 4f;
					Main.dust[num35].velocity -= NPC.velocity;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num4)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				if (NPC.alpha < 175)
				{
					NPC.alpha += 12;
					if (NPC.alpha > 175)
					{
						NPC.alpha = 175;
					}
				}
				NPC.velocity *= 0.98f;
				NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, 0f, 0.02f);
				if (NPC.ai[2] == (float)(num12 / 2))
                {
                    SoundEngine.PlaySound(soundChoice, vector);
                }
				if (Main.netMode != 1 && NPC.ai[2] == (float)(num12 / 2))
				{
					if (NPC.ai[1] == 0f)
					{
						NPC.ai[1] = (float)(300 * Math.Sign((vector - player.Center).X));
					}
					Vector2 center = player.Center + new Vector2(-NPC.ai[1], teleportLocation); //was -800
					vector = (NPC.Center = center);
					int num36 = Math.Sign(player.Center.X - vector.X);
					NPC.rotation -= num15 * (float)NPC.direction;
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num12)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					if (NPC.ai[3] >= 9f)
					{
						NPC.ai[3] = 0f;
					}
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				if (NPC.ai[2] == 0f)
                {
                    SoundEngine.PlaySound(soundChoice, vector);
                }
				NPC.velocity = NPC.velocity.RotatedBy((double)(-(double)num15 * (float)NPC.direction), default(Vector2));
				NPC.rotation -= num15 * (float)NPC.direction;
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= (float)num13)
				{
					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] += 1f;
					NPC.netUpdate = true;
				}
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
        	target.AddBuff(BuffID.Wet, 240, true);
        }
		
		public override bool CheckActive()
		{
			return false;
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
		
		public override void FindFrame(int frameHeight)
		{
			if (NPC.ai[0] == 0f || NPC.ai[0] == 1f)
			{
				int num84 = 5;
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > (double)num84)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y >= frameHeight * 6)
				{
					NPC.frame.Y = 0;
				}
			}
			if (NPC.ai[0] == 2f)
			{
				NPC.frame.Y = frameHeight * 6;
			}
			if (NPC.ai[0] == 3f)
			{
				int num85 = 90;
				if (NPC.ai[2] < (float)(num85 - 30) || NPC.ai[2] > (float)(num85 - 10))
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 5.0)
					{
						NPC.frameCounter = 0.0;
						NPC.frame.Y = NPC.frame.Y + frameHeight;
					}
					if (NPC.frame.Y >= frameHeight * 6)
					{
						NPC.frame.Y = 0;
					}
				}
				else
				{
					NPC.frame.Y = frameHeight * 6;
					if (NPC.ai[2] > (float)(num85 - 20) && NPC.ai[2] < (float)(num85 - 15))
					{
						NPC.frame.Y = frameHeight * 7;
					}
				}
			}
			if (NPC.ai[0] == 4f)
			{
				int num86 = 180;
				if (NPC.ai[2] < (float)(num86 - 60) || NPC.ai[2] > (float)(num86 - 20))
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 5.0)
					{
						NPC.frameCounter = 0.0;
						NPC.frame.Y = NPC.frame.Y + frameHeight;
					}
					if (NPC.frame.Y >= frameHeight * 6)
					{
						NPC.frame.Y = 0;
					}
				}
				else
				{
					NPC.frame.Y = frameHeight * 6;
					if (NPC.ai[2] > (float)(num86 - 50) && NPC.ai[2] < (float)(num86 - 25))
					{
						NPC.frame.Y = frameHeight * 7;
					}
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 999999;
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
	}
}