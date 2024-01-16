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
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class CalamitasRun2 : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "InjuredCalamitas2");
			//Tooltip.SetDefault("Catastrophe");
			NPC.damage = 80;
			NPC.npcSlots = 5f;
			NPC.width = 100; //324
			NPC.height = 110; //216
			NPC.defense = 35;
			AnimationType = 126;
			NPC.alpha = 50;
			NPC.lifeMax = 5000;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			Main.npcFrameCount[NPC.type] = 3;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.boss = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.timeLeft = NPC.activeTime * 30;
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TerrariaBoss2");
		}


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Like really, are they robots? Clones of Calamitas' brothers? Weird mock shades made by the Calamitas Clone?")

            });
        }

        public override void AI()
		{
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			bool dead3 = Main.player[NPC.target].dead;
			float num840 = NPC.position.X + (float)(NPC.width / 2) - Main.player[NPC.target].position.X - (float)(Main.player[NPC.target].width / 2);
			float num841 = NPC.position.Y + (float)NPC.height - 59f - Main.player[NPC.target].position.Y - (float)(Main.player[NPC.target].height / 2);
			float num842 = (float)Math.Atan2((double)num841, (double)num840) + 1.57f;
			if (num842 < 0f)
			{
				num842 += 6.283f;
			}
			else if ((double)num842 > 6.283)
			{
				num842 -= 6.283f;
			}
			float num843 = 0.15f;
			if (NPC.rotation < num842)
			{
				if ((double)(num842 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num843;
				}
				else
				{
					NPC.rotation += num843;
				}
			}
			else if (NPC.rotation > num842)
			{
				if ((double)(NPC.rotation - num842) > 3.1415)
				{
					NPC.rotation += num843;
				}
				else
				{
					NPC.rotation -= num843;
				}
			}
			if (NPC.rotation > num842 - num843 && NPC.rotation < num842 + num843)
			{
				NPC.rotation = num842;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num842 - num843 && NPC.rotation < num842 + num843)
			{
				NPC.rotation = num842;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num844 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), 235, NPC.velocity.X, 2f, 0, default(Color), 1f);
				Dust expr_3133D_cp_0 = Main.dust[num844];
				expr_3133D_cp_0.velocity.X = expr_3133D_cp_0.velocity.X * 0.5f;
				Dust expr_3135D_cp_0 = Main.dust[num844];
				expr_3135D_cp_0.velocity.Y = expr_3135D_cp_0.velocity.Y * 0.1f;
			}
			if (Main.netMode != 1 && !dead3 && NPC.timeLeft < 10)
			{
				for (int num845 = 0; num845 < 200; num845++)
				{
					if (num845 != NPC.whoAmI && Main.npc[num845].active && Main.npc[num845].timeLeft - 1 > NPC.timeLeft)
					{
						NPC.timeLeft = Main.npc[num845].timeLeft - 1;
					}
				}
			}
			if (dead3)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 2f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 2f;
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
			else
			{
				if (NPC.ai[1] == 0f)
				{
					float num861 = 4.5f;
					float num862 = 0.15f;
					int num863 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
					{
						num863 = -1;
					}
					Vector2 vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num863 * 180) - vector86.X;
					float num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
					float num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
					if (Main.expertMode)
					{
						if (num866 > 300f)
						{
							num861 += 0.5f;
						}
						if (num866 > 400f)
						{
							num861 += 0.5f;
						}
						if (num866 > 500f)
						{
							num861 += 0.55f;
						}
						if (num866 > 600f)
						{
							num861 += 0.55f;
						}
						if (num866 > 700f)
						{
							num861 += 0.6f;
						}
						if (num866 > 800f)
						{
							num861 += 0.6f;
						}
					}
					num866 = num861 / num866;
					num864 *= num866;
					num865 *= num866;
					if (NPC.velocity.X < num864)
					{
						NPC.velocity.X = NPC.velocity.X + num862;
						if (NPC.velocity.X < 0f && num864 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num862;
						}
					}
					else if (NPC.velocity.X > num864)
					{
						NPC.velocity.X = NPC.velocity.X - num862;
						if (NPC.velocity.X > 0f && num864 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num862;
						}
					}
					if (NPC.velocity.Y < num865)
					{
						NPC.velocity.Y = NPC.velocity.Y + num862;
						if (NPC.velocity.Y < 0f && num865 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num862;
						}
					}
					else if (NPC.velocity.Y > num865)
					{
						NPC.velocity.Y = NPC.velocity.Y - num862;
						if (NPC.velocity.Y > 0f && num865 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num862;
						}
					}
					NPC.ai[2] += 1f;
					if (NPC.ai[2] >= 400f)
					{
						NPC.ai[1] = 1f;
						NPC.ai[2] = 0f;
						NPC.ai[3] = 0f;
						NPC.target = 255;
						NPC.netUpdate = true;
					}
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						NPC.localAI[2] += 1f;
						if (NPC.localAI[2] > 22f)
						{
							NPC.localAI[2] = 0f;
							SoundEngine.PlaySound(SoundID.Item34, NPC.position);
						}
						if (Main.netMode != 1)
						{
							NPC.localAI[1] += 1f;
							if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
							{
								NPC.localAI[1] += 1f;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
							{
								NPC.localAI[1] += 1f;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
							{
								NPC.localAI[1] += 1f;
							}
							if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
							{
								NPC.localAI[1] += 1f;
							}
							if (CalamityGlobalNPC.bossBuff && CalamityGlobalNPC.superBossBuff)
							{
								NPC.localAI[1] += 2f;
							}
							if (NPC.localAI[1] > 50f)
							{
								NPC.localAI[1] = 0f;
								float num867 = 6f;
								int num868 = 26;
								if (Main.expertMode)
								{
									num868 = 15;
								}
								if (Main.dayTime)
								{
									num868 = 27;
								}
								if (NPC.downedMoonlord)
								{
									num868 = 35;
									if (Main.expertMode)
									{
										num868 = 19;
									}
								}
								int num869 = Mod.Find<ModProjectile>("BrimstoneBall").Type;
								vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								num864 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector86.X;
								num865 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector86.Y;
								num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
								num866 = num867 / num866;
								num864 *= num866;
								num865 *= num866;
								num865 += (float)Main.rand.Next(-40, 41) * 0.01f;
								num864 += (float)Main.rand.Next(-40, 41) * 0.01f;
								num865 += NPC.velocity.Y * 0.5f;
								num864 += NPC.velocity.X * 0.5f;
								vector86.X -= num864 * 1f;
								vector86.Y -= num865 * 1f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(), vector86.X, vector86.Y, num864, num865, num869, num868, 0f, Main.myPlayer, 0f, 0f);
								return;
							}
						}
					}
				}
				else
				{
					if (NPC.ai[1] == 1f)
					{
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
						NPC.rotation = num842;
						float num870 = 16f;
						if (Main.expertMode)
						{
							num870 += 2.5f;
						}
						if (Main.dayTime)
						{
							num870 += 2f;
						}
						if (NPC.downedMoonlord)
						{
							num870 += 2f;
						}
						Vector2 vector87 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num871 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector87.X;
						float num872 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector87.Y;
						float num873 = (float)Math.Sqrt((double)(num871 * num871 + num872 * num872));
						num873 = num870 / num873;
						NPC.velocity.X = num871 * num873;
						NPC.velocity.Y = num872 * num873;
						NPC.ai[1] = 2f;
						return;
					}
					if (NPC.ai[1] == 2f)
					{
						NPC.ai[2] += 1f;
						if (Main.expertMode)
						{
							NPC.ai[2] += 0.5f;
						}
						if (NPC.downedMoonlord)
						{
							NPC.ai[2] += 0.5f;
						}
						if (NPC.life <= (NPC.lifeMax * 0.75f))
						{
							NPC.ai[2] += 0.1f;
						}
						if (NPC.life <= (NPC.lifeMax * 0.5f))
						{
							NPC.ai[2] += 0.15f;
						}
						if (NPC.life <= (NPC.lifeMax * 0.25f))
						{
							NPC.ai[2] += 0.2f;
						}
						if (NPC.life <= (NPC.lifeMax * 0.1f))
						{
							NPC.ai[2] += 0.5f;
						}
						if (CalamityGlobalNPC.bossBuff && CalamityGlobalNPC.superBossBuff)
						{
							NPC.ai[2] += 1f;
						}
						if (NPC.ai[2] >= 50f)
						{
							NPC.velocity.X = NPC.velocity.X * 0.93f;
							NPC.velocity.Y = NPC.velocity.Y * 0.93f;
							if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
							{
								NPC.velocity.X = 0f;
							}
							if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
							{
								NPC.velocity.Y = 0f;
							}
						}
						else
						{
							NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
						}
						if (NPC.ai[2] >= 80f)
						{
							NPC.ai[3] += 1f;
							NPC.ai[2] = 0f;
							NPC.target = 255;
							NPC.rotation = num842;
							if (NPC.ai[3] >= 6f)
							{
								NPC.ai[1] = 0f;
								NPC.ai[3] = 0f;
								return;
							}
							NPC.ai[1] = 1f;
							return;
						}
					}
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<CrushsawCrasher>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CrushsawCrasher>(), 4));
        }
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Catastrophe Shade";
			potionType = ItemID.GreaterHealingPotion;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 250, true);
			}
			else
			{
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 150, true);
			}
		}
	}
}