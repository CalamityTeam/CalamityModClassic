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
using CalamityModClassic1Point2.Items.Providence;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.ProfanedGuardianBoss
{
	public class ProfanedGuardianBoss : ModNPC
	{
		public int flareTimer = 0; //projectile stuff
		public int flareProjectiles = 1;
		public int dustTimer = 3;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Profaned Guardian");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 10f;
			NPC.aiStyle = -1;
			NPC.damage = 130;
			NPC.width = 100; //324
			NPC.height = 80; //216
			NPC.defense = 88;
			NPC.lifeMax = 120000;
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
                new FlavorTextBestiaryInfoElement("Devotion.")

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
			CalamityGlobalNPC.doughnutBoss = NPC.whoAmI;
			bool fireBalls = (double)NPC.life <= (double)NPC.lifeMax * 0.75;
			bool powerBoost = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			bool fireDust = (double)NPC.life <= (double)NPC.lifeMax * 0.25;
			Player player = Main.player[NPC.target];
			Vector2 vector = NPC.Center;
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld.revenge;
			bool isHoly = player.ZoneHallow;
			bool isHell = player.ZoneUnderworldHeight;
			NPC.defense = (isHoly || isHell) ? 88 : 99999;
			Vector2 vectorCenter = NPC.Center;
			NPC.TargetClosest(false);
			if (!Main.dayTime)
			{
				NPC.TargetClosest(false);
				NPC.velocity = new Vector2(0f, -10f);
				if (NPC.timeLeft > 150)
				{
					NPC.timeLeft = 150;
				}
				return;
			}
			if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
			}
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if ((Main.npc[num569].active && Main.npc[num569].type == (Mod.Find<ModNPC>("ProfanedGuardianBoss2").Type)) || (Main.npc[num569].active && Main.npc[num569].type == Mod.Find<ModNPC>("ProfanedGuardianBoss3").Type))
				{
					flag100 = true;
				}
			}
			if (flag100)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
			if (Math.Sign(NPC.velocity.X) != 0) 
			{
				NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
			}
			NPC.spriteDirection = Math.Sign(NPC.velocity.X);
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1000 = powerBoost ? 16f : 20f;
			if (revenge)
			{
				num1000 *= 1.15f;
			}
			float num1001 = 5f;
			float scaleFactor4 = 0.8f;
			int num1002 = 0;
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1005 = powerBoost ? 16f : 20f;
			if (revenge)
			{
				num1005 *= 1.15f;
			}
			float num1006 = 0.333333343f;
			float num1007 = 8f;
			bool flag63 = false;
			num1006 *= num1005;
			int num1009 = (NPC.ai[0] == 2f) ? 2 : 1;
			int num1010 = (NPC.ai[0] == 2f) ? 80 : 60;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, DustID.CopperCoin, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.localAI[0] += expertMode ? 2f : 1f;
				if (NPC.localAI[0] >= 300f)
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float spread = 45f * 0.0174f;
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
				    	double deltaAngle = spread/8f;
				    	double offsetAngle;
				    	int damage = expertMode ? 55 : 60;
				    	int projectileShot = Mod.Find<ModProjectile>("ProfanedSpear").Type;
				    	int i;
				    	for (i = 0; i < 8; i++ )
				    	{
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
				        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
				    	}
					}
				}
			}
			if (fireBalls && flareTimer == 0)
			{
				flareTimer = 600;
			}
			if (flareTimer > 0)
			{
				flareTimer--;
				if (flareTimer == 0)
				{
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							SoundEngine.PlaySound(SoundID.Item20, NPC.position);
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 90f * 0.0174f;
					    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
					    	double deltaAngle = spread/8f;
					    	double offsetAngle;
					    	int damage = expertMode ? 50 : 57;
					    	int j;
					    	for (j = 0; j < flareProjectiles; j++ )
					    	{
					    		int randomTime = Main.rand.Next(60, 120);
					   			offsetAngle = (startAngle + deltaAngle * ( j + j * j ) / 2f ) + 32f * j;
					   			int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, 0f, 0f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					        	int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, 0f, 0f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					        	Main.projectile[projectile].timeLeft = randomTime;
					        	Main.projectile[projectile2].timeLeft = randomTime;
					        	Main.projectile[projectile].velocity.X = (float)Main.rand.Next(-50, 51) * 0.125f;
					        	Main.projectile[projectile].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.125f;
								Main.projectile[projectile2].velocity.X = (float)Main.rand.Next(-50, 51) * 0.125f;
								Main.projectile[projectile2].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.125f;
					    	}
						}
					}
				}
			}
			if (NPC.ai[0] == 0f) 
			{
				NPC.knockBackResist = 0f;
				float scaleFactor6 = num998;
				Vector2 center4 = NPC.Center;
				Vector2 center5 = Main.player[NPC.target].Center;
				Vector2 vector126 = center5 - center4;
				Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
				float num1013 = vector126.Length();
				vector126 = Vector2.Normalize(vector126) * scaleFactor6;
				vector127 = Vector2.Normalize(vector127) * scaleFactor6;
				bool flag64 = Collision.CanHit(NPC.Center, 1, 1, Main.player[NPC.target].Center, 1, 1);
				if (NPC.ai[3] >= 120f) 
				{
					flag64 = true;
				}
				float num1014 = 8f;
				flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
				if (num1013 > num999 || !flag64) 
				{
					NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / num1000;
					NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / num1000;
					if (!flag64) 
					{
						NPC.ai[3] += 1f;
						if (NPC.ai[3] == 120f) 
						{
							NPC.netUpdate = true;
						}
					} 
					else
					{
						NPC.ai[3] = 0f;
					}
				} 
				else 
				{
					NPC.ai[0] = 1f;
					NPC.ai[2] = vector126.X;
					NPC.ai[3] = vector126.Y;
					NPC.netUpdate = true;
				}
			} 
			else if (NPC.ai[0] == 1f) 
			{
				NPC.knockBackResist = 0f;
				NPC.velocity *= scaleFactor4;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= num1001) 
				{
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					Vector2 velocity = new Vector2(NPC.ai[2], NPC.ai[3]) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
					velocity.Normalize();
					velocity *= scaleFactor5;
					NPC.velocity = velocity;
				}
			} 
			else if (NPC.ai[0] == 2f) 
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					dustTimer--;
					if (fireDust && dustTimer <= 0)
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						int damage = expertMode ? 50 : 57;
						Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)vector173.X, (int)vector173.Y, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = 120;
						Main.projectile[projectile].velocity.X = 0f;
				        Main.projectile[projectile].velocity.Y = 0f;
			       		dustTimer = 3;
					}
				}
				NPC.knockBackResist = 0f;
				float num1016 = num1003;
				NPC.ai[1] += 1f;
				bool flag65 = Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > num1004 && NPC.Center.Y > Main.player[NPC.target].Center.Y;
				if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.velocity /= 2f;
					NPC.netUpdate = true;
					NPC.ai[1] = 45f;
					NPC.ai[0] = 4f;
				} 
				else 
				{
					Vector2 center6 = NPC.Center;
					Vector2 center7 = Main.player[NPC.target].Center;
					Vector2 vec2 = center7 - center6;
					vec2.Normalize();
					if (vec2.HasNaNs()) 
					{
						vec2 = new Vector2((float)NPC.direction, 0f);
					}
					NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / num1005;
				}
				if (flag63 && Collision.SolidCollision(NPC.position, NPC.width, NPC.height)) 
				{
					NPC.ai[0] = 3f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
			} 
			else if (NPC.ai[0] == 4f) 
			{
				NPC.ai[1] -= 3f;
				if (NPC.ai[1] <= 0f) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
				NPC.velocity *= 0.95f;
			}
			if (flag63 && NPC.ai[0] != 3f && Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) < 64f) 
			{
				NPC.ai[0] = 3f;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] == 3f) 
			{
				float damageMult = revenge ? 150f : 100f;
				NPC.position = NPC.Center;
				NPC.width = (NPC.height = 192);
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				NPC.velocity = Vector2.Zero;
				NPC.damage = (int)(damageMult * Main.GameModeInfo.EnemyDamageMultiplier);
				NPC.alpha = 255;
				Lighting.AddLight((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16, 2f, 0.75f, 0f);
				for (int num1017 = 0; num1017 < 10; num1017++) 
				{
					int num1018 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num1018].velocity *= 1.4f;
					Main.dust[num1018].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
				}
				for (int num1019 = 0; num1019 < 40; num1019++) 
				{
					int num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[num1020].noGravity = true;
					Main.dust[num1020].velocity *= 2f;
					Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
					Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
					if (Main.rand.NextBool(2)) 
					{
						num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 0.9f);
						Main.dust[num1020].noGravity = true;
						Main.dust[num1020].velocity *= 1.2f;
						Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
						Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
					}
					if (Main.rand.NextBool(4)) 
					{
						num1020 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 0.7f);
						Main.dust[num1020].velocity *= 1.2f;
						Main.dust[num1020].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + NPC.Center;
						Main.dust[num1020].velocity = Main.dust[num1020].velocity / 2f + Vector2.Normalize(Main.dust[num1020].position - NPC.Center);
					}
				}
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 3f) 
				{
					SoundEngine.PlaySound(SoundID.Item14, NPC.position);
					NPC.life = 0;
					NPC.HitEffect(0, 10.0);
					NPC.active = false;
					return;
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<ProfanedCore>(), 1));
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.OnFire, 600, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
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