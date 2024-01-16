using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Crystalline2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Crystalline");
            Projectile.width = 38;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.scale = 0.75f;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 113;
            Projectile.timeLeft = 30;
            AIType = 598;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 154, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}