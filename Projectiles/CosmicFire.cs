﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class CosmicFire : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fire");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 90;
        }

        public override void AI()
        {
        	if (Projectile.scale <= 1.5f)
        	{
        		Projectile.scale *= 1.01f;
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
			if (Projectile.timeLeft > 90)
			{
				Projectile.timeLeft = 90;
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
				int num297 = 58;
				if (Main.rand.NextBool(2))
				{
					for (int num298 = 0; num298 < 1; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if ((num297 == 58 && Main.rand.NextBool(3)))
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
							Main.dust[num299].scale *= 1.5f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
						if (num297 == 58)
						{
							Main.dust[num299].velocity += Projectile.velocity;
							if (!Main.dust[num299].noGravity)
							{
								Main.dust[num299].velocity *= 0.5f;
							}
						}
					}
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
			}
			Projectile.rotation += 0.3f * (float)Projectile.direction;
			return;	
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 4;
        	target.AddBuff(BuffID.CursedInferno, 500);
        }
    }
}