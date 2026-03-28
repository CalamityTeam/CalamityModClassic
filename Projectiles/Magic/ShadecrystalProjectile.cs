using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class ShadecrystalProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystal");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.alpha = 50;
            Projectile.scale = 1.2f;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f);
			Projectile.rotation += Projectile.velocity.X * 0.2f;
			Projectile.ai[1] += 1f;
			if (Main.rand.Next(4) == 0)
			{
				int num300 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 70, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num300].noGravity = true;
				Main.dust[num300].velocity *= 0.5f;
				Main.dust[num300].scale *= 0.9f;
			}
			Projectile.velocity *= 0.985f;
			if (Projectile.ai[1] > 130f)
			{
				Projectile.scale -= 0.05f;
				if ((double)Projectile.scale <= 0.2)
				{
					Projectile.scale = 0.2f;
					Projectile.Kill();
					return;
				}
			}
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 70, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Frostburn, 100);
        }
    }
}