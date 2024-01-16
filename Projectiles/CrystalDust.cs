using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class CrystalDust : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Crystal Dust");
            Projectile.width = 80;
            Projectile.height = 46;
            AIType = 348;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 100;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.45f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
	            int num307 = Main.rand.Next(3);
				if (num307 == 0)
				{
					num307 = 173;
				}
				else if (num307 == 1)
				{
					num307 = 57;
				}
				else
				{
					num307 = 58;
				}
				for (int num468 = 0; num468 < 5; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num307, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
        }
    }
}