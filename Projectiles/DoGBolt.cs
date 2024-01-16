using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DoGBolt : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Bolt");
            Projectile.width = 4;
            Projectile.height = 64;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 226, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.65f) / 255f);
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item122, Projectile.position);
			}
        	Projectile.velocity.X *= 1.015f;
        	Projectile.velocity.Y *= 1.015f;
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
        }

        public override void OnKill(int timeLeft)
        {
        	for (int dust = 0; dust <= 10; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 226, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}