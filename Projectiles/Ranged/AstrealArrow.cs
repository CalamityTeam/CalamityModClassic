using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class AstrealArrow : ModProjectile
    {
    	public int flameTimer = 180;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astreal Arrow");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        
        public override void AI()
        {
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
			int random = Main.rand.Next(1, 4);
			flameTimer -= random;
        	int choice = Main.rand.Next(2);
        	Projectile.velocity.X *= 1.05f;
        	Projectile.velocity.Y *= 1.05f;
        	if (choice == 0 && (Projectile.velocity.X >= 25f || Projectile.velocity.Y >= 25f))
        	{
        		Projectile.velocity.X = 0f;
        		Projectile.velocity.Y = 10f;
        	}
        	else if (choice == 1 && (Projectile.velocity.X >= 25f || Projectile.velocity.Y >= 25f))
        	{
        		Projectile.velocity.X = 10f;
        		Projectile.velocity.Y = 0f;
        	}
        	else if (choice == 0 && (Projectile.velocity.X <= -25f || Projectile.velocity.Y <= -25f))
        	{
        		Projectile.velocity.X = 0f;
        		Projectile.velocity.Y = -10f;
        	}
        	else if (choice == 1 && (Projectile.velocity.X <= -25f || Projectile.velocity.Y <= -25f))
        	{
        		Projectile.velocity.X = -10f;
        		Projectile.velocity.Y = 0f;
        	}
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 173, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (flameTimer <= 0)
			{
                float xPos = (Main.rand.Next(2) == 0 ? Projectile.position.X + 800 : Projectile.position.X - 800);
                Vector2 vector2 = new Vector2(xPos, Projectile.position.Y + Main.rand.Next(-800, 801));
                float num80 = xPos;
                float speedX = (float)Projectile.position.X - vector2.X;
                float speedY = (float)Projectile.position.Y - vector2.Y;
                float dir = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                dir = 10 / num80;
                speedX *= dir * 150;
                speedY *= dir * 150;
                if (speedX > 15f)
                {
                    speedX = 15f;
                }
                if (speedX < -15f)
                {
                    speedX = -15f;
                }
                if (speedY > 15f)
                {
                    speedY = 15f;
                }
                if (speedY < -15f)
                {
                    speedY = -15f;
                }
                if (Projectile.owner == Main.myPlayer)
            	{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX, speedY, Mod.Find<ModProjectile>("AstrealFlame").Type, (int)((double)Projectile.damage * 0.5), Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
				flameTimer = 180;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 173, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.ShadowFlame, 360);
        }
    }
}