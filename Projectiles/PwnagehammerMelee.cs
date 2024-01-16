using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class PwnagehammerMelee : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Pwnage Hammer");
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.scale = 0.8f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 120;
            Projectile.extraUpdates = 2;
            AIType = 301;
        }
        
        public override void AI()
        {
        	Projectile.velocity.X *= 1.05f;
        	Projectile.velocity.Y *= 1.05f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
    }
}