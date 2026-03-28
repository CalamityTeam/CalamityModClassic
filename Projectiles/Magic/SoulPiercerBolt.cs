using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class SoulPiercerBolt : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Piercer");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.extraUpdates = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
			for (int num441 = 0; num441 < 2; num441++)
			{
				Vector2 vector30 = Projectile.position;
				vector30 -= Projectile.velocity * ((float)num441 * 0.25f);
				Projectile.alpha = 255;
				int num442 = Dust.NewDust(vector30, 1, 1, 173, 0f, 0f, 0, default(Color), 1f);
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