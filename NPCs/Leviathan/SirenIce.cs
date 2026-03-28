using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	public class SirenIce : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Shield");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.canGhostHeal = false;
			NPC.noTileCollide = true;
			NPC.damage = 45;
			NPC.width = 100;
			NPC.height = 100;
			NPC.defense = 10;
			NPC.lifeMax = 650;
			if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 400000;
            }
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
            int num989 = (int)NPC.ai[0];
            if (Main.npc[num989].active && Main.npc[num989].type == Mod.Find<ModNPC>("Siren").Type)
            {
                NPC.rotation = Main.npc[num989].rotation;
                NPC.spriteDirection = Main.npc[num989].direction;
                NPC.velocity = Vector2.Zero;
                NPC.position = Main.npc[num989].Center;
                NPC.position.X = NPC.position.X - (float)(NPC.width / 2) + ((NPC.spriteDirection == 1) ? -20f : 20f);
                NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2) - 30;
                NPC.gfxOffY = Main.npc[num989].gfxOffY;
                Lighting.AddLight((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16, 0f, 0.8f, 1.1f);
                return;
            }
            NPC.life = 0;
            NPC.HitEffect(0, 10.0);
            NPC.active = false;
        }

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
            if (projectile.type != Mod.Find<ModProjectile>("FlakKraken").Type)
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
		}

		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(200, 200, 200, NPC.alpha);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Frostburn, 240, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}