﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class PhantomShot2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Shot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 5;
			CooldownSlot = 1;
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

        public override void AI()
        {
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 9f)
			{
				Projectile.alpha -= 5;
				if (Projectile.alpha < 30)
				{
					Projectile.alpha = 30;
				}
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(250, 100, 100, Projectile.alpha);
        }
    }
}