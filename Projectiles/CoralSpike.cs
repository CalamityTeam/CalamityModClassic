using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class CoralSpike : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Coral Spike");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 16;
            Projectile.scale = 0.75f;
            Projectile.friendly = true;
            Projectile.aiStyle = 14;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 360;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 0.9f;
        	Projectile.velocity.Y *= 0.99f;
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	int num199 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water_Desert, 0f, 0f, 100, default(Color), 1f);
			Dust expr_8976_cp_0 = Main.dust[num199];
			expr_8976_cp_0.position.X = expr_8976_cp_0.position.X - 2f;
			Dust expr_8994_cp_0 = Main.dust[num199];
			expr_8994_cp_0.position.Y = expr_8994_cp_0.position.Y + 2f;
			Main.dust[num199].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[num199].noGravity = true;
			Dust expr_89E7_cp_0 = Main.dust[num199];
			expr_89E7_cp_0.velocity.Y = expr_89E7_cp_0.velocity.Y - 2f;
			if (Main.rand.NextBool(2))
			{
				int num200 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water_Desert, 0f, 0f, 100, default(Color), 1f);
				Dust expr_8A4E_cp_0 = Main.dust[num200];
				expr_8A4E_cp_0.position.X = expr_8A4E_cp_0.position.X - 2f;
				Dust expr_8A6C_cp_0 = Main.dust[num200];
				expr_8A6C_cp_0.position.Y = expr_8A6C_cp_0.position.Y + 2f;
				Main.dust[num200].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
				Main.dust[num200].noGravity = true;
				Main.dust[num200].velocity *= 0.1f;
			}
        }
    }
}