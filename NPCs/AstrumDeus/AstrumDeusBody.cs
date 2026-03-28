using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.AstrumDeus
{
	public class AstrumDeusBody : ModNPC
	{
		public int spawn = 0;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astrum Deus");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 70; //70
			NPC.npcSlots = 5f;
			NPC.width = 38; //324
			NPC.height = 44; //216
			NPC.defense = 45;
            NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 12000 : 8000; //250000
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 19400;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = CalamityWorldPreTrailer.death ? 420000 : 360000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.2f;
			if (Main.expertMode)
			{
				NPC.scale = 1.35f;
			}
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
            NPC.dontCountMe = true;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			int defenseDown = (int)(30f * (1f - (float)NPC.life / (float)NPC.lifeMax));
			NPC.defense = NPC.defDefense - defenseDown;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
			if (Main.npc[(int)NPC.ai[1]].alpha < 128)
			{
				if (NPC.alpha != 0)
				{
					for (int num934 = 0; num934 < 2; num934++)
					{
						int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num935].noGravity = true;
						Main.dust[num935].noLight = true;
					}
				}
				NPC.alpha -= 42;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
			}
			if (Main.netMode != 1)
			{
				int shootTime = 4;
				if ((double)NPC.life <= (double)NPC.lifeMax * 0.3 || CalamityWorldPreTrailer.bossRushActive)
				{
					shootTime += 2;
				}
				NPC.localAI[0] += (float)Main.rand.Next(shootTime);
				if (NPC.localAI[0] >= (float)Main.rand.Next(1400, 26000))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						float num941 = revenge ? 11f : 9f; //speed
                        if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
                        {
                            num941 = 16f;
                        }
						Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
						float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
						float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
						num944 = num941 / num944;
						num942 *= num944;
						num943 *= num944;
						num942 += (float)Main.rand.Next(-25, 26) * 0.05f;
						num943 += (float)Main.rand.Next(-25, 26) * 0.05f;
						int num945 = expertMode ? 31 : 37;
						int num946 = Mod.Find<ModProjectile>("AstralShot2").Type;
						vector104.X += num942 * 5f;
						vector104.Y += num943 * 5f;
						int num947 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
						NPC.netUpdate = true;
					}
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstrumDeus/AstrumDeusBodyAlt").Value;
			CalamityModClassicPreTrailer.DrawTexture(spriteBatch, (NPC.localAI[3] == 1f ? texture : TextureAssets.Npc[NPC.type].Value), 0, NPC, drawColor);
			return false;
		}

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
			if (projectile.type == Mod.Find<ModProjectile>("TerraFireGreen").Type || projectile.type == Mod.Find<ModProjectile>("AtlantisSpear").Type || projectile.type == Mod.Find<ModProjectile>("AtlantisSpear2").Type)
			{
				projectile.damage = (int)((double)projectile.damage * 0.66);
			}
            if ((double)NPC.life <= (double)NPC.lifeMax * 0.33)
            {
	            projectile.damage = (int)((double)projectile.damage * 0.33);
            }
            else
            {
                if (projectile.penetrate == -1 && !projectile.minion && !projectile.CountsAsClass(DamageClass.Throwing))
                {
	                projectile.damage /= 2;
                }
                else if (projectile.penetrate > 1)
                {
	                projectile.damage /= projectile.penetrate;
                }
            }
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
			if (NPC.CountNPCS(Mod.Find<ModNPC>("AstrumDeusProbe3").Type) < 3 && CalamityWorldPreTrailer.revenge)
			{
				if (NPC.life > 0 && Main.netMode != 1 && spawn == 0 && Main.rand.Next(25) == 0)
				{
					spawn = 1;
					int num660 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("AstrumDeusProbe3").Type, 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2 && num660 < 200)
					{
						NetMessage.SendData(23, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
					}
					NPC.netUpdate = true;
				}
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 10; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 120, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
	}
}