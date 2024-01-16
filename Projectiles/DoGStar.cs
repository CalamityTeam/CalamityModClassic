using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DoGStar : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Star");
            Projectile.width = 60;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 242, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.6f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item105, Projectile.position);
			}
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
        	for (int dust = 0; dust <= 10; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 242, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}