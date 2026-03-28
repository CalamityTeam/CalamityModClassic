using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class Dark : ModProjectile
    {
    	public int speedTimer = 120;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
        	Projectile.rotation += 0.5f;
        	speedTimer--;
        	if (speedTimer > 60)
        	{
        		Projectile.velocity.X = 10f;
        		Projectile.velocity.Y = 0f;
        	}
        	else if (speedTimer <= 60)
        	{
        		Projectile.velocity.X = -10f;
        		Projectile.velocity.Y = 0f;
        	}
        	if (speedTimer <= 0)
        	{
        		speedTimer = 120;
        	}
        }
    }
}