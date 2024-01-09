﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class CosmicBolt : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bolt");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.extraUpdates = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
        	Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num441 = 0; num441 < 4; num441++)
				{
					Vector2 vector30 = Projectile.position;
					vector30 -= Projectile.velocity * ((float)num441 * 0.25f);
					Projectile.alpha = 255;
					int num442 = Dust.NewDust(vector30, 1, 1, DustID.TerraBlade, 0f, 0f, 0, default(Color), 0.7f);
					Main.dust[num442].position = vector30;
					Dust expr_13A3E_cp_0 = Main.dust[num442];
					expr_13A3E_cp_0.position.X = expr_13A3E_cp_0.position.X + (float)(Projectile.width / 2);
					Dust expr_13A62_cp_0 = Main.dust[num442];
					expr_13A62_cp_0.position.Y = expr_13A62_cp_0.position.Y + (float)(Projectile.height / 2);
					Main.dust[num442].scale = (float)Main.rand.Next(70, 110) * 0.007f;
					Main.dust[num442].velocity *= 0.2f;
				}
				return;
			}
        }
    }
}