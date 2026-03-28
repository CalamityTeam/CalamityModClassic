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

namespace CalamityModClassicPreTrailer.NPCs.SupremeCalamitas
{
	[AutoloadBossHead]
	public class SupremeCatastrophe : ModNPC
	{
		private float distanceY = 375f;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Catastrophe");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
				new FlavorTextBestiaryInfoElement("The Witch's revived brother, Catastrophe. He is a shell of his former self however.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 5f;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 150;
            NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 1400000 : 1200000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 1000000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
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
			bool expertMode = Main.expertMode;
			if (!Main.npc[CalamityGlobalNPC.SCal].active)
			{
				NPC.active = false;
				NPC.netUpdate = true;
				return;
			}
			NPC.TargetClosest(true);
			float num676 = 60f;
			float num677 = 1.5f;
			float distanceX = 750f;
			if (NPC.localAI[1] < 750f)
			{
				NPC.localAI[1] += 1f;
				distanceY -= 1f;
			}
			else if (NPC.localAI[1] < 1500f)
			{
				NPC.localAI[1] += 1f;
				distanceY += 1f;
			}
			if (NPC.localAI[1] >= 1500f)
			{
				NPC.localAI[1] = 0f;
			}
			Vector2 vector83 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num678 = Main.player[NPC.target].Center.X - vector83.X - distanceX;
			float num679 = Main.player[NPC.target].Center.Y - vector83.Y + distanceY;
			NPC.rotation = 4.71f;
			float num680 = (float)Math.Sqrt((double)(num678 * num678 + num679 * num679));
			num680 = num676 / num680;
			num678 *= num680;
			num679 *= num680;
			if (NPC.velocity.X < num678)
			{
				NPC.velocity.X = NPC.velocity.X + num677;
				if (NPC.velocity.X < 0f && num678 > 0f)
				{
					NPC.velocity.X = NPC.velocity.X + num677;
				}
			}
			else if (NPC.velocity.X > num678)
			{
				NPC.velocity.X = NPC.velocity.X - num677;
				if (NPC.velocity.X > 0f && num678 < 0f)
				{
					NPC.velocity.X = NPC.velocity.X - num677;
				}
			}
			if (NPC.velocity.Y < num679)
			{
				NPC.velocity.Y = NPC.velocity.Y + num677;
				if (NPC.velocity.Y < 0f && num679 > 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y + num677;
				}
			}
			else if (NPC.velocity.Y > num679)
			{
				NPC.velocity.Y = NPC.velocity.Y - num677;
				if (NPC.velocity.Y > 0f && num679 < 0f)
				{
					NPC.velocity.Y = NPC.velocity.Y - num677;
				}
			}
			if (NPC.localAI[0] < 120f)
			{
				NPC.localAI[0] += 1f;
			}
			if (NPC.localAI[0] >= 120f)
			{
				NPC.ai[1] += 1f;
				if (NPC.ai[1] >= 30f)
				{
					NPC.ai[1] = 0f;
					Vector2 vector85 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num689 = 4f;
					int num690 = expertMode ? 150 : 200; //600 500
					int num691 = Mod.Find<ModProjectile>("BrimstoneHellblast2").Type;
					if (Main.netMode != 1)
					{
						int num695 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector85.X, vector85.Y, num689, 0f, num691, num690, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				NPC.ai[2] += 1f;
				if (!NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCataclysm").Type))
				{
					NPC.ai[2] += 2f;
				}
				if (NPC.ai[2] >= 300f)
				{
					NPC.ai[2] = 0f;
					float num689 = 7f;
					int num690 = expertMode ? 150 : 200; //600 500
					SoundEngine.PlaySound(SoundID.Item20, NPC.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					if (Main.netMode != 1)
					{
						for (i = 0; i < 8; i++)
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * num689), (float)(Math.Cos(offsetAngle) * num689), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, num690, 0f, Main.myPlayer, 0f, 1f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, (float)(-Math.Sin(offsetAngle) * num689), (float)(-Math.Cos(offsetAngle) * num689), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, num690, 0f, Main.myPlayer, 0f, 1f);
						}
					}
					for (int dust = 0; dust <= 5; dust++)
					{
						Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, 235, 0f, 0f);
					}
				}
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.type == Mod.Find<ModProjectile>("AngryChicken").Type)
			{
				modifiers.FinalDamage.Base /= 2;
			}
			if (projectile.type == Mod.Find<ModProjectile>("ApothMark").Type || projectile.type == Mod.Find<ModProjectile>("ApothJaws").Type)
			{
				modifiers.FinalDamage.Base /= 3;
			}
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 2)
			{
				modifiers.SetMaxDamage(0);
			}
			double newDamage = (modifiers.FinalDamage.Base + (int)(NPC.defense * 0.25));
			float protection = (CalamityWorldPreTrailer.death ? 0.75f : 0.7f); //45%
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				protection = 0.6f;
			}
			if (newDamage < 1.0)
			{
				newDamage = 1.0;
			}
			if (NPC.ichor)
			{
				protection *= 0.9f; //41%
			}
			else if (NPC.onFire2)
			{
				protection *= 0.91f;
			}
			if (newDamage >= 1.0)
			{
				newDamage = (double)((int)((double)(1f - protection) * newDamage));
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
			}
			modifiers.FinalDamage.Base = (float)newDamage;
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

		public override bool CheckActive()
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
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
	}
}