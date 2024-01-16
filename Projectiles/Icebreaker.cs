using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Icebreaker : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Icebreaker");
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 300;
            AIType = 52;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 67, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = +oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = +oldVelocity.Y;
            }
            return false;
        }
    }
}