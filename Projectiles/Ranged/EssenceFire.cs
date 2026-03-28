using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class EssenceFire : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fire");
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
            Projectile.timeLeft = 50;
        }

        public override void AI()
        {
        	if (Projectile.scale <= 1.5f)
        	{
        		Projectile.scale *= 1.01f;
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.35f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.45f) / 255f);
			if (Projectile.timeLeft > 50)
			{
				Projectile.timeLeft = 50;
			}
			if (Projectile.ai[0] > 5f)
			{
				float num296 = 1f;
				if (Projectile.ai[0] == 6f)
				{
					num296 = 0.25f;
				}
				else if (Projectile.ai[0] == 7f)
				{
					num296 = 0.5f;
				}
				else if (Projectile.ai[0] == 8f)
				{
					num296 = 0.75f;
				}
				Projectile.ai[0] += 1f;
				int num297 = 173;
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 1; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if ((num297 == 173 && Main.rand.Next(3) == 0))
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
						if (num297 == 173)
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
        	target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 500);
        	target.immune[Projectile.owner] = 1;
		}
    }
}