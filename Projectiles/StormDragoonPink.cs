using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class StormDragoonPink : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bullet");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.light = 0.5f;
            Projectile.alpha = 255;
			Projectile.extraUpdates = 2;
			Projectile.scale = 1.18f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 1;
            AIType = 14;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(250, 0, 150, Projectile.alpha);
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Pink, Projectile.oldVelocity.X * 0.05f, -10f * Main.rand.NextFloat());
            }
        }
    }
}