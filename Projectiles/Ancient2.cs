﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Ancient2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ancient");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 12;
            Projectile.timeLeft = 30;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 2;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.6f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
			if (Projectile.timeLeft > 30)
			{
				Projectile.timeLeft = 30;
			}
			if (Projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (Projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (Projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (Projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				Projectile.ai[0] += 1f;
				int num297 = 32;
				for (int num298 = 0; num298 < 2; num298++)
				{
					int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					if ((num297 == 32 && Main.rand.NextBool(2)))
					{
						Main.dust[num299].noGravity = true;
						Main.dust[num299].scale *= 1.5f;
						Dust expr_DBEF_cp_0 = Main.dust[num299];
						expr_DBEF_cp_0.velocity.X = expr_DBEF_cp_0.velocity.X * 3f;
						Dust expr_DC0F_cp_0 = Main.dust[num299];
						expr_DC0F_cp_0.velocity.Y = expr_DC0F_cp_0.velocity.Y * 3f;
					}
					Dust expr_DC74_cp_0 = Main.dust[num299];
					expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 2f;
					Dust expr_DC94_cp_0 = Main.dust[num299];
					expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 2f;
					Main.dust[num299].scale *= num296;
				}
				for (int num298 = 0; num298 < 2; num298++)
				{
					int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					if ((num297 == 32 && Main.rand.NextBool(3)))
					{
						Main.dust[num299].noGravity = true;
						Main.dust[num299].scale *= 3f;
						Dust expr_DBEF_cp_0 = Main.dust[num299];
						expr_DBEF_cp_0.velocity.X = expr_DBEF_cp_0.velocity.X * 2f;
						Dust expr_DC0F_cp_0 = Main.dust[num299];
						expr_DC0F_cp_0.velocity.Y = expr_DC0F_cp_0.velocity.Y * 2f;
					}
					else
					{
						Main.dust[num299].scale *= 2f;
					}
					Dust expr_DC74_cp_0 = Main.dust[num299];
					expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 1.2f;
					Dust expr_DC94_cp_0 = Main.dust[num299];
					expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 1.2f;
					Main.dust[num299].scale *= num296;
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
			}
			Projectile.rotation += 0.3f * (float)Projectile.direction;
			return;	
        }
    }
}