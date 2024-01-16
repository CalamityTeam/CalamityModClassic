using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class WhiteOrb : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("White Orb");
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Melee;
        }
        
        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.2f) / 255f, ((255 - Projectile.alpha) * 0.2f) / 255f, ((255 - Projectile.alpha) * 0.2f) / 255f);
			for (int num457 = 0; num457 < 10; num457++)
			{
				int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 91, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[num458].noGravity = true;
				Main.dust[num458].velocity *= 0.5f;
				Main.dust[num458].velocity += Projectile.velocity * 0.1f;
			}
			return;
        }
    }
}