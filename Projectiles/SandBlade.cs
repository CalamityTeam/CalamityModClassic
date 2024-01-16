using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SandBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Sand Blade");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
        	Projectile.rotation += 0.5f;
        	Projectile.ai[1] += 1f;
        	if (Projectile.ai[1] <= 30f)
        	{
        		Projectile.velocity.X *= 0.925f;
        		Projectile.velocity.Y *= 0.925f;
        	}
            else if (Projectile.ai[1] > 30f && Projectile.ai[1] <= 59f)
        	{
            	Projectile.velocity.X *= 1.15f;
        		Projectile.velocity.Y *= 1.15f;
        	}
            else if (Projectile.ai[1] == 60f)
            {
            	Projectile.ai[1] = 0f;
            }
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 159, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 159, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}