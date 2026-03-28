using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class AuraRain : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aura Rain");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 45;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.scale = 1.1f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 1;
            AIType = 239;
        }

		public override void AI()
		{
			Dust dust4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 14, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f)];
			dust4.velocity = Vector2.Zero;
			dust4.position -= Projectile.velocity / 5f;
			dust4.noGravity = true;
			dust4.scale = 0.8f;
			dust4.noLight = true;
		}
    }
}