﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DemonicPitchfork : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Demonic Pitchfork");
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            AIType = 114;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.2f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.2f) / 255f);
            if (Projectile.localAI[1] > 7f)
			{
				int num307 = Main.rand.Next(3);
				if (num307 == 0)
				{
					num307 = 14;
				}
				else if (num307 == 1)
				{
					num307 = 27;
				}
				else
				{
					num307 = 173;
				}
				int num308 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, num307, 0f, 0f, 100, default(Color), 1.25f);
				Main.dust[num308].velocity *= 0.1f;
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 14, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 27, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 173, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}