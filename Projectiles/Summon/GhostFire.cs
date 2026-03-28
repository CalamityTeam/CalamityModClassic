using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class GhostFire : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fire");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 80;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 8;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
        }

        public override void AI()
        {
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 6f)
			{
				for (int num447 = 0; num447 < 1; num447++)
				{
					Vector2 vector33 = Projectile.position;
					vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
					Projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, 1, 1, 180, 0f, 0f, 0, default(Color), 0.2f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].noGravity = true;
					Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[num448].velocity *= 0.2f;
				}
				return;
			}
        }
    }
}