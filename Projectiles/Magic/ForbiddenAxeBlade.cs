using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class ForbiddenAxeBlade : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.alpha -= 3;
        	Projectile.rotation += 0.75f;
        	Projectile.ai[1] += 1f;
        	if (Projectile.ai[1] <= 20f)
        	{
        		Projectile.velocity.X *= 0.85f;
        		Projectile.velocity.Y *= 0.85f;
        	}
            else if (Projectile.ai[1] > 20f && Projectile.ai[1] <= 39f)
        	{
            	Projectile.velocity.X *= 1.25f;
        		Projectile.velocity.Y *= 1.25f;
        	}
            else if (Projectile.ai[1] == 40f)
            {
            	Projectile.ai[1] = 0f;
            }
            if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 159, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 159, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}