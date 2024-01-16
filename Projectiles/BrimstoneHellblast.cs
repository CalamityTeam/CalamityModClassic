using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BrimstoneHellblast : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Brimstone Hellblast");
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 255;
            Projectile.alpha = 0;
        }

        public override void AI()
        {
        	Projectile.alpha += 1;
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 235, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.9f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	Projectile.velocity.X *= 1.02f;
        	Projectile.velocity.Y *= 1.02f;
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

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        	for (int dust = 0; dust <= 30; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 235, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}