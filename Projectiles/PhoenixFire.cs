using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class PhoenixFire : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Phoenix Fire");
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 360;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 1.005f;
        	Projectile.velocity.Y *= 1.005f;
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.45f) / 255f, ((255 - Projectile.alpha) * 0.35f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
			for (int num457 = 0; num457 < 10; num457++)
			{
				int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 0.4f);
				Main.dust[num458].noGravity = true;
				Main.dust[num458].velocity *= 0.5f;
				Main.dust[num458].velocity += Projectile.velocity * 0.1f;
			}
			return;
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 4; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}