using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class FossilSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Fossil Spike");
            Projectile.width = 20;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 4;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 32, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
			   Projectile.frame = 0;
			}
        }
    }
}