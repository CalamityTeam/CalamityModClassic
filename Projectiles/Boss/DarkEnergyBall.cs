using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class DarkEnergyBall : ModProjectile
    {
        private double timeElapsed = 0.0;
        private double circleSize = 1.0;
        private double circleGrowth = 0.02;

    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Energy");
            Main.projFrames[Projectile.type] = 6;
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
			CooldownSlot = 1;
		}

        public override void AI()
        {
            timeElapsed += 0.02;
            Projectile.velocity.X = (float)(Math.Sin(timeElapsed * (double)(0.5f * Projectile.ai[0])) * circleSize);
            Projectile.velocity.Y = (float)(Math.Cos(timeElapsed * (double)(0.5f * Projectile.ai[0])) * circleSize);
            circleSize += circleGrowth;
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 5)
            {
                Projectile.frame = 0;
            }
            int num1009 = 1;
            int num1010 = 60;
            for (int num1011 = 0; num1011 < 2; num1011++)
            {
                if (Main.rand.Next(3) < num1009)
                {
                    int num1012 = Dust.NewDust(Projectile.Center - new Vector2((float)num1010), num1010 * 2, num1010 * 2, 173, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 1.5f);
                    Main.dust[num1012].noGravity = true;
                    Main.dust[num1012].velocity *= 0.2f;
                    Main.dust[num1012].fadeIn = 1f;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 90, 0f, 0f);
            }
        }
    }
}