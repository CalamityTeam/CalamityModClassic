using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class FlareBat : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Flare Bat");
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Main.projFrames[Projectile.type] = 5;
            Projectile.timeLeft = 300;
            Projectile.light = 0.25f;
        }

        public override void AI()
        {
        	int num103 = (int)Player.FindClosest(Projectile.Center, 1, 1);
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] < 110f && Projectile.ai[1] > 30f)
			{
				float scaleFactor2 = Projectile.velocity.Length();
				Vector2 vector11 = Main.player[num103].Center - Projectile.Center;
				vector11.Normalize();
				vector11 *= scaleFactor2;
				Projectile.velocity = (Projectile.velocity * 24f + vector11) / 25f;
				Projectile.velocity.Normalize();
				Projectile.velocity *= scaleFactor2;
			}
			if (Projectile.ai[0] < 0f)
			{
				if (Projectile.velocity.Length() < 18f)
				{
					Projectile.velocity *= 1.02f;
				}
			}
			int num192 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 1f);
			Main.dust[num192].noGravity = true;
			Main.dust[num192].velocity *= 0.2f;
			Main.dust[num192].position = (Main.dust[num192].position + Projectile.Center) / 2f;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame >= 5)
				{
					Projectile.frame = 0;
				}
			}
			if (Projectile.velocity.X < 0f)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2((double)(-(double)Projectile.velocity.Y), (double)(-(double)Projectile.velocity.X));
			}
			else
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
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
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}