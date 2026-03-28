using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Scavenger
{
	public class ScavengerLegRight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ravager");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.width = 60; //324
			NPC.height = 60; //216
			NPC.defense = 60;
			NPC.lifeMax = 21010;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.noGravity = true;
			NPC.canGhostHeal = false;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit41;
			NPC.DeathSound = SoundID.NPCDeath14;
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.defense = 135;
				NPC.lifeMax = 200000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 450000 : 400000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
		}

		public override void AI()
		{
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			Vector2 center = NPC.Center;
			if (!Main.npc[CalamityGlobalNPC.scavenger].active)
			{
				NPC.active = false;
				NPC.netUpdate = true;
				return;
			}
			if (NPC.timeLeft < 3000)
			{
				NPC.timeLeft = 3000;
			}
			if (NPC.alpha > 0)
			{
				NPC.alpha -= 10;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				NPC.ai[1] = 0f;
			}
			if (Main.npc[CalamityGlobalNPC.scavenger].ai[0] == 1f && Main.npc[CalamityGlobalNPC.scavenger].velocity.Y == 0f)
			{
				if (Main.netMode != 1)
				{
					int smash = Projectile.NewProjectile(NPC.GetSource_FromThis(null), (float)center.X, (float)center.Y, 0f, 0f, Mod.Find<ModProjectile>("HiveExplosion").Type, 35 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[smash].timeLeft = 30;
					Main.projectile[smash].hostile = true;
					Main.projectile[smash].friendly = false;
				}
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.noTileCollide = true;
				float num659 = 14f;
				if (NPC.life < NPC.lifeMax / 2)
				{
					num659 += 3f;
				}
				if (NPC.life < NPC.lifeMax / 3)
				{
					num659 += 3f;
				}
				if (NPC.life < NPC.lifeMax / 5)
				{
					num659 += 8f;
				}
				Vector2 vector79 = new Vector2(center.X, center.Y);
				float num660 = Main.npc[CalamityGlobalNPC.scavenger].Center.X - vector79.X;
				float num661 = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - vector79.Y;
				num661 += 88f;
				num660 += 70f;
				float num662 = (float)Math.Sqrt((double)(num660 * num660 + num661 * num661));
				if (num662 < 12f + num659)
				{
					NPC.rotation = 0f;
					NPC.velocity.X = num660;
					NPC.velocity.Y = num661;
				}
				else
				{
					num662 = num659 / num662;
					NPC.velocity.X = num660 * num662;
					NPC.velocity.Y = num661 * num662;
				}
			}
		}

		public override bool PreKill()
		{
			return false;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerLegRight").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("ScavengerLegRight2").Type, 1f);
				}
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}