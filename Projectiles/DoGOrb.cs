using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class DoGOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Orb");
			Main.projFrames[Projectile.type] = 2;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.scale = 0.8f;
            Projectile.alpha = 255;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.85f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.35f) / 255f);
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 2)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 1)
			{
			   Projectile.frame = 0;
			}
			Projectile.rotation += 1f;
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
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
        }
    }
}