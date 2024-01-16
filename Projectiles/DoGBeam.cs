using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DoGBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("DoG Laser");
            Projectile.width = 2;
            Projectile.height = 32;
            Projectile.hostile = true;
            Projectile.scale = 2f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 1.005f;
        	Projectile.velocity.Y *= 1.005f;
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 226, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.35f) / 255f, ((255 - Projectile.alpha) * 0.45f) / 255f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale -= 0.02f;
				Projectile.alpha += 30;
				if (Projectile.alpha >= 250)
				{
					Projectile.alpha = 255;
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 30;
				if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.localAI[0] = 0f;
				}
			}
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
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 226, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(BuffID.Frozen, 30);
        }
    }
}