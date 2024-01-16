﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class CrimsonSpike : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Crimson Spike");
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (Projectile.alpha == 0 && Main.rand.Next(3) == 0)
			{
				int num67 = Dust.NewDust(Projectile.position - Projectile.velocity * 3f, Projectile.width, Projectile.height, 260, 0f, 0f, 50, new Color(255, 136, 78, 150), 1.2f);
				Main.dust[num67].velocity *= 0.3f;
				Main.dust[num67].velocity += Projectile.velocity * 0.3f;
				Main.dust[num67].noGravity = true;
			}
			Projectile.alpha -= 50;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
			}
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 5f)
			{
				Projectile.ai[0] = 5f;
				Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
			}
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(BuffID.Cursed, 30);
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 260, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}