using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Polterghast
{
	public class PhantomTurret2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Phantom Turret");
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 130;
			NPC.width = 20; //324
			NPC.height = 20; //216
			NPC.defense = 50;
			NPC.lifeMax = 200;
			NPC.alpha = 255;
			NPC.knockBackResist = 0f;
			NPC.dontTakeDamage = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit36;
			NPC.DeathSound = SoundID.NPCDeath39;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("Spooky.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld1Point2.revenge;
			float num418 = 15f;
			float num1261 = revenge ? 15f : 12f;
			Player player = Main.player[NPC.target];
			if (NPC.ai[1] == 0f)
			{
				NPC.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, NPC.position);
			}
			NPC.alpha -= 5;
			if (NPC.alpha < 30)
			{
				NPC.alpha = 30;
			}
			NPC.localAI[0] += 1f;
			if (NPC.localAI[0] >= 1200f)
			{
				NPC.dontTakeDamage = false;
			}
			NPC.localAI[1] += 1f;
			if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 5)
			{
				NPC.localAI[1] += 1f;
				num418 += 1f;
				num1261 += 2f;
			}
			if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 8)
			{
				NPC.localAI[1] += 1f;
				num418 += 1f;
				num1261 += 2f;
			}
			if (Main.npc[CalamityGlobalNPC1Point2.ghostBoss].life < Main.npc[CalamityGlobalNPC1Point2.ghostBoss].lifeMax / 10)
			{
				NPC.localAI[1] += 2f;
				num418 += 2f;
				num1261 += 3f;
			}
			if (NPC.localAI[1] >= 240f) 
			{
				NPC.localAI[1] = 0f;
				if (Collision.CanHit(NPC.Center, 1, 1, player.position, player.width, player.height))
				{
					Vector2 vector34 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num349 = player.position.X + (float)(player.width / 2) - vector34.X;
					float num350 = player.position.Y + (float)(player.height / 2) - vector34.Y;
					float num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
					num349 *= num351;
					num350 *= num351;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int num419 = 53;
						int num420 = Mod.Find<ModProjectile>("PhantomShot2").Type;
						num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
						num351 = num418 / num351;
						num349 *= num351;
						num350 *= num351;
						num349 += (float)Main.rand.Next(-20, 21) * 0.05f;
						num350 += (float)Main.rand.Next(-20, 21) * 0.05f;
						vector34.X += num349 * 4f;
						vector34.Y += num350 * 4f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), vector34.X, vector34.Y, num349, num350, num420, num419, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (player.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = NPC.velocity.Y + 3f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 3f;
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
			NPC.rotation = NPC.velocity.X * 0.08f;
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			NPC.TargetClosest(true);
			Vector2 vector145 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num1258 = Main.player[NPC.target].Center.X - vector145.X;
			float num1259 = Main.player[NPC.target].Center.Y - vector145.Y;
			float num1260 = (float)Math.Sqrt((double)(num1258 * num1258 + num1259 * num1259));
			num1260 = num1261 / num1260;
			num1258 *= num1260;
			num1259 *= num1260;
			NPC.velocity.X = (NPC.velocity.X * 100f + num1258) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num1259) / 101f;
			int num1262 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num1262].velocity *= 0.1f;
			Main.dust[num1262].scale = 1.3f;
			Main.dust[num1262].noGravity = true;
			return;
		}
		
		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(200, 200, 200, NPC.alpha);
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 0;
			return true;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}