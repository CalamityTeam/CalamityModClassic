using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Scavenger
{
	public class ScavengerHead : ModNPC
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
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 70;
			NPC.lifeMax = 32705;
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
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.canGhostHeal = false;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit41;
			NPC.DeathSound = null;
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				NPC.defense = 150;
				NPC.lifeMax = 260000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 500000 : 450000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
		}

		public override void AI()
		{
			bool provy = (CalamityWorldPreTrailer.downedProvidence && !CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			Player player = Main.player[NPC.target];
			NPC.noTileCollide = true;
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
			float speed = 12f;
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			float centerX = Main.npc[CalamityGlobalNPC.scavenger].Center.X - center.X;
			float centerY = Main.npc[CalamityGlobalNPC.scavenger].Center.Y - center.Y;
			centerY -= 20f;
			centerX += 1f;
			float totalSpeed = (float)Math.Sqrt((double)(centerX * centerX + centerY * centerY));
			if (totalSpeed < 20f)
			{
				NPC.rotation = 0f;
				NPC.velocity.X = centerX;
				NPC.velocity.Y = centerY;
			}
			else
			{
				totalSpeed = speed / totalSpeed;
				NPC.velocity.X = centerX * totalSpeed;
				NPC.velocity.Y = centerY * totalSpeed;
				NPC.rotation = NPC.velocity.X * 0.1f;
			}
			if (NPC.alpha > 0)
			{
				NPC.alpha -= 10;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
				NPC.ai[1] = 30f;
			}
			NPC.ai[1] += 1f;
			int nukeTimer = 450;
			if (NPC.ai[1] >= (float)nukeTimer)
			{
				SoundEngine.PlaySound(SoundID.Item62, NPC.position);
				NPC.TargetClosest(true);
				NPC.ai[1] = 0f;
				Vector2 shootFromVector = new Vector2(NPC.Center.X, NPC.Center.Y - 20f);
				float nukeSpeed = 1f;
				float playerDistanceX = player.position.X + (float)player.width * 0.5f - shootFromVector.X;
				float playerDistanceY = player.position.Y + (float)player.height * 0.5f - shootFromVector.Y;
				float totalPlayerDistance = (float)Math.Sqrt((double)(playerDistanceX * playerDistanceX + playerDistanceY * playerDistanceY));
				totalPlayerDistance = nukeSpeed / totalPlayerDistance;
				playerDistanceX *= totalPlayerDistance;
				playerDistanceY *= totalPlayerDistance;
				int nukeDamage = expertMode ? 40 : 60;
				int projectileType = Mod.Find<ModProjectile>("ScavengerNuke").Type;
				if (Main.netMode != 1)
				{
					int nuke = Projectile.NewProjectile(NPC.GetSource_FromThis(null), shootFromVector.X, shootFromVector.Y, playerDistanceX, playerDistanceY, projectileType, nukeDamage + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[nuke].velocity.Y = -15f;
				}
			}
		}

		public override bool PreKill()
		{
			return false;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life > 0)
			{
				int num285 = 0;
				while ((double)num285 < hit.Damage / (double)NPC.lifeMax * 100.0)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, (float)hit.HitDirection, -1f, 0, default(Color), 1f);
					num285++;
				}
			}
			else if (Main.netMode != 1)
			{
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.position.Y + NPC.height, Mod.Find<ModNPC>("ScavengerHead2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			}
		}
	}
}