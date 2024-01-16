using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BlueAura : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Blue Aura");
            Projectile.width = 20;
            Projectile.height = 20;
            AIType = 348;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 5; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color), 1.3f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
        }
    }
}