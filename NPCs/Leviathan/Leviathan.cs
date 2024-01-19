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
using CalamityModClassic1Point2.Projectiles;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Leviathan;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Weapons.Leviathan;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Leviathan
{
	[AutoloadBossHead]
	public class Leviathan : ModNPC
	{
		public int oneTime = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Leviathan");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 10f;
			NPC.damage = 80;
			NPC.width = 450;
			NPC.height = 250;
			NPC.scale = 2f;
			NPC.defense = 45;
			NPC.lifeMax = CalamityWorld.revenge ? 55000 : 42000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 30, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.HitSound = SoundID.NPCHit56;
			NPC.DeathSound = SoundID.NPCDeath60;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
			NPC.boss = true;
			NPC.netAlways = true;
			NPC.chaseable = false;
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/LeviathanAndSiren");
			NPC.timeLeft = NPC.activeTime * 30;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("LeviathanBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("The apex predator of the ocean. Perhaps the last female of her kind, she and her trusted Siren are masters at hunting.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			Vector2 vector = NPC.Center;
			Player player = Main.player[NPC.target];
			bool playerWet = player.wet;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			int npcType = Mod.Find<ModNPC>("Siren").Type;
			bool sirenAlive = false;
			if (NPC.CountNPCS(npcType) > 0)
			{
				sirenAlive = true;
			}
			if (oneTime == 0)
			{
				RainStart();
				oneTime++;
			}
			int soundChoicer = Main.rand.Next(3);
			SoundStyle soundChoiceRage = SoundID.Zombie92;
			SoundStyle soundChoice = SoundID.Zombie38;
			if (soundChoicer == 0)
			{
				soundChoice = SoundID.Zombie38;
			}
			else if (soundChoicer == 1)
			{
				soundChoice = SoundID.Zombie39;
			}
			else
			{
				soundChoice = SoundID.Zombie40;
			}
			if (Main.rand.NextBool(600))
			{
				SoundEngine.PlaySound((sirenAlive ? soundChoice : soundChoiceRage), NPC.position);
			}
			bool flag6 = player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400));
			if (flag6 || !sirenAlive)
			{
				NPC.damage = NPC.defDamage * 2;
				NPC.defense = NPC.defDefense * 2;
			}
			NPC.dontTakeDamage = flag6;
			int num1038 = 0;
			for (int num1039 = 0; num1039 < 255; num1039++)
			{
				if (Main.player[num1039].active && !Main.player[num1039].dead && (NPC.Center - Main.player[num1039].Center).Length() < 1000f)
				{
					num1038++;
				}
			}
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			if (!player.active || player.dead || Vector2.Distance(player.Center, vector) > 5600f)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead || Vector2.Distance(player.Center, vector) > 5600f)
				{
					NPC.velocity = new Vector2(0f, 10f);
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
					return;
				}
			}
			else if (NPC.ai[0] == 0f) 
			{
				if (NPC.ai[1] == 0f) 
				{
					NPC.TargetClosest(true);
					float num412 = sirenAlive ? 4f : 9f;
					float num413 = sirenAlive ? 0.2f : 0.35f;
					int num414 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width) 
					{
						num414 = -1;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = player.position.X + (float)(player.width / 2) + (float)(num414 * 800) - vector40.X;
					float num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
					float num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
					num417 = num412 / num417;
					num415 *= num417;
					num416 *= num417;
					if (NPC.velocity.X < num415) 
					{
						NPC.velocity.X = NPC.velocity.X + num413;
						if (NPC.velocity.X < 0f && num415 > 0f) 
						{
							NPC.velocity.X = NPC.velocity.X + num413;
						}
					} 
					else if (NPC.velocity.X > num415) 
					{
						NPC.velocity.X = NPC.velocity.X - num413;
						if (NPC.velocity.X > 0f && num415 < 0f) 
						{
							NPC.velocity.X = NPC.velocity.X - num413;
						}
					}
					if (NPC.velocity.Y < num416) 
					{
						NPC.velocity.Y = NPC.velocity.Y + num413;
						if (NPC.velocity.Y < 0f && num416 > 0f) 
						{
							NPC.velocity.Y = NPC.velocity.Y + num413;
						}
					} 
					else if (NPC.velocity.Y > num416) 
					{
						NPC.velocity.Y = NPC.velocity.Y - num413;
						if (NPC.velocity.Y > 0f && num416 < 0f) 
						{
							NPC.velocity.Y = NPC.velocity.Y - num413;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 500f) 
					{
						NPC.ai[1] = 1f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.target = 255;
						NPC.netUpdate = true;
					} 
					else 
					{
						if (!player.dead) 
						{
							NPC.ai[3] += 1f;
							if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
							{
								NPC.ai[3] += 1f;
							}
							if (revenge)
							{
								NPC.ai[3] += 1f;
							}
							if (!sirenAlive)
							{
								NPC.ai[3] += 2f;
							}
							else
							{
								if (!playerWet)
								{
									NPC.ai[3] += 0.5f;
								}
								if (Siren.phase2)
								{
									NPC.ai[3] += 0.5f;
								}
								if (Siren.phase3)
								{
									NPC.ai[3] += 0.5f;
								}
							}
						}
						if (NPC.ai[3] >= 120f) 
						{
							NPC.ai[3] = 0f;
							vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num415 = player.position.X + (float)(player.width / 2) - vector40.X;
							num416 = player.position.Y + (float)(player.height / 2) - vector40.Y;
							if (Main.netMode != NetmodeID.MultiplayerClient) 
							{
								float num418 = sirenAlive ? 13.5f : 16f;
								int num419 = playerWet ? 50 : 55;
								int num420 = Mod.Find<ModProjectile>("LeviathanBomb").Type;
								if (expertMode)
								{
									num418 = sirenAlive ? 14f : 17f;
									num419 = playerWet ? 30 : 35;
								}
								num417 = (float)Math.Sqrt((double)(num415 * num415 + num416 * num416));
								num417 = num418 / num417;
								num415 *= num417;
								num416 *= num417;
								num415 += (float)Main.rand.Next(-10, 11) * 0.05f;
								num416 += (float)Main.rand.Next(-10, 11) * 0.05f;
								vector40.X += num415 * 4f;
								vector40.Y += num416 * 4f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector40.X, vector40.Y, num415, num416, num420, num419 + (sirenAlive ? 10 : 0), 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				} 
				else if (NPC.ai[1] == 1f)
				{
					NPC.TargetClosest(true);
					Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
					Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
					float num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
					float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
					NPC.ai[2] += 1f;
					NPC.ai[2] += (float)(num1038 / 2);
					if (revenge)
					{
						NPC.ai[2] += 1f;
					}
					if (!sirenAlive)
					{
						NPC.ai[2] += 2f;
					}
					else
					{
						if (Siren.phase2)
						{
							NPC.ai[2] += 0.5f;
						}
						if (Siren.phase3)
						{
							NPC.ai[2] += 0.5f;
						}
					}
					bool flag103 = false;
					int spawnLimit = sirenAlive ? 2 : 4;
					int spawnLimit2 = sirenAlive ? 3 : 6;
					if (NPC.ai[2] > 80f) //changed from 40 not a prob
					{
						NPC.ai[2] = 0f;
						NPC.ai[3] += 1f;
						flag103 = true;
					}
					if (Collision.CanHit(vector119, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && flag103)
					{
						SoundEngine.PlaySound(soundChoice, NPC.position);
						if (Main.netMode != NetmodeID.MultiplayerClient && NPC.CountNPCS(Mod.Find<ModNPC>("Parasea").Type) < spawnLimit2 && NPC.CountNPCS(Mod.Find<ModNPC>("AquaticAberration").Type) < spawnLimit)
						{
							int num1061;
							if (Main.rand.NextBool(4))
							{
								num1061 = Mod.Find<ModNPC>("AquaticAberration").Type;
							}
							else
							{
								num1061 = Mod.Find<ModNPC>("Parasea").Type;
							}
							int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.01f;
							Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.01f;
							Main.npc[num1062].localAI[0] = 60f;
							Main.npc[num1062].netUpdate = true;
						}
					}
					if (num1060 > 400f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num1063 = sirenAlive ? 7f : 8f; //changed from 14 not a prob
						float num1064 = sirenAlive ? 0.05f : 0.065f; //changed from 0.1 not a prob
						vector120 = vector119;
						num1058 = player.position.X + (float)(player.width / 2) - vector120.X;
						num1059 = player.position.Y + (float)(player.height / 2) - vector120.Y;
						num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
						num1060 = num1063 / num1060;
						if (NPC.velocity.X < num1058)
						{
							NPC.velocity.X = NPC.velocity.X + num1064;
							if (NPC.velocity.X < 0f && num1058 > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num1064;
							}
						}
						else if (NPC.velocity.X > num1058)
						{
							NPC.velocity.X = NPC.velocity.X - num1064;
							if (NPC.velocity.X > 0f && num1058 < 0f)
							{
								NPC.velocity.X = NPC.velocity.X - num1064;
							}
						}
						if (NPC.velocity.Y < num1059)
						{
							NPC.velocity.Y = NPC.velocity.Y + num1064;
							if (NPC.velocity.Y < 0f && num1059 > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num1064;
							}
						}
						else if (NPC.velocity.Y > num1059)
						{
							NPC.velocity.Y = NPC.velocity.Y - num1064;
							if (NPC.velocity.Y > 0f && num1059 < 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y - num1064;
							}
						}
					}
					else
					{
						NPC.velocity *= 0.9f;
					}
					NPC.spriteDirection = NPC.direction;
					if (NPC.ai[3] > 3f)
					{
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
						return;
					}
				}
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
				float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("Leviathangib1").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("Leviathangib2").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("Leviathangib3").Type, 1f);
			}
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule noLevi = new LeadingConditionRule(new NoSiren());
            LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
            LeadingConditionRule expert = new LeadingConditionRule(new Conditions.IsExpert());
            noLevi.OnSuccess(new CommonDrop(ModContent.ItemType<EnchantedPearl>(), 1));
            noLevi.OnSuccess(ItemDropRule.ByCondition(new NotDownedPlantera(), ModContent.ItemType<IOU>(), 1));
            noLevi.OnSuccess(new CommonDrop(ModContent.ItemType<LeviathanTrophy>(), 10));
            noLevi.OnSuccess(expert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<LeviathanBag>()));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<LeviathanMask>(), 7));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Atlantis>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Greentide>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Items.Weapons.Leviathan.BrackishFlask>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Items.Weapons.Leviathan.SirensSong>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<LureofEnthrallment>(), 4));
            noLevi.OnSuccess(notExpert).OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ModContent.ItemType<Leviatitan>(), 4));
            npcLoot.Add(noLevi);
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
				if (Main.rand.NextBool(3))
				{
					Main.rainTime += Main.rand.Next(0, num2);
				}
				if (Main.rand.NextBool(4))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 2);
				}
				if (Main.rand.NextBool(5))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 2);
				}
				if (Main.rand.NextBool(6))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 3);
				}
				if (Main.rand.NextBool(7))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 4);
				}
				if (Main.rand.NextBool(8))
				{
					Main.rainTime += Main.rand.Next(0, num2 * 5);
				}
				float num3 = 1f;
				if (Main.rand.NextBool(2))
				{
					num3 += 0.05f;
				}
				if (Main.rand.NextBool(3))
				{
					num3 += 0.1f;
				}
				if (Main.rand.NextBool(4))
				{
					num3 += 0.15f;
				}
				if (Main.rand.NextBool(5))
				{
					num3 += 0.2f;
				}
				Main.rainTime = (int)((float)Main.rainTime * num3);
				Main.raining = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
			Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Leviathan/LeviathanAlt").Value;
			CalamityModClassic1Point2.DrawTexture(spriteBatch, (NPC.ai[1] != 1f ? texture : TextureAssets.Npc[NPC.type].Value), 0, NPC, drawColor);
			return false;
		}
		
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 6)
			{
				NPC.frame.Y = 0;
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
	}
}