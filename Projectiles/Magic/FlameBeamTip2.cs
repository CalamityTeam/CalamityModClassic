using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class FlameBeamTip2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 4;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	if (Projectile.ai[0] == 0f)
			{
        		Projectile.alpha -= 50;
        		if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.ai[0] = 1f;
					if (Projectile.ai[1] == 0f)
					{
						Projectile.ai[1] += 1f;
						Projectile.position += Projectile.velocity * 1f;
					}
        		}
        	}
        	else
			{
				if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
				{
					for (int num55 = 0; num55 < 8; num55++)
					{
						int num56 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 60, Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f, 200, default(Color), 1f);
						Main.dust[num56].noGravity = true;
						Main.dust[num56].velocity *= 0.5f;
					}
				}
				Projectile.alpha += 7;
				if (Projectile.alpha >= 255)
				{
					Projectile.Kill();
					return;
				}
        	}
            if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 60, 0f, 0f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 60, 0f, 0f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 8;
        	target.AddBuff(BuffID.OnFire, 240);
        }
    }
}