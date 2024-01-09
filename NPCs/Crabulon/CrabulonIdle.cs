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
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Crabulon;
using CalamityModClassic1Point2.Items.Weapons.Crabulon;
using Terraria.UI;
using CalamityModClassic1Point2.Items.Placeables;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Crabulon
{
	[AutoloadBossHead]
	public class CrabulonIdle : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crabulon");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 8f;
			NPC.damage = 0;
			NPC.width = 164; //324
			NPC.height = 154; //216
			NPC.defense = 8;
			NPC.lifeMax = CalamityWorld.revenge ? 4500 : 3000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 4, 0, 0);
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.HitSound = SoundID.NPCHit45;
			NPC.DeathSound = SoundID.NPCDeath1;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("CrabulonBag").Type;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
                new FlavorTextBestiaryInfoElement("The Perfect One.")

            });
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 1f);
			Player player = Main.player[NPC.target];
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.noTileCollide = true;
					NPC.velocity = new Vector2(0f, 10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else
			{
				if (NPC.timeLeft > 1800)
				{
					NPC.timeLeft = 1800;
				}
			}
			if (NPC.ai[0] != 0f)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.localAI[1] += (float)Main.rand.Next(4);
					if (NPC.localAI[1] >= (float)Main.rand.Next(250, 301))
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						if (!Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height) || Main.rand.NextBool(5))
						{
							SoundEngine.PlaySound(SoundID.Item42, NPC.position);
							float num179 = 7f;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num184 = expertMode ? 11 : 13;
							int num185 = Mod.Find<ModProjectile>("MushBomb").Type;
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 10; num186++)
							{
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = 12f / num183;
								num180 += (float)Main.rand.Next(-180, 181);
								num182 += (float)Main.rand.Next(-180, 181);
								num180 *= num183;
								num182 *= num183;
								int mushroom = Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
		       	}
			}
			if (NPC.ai[0] == 0f)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					if (!player.dead && player.active && (player.Center - NPC.Center).Length() < 800f)
					{
						Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("Mushy").Type, 2);
					}
				}
				int sporeDust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueFairy, NPC.velocity.X, NPC.velocity.Y, 255, new Color(0, 80, 255, 80), NPC.scale * 1.2f);
				Main.dust[sporeDust].noGravity = true;
				Main.dust[sporeDust].velocity *= 0.5f;
				if (NPC.justHit)
				{
					NPC.ai[0] = 1f;
					NPC.boss = true;
					Music = MusicID.Boss4;
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.damage = 0;
				NPC.velocity.X *= 0.98f;
				NPC.velocity.Y *= 0.98f;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 60f)
				{
					NPC.noGravity = true;
					NPC.noTileCollide = true;
					NPC.ai[0] = 2f;
					NPC.ai[1] = 0f;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.damage = 30;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.66) 
				{
					NPC.damage = 40;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.33) 
				{
					NPC.damage = 50;
				}
				float num823 = 2f;
				bool flag51 = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5) 
				{
					num823 = 3f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.15) 
				{
					num823 = 4f;
				}
				if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 50f) 
				{
					flag51 = true;
				}
				if (flag51) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.9f;
					if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) 
					{
						NPC.velocity.X = 0f;
					}
				} 
				else 
				{
					if (NPC.direction > 0) 
					{
						NPC.velocity.X = (NPC.velocity.X * 20f + num823) / 21f;
					}
					if (NPC.direction < 0) 
					{
						NPC.velocity.X = (NPC.velocity.X * 20f - num823) / 21f;
					}
				}
				int num854 = 80;
				int num855 = 20;
				Vector2 position2 = new Vector2(NPC.Center.X - (float)(num854 / 2), NPC.position.Y + (float)NPC.height - (float)num855);
				bool flag52 = false;
				if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height - 16f) 
				{
					flag52 = true;
				}
				if (flag52) 
				{
					NPC.velocity.Y = NPC.velocity.Y + 0.5f;
				} 
				else if (Collision.SolidCollision(position2, num854, num855))
				{
					if (NPC.velocity.Y > 0f) 
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y > -0.2) 
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.025f;
					} 
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.2f;
					}
					if (NPC.velocity.Y < -4f) 
					{
						NPC.velocity.Y = -4f;
					}
				} 
				else 
				{
					if (NPC.velocity.Y < 0f) 
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y < 0.1) 
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.025f;
					} 
					else 
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.5f;
					}
				}
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 360f)
				{
					NPC.noGravity = false;
					NPC.noTileCollide = false;
					NPC.ai[0] = 3f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
				}
				if (NPC.velocity.Y > 10f) 
				{
					NPC.velocity.Y = 10f;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.damage = 45;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.66) 
				{
					NPC.damage = 55;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.33) 
				{
					NPC.damage = 70;
				}
				NPC.noTileCollide = false;
				if (NPC.velocity.Y == 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.8f;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] > 0f)
					{
						if (NPC.life < NPC.lifeMax) 
						{
							NPC.ai[1] += 1f;
						}
						if (NPC.life < NPC.lifeMax / 2) 
						{
							NPC.ai[1] += 4f;
						}
						if (NPC.life < NPC.lifeMax / 3) 
						{
							NPC.ai[1] += 8f;
						}
					}
					if (NPC.ai[1] >= 300f) 
					{
						NPC.ai[1] = -20f;
						NPC.frameCounter = 0.0;
					} 
					else if (NPC.ai[1] == -1f)
					{
						NPC.TargetClosest(true);
						NPC.velocity.X = (float)(4 * NPC.direction);
						NPC.velocity.Y = -12.1f;
						NPC.ai[0] = 4f;
						NPC.ai[1] = 0f;
					}
				}
			}
			else
			{
				if (NPC.velocity.Y == 0f) 
				{
					SoundEngine.PlaySound(SoundID.Item14, NPC.position);
					Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), value9.X, value9.Y + 100, 0f, 5f, Mod.Find<ModProjectile>("Mushmash").Type, 20, 0f, Main.myPlayer, 0f, 0f);
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 3f)
					{
						NPC.ai[0] = 1f;
						NPC.ai[2] = 0f;
					}
					else
					{
						NPC.ai[0] = 3f;
					}
					for (int num622 = (int)NPC.position.X - 20; num622 < (int)NPC.position.X + NPC.width + 40; num622 += 20) 
					{
						for (int num623 = 0; num623 < 4; num623++) 
						{
							int num624 = Dust.NewDust(new Vector2(NPC.position.X - 20f, NPC.position.Y + (float)NPC.height), NPC.width + 20, 4, DustID.BlueFairy, 0f, 0f, 100, default(Color), 1.5f);
							Main.dust[num624].velocity *= 0.2f;
						}
					}
				} 
				else 
				{
					NPC.TargetClosest(true);
					if (NPC.position.X < player.position.X && NPC.position.X + (float)NPC.width > player.position.X + (float)player.width) 
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
						float num626 = 4f;
						if (NPC.life < NPC.lifeMax / 2) 
						{
							num626 += 1f;
						}
						if (NPC.life < NPC.lifeMax / 4) 
						{
							num626 += 1f;
						}
						if (NPC.velocity.X < -num626) 
						{
							NPC.velocity.X = -num626;
						}
						if (NPC.velocity.X > num626) 
						{
							NPC.velocity.X = num626;
						}
					}
				}
			}
			if (NPC.localAI[0] == 0f && NPC.life > 0)
			{
				NPC.localAI[0] = (float)NPC.lifeMax;
			}
	       	if (NPC.life > 0)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num660 = (int)((double)NPC.lifeMax * 0.05);
					if ((float)(NPC.life + num660) < NPC.localAI[0])
					{
						NPC.localAI[0] = (float)NPC.life;
						int num661 = Main.rand.Next(2, 4);
						for (int num662 = 0; num662 < num661; num662++)
						{
							int x = (int)(NPC.position.X + (float)Main.rand.Next(NPC.width - 32));
							int y = (int)(NPC.position.Y + (float)Main.rand.Next(NPC.height - 32));
							int num663 = Mod.Find<ModNPC>("CrabShroom").Type;
							int num664 = NPC.NewNPC(NPC.GetSource_FromThis(), x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num664].SetDefaults(num663);
							Main.npc[num664].velocity.X = (float)Main.rand.Next(-50, 51) * 0.1f;
							Main.npc[num664].velocity.Y = (float)Main.rand.Next(-50, -31) * 0.1f;
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
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassic1Point2");
			Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Crabulon/CrabulonIdleAlt").Value;
			Texture2D textureAttack = ModContent.Request<Texture2D>("CalamityModClassic1Point2/NPCs/Crabulon/CrabulonAttack").Value;
			if (NPC.ai[0] > 2f)
			{
				CalamityModClassic1Point2.DrawTexture(spriteBatch, textureAttack, 0, NPC, drawColor);
			}
			else
			{
				CalamityModClassic1Point2.DrawTexture(spriteBatch, (NPC.ai[0] == 2f ? texture : TextureAssets.Npc[NPC.type].Value), 0, NPC, drawColor);
			}
			return false;
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CrabulonBag>()));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CrabulonTrophy>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.Crabulon.Mycoroot>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MycelialClaws>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<HyphaeRod>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Fungicide>(), 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.GlowingMushroom, 1, 20, 30));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.MushroomGrassSeeds, 1, 3, 6));
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BlueFairy, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 200;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BlueFairy, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BlueFairy, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BlueFairy, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}