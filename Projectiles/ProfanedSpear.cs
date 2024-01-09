using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class ProfanedSpear : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Spear");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
        	Projectile.alpha -= 3;
        	Projectile.ai[1] += 1f;
        	if (Projectile.ai[1] <= 20f)
        	{
        		Projectile.velocity.X *= 0.95f;
        		Projectile.velocity.Y *= 0.95f;
        	}
            else if (Projectile.ai[1] > 20f && Projectile.ai[1] <= 39f)
        	{
            	Projectile.velocity.X *= 1.1f;
        		Projectile.velocity.Y *= 1.1f;
        	}
            else if (Projectile.ai[1] == 40f)
            {
            	Projectile.ai[1] = 0f;
            }
        }
    }
}