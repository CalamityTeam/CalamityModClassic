﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class AtlantisSpear2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Atlantis Spear");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 52;
            Projectile.aiStyle = 4;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            AIType = 494;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
        	if (Projectile.ai[0] == 0f)
			{
        		Projectile.alpha -= 50;
        		if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.ai[0] = 1f;
					if (Projectile.ai[1] == 0f)
					{
						Projectile.ai[1] += 1f;
						Projectile.position += Projectile.velocity * 1f;
					}
        		}
        	}
        	else
			{
				if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
				{
					for (int num55 = 0; num55 < 8; num55++)
					{
						int num56 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, Projectile.velocity.X * 0.005f, Projectile.velocity.Y * 0.005f, 200, default(Color), 1f);
						Main.dust[num56].noGravity = true;
						Main.dust[num56].velocity *= 0.5f;
					}
				}
				Projectile.alpha += 7;
				if (Projectile.alpha >= 255)
				{
					Projectile.Kill();
					return;
				}
        	}
            if (Main.rand.NextBool(4))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, Projectile.velocity.X * 0.005f, Projectile.velocity.Y * 0.005f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, Projectile.oldVelocity.X * 0.005f, Projectile.oldVelocity.Y * 0.005f);
            }
        }
    }
}