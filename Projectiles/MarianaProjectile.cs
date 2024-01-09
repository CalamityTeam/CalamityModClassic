﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Projectiles
{
    public class MarianaProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //DisplayName.SetDefault("Mariana Projectile");
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 44;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            AIType = 228;
        }

        public override void AI()
        {
        	{
        		Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 1.0f) / 255f);
        	}
            Projectile.velocity.Y += Projectile.ai[0];
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 29, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.75f;
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 29, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}