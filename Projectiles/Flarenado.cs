using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Flarenado : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Flarenado");
            Projectile.width = 150;
            Projectile.height = 42;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 400;
            Main.projFrames[Projectile.type] = 6;
        }
        
        public override void AI()
        {
        	int num613 = 10;
			int num614 = 15;
			float num615 = 1.5f;
			int num616 = 150;
			int num617 = 42;
			if (Main.rand.Next(25) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
			if (Projectile.velocity.X != 0f)
			{
				Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.scale = ((float)(num613 + num614) - Projectile.ai[1]) * num615 / (float)(num614 + num613);
				Projectile.width = (int)((float)num616 * Projectile.scale);
				Projectile.height = (int)((float)num617 * Projectile.scale);
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[1] != -1f)
			{
				Projectile.scale = ((float)(num613 + num614) - Projectile.ai[1]) * num615 / (float)(num614 + num613);
				Projectile.width = (int)((float)num616 * Projectile.scale);
				Projectile.height = (int)((float)num617 * Projectile.scale);
			}
			if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.alpha -= 30;
				if (Projectile.alpha < 60)
				{
					Projectile.alpha = 60;
				}
			}
			else
			{
				Projectile.alpha += 30;
				if (Projectile.alpha > 150)
				{
					Projectile.alpha = 150;
				}
			}
			if (Projectile.ai[0] > 0f)
			{
				Projectile.ai[0] -= 1f;
			}
			if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
			{
				Projectile.netUpdate = true;
				Vector2 center = Projectile.Center;
				center.Y -= (float)num617 * Projectile.scale / 2f;
				float num618 = ((float)(num613 + num614) - Projectile.ai[1] + 1f) * num615 / (float)(num614 + num613);
				center.Y -= (float)num617 * num618 / 2f;
				center.Y += 2f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), center.X, center.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
			}
			if (Projectile.ai[0] <= 0f)
			{
				float num622 = 0.104719758f;
				float num623 = (float)Projectile.width / 5f;
				//num623 *= 2f;
				float num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X - num624 * (float)(-(float)Projectile.direction);
				Projectile.ai[0] -= 1f;
				num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X + num624 * (float)(-(float)Projectile.direction);
				return;
			}
        }
    }
}