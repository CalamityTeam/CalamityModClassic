﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class PoisonousSeawater : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Poisonous Seawater");
            Projectile.width = 30;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.4f) / 255f);
        	int randomDust = Main.rand.Next(4);
			if (randomDust == 0)
			{
				randomDust = 33;
			}
			else if (randomDust == 1)
			{
				randomDust = 33;
			}
			else if (randomDust == 2)
			{
				randomDust = 33;
			}
			else
			{
				randomDust = 89;
			}
            Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 5; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, randomDust, 0f, 0f, 100, default(Color), 1f);
					if (randomDust == 89)
					{
						Main.dust[num469].scale *= 0.35f;
					}
					Main.dust[num469].velocity *= 0f;
				}
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Venom, 120);
        	target.AddBuff(BuffID.Poisoned, 120);
        }
    }
}