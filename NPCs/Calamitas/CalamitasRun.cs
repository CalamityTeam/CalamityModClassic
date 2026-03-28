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

namespace CalamityModClassicPreTrailer.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class CalamitasRun : ModNPC
	{
		public bool canDespawn = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cataclysm");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("One of the clone's creations, and a recreation of the Witch's brother Cataclysm, yet it possesses no will of its own.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 60;
			NPC.npcSlots = 5f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 10;
			NPC.alpha = 25;
			NPC.value = 0f;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 4400 : 3000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 5000;
			}
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("MarkedforDeath").Type] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Calamitas");
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.damage = 170;
				NPC.defense = 80;
				NPC.lifeMax = 35000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 1000000 : 800000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
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
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool dayTime = Main.dayTime;
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			Player player = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			float num840 = NPC.position.X + (float)(NPC.width / 2) - player.position.X - (float)(player.width / 2);
			float num841 = NPC.position.Y + (float)NPC.height - 59f - player.position.Y - (float)(player.height / 2);
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
			if (!player.active || player.dead || (dayTime && !Main.eclipse))
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead || (dayTime && !Main.eclipse))
				{
					NPC.velocity = new Vector2(0f, -10f);
					canDespawn = true;
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else
			{
				canDespawn = false;
			}
			if (NPC.ai[1] == 0f)
			{
				float num861 = 5f;
				float num862 = 0.1f;
				int num863 = 1;
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
				{
					num863 = -1;
				}
				Vector2 vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num864 = player.position.X + (float)(player.width / 2) + (float)(num863 * 180) - vector86.X;
				float num865 = player.position.Y + (float)(player.height / 2) - vector86.Y;
				float num866 = (float)Math.Sqrt((double)(num864 * num864 + num865 * num865));
				if (expertMode)
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
				NPC.ai[2] += ((NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive)) ? 2f : 1f);
				if (NPC.ai[2] >= 400f)
				{
					NPC.ai[1] = 1f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.target = 255;
					NPC.netUpdate = true;
				}
				bool fireDelay = NPC.ai[2] > 120f || (double)NPC.life < (double)NPC.lifeMax * 0.9;
				if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height) && fireDelay)
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
						if (revenge)
						{
							NPC.localAI[1] += 0.5f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
						{
							NPC.localAI[1] += 1f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
						{
							NPC.localAI[1] += 1f;
						}
						if (NPC.localAI[1] > 8f)
						{
							NPC.localAI[1] = 0f;
							float num867 = 6f;
							int num868 = expertMode ? 26 : 32;
							int num869 = Mod.Find<ModProjectile>("BrimstoneFire").Type;
							vector86 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
							num864 = player.position.X + (float)(player.width / 2) - vector86.X;
							num865 = player.position.Y + (float)(player.height / 2) - vector86.Y;
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
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector86.X, vector86.Y, num864, num865, num869, num868 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
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
					float num870 = 15f;
					if (expertMode)
					{
						num870 += 2.5f;
					}
					if (revenge)
					{
						num870 += 1f;
					}
					if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						num870 += 4f;
					}
					Vector2 vector87 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num871 = player.position.X + (float)(player.width / 2) - vector87.X;
					float num872 = player.position.Y + (float)(player.height / 2) - vector87.Y;
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
					if (expertMode)
					{
						NPC.ai[2] += 0.5f;
					}
					if (revenge)
					{
						NPC.ai[2] += 0.1f;
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
						if (NPC.ai[3] >= 4f)
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

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
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
			while (NPC.ai[1] > 0f && Lighting.NotRetro && ((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)))
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
			var something = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture2D3, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, something, 0);
			return false;
		}

		public override bool CheckActive()
		{
			return canDespawn;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("CataclysmTrophy").Type, 10));
			npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("BrimstoneFlamesprayer").Type, 4, 10));
			npcLoot.Add(ItemDropRule.NormalvsExpert(Mod.Find<ModItem>("BrimstoneFlameblaster").Type, 4, 10));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Cataclysm").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Cataclysm2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Cataclysm3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Cataclysm4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Cataclysm5").Type, 1f);
					NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
					NPC.width = 100;
					NPC.height = 100;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					for (int num621 = 0; num621 < 40; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							235, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}

					for (int num623 = 0; num623 < 70; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height,
							235, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235,
							0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 180);
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
			}
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300, true);
		}
	}
}