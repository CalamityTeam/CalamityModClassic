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
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Accessories;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Scavenger
{
	public class ScavengerBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ravager");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 10f;
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.width = 300; //324
			NPC.height = 240; //216
			NPC.defense = 80;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 52000 : 40000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.boss = true;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			if (CalamityWorld1Point2.downedProvidence)
			{
				NPC.damage = 0;
				NPC.defense = 180;
				NPC.lifeMax = 540000;
				NPC.value = Item.buyPrice(5, 0, 0, 0);
			}
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple,
                new FlavorTextBestiaryInfoElement("An old mining unit repurposed for war.")

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
			bool provy = CalamityWorld1Point2.downedProvidence;
			bool expertMode = Main.expertMode;
			Lighting.AddLight((int)(NPC.position.X - 100f) / 16, (int)(NPC.position.Y - 20f) / 16, 0f, 0.51f, 2f);
			Lighting.AddLight((int)(NPC.position.X + 100f) / 16, (int)(NPC.position.Y - 20f) / 16, 0f, 0.51f, 2f);
			CalamityGlobalNPC1Point2.scavenger = NPC.whoAmI;
			if (NPC.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient) 
			{
				NPC.localAI[0] = 1f;
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 70, (int)NPC.Center.Y + 88, Mod.Find<ModNPC>("ScavengerLegLeft").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 70, (int)NPC.Center.Y + 88, Mod.Find<ModNPC>("ScavengerLegRight").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 120, (int)NPC.Center.Y + 50, Mod.Find<ModNPC>("ScavengerClawLeft").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 120, (int)NPC.Center.Y + 50, Mod.Find<ModNPC>("ScavengerClawRight").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y - 20, Mod.Find<ModNPC>("ScavengerHead").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
			if (NPC.target >= 0 && Main.player[NPC.target].dead) 
			{
				NPC.TargetClosest(true);
				if (Main.player[NPC.target].dead) 
				{
					NPC.noTileCollide = true;
				}
			}
			if (NPC.alpha > 0) 
			{
				NPC.alpha -= 10;
				if (NPC.alpha < 0) 
				{
					NPC.alpha = 0;
				}
				NPC.ai[1] = 0f;
			}
			bool leftLegActive = false;
			bool rightLegActive = false;
			bool headActive = false;
			bool rightClawActive = false;
			bool leftClawActive = false;
			for (int num619 = 0; num619 < 200; num619++) 
			{
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerHead").Type) 
				{
					headActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerClawRight").Type) 
				{
					rightClawActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerClawLeft").Type) 
				{
					leftClawActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerLegRight").Type) 
				{
					rightLegActive = true;
				}
				if (Main.npc[num619].active && Main.npc[num619].type == Mod.Find<ModNPC>("ScavengerLegLeft").Type) 
				{
					leftLegActive = true;
				}
			}
			if (headActive || rightClawActive || leftClawActive || rightLegActive || leftLegActive)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
			if (!headActive) 
			{
				int rightDust = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y - 50f), 8, 8, DustID.Blood, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[rightDust].alpha += Main.rand.Next(100);
				Main.dust[rightDust].velocity *= 0.2f;
				Dust rightDustExpr = Main.dust[rightDust];
				rightDustExpr.velocity.Y = rightDustExpr.velocity.Y - (3f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10)) 
				{
					rightDust = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y - 50f), 8, 8, DustID.Torch, 0f, 0f, 0, default(Color), 1.5f);
					if (!Main.rand.NextBool(20)) 
					{
						Main.dust[rightDust].noGravity = true;
						Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust rightDustExpr2 = Main.dust[rightDust];
						rightDustExpr2.velocity.Y = rightDustExpr2.velocity.Y - 4f;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += 1f;
					if (NPC.localAI[1] >= 180f)
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
					    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
					    	double deltaAngle = spread/8f;
					    	double offsetAngle;
					    	int i;
					    	int laserDamage = expertMode ? 42 : 50;
					    	for (i = 0; i < 4; i++ )
					    	{
					   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					        	Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)( Math.Sin(offsetAngle) * 11f ), (float)( Math.Cos(offsetAngle) * 11f ), 259, laserDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					        	Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)( -Math.Sin(offsetAngle) * 11f ), (float)( -Math.Cos(offsetAngle) * 11f ), 259, laserDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					    	}
						}
					}
		       	}
			}
			if (!rightClawActive) 
			{
				int rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 80f, NPC.Center.Y + 45f), 8, 8, DustID.Blood, 0f, 0f, 100, default(Color), 3f);
				Main.dust[rightDust].alpha += Main.rand.Next(100);
				Main.dust[rightDust].velocity *= 0.2f;
				Dust rightDustExpr = Main.dust[rightDust];
				rightDustExpr.velocity.X = rightDustExpr.velocity.X + (3f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10)) 
				{
					rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 80f, NPC.Center.Y + 45f), 8, 8, DustID.Torch, 0f, 0f, 0, default(Color), 2f);
					if (!Main.rand.NextBool(20)) 
					{
						Main.dust[rightDust].noGravity = true;
						Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust rightDustExpr2 = Main.dust[rightDust];
						rightDustExpr2.velocity.X = rightDustExpr2.velocity.X + 4f;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[2] += 1f;
					if (NPC.localAI[2] >= 120f)
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						NPC.localAI[2] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X + 80f, NPC.Center.Y + 45f);
						int laser = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, 14f, 0f, 258, 42 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (!leftClawActive) 
			{
				int leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 80f, NPC.Center.Y + 45f), 8, 8, DustID.Blood, 0f, 0f, 100, default(Color), 3f);
				Main.dust[leftDust].alpha += Main.rand.Next(100);
				Main.dust[leftDust].velocity *= 0.2f;
				Dust leftDustExpr = Main.dust[leftDust];
				leftDustExpr.velocity.X = leftDustExpr.velocity.X - (3f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[leftDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10)) 
				{
					leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 80f, NPC.Center.Y + 45f), 8, 8, DustID.Torch, 0f, 0f, 0, default(Color), 2f);
					if (!Main.rand.NextBool(20)) 
					{
						Main.dust[leftDust].noGravity = true;
						Main.dust[leftDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust leftDustExpr2 = Main.dust[leftDust];
						leftDustExpr2.velocity.X = leftDustExpr2.velocity.X - 4f;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[3] += 1f;
					if (NPC.localAI[3] >= 120f)
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						NPC.localAI[3] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X - 80f, NPC.Center.Y + 45f);
						int laser = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, -14f, 0f, 258, 42 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (!rightLegActive) 
			{
				int rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 60f, NPC.Center.Y + 60f), 8, 8, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
				Main.dust[rightDust].alpha += Main.rand.Next(100);
				Main.dust[rightDust].velocity *= 0.2f;
				Dust rightDustExpr = Main.dust[rightDust];
				rightDustExpr.velocity.Y = rightDustExpr.velocity.Y + (0.5f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[rightDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10)) 
				{
					rightDust = Dust.NewDust(new Vector2(NPC.Center.X + 60f, NPC.Center.Y + 60f), 8, 8, DustID.Torch, 0f, 0f, 0, default(Color), 1.5f);
					if (!Main.rand.NextBool(20)) 
					{
						Main.dust[rightDust].noGravity = true;
						Main.dust[rightDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust rightDustExpr2 = Main.dust[rightDust];
						rightDustExpr2.velocity.Y = rightDustExpr2.velocity.Y + 1f;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 60f)
					{
						NPC.ai[2] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X + 60f, NPC.Center.Y + 60f);
						int fire = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, 0f, 3f, 326 + Main.rand.Next(3), 42 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[fire].timeLeft = 210;
					}
				}
			}
			if (!leftLegActive) 
			{
				int leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 60f, NPC.Center.Y + 60f), 8, 8, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
				Main.dust[leftDust].alpha += Main.rand.Next(100);
				Main.dust[leftDust].velocity *= 0.2f;
				Dust leftDustExpr = Main.dust[leftDust];
				leftDustExpr.velocity.Y = leftDustExpr.velocity.Y + (0.5f + (float)Main.rand.Next(10) * 0.1f);
				Main.dust[leftDust].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10)) 
				{
					leftDust = Dust.NewDust(new Vector2(NPC.Center.X - 60f, NPC.Center.Y + 60f), 8, 8, DustID.Torch, 0f, 0f, 0, default(Color), 1.5f);
					if (!Main.rand.NextBool(20)) 
					{
						Main.dust[leftDust].noGravity = true;
						Main.dust[leftDust].scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						Dust leftDustExpr2 = Main.dust[leftDust];
						leftDustExpr2.velocity.Y = leftDustExpr2.velocity.Y + 1f;
					}
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.ai[3] += 1f;
					if (NPC.ai[3] >= 60f)
					{
						NPC.ai[3] = 0f;
						Vector2 shootFromVector = new Vector2(NPC.Center.X - 60f, NPC.Center.Y + 60f);
						int fire = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, 0f, 3f, 326 + Main.rand.Next(3), 42 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[fire].timeLeft = 210;
					}
				}
			}
			if (NPC.ai[0] == 0f) 
			{
				NPC.noTileCollide = false;
				if (NPC.velocity.Y == 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.8f;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 0f) 
					{
						if (!rightClawActive)
						{
							NPC.ai[1] += 4f;
						}
						if (!leftClawActive)
						{
							NPC.ai[1] += 4f;
						}
						if (!headActive)
						{
							NPC.ai[1] += 4f;
						}
						if (!rightLegActive)
						{
							NPC.ai[1] += 4f;
						}
						if (!leftLegActive)
						{
							NPC.ai[1] += 4f;
						}
					}
					if (NPC.ai[1] >= 300f) 
					{
						NPC.ai[1] = -20f;
					} 
					else if (NPC.ai[1] == -1f)
					{
						NPC.TargetClosest(true);
						NPC.velocity.X = (float)(4 * NPC.direction);
						NPC.velocity.Y = -12.1f;
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
					}
				}
			} 
			else if (NPC.ai[0] == 1f) 
			{
				if (NPC.velocity.Y == 0f) 
				{
					SoundEngine.PlaySound(SoundID.Item14, NPC.position);
					NPC.ai[0] = 0f;
					for (int stompDustArea = (int)NPC.position.X - 30; stompDustArea < (int)NPC.position.X + NPC.width + 60; stompDustArea += 30) 
					{
						for (int stompDustAmount = 0; stompDustAmount < 6; stompDustAmount++) 
						{
							int stompDust = Dust.NewDust(new Vector2(NPC.position.X - 30f, NPC.position.Y + (float)NPC.height), NPC.width + 30, 4, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[stompDust].velocity *= 0.2f;
						}
						int stompGore = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2((float)(stompDustArea - 30), NPC.position.Y + (float)NPC.height - 12f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[stompGore].velocity *= 0.4f;
					}
				} 
				else 
				{
					NPC.TargetClosest(true);
					if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width) 
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
						NPC.velocity.Y = NPC.velocity.Y + 0.2f;
					} 
					else
					{
						if (NPC.direction < 0) 
						{
							NPC.velocity.X = NPC.velocity.X - 0.2f;
						} 
						else if (NPC.direction > 0)
						{
							NPC.velocity.X = NPC.velocity.X + 0.2f;
						}
						float velocityX = 3f;
						if (!rightClawActive) 
						{
							velocityX += 1f;
						}
						if (!leftClawActive) 
						{
							velocityX += 1f;
						}
						if (!headActive) 
						{
							velocityX += 1f;
						}
						if (!rightLegActive) 
						{
							velocityX += 1f;
						}
						if (!leftLegActive) 
						{
							velocityX += 1f;
						}
						if (NPC.velocity.X < -velocityX) 
						{
							NPC.velocity.X = -velocityX;
						}
						if (NPC.velocity.X > velocityX) 
						{
							NPC.velocity.X = velocityX;
						}
					}
				}
			}
			if (NPC.target <= 0 || NPC.target == 255 || Main.player[NPC.target].dead) 
			{
				NPC.TargetClosest(true);
			}
			int distanceFromTarget = 3000;
			if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget) 
			{
				NPC.TargetClosest(true);
				if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)distanceFromTarget) 
				{
					NPC.active = false;
					return;
				}
			}
		}
		
		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Microsoft.Xna.Framework.Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerLegRight").Value, new Vector2(center.X - Main.screenPosition.X + 33f, center.Y - Main.screenPosition.Y - 72f), 
			    new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerLegRight").Value.Width, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerLegRight").Value.Height)), 
			    color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerLegLeft").Value, new Vector2(center.X - Main.screenPosition.X - 107f, center.Y - Main.screenPosition.Y - 72f), 
			    new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerLegLeft").Value.Width, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerLegLeft").Value.Height)), 
			    color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerHead").Value, new Vector2(center.X - Main.screenPosition.X - 49f, center.Y - Main.screenPosition.Y - 120f), 
			    new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerHead").Value.Width, ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Scavenger/ScavengerHead").Value.Height)), 
			    color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 2f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 2f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Ravager";
			potionType = ItemID.GreaterHealingPotion;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(ItemDropRule.ByCondition(new ProvidenceDowned(), ModContent.ItemType<Bloodstone>(), 1, 50, 60));
			LeadingConditionRule expert = new LeadingConditionRule(new Conditions.IsExpert());
            expert.OnSuccess(ItemDropRule.ByCondition(new ProvidenceDowned(), ModContent.ItemType<BloodflareCore>(), 1));
			npcLoot.Add(expert);
            npcLoot.Add(new CommonDrop(ModContent.ItemType<VerstaltiteBar>(), 1, 5, 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<DraedonBar>(), 1, 5, 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CruptixBar>(), 1, 5, 10));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CoreofCinder>(), 1, 1, 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CoreofEleum>(), 1, 1, 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CoreofChaos>(), 1, 1, 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CoreofCalamity>(), 3));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<BarofLife>(), 2));
        }
	}
}