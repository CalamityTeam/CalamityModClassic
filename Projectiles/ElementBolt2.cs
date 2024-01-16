using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ElementBolt2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Element Bolt");
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.extraUpdates = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 30;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
			for (int num447 = 0; num447 < 1; num447++)
			{
				Vector2 vector33 = Projectile.position;
				vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
				Projectile.alpha = 255;
				int num448 = Dust.NewDust(vector33, 1, 1, 66, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.25f);
				Main.dust[num448].noGravity = true;
				Main.dust[num448].position = vector33;
				Dust expr_13A3E_cp_0 = Main.dust[num448];
				expr_13A3E_cp_0.position.X = expr_13A3E_cp_0.position.X + (float)(Projectile.width / 2);
				Dust expr_13A62_cp_0 = Main.dust[num448];
				expr_13A62_cp_0.position.Y = expr_13A62_cp_0.position.Y + (float)(Projectile.height / 2);
				Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.007f;
				Main.dust[num448].velocity *= 0.2f;
			}
			return;
        }
    }
}