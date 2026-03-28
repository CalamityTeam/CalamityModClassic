using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.TheDevourerofGods
{
	public class DevourerofGodsBody : ModNPC
	{
		private int invinceTime = 360;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Devourer of Gods");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.damage = 180; //70
			NPC.npcSlots = 5f;
			NPC.width = 34; //38
			NPC.height = 34; //30
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 500000 : 450000; //1000000 960000
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 850000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = 6; //new
			AIType = -1; //new
			AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.4f;
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.chaseable = false;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/ScourgeofTheUniverse");
			NPC.dontCountMe = true;
			if (Main.expertMode)
			{
				NPC.scale = 1.5f;
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

		public override void AI()
		{
			bool expertMode = Main.expertMode;
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (invinceTime > 0)
			{
				invinceTime--;
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
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
				float shootTimer = 2f + (expertMode ? 1f : 0f);
				NPC.localAI[0] += shootTimer;
				int projectileType = Mod.Find<ModProjectile>("DoGNebulaShot").Type;
				int damage = expertMode ? 55 : 68;
				float num941 = 5f;
				if (NPC.localAI[0] >= (float)Main.rand.Next(2800, 32000))
				{
					NPC.localAI[0] = 0f;
					NPC.TargetClosest(true);
					Vector2 vector104 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
					float num942 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
					float num943 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
					float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
					num944 = num941 / num944;
					num942 *= num944;
					num943 *= num944;
					num942 += (float)Main.rand.Next(-5, 6) * 0.05f;
					num943 += (float)Main.rand.Next(-5, 6) * 0.05f;
					vector104.X += num942 * 5f;
					vector104.Y += num943 * 5f;
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector104.X, vector104.Y, num942, num943, projectileType, damage, 0f, Main.myPlayer, 0f, 0f);
					NPC.netUpdate = true;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 0;
			return true;
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 2)
			{
				modifiers.SetMaxDamage(0);
			}
			double protection = CalamityWorldPreTrailer.death ? 0.05 : 0.075;
			modifiers.FinalDamage *= (float)protection;
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
				float randomSpread = (float)(Main.rand.Next(-100, 100) / 100);
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoGBody").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoGBody2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
						NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoGBody3").Type, 1f);
				}
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 20; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
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
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 240, true);
			target.AddBuff(BuffID.Frostburn, 240, true);
			target.AddBuff(BuffID.Darkness, 240, true);
		}
	}
}