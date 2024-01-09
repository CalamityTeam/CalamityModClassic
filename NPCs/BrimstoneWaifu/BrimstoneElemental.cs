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
using CalamityModClassic1Point2.NPCs.AstralBiomeNPCs;
using CalamityModClassic1Point2.Items.BrimstoneWaifu;
using CalamityModClassic1Point2.Items.Weapons.BrimstoneWaifu;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.BrimstoneWaifu
{
	[AutoloadBossHead]
	public class BrimstoneElemental : ModNPC
	{
		public int dustTimer = 60;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("A Brimstone Elemental");
			Main.npcFrameCount[NPC.type] = 12;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 15f;
			NPC.damage = 60;
			NPC.width = 100;
			NPC.height = 150;
			NPC.defense = 20;
			NPC.lifeMax = CalamityWorld.revenge ? 22000 : 20000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath39;
			Music = MusicID.Boss2;
			//bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("BrimstoneWaifuBag").Type;
			if (CalamityWorld.downedProvidence)
			{
				NPC.damage = 210;
				NPC.defense = 120;
				NPC.lifeMax = 300000;
				NPC.value = Item.buyPrice(1, 0, 0, 0);
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.BrimstoneCragsBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Something smells like sulphur.")

            });
        }

        public override void AI()
		{
			Player player = Main.player[NPC.target];
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			bool brimDust = (double)NPC.life <= (double)NPC.lifeMax * 0.75;
			bool speedBoost = (double)NPC.life <= (double)NPC.lifeMax * 0.65;
			bool brimRain = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			bool brimSpeed = (double)NPC.life <= (double)NPC.lifeMax * 0.35;
			bool brimTeleport = (double)NPC.life <= (double)NPC.lifeMax * 0.2;
			bool provy = CalamityWorld.downedProvidence;
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			bool calamity = modPlayer.ZoneCalamity;
			bool isHell = player.ZoneUnderworldHeight;
			NPC.dontTakeDamage = !isHell;
			NPC.TargetClosest(true);
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vectorCenter = NPC.Center;
			float xDistance = player.Center.X - center.X;
			float yDistance = player.Center.Y - center.Y;
			float totalDistance = (float)Math.Sqrt((double)(xDistance * xDistance + yDistance * yDistance));
			int dustAmt = (NPC.ai[0] == 2f) ? 2 : 1;
			int size = (NPC.ai[0] == 2f) ? 50 : 35;
			float speed = expertMode ? 5f : 4.5f;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < dustAmt) 
				{
					int dust = Dust.NewDust(NPC.Center - new Vector2((float)size), size * 2, size * 2, DustID.LifeDrain, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.2f;
					Main.dust[dust].fadeIn = 1f;
				}
			}
			if (Vector2.Distance(player.Center, vectorCenter) > 5600f)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			else if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (revenge)
			{
				int damageBoost = (int)(30f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.damage = NPC.defDamage + damageBoost;
			}
			if (!calamity)
			{
				speed = 7.5f;
			}
			else if (speedBoost)
			{
				speed = expertMode ? 6f : 5f;
			}
			if (NPC.ai[0] <= 2f)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					dustTimer--;
					if (dustTimer <= 0)
					{
						int damage = expertMode ? 30 : 35;
						Vector2 position = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)position.X, (int)position.Y, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("BrimDust").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = 90;
						Main.projectile[projectile].velocity.X = 0f;
				        Main.projectile[projectile].velocity.Y = 0f;
			    	    dustTimer = 60;
					}
				}
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				if (totalDistance < speed)
				{
					NPC.velocity.X = xDistance;
					NPC.velocity.Y = yDistance;
				}
				else
				{
					totalDistance = speed / totalDistance;
					NPC.velocity.X = xDistance * totalDistance;
					NPC.velocity.Y = yDistance * totalDistance;
				}
			}
			if (NPC.ai[0] == 0f) 
			{
				NPC.defense = 20;
				NPC.chaseable = true;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += 1f;
					if (revenge)
					{
						NPC.localAI[1] += 1f;
					}
					if (NPC.justHit)
					{
						NPC.localAI[1] += 1f;
					}
					if (brimDust)
					{
						NPC.localAI[1] += 1f;
					}
					if (brimRain)
					{
						NPC.localAI[1] += 2f;
					}
					if (brimTeleport)
					{
						NPC.localAI[1] += 3f;
					}
					if (!calamity)
					{
						NPC.localAI[1] += 3f;
					}
					if (NPC.localAI[1] >= (float)(200 + Main.rand.Next(100)))
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						int timer = 0;
						int playerPosX;
						int playerPosY;
						while (true)
						{
							timer++;
							playerPosX = (int)player.Center.X / 16;
							playerPosY = (int)player.Center.Y / 16;
							playerPosX += Main.rand.Next(-50, 51);
							playerPosY += Main.rand.Next(-50, 51);
							if (!WorldGen.SolidTile(playerPosX, playerPosY) && Collision.CanHit(new Vector2((float)(playerPosX * 16), (float)(playerPosY * 16)), 1, 1, player.position, player.width, player.height))
							{
								break;
							}
							if (timer > 100)
							{
								return;
							}
						}
						NPC.ai[0] = 1f;
						NPC.ai[1] = (float)playerPosX;
						NPC.ai[2] = (float)playerPosY;
						NPC.netUpdate = true;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 1f) 
			{
				NPC.defense = 20;
				NPC.chaseable = true;
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
				NPC.defense = 20;
				NPC.chaseable = true;
				NPC.alpha -= 5;
				if (NPC.alpha <= 0)
				{
					NPC.ai[3] += 1f;
					NPC.alpha = 0;
					if (NPC.ai[3] >= 2f) 
					{
						NPC.ai[0] = 3f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
					} 
					else
					{
						NPC.ai[0] = 0f;
					}
					return;
				}
			}
			else if (NPC.ai[0] == 3f) 
			{
				NPC.defense = 20;
				NPC.chaseable = true;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				float xVelocity = 6f; //changed from 6 to 7.5 modifies speed while firing projectiles
				float yVelocity = 0.075f; //changed from 0.075 to 0.09375 modifies speed while firing projectiles
				Vector2 shootFromVectorX = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 shootFromVectorY = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float playerDistanceX = player.position.X + (float)(player.width / 2) - shootFromVectorY.X;
				float playerDistanceY = player.position.Y + (float)(player.height / 2) - 300f - shootFromVectorY.Y;
				float totalPlayerDistance = (float)Math.Sqrt((double)(playerDistanceX * playerDistanceX + playerDistanceY * playerDistanceY));
				NPC.ai[1] += 1f;
				bool shootProjectile = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
				{
					if (NPC.ai[1] % 15f == 14f)
					{
						shootProjectile = true;
					}
				}
				else if (NPC.life < NPC.lifeMax / 3)
				{
					if (NPC.ai[1] % 25f == 24f)
					{
						shootProjectile = true;
					}
				}
				else if (NPC.life < NPC.lifeMax / 2)
				{
					if (NPC.ai[1] % 30f == 29f)
					{
						shootProjectile = true;
					}
				}
				else if (NPC.ai[1] % 35f == 34f)
				{
					shootProjectile = true;
				}
				if (shootProjectile && NPC.position.Y + (float)NPC.height < player.position.Y && Collision.CanHit(shootFromVectorX, 1, 1, player.position, player.width, player.height))
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						float projectileSpeed = 7f; //changed from 10
						if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							projectileSpeed += 1f;
						}
						if (revenge)
						{
							projectileSpeed += 1f;
						}
						if (brimRain)
						{
							projectileSpeed += 1f; //changed from 3 not a prob
						}
						if (brimSpeed)
						{
							projectileSpeed += 2f;
						}
						if (!calamity)
						{
							projectileSpeed += 3f;
						}
						float relativeSpeedX = player.position.X + (float)player.width * 0.5f - shootFromVectorX.X + (float)Main.rand.Next(-80, 81);
						float relativeSpeedY = player.position.Y + (float)player.height * 0.5f - shootFromVectorX.Y + (float)Main.rand.Next(-40, 41);
						float totalRelativeSpeed = (float)Math.Sqrt((double)(relativeSpeedX * relativeSpeedX + relativeSpeedY * relativeSpeedY));
						totalRelativeSpeed = projectileSpeed / totalRelativeSpeed;
						relativeSpeedX *= totalRelativeSpeed;
						relativeSpeedY *= totalRelativeSpeed;
						int projectileDamage = expertMode ? 25 : 30; //projectile damage
						int projectileType = Mod.Find<ModProjectile>("BrimstoneHellfireball").Type; //projectile type
						int projectileShot = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVectorX.X, shootFromVectorX.Y, relativeSpeedX, relativeSpeedY, projectileType, projectileDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[projectileShot].timeLeft = 240;
					}
				}
				if (!Collision.CanHit(new Vector2(shootFromVectorX.X, shootFromVectorX.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					xVelocity = 14f; //changed from 14 not a prob
					yVelocity = 0.1f; //changed from 0.1 not a prob
					shootFromVectorY = shootFromVectorX;
					playerDistanceX = player.position.X + (float)(player.width / 2) - shootFromVectorY.X;
					playerDistanceY = player.position.Y + (float)(player.height / 2) - shootFromVectorY.Y;
					totalPlayerDistance = (float)Math.Sqrt((double)(playerDistanceX * playerDistanceX + playerDistanceY * playerDistanceY));
					totalPlayerDistance = xVelocity / totalPlayerDistance;
					if (NPC.velocity.X < playerDistanceX)
					{
						NPC.velocity.X = NPC.velocity.X + yVelocity;
						if (NPC.velocity.X < 0f && playerDistanceX > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + yVelocity;
						}
					}
					else if (NPC.velocity.X > playerDistanceX)
					{
						NPC.velocity.X = NPC.velocity.X - yVelocity;
						if (NPC.velocity.X > 0f && playerDistanceX < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - yVelocity;
						}
					}
					if (NPC.velocity.Y < playerDistanceY)
					{
						NPC.velocity.Y = NPC.velocity.Y + yVelocity;
						if (NPC.velocity.Y < 0f && playerDistanceY > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + yVelocity;
						}
					}
					else if (NPC.velocity.Y > playerDistanceY)
					{
						NPC.velocity.Y = NPC.velocity.Y - yVelocity;
						if (NPC.velocity.Y > 0f && playerDistanceY < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - yVelocity;
						}
					}
				}
				else if (totalPlayerDistance > 100f)
				{
					NPC.TargetClosest(true);
					NPC.spriteDirection = NPC.direction;
					totalPlayerDistance = xVelocity / totalPlayerDistance;
					if (NPC.velocity.X < playerDistanceX)
					{
						NPC.velocity.X = NPC.velocity.X + yVelocity;
						if (NPC.velocity.X < 0f && playerDistanceX > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + yVelocity * 2f;
						}
					}
					else if (NPC.velocity.X > playerDistanceX)
					{
						NPC.velocity.X = NPC.velocity.X - yVelocity;
						if (NPC.velocity.X > 0f && playerDistanceX < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - yVelocity * 2f;
						}
					}
					if (NPC.velocity.Y < playerDistanceY)
					{
						NPC.velocity.Y = NPC.velocity.Y + yVelocity;
						if (NPC.velocity.Y < 0f && playerDistanceY > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + yVelocity * 2f;
						}
					}
					else if (NPC.velocity.Y > playerDistanceY)
					{
						NPC.velocity.Y = NPC.velocity.Y - yVelocity;
						if (NPC.velocity.Y > 0f && playerDistanceY < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - yVelocity * 2f;
						}
					}
				}
				if (NPC.ai[1] > 500f)
				{
					NPC.ai[0] = 4f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				NPC.defense = 99999;
				NPC.chaseable = false;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[0] += (float)Main.rand.Next(4);
					if (NPC.localAI[0] >= (float)Main.rand.Next(140, 141))
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						float projectileSpeed = revenge ? 8f : 6f;
						Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num180 = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
						float num181 = Math.Abs(num180) * 0.1f;
						float num182 = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y - num181;
						float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
						NPC.netUpdate = true;
						num183 = projectileSpeed / num183;
						num180 *= num183;
						num182 *= num183;
						int num184 = expertMode ? 25 : 30;
						int num185 = Mod.Find<ModProjectile>("BrimstoneHellblast").Type;
						shootFromVector.X += num180;
						shootFromVector.Y += num182;
						for (int num186 = 0; num186 < 6; num186++)
						{
							num180 = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
							num182 = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y;
							num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							num183 = 12f / num183;
							num180 += (float)Main.rand.Next(-90, 91);
							num182 += (float)Main.rand.Next(-90, 91);
							num180 *= num183;
							num182 *= num183;
							int randomTime = Main.rand.Next(200, 600);
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, num180, num182, num185, num184 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[projectile].timeLeft = randomTime;
							Main.projectile[projectile].tileCollide = false;
						}
						float spread = 45f * 0.0174f;
					   	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
					   	double deltaAngle = spread/8f;
					   	double offsetAngle;
					   	int damage = expertMode ? 25 : 30;
					   	int i;
					   	for (i = 0; i < 6; i++ )
					   	{
					   		int randomTime = Main.rand.Next(400, 700);
					   		offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					       	int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)( Math.Sin(offsetAngle) * 6f ), (float)( Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					       	int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)( -Math.Sin(offsetAngle) * 6f ), (float)( -Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					       	Main.projectile[projectile].timeLeft = randomTime;
					       	Main.projectile[projectile2].timeLeft = randomTime;
					   	}
					}
		       	}
				NPC.TargetClosest(true);
				NPC.ai[1] += 1f;
				NPC.velocity *= 0.95f;
				NPC.rotation = NPC.velocity.X * 0.04f;
				NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
				if (NPC.ai[1] > 300f)
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
		
		public override void FindFrame(int frameHeight) //9 total frames
		{
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] <= 2f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y >= frameHeight * 4)
				{
					NPC.frame.Y = 0;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 4)
				{
					NPC.frame.Y = frameHeight * 4;
				}
				if (NPC.frame.Y >= frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 4;
				}
			}
			else
			{
				if (NPC.frameCounter > 12.0)
				{
					NPC.frame.Y = NPC.frame.Y + frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 8)
				{
					NPC.frame.Y = frameHeight * 8;
				}
				if (NPC.frame.Y >= frameHeight * 12)
				{
					NPC.frame.Y = frameHeight * 8;
				}
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BrimstoneWaifuBag>()));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<RoseStone>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new ProvidenceDowned(), ModContent.ItemType<Bloodstone>(), 1, 20, 30));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofChaos>(), 1, 2, 3));
            LeadingConditionRule notExp = new LeadingConditionRule(new Conditions.NotExpert());
			notExp.OnSuccess(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Abaddon>(), ModContent.ItemType<Items.Weapons.BrimstoneWaifu.Brimlance>(), ModContent.ItemType<SeethingDischarge>() }));
			npcLoot.Add(notExp);
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeDrain, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 200;
				NPC.height = 150;
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
				for (int num623 = 0; num623 < 60; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
				float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore1").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore3").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore4").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore5").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore6").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("BrimstoneGore7").Type, 1f);
			}
		}
	}
}