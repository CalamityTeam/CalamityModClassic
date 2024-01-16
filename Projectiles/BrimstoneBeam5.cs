﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BrimstoneBeam5 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Brimstone Beam");
            Projectile.width = 2;
            Projectile.height = 2;
            //projectile.aiStyle = 48;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 20;
        }

        public override void AI()
        {
        	if (Projectile.velocity.X != Projectile.velocity.X)
			{
				Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
				Projectile.velocity.X = -Projectile.velocity.X;
			}
			if (Projectile.velocity.Y != Projectile.velocity.Y)
			{
				Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
				Projectile.velocity.Y = -Projectile.velocity.Y;
			}
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 9f)
			{
				for (int num447 = 0; num447 < 1; num447++)
				{
					Vector2 vector33 = Projectile.position;
					vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
					Projectile.alpha = 255;
					int num249 = 235;
					int num448 = Dust.NewDust(vector33, 1, 1, num249, 0f, 0f, 0, default(Color), 1.5f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].velocity *= 0.1f;
					Main.dust[num448].noGravity = true;
				}
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
        }
    }
}