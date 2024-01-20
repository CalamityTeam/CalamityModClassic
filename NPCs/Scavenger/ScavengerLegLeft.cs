using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.Scavenger
{
	public class ScavengerLegLeft : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ravager");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
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
			NPC.lifeMax = 16000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.noGravity = true;
			NPC.alpha = 255;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			if (CalamityWorld1Point2.downedProvidence)
			{
				NPC.damage = 0;
				NPC.defense = 135;
				NPC.lifeMax = 250000;
			}
		}
		
		public override void AI()
		{
			bool provy = CalamityWorld1Point2.downedProvidence;
			Vector2 center = NPC.Center;
			if (CalamityGlobalNPC1Point2.scavenger < 0)
            {
                NPC.SimpleStrikeNPC(9999, 0, false, noPlayerInteraction: true);
                return;
			}
			if (NPC.timeLeft > 1800)
			{
				NPC.timeLeft = 1800;
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
			if (Main.npc[CalamityGlobalNPC1Point2.scavenger].ai[0] == 1f && Main.npc[CalamityGlobalNPC1Point2.scavenger].velocity.Y == 0f)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int smash = Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)center.X, (float)center.Y, 0f, 0f, Mod.Find<ModProjectile>("HiveExplosion").Type, 35 + (provy ? 30 : 0), 0f, Main.myPlayer, 0f, 0f);
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
				float num660 = Main.npc[CalamityGlobalNPC1Point2.scavenger].Center.X - vector79.X;
				float num661 = Main.npc[CalamityGlobalNPC1Point2.scavenger].Center.Y - vector79.Y;
				num661 += 88f;
				num660 -= 70f;
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
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}