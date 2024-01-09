using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.Leviathan
{
	public class SirenIce : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ice Shield");
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.canGhostHeal = false;
			NPC.noTileCollide = true;
			NPC.damage = 20;
			NPC.width = 160; //324
			NPC.height = 160; //216
			NPC.defense = 10;
			NPC.lifeMax = 1900;
			NPC.alpha = 255;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
		}
		
		public override void AI()
		{
			if (NPC.alpha > 100)
			{
				NPC.alpha -= 2;
			}
			Player player = Main.player[NPC.target];
			if (NPC.type == Mod.Find<ModNPC>("SirenIce").Type)
			{
				int num989 = (int)NPC.ai[0];
				if (Main.npc[num989].active && Main.npc[num989].type == Mod.Find<ModNPC>("Siren").Type) 
				{
					NPC.rotation = Main.npc[num989].rotation;
					NPC.spriteDirection = Main.npc[num989].direction;
					NPC.velocity = Vector2.Zero;
					NPC.position = Main.npc[num989].Center;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2) + ((NPC.spriteDirection == 1) ? -30f : 30f);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					NPC.gfxOffY = Main.npc[num989].gfxOffY;
					Lighting.AddLight((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16, 0f, 0.8f, 1.1f);
					return;
				}
				NPC.life = 0;
				NPC.HitEffect(0, 10.0);
				NPC.active = false;
				return;
			}
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.penetrate == -1 && !projectile.minion)
			{
				projectile.penetrate = 1;
			}
			else if (projectile.penetrate >= 1)
			{
				projectile.penetrate = 1;
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = 20;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Frozen, 120, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}