using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DracoBeam2 : ModProjectile
    {
    	public int start = 60;
    	public int speedTimer = 120;
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Draco Beam");
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
        	start--;
        	if (start <= 0)
        	{
        		speedTimer--;
        		if (speedTimer > 60)
        		{
        			Projectile.velocity.X = 0f;
        			Projectile.velocity.Y = 10f;
        		}
        		else if (speedTimer <= 60)
        		{
        			Projectile.velocity.X = 0f;
        			Projectile.velocity.Y = -10f;
        		}
        		if (speedTimer <= 0)
        		{
        			speedTimer = 120;
        		}
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.75f) / 255f);
            if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 500);
            target.AddBuff(BuffID.OnFire, 500);
        }
    }
}