using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class AstrealArrow : ModProjectile
    {
    	public int flameTimer = 120;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Astreal Arrow");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
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
			int random = Main.rand.Next(1, 5);
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
            if (Main.rand.NextBool(5))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.ShadowbeamStaff, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (flameTimer == 0)
			{
            	if (Projectile.owner == Main.myPlayer)
            	{
					int explosion = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, 659, (int)((double)Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[explosion].timeLeft = 180;
            	}
				flameTimer = 120;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.ShadowbeamStaff, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.ShadowFlame, 360);
        }
    }
}