using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ProfanedSpear : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Profaned Spear");
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
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
            if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}