using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items.SlimeGod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.SlimeGod
{
	[AutoloadBossHead]
	public class SlimeGodCore : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Slime God");
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				new FlavorTextBestiaryInfoElement("One of the oldest creatures in the world, and a god revered by the Statis clan.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 50;
			NPC.npcSlots = 10f;
			NPC.width = 44; //324
			NPC.height = 44; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 3750 : 3000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 5250;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = CalamityWorldPreTrailer.death ? 2700000 : 2500000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 8, 0, 0);
            NPC.alpha = 80;
			AnimationType = 10;
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/SlimeGod");
        }
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<SlimeGodBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SlimeGodBag>()));
		}

		
		public override void AI()
		{
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			Player player = Main.player[NPC.target];
			int randomDust = Main.rand.Next(2);
			if (randomDust == 0)
			{
				randomDust = 173;
			}
			else
			{
				randomDust = 260;
			}
			int num658 = Dust.NewDust(NPC.position, NPC.width, NPC.height, randomDust, NPC.velocity.X, NPC.velocity.Y, 255, new Color(0, 80, 255, 80), NPC.scale * 1.5f);
			Main.dust[num658].noGravity = true;
			Main.dust[num658].velocity *= 0.5f;
			bool flag100 = false;
            if ((NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGod").Type) ||
                NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodSplit").Type) ||
                NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRun").Type) ||
                NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodRunSplit").Type)) && 
                !CalamityWorldPreTrailer.bossRushActive)
            {
                flag100 = true;
            }
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, 30f);
                    if ((double)NPC.position.Y > Main.rockLayer * 16.0)
                    {
                        for (int x = 0; x < 200; x++)
                        {
                            if (Main.npc[x].type == Mod.Find<ModNPC>("SlimeGod").Type || Main.npc[x].type == Mod.Find<ModNPC>("SlimeGodSplit").Type ||
                                Main.npc[x].type == Mod.Find<ModNPC>("SlimeGodRun").Type || Main.npc[x].type == Mod.Find<ModNPC>("SlimeGodRunSplit").Type)
                            {
                                Main.npc[x].active = false;
                                Main.npc[x].netUpdate = true;
                            }
                        }
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                    return;
				}
			}
			else if (NPC.timeLeft < 2400)
			{
				NPC.timeLeft = 2400;
			}
			if (!flag100)
			{
				NPC.damage = 75;
				if (Main.netMode != 1)
				{
                    NPC.localAI[1] += ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 2f : 1f);
                    if (expertMode && Main.rand.Next(2) == 0)
                    {
                        if (NPC.localAI[0] >= 75f)
						{
							NPC.localAI[0] = 0f;
							NPC.TargetClosest(true);
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
							{
								float num179 = revenge ? 2f : 3f;
								Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
								float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								float num181 = Math.Abs(num180) * 0.1f;
								float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
								float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								NPC.netUpdate = true;
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								int num184 = 21;
								int num185 = Main.rand.Next(2);
								if (num185 == 0)
								{
									num185 = Mod.Find<ModProjectile>("AbyssMine").Type;
								}
								else
								{
									num185 = Mod.Find<ModProjectile>("AbyssMine2").Type;
                                    num184 = 19;
                                }
								value9.X += num180;
								value9.Y += num182;
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = num179 / num183;
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
					else if (NPC.localAI[1] >= 75f)
					{
						NPC.localAI[1] = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						{
							float num179 = revenge ? 6f : 5f;
							Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							float num180 = player.position.X + (float)player.width * 0.5f - value9.X;
							float num181 = Math.Abs(num180) * 0.1f;
							float num182 = player.position.Y + (float)player.height * 0.5f - value9.Y - num181;
							float num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
							NPC.netUpdate = true;
							num183 = num179 / num183;
							num180 *= num183;
							num182 *= num183;
							int num184 = expertMode ? 16 : 18;
							int num185 = Main.rand.Next(2);
							if (num185 == 0)
							{
								num185 = Mod.Find<ModProjectile>("AbyssBallVolley").Type;
							}
							else
							{
								num185 = Mod.Find<ModProjectile>("AbyssBallVolley2").Type;
                                num184 = expertMode ? 14 : 16;
                            }
							value9.X += num180;
							value9.Y += num182;
							for (int num186 = 0; num186 < 2; num186++)
							{
								num180 = player.position.X + (float)player.width * 0.5f - value9.X;
								num182 = player.position.Y + (float)player.height * 0.5f - value9.Y;
								num183 = (float)Math.Sqrt((double)(num180 * num180 + num182 * num182));
								num183 = num179 / num183;
								num180 += (float)Main.rand.Next(-30, 31);
								num182 += (float)Main.rand.Next(-30, 31);
								num180 *= num183;
								num182 *= num183;
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, num180, num182, num185, num184, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
			}
			NPC.TargetClosest(true);
			float num1372 = 6f;
			if (!flag100)
			{
				num1372 = 14f;
			}
			else if (revenge)
			{
				num1372 = 10f;
			}
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                num1372 = 22f;
            }
            if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
            {
                num1372 += 8f;
            }
			Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
			float num1373 = player.position.X + (float)player.width * 0.5f - vector167.X;
			float num1374 = player.Center.Y - vector167.Y;
			float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
			float num1376 = num1372 / num1375;
			num1373 *= num1376;
			num1374 *= num1376;
			NPC.ai[0] -= 1f;
			if (num1375 < 200f || NPC.ai[0] > 0f)
			{
				if (num1375 < 200f)
				{
					NPC.ai[0] = 20f;
				}
				if (NPC.velocity.X < 0f)
				{
					NPC.direction = -1;
				}
				else
				{
					NPC.direction = 1;
				}
				return;
			}
			NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
			NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
			if (num1375 < 350f)
			{
				NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
				NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
			}
			if (num1375 < 300f)
			{
				NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
				NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
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
			while (((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)) && Lighting.NotRetro)
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

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 40;
				NPC.height = 40;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 4, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 4, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 4, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.VortexDebuff, 240, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120, true);
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 120);
			}
		}
	}
}