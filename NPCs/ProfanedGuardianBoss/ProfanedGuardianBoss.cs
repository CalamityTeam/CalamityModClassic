using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.ProfanedGuardianBoss
{
    [AutoloadBossHead]
    public class ProfanedGuardianBoss : ModNPC
	{
		private int flareTimer = 0;
		private int flareProjectiles = 1;
		private int dustTimer = 3;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Profaned Guardian");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				PortraitPositionXOverride = 0,
				PortraitScale = 0.75f,
				Scale = 0.75f
			};
			value.Position.X += 25;
			value.Position.Y += 15;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				new FlavorTextBestiaryInfoElement("mr. donut one, he stabs real good")
			});
		}

		
		public override void SetDefaults()
		{
			NPC.npcSlots = 20f;
			NPC.aiStyle = -1;
			NPC.damage = 100;
			NPC.width = 100; //324
			NPC.height = 80; //216
			NPC.defense = 50;
			NPC.lifeMax = 102500;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = CalamityWorldPreTrailer.death ? 600000 : 500000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = -1;
			NPC.boss = true;
            Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Guardians");
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
            }
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.value = Item.buyPrice(0, 25, 0, 0);
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
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
			if (Main.netMode != 1 && NPC.localAI[1] == 0f)
			{
				NPC.localAI[1] = 1f;
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("ProfanedGuardianBoss2").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector.X, (int)vector.Y, Mod.Find<ModNPC>("ProfanedGuardianBoss3").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorldPreTrailer.revenge;
			bool isHoly = player.ZoneHallow;
			bool isHell = player.ZoneUnderworldHeight;
			NPC.defense = (isHoly || isHell || CalamityWorldPreTrailer.bossRushActive) ? 50 : 99999;
			Vector2 vectorCenter = NPC.Center;
			NPC.TargetClosest(false);
			if (!Main.dayTime || !player.active || player.dead)
			{
				NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!Main.dayTime || !player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, -10f);
                    if (NPC.timeLeft > 150)
                    {
                        NPC.timeLeft = 150;
                    }
                    return;
                }
			}
			if (NPC.timeLeft < 1800)
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
			float num1000 = powerBoost ? 14f : 16f;
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
			float num1005 = powerBoost ? 14f : 16f;
			if (revenge)
			{
				num1005 *= 1.15f;
			}
			float num1006 = 0.333333343f;
			float num1007 = 8f;
			num1006 *= num1005;
			int num1009 = (NPC.ai[0] == 2f) ? 2 : 1;
			int num1010 = (NPC.ai[0] == 2f) ? 80 : 60;
			for (int num1011 = 0; num1011 < 2; num1011++) 
			{
				if (Main.rand.Next(3) < num1009) 
				{
					int num1012 = Dust.NewDust(NPC.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, 244, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
					Main.dust[num1012].noGravity = true;
					Main.dust[num1012].velocity *= 0.2f;
					Main.dust[num1012].fadeIn = 1f;
				}
			}
			if (Main.netMode != 1)
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
				    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
				    	double deltaAngle = spread / 8f;
				    	double offsetAngle;
				    	int damage = expertMode ? 33 : 48;
				    	int projectileShot = Mod.Find<ModProjectile>("ProfanedSpear").Type;
				    	int i;
				    	for (i = 0; i < 8; i++)
				    	{
				   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				        	Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
				        	Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), projectileShot, damage, 0f, Main.myPlayer, 0f, 0f);
				    	}
					}
				}
			}
			if (fireBalls && flareTimer == 0)
			{
				flareTimer = 420;
			}
			if (flareTimer > 0)
			{
				flareTimer--;
				if (flareTimer == 0)
				{
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						if (Main.netMode != 1)
						{
							SoundEngine.PlaySound(SoundID.Item20, NPC.position);
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 90f * 0.0174f;
					    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
					    	double deltaAngle = spread / 8f;
					    	double offsetAngle;
					    	int damage = expertMode ? 34 : 50;
					    	int j;
					    	for (j = 0; j < flareProjectiles; j++)
					    	{
					    		int randomTime = Main.rand.Next(60, 120);
					   			offsetAngle = (startAngle + deltaAngle * ( j + j * j ) / 2f ) + 32f * j;
					   			int projectile = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, 0f, 0f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
					        	int projectile2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, 0f, 0f, Mod.Find<ModProjectile>("GigaFlare").Type, damage, 0f, Main.myPlayer, 1f, (float)(NPC.target + 1));
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
			NPC.damage = expertMode ? 200 : 100;
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
				if (Main.netMode != 1)
				{
					dustTimer--;
					if (fireDust && dustTimer <= 0)
					{
						SoundEngine.PlaySound(SoundID.Item20, NPC.position);
						int damage = expertMode ? 38 : 56;
						Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(NPC.width + 20) / 2f + vectorCenter;
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(null), (int)vector173.X, (int)vector173.Y, (float)(NPC.direction * 2), 4f, Mod.Find<ModProjectile>("FlareDust").Type, damage, 0f, Main.myPlayer, 0f, 0f); //changed
						Main.projectile[projectile].timeLeft = 120;
						Main.projectile[projectile].velocity.X = 0f;
				        Main.projectile[projectile].velocity.Y = 0f;
			       		dustTimer = 3;
					}
				}
				NPC.damage = expertMode ? 240 : 120;
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
					NPC.ai[0] = 3f;
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
			} 
			else if (NPC.ai[0] == 3f) 
			{
				NPC.ai[1] -= 1f;
				if (NPC.ai[1] <= 0f) 
				{
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
				}
				NPC.velocity *= 0.98f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Microsoft.Xna.Framework.Color color24 = NPC.GetAlpha(drawColor);
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			Texture2D texture2D3 = TextureAssets.Npc[NPC.type].Value;
			int num156 = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
			int y3 = num156 * (int)NPC.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture2D3.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num157 = 8;
			int num158 = 2;
			int num159 = 1;
			float num160 = 0f;
			int num161 = num159;
			spriteBatch.Draw(texture2D3, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, spriteEffects, 0);
			while (NPC.ai[0] == 2f && Lighting.NotRetro && ((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)))
			{
				Microsoft.Xna.Framework.Color color26 = NPC.GetAlpha(color25);
				{
					goto IL_6899;
				}
				IL_6881:
				num161 += num158;
				continue;
				IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				color26 *= num164 / ((float)NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f);
				Vector2 value4 = (NPC.oldPos[num161]);
				float num165 = NPC.rotation;
				Main.spriteBatch.Draw(texture2D3, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, spriteEffects, 0f);
				goto IL_6881;
			}
			return false;
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
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("ProfanedCore").Type, 1));
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.OnFire, 600, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ProfanedGuardianBossA").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ProfanedGuardianBossA2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ProfanedGuardianBossA3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ProfanedGuardianBossA4").Type, 1f);
				}
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}