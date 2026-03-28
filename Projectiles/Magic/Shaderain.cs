using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class Shaderain : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rain");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 3;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.alpha = 50;
			return;
        }
        
        public override void OnKill(int timeLeft)
        {
            int num310 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + (float)Projectile.height - 2f), 2, 2, 14, 0f, 0f, 0, default(Color), 1f);
			Dust expr_A0A0_cp_0 = Main.dust[num310];
			expr_A0A0_cp_0.position.X = expr_A0A0_cp_0.position.X - 2f;
			Main.dust[num310].alpha = 38;
			Main.dust[num310].velocity *= 0.1f;
			Main.dust[num310].velocity += -Projectile.oldVelocity * 0.25f;
			Main.dust[num310].scale = 0.95f;
        }
    }
}