using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.HiveMind;
using CalamityModClassic1Point2.Items.Placeables;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Weapons.HiveMind;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.HiveMind
{
	[AutoloadBossHead]
	public class HiveMindP2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Hive Mind");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 5f;
			NPC.damage = 30;
			NPC.width = 230; //324
			NPC.height = 180; //216
			NPC.defense = 5;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 3000 : 2000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 5, 0, 0);
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss3;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("HiveMindBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("The mastermind of the corruption.")

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
			Player player = Main.player[NPC.target];
			bool isCorrupt = player.ZoneCorrupt;
			NPC.dontTakeDamage = isCorrupt ? false : true;
			bool expertMode = Main.expertMode;
			bool revenge = CalamityWorld1Point2.revenge;
			CalamityGlobalNPC1Point2.hiveMind2 = NPC.whoAmI;
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (NPC.localAI[2] == 0f) 
				{
					NPC.localAI[2] = 1f;
					for (int num723 = 0; num723 < 5; num723++) 
					{
						NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("HiveBlob2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
					}
				}
				NPC.localAI[0] += (float)Main.rand.Next(4);
				if (NPC.localAI[0] >= (float)Main.rand.Next(500, 600))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Main.rand.NextBool(2))
					{
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							float num179 = (float)Main.rand.Next(4, 9);
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num184 = expertMode ? 11 : 14;
							int num185 = Mod.Find<ModProjectile>("VileClot").Type;
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 12; num186++)
							{
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = 12f / num183;
								num180 += (float)Main.rand.Next(-100, 101);
								num182 += (float)Main.rand.Next(-100, 101);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
					else
					{
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float spread = 45f * 0.0174f;
					    	double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y)- spread/2;
					    	double deltaAngle = spread/8f;
					    	double offsetAngle;
					    	int i;
					    	int num184 = expertMode ? 13 : 16;
					    	for (i = 0; i < 6; i++ )
					    	{
					   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("VileClot").Type, num184, 0f, Main.myPlayer, 0f, 0f);
					        	Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("VileClot").Type, num184, 0f, Main.myPlayer, 0f, 0f);
					    	}
						}
					}
				}
	       	}
			NPC.TargetClosest(true);
			Vector2 vector142 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num1243 = player.Center.X - vector142.X;
			float num1244 = player.Center.Y - vector142.Y;
			float num1245 = (float)Math.Sqrt((double)(num1243 * num1243 + num1244 * num1244));
			float speed = isCorrupt ? 2.5f : 3f;
			if (revenge)
			{
				speed = isCorrupt ? 4f : 5f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.9f) && NPC.life >= (NPC.lifeMax * 0.6f))
			{
				speed = isCorrupt ? 2.75f : 3.25f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.6f) && NPC.life >= (NPC.lifeMax * 0.3f))
			{
				speed = isCorrupt ? 3f : 3.5f;
			}
			else if (NPC.life < (NPC.lifeMax * 0.3f))
			{
				speed = isCorrupt ? 3.5f : 4f;
			}
			if (num1245 < speed)
			{
				NPC.velocity.X = num1243;
				NPC.velocity.Y = num1244;
			}
			else
			{
				num1245 = speed / num1245;
				NPC.velocity.X = num1243 * num1245;
				NPC.velocity.Y = num1244 * num1245;
			}
			if (NPC.ai[0] == 0f)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += 1f;
					if (NPC.localAI[1] >= (float)(100 + Main.rand.Next(100)))
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						int num1249 = 0;
						int num1250;
						int num1251;
						while (true)
						{
							num1249++;
							num1250 = (int)player.Center.X / 16;
							num1251 = (int)player.Center.Y / 16;
							num1250 += Main.rand.Next(-50, 51);
							num1251 += Main.rand.Next(-50, 51);
							if (!WorldGen.SolidTile(num1250, num1251) && Collision.CanHit(new Vector2((float)(num1250 * 16), (float)(num1251 * 16)), 1, 1, player.position, player.width, player.height))
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
			if (player.dead) 
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
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < hit.Damage / NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("DarkHeart").Type) < 2)
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("DarkHeart").Type);
				if (Main.netMode == NetmodeID.Server && spawn < 200)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, spawn, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("HiveBlob2").Type) < 16)
			{
				if (Main.rand.NextBool(15))
				{
					Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
					int spawn = NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("HiveBlob2").Type);
					if (Main.netMode == NetmodeID.Server && spawn < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, spawn, 0f, 0f, 0f, 0, 0, 0);
					}
				}
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
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Demonite, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Demonite, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Demonite, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<HiveMindTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<HiveMindBag>()));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HiveMindMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<TrueShadowScale>(), 1, 25, 30));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.DemoniteBar, 1, 7, 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.RottenChunk, 1, 9, 15));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ShaderainStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LeechingDagger>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ShadowdropStaff>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PerfectDark>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Shadethrower>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RotBall>(), 4, 25, 50));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DankStaff>(), 4));
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}