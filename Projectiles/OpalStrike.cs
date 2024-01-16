using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class OpalStrike : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Opal Strike");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.scale = 0.5f;
            Projectile.alpha = 200;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
        	if (Projectile.scale <= 1.5f)
        	{
        		Projectile.scale *= 1.01f;
        	}
        	Projectile.alpha -= 2;
        	if (Projectile.alpha == 0)
        	{
        		Projectile.alpha = 200;
        	}
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 57, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 57, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}