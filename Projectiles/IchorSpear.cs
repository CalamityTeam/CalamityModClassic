using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class IchorSpear : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Ichor Spear");
            Projectile.width = 52;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 113;
            Projectile.timeLeft = 600;
            AIType = 598;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 246, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int i = 0; i <= 10; i++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 246, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Ichor, 120);
        }
    }
}