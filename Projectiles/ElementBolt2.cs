﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class ElementBolt2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bolt");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.extraUpdates = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 15;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 9f)
			{
				for (int num447 = 0; num447 < 1; num447++)
				{
					Vector2 vector33 = Projectile.position;
					vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
					Projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, 1, 1, DustID.RainbowTorch, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.25f);
					Main.dust[num448].noGravity = true;
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[num448].velocity *= 0.2f;
				}
				return;
			}
        }
    }
}