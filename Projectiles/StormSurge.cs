using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class StormSurge : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Storm Surge");
            Projectile.width = 40;
            Projectile.height = 74;
            Projectile.scale = 0.5f;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Main.projFrames[Projectile.type] = 6;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 1.25f) / 255f, ((255 - Projectile.alpha) * 1.25f) / 255f);
        	if (Projectile.scale <= 2f)
        	{
        		Projectile.scale *= 1.03f;
        	}
        	if (Projectile.scale >= 2f)
        	{
        		Projectile.Kill();
        	}
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
    }
}