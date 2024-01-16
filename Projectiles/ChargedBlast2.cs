using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ChargedBlast2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Charged Dart");
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.alpha = 255;
            Projectile.timeLeft = 200;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 3f)
			{
				for (int num468 = 0; num468 < 1; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 206, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
				for (int num92 = 0; num92 < 1; num92++)
				{
					int num96 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 160, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num96].noGravity = true;
					Main.dust[num96].velocity *= 0f;
				}
				for (int num105 = 0; num105 < 1; num105++)
				{
					int num102 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 226, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num102].noGravity = true;
					Main.dust[num102].velocity *= 0f;
				}
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 160, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	Projectile.Kill();
        }
    }
}