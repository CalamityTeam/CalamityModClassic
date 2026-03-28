using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Calamitas
{
	[AutoloadBossHead]
	public class Calamitas : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Calamitas");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.damage = 70;
			NPC.npcSlots = 14f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 15;
			NPC.value = 0f;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 15000 : 10000;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 22500;
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
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			Music = MusicID.Boss2;
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.damage = 150;
				NPC.defense = 130;
				NPC.lifeMax = 80000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 1300000 : 1100000;
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
			float num801 = NPC.position.X + (float)(NPC.width / 2) - player.position.X - (float)(player.width / 2);
			float num802 = NPC.position.Y + (float)NPC.height - 59f - player.position.Y - (float)(player.height / 2);
			float num803 = (float)Math.Atan2((double)num802, (double)num801) + 1.57f;
			if (num803 < 0f)
			{
				num803 += 6.283f;
			}
			else if ((double)num803 > 6.283)
			{
				num803 -= 6.283f;
			}
			float num804 = 0.1f;
			if (NPC.rotation < num803)
			{
				if ((double)(num803 - NPC.rotation) > 3.1415)
				{
					NPC.rotation -= num804;
				}
				else
				{
					NPC.rotation += num804;
				}
			}
			else if (NPC.rotation > num803)
			{
				if ((double)(NPC.rotation - num803) > 3.1415)
				{
					NPC.rotation += num804;
				}
				else
				{
					NPC.rotation -= num804;
				}
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (NPC.rotation < 0f)
			{
				NPC.rotation += 6.283f;
			}
			else if ((double)NPC.rotation > 6.283)
			{
				NPC.rotation -= 6.283f;
			}
			if (NPC.rotation > num803 - num804 && NPC.rotation < num803 + num804)
			{
				NPC.rotation = num803;
			}
			if (!player.active || player.dead || (dayTime && !Main.eclipse))
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead || (dayTime && !Main.eclipse))
				{
					NPC.velocity = new Vector2(0f, -10f);
					if (NPC.timeLeft > 150)
					{
						NPC.timeLeft = 150;
					}
					return;
				}
			}
			else if (NPC.timeLeft < 1800)
			{
				NPC.timeLeft = 1800;
			}
			if (NPC.ai[1] == 0f)
			{
				float num823 = expertMode ? 9.5f : 8f;
				float num824 = expertMode ? 0.175f : 0.15f;
				Vector2 vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num825 = player.position.X + (float)(player.width / 2) - vector82.X;
				float num826 = player.position.Y + (float)(player.height / 2) - 300f - vector82.Y;
				float num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
				num827 = num823 / num827;
				num825 *= num827;
				num826 *= num827;
				if (NPC.velocity.X < num825)
				{
					NPC.velocity.X = NPC.velocity.X + num824;
					if (NPC.velocity.X < 0f && num825 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num824;
					}
				}
				else if (NPC.velocity.X > num825)
				{
					NPC.velocity.X = NPC.velocity.X - num824;
					if (NPC.velocity.X > 0f && num825 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num824;
					}
				}
				if (NPC.velocity.Y < num826)
				{
					NPC.velocity.Y = NPC.velocity.Y + num824;
					if (NPC.velocity.Y < 0f && num826 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num824;
					}
				}
				else if (NPC.velocity.Y > num826)
				{
					NPC.velocity.Y = NPC.velocity.Y - num824;
					if (NPC.velocity.Y > 0f && num826 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num824;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= 300f)
				{
					NPC.ai[1] = 1f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.TargetClosest(true);
					NPC.netUpdate = true;
				}
				vector82 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				num825 = player.position.X + (float)(player.width / 2) - vector82.X;
				num826 = player.position.Y + (float)(player.height / 2) - vector82.Y;
				NPC.rotation = (float)Math.Atan2((double)num826, (double)num825) - 1.57f;
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
						NPC.localAI[1] += 2f;
					}
					if (NPC.localAI[1] > 180f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						NPC.localAI[1] = 0f;
						float num828 = expertMode ? 13f : 10.5f;
						int num829 = expertMode ? 24 : 30;
						int num830 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
						num827 = (float)Math.Sqrt((double)(num825 * num825 + num826 * num826));
						num827 = num828 / num827;
						num825 *= num827;
						num826 *= num827;
						vector82.X += num825 * 15f;
						vector82.Y += num826 * 15f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector82.X, vector82.Y, num825, num826, num830, num829 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
						return;
					}
				}
			}
			else
			{
				int num831 = 1;
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)player.width)
				{
					num831 = -1;
				}
				float num832 = expertMode ? 9.5f : 8f;
				float num833 = expertMode ? 0.25f : 0.2f;
				Vector2 vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num834 = player.position.X + (float)(player.width / 2) + (float)(num831 * 360) - vector83.X;
				float num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
				float num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
				num836 = num832 / num836;
				num834 *= num836;
				num835 *= num836;
				if (NPC.velocity.X < num834)
				{
					NPC.velocity.X = NPC.velocity.X + num833;
					if (NPC.velocity.X < 0f && num834 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num833;
					}
				}
				else if (NPC.velocity.X > num834)
				{
					NPC.velocity.X = NPC.velocity.X - num833;
					if (NPC.velocity.X > 0f && num834 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num833;
					}
				}
				if (NPC.velocity.Y < num835)
				{
					NPC.velocity.Y = NPC.velocity.Y + num833;
					if (NPC.velocity.Y < 0f && num835 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num833;
					}
				}
				else if (NPC.velocity.Y > num835)
				{
					NPC.velocity.Y = NPC.velocity.Y - num833;
					if (NPC.velocity.Y > 0f && num835 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num833;
					}
				}
				vector83 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				num834 = player.position.X + (float)(player.width / 2) - vector83.X;
				num835 = player.position.Y + (float)(player.height / 2) - vector83.Y;
				NPC.rotation = (float)Math.Atan2((double)num835, (double)num834) - 1.57f;
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
						NPC.localAI[1] += 1.5f;
					}
					if (Main.expertMode || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.localAI[1] += 1.5f;
					}
					if (NPC.localAI[1] > 60f && Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
					{
						NPC.localAI[1] = 0f;
						float num837 = 10.5f;
						int num838 = expertMode ? 15 : 22;
						int num839 = Mod.Find<ModProjectile>("BrimstoneLaser").Type;
						num836 = (float)Math.Sqrt((double)(num834 * num834 + num835 * num835));
						num836 = num837 / num836;
						num834 *= num836;
						num835 *= num836;
						vector83.X += num834 * 15f;
						vector83.Y += num835 * 15f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector83.X, vector83.Y, num834, num835, num839, num838 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] >= 180f)
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					NPC.TargetClosest(true);
					NPC.netUpdate = true;
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
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
			var something = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture2D3, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, something, 0);
			return false;
		}

		public override bool PreKill()
		{
			return false;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life > 0)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
			else
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

		public override bool CheckDead()
		{
			if (Main.netMode != 1)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height, Mod.Find<ModNPC>("CalamitasRun3").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height, Mod.Find<ModNPC>("CalamitasRun").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height, Mod.Find<ModNPC>("CalamitasRun2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			}
			string key = "You underestimate my power...";
			string key2 = "The brothers have awoken!";
			Color messageColor = Color.Orange;
			if (Main.netMode == 0)
			{
				Main.NewText(Language.GetTextValue(key), messageColor);
				Main.NewText(Language.GetTextValue(key2), messageColor);
			}
			else if (Main.netMode == 2)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key2), messageColor);
			}
			return true;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
			}
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300, true);
		}
	}
}