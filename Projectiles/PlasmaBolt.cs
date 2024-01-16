using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class PlasmaBolt : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Plasma Bolt");
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 200;
            Projectile.light = 0.05f;
        }

        public override void AI()
        {
			for (int num121 = 0; num121 < 5; num121++)
			{
				Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 107, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f)];
				dust.velocity = Vector2.Zero;
				dust.position -= Projectile.velocity / 5f * (float)num121;
				dust.noGravity = true;
				dust.scale = 0.5f;
				dust.noLight = true;
			}
        }
    }
}