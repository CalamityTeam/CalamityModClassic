﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Light : ModProjectile
    {
    	public int speedTimer = 120;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Light");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 27;
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
    }
}