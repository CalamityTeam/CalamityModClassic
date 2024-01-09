﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class HolyLaser : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Laser");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 220;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
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
				for (int num447 = 0; num447 < 2; num447++)
				{
					Vector2 vector33 = Projectile.position;
					vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
					Projectile.alpha = 255;
					int num249 = Main.rand.Next(2);
					if (num249 == 0)
					{
						num249 = 244;
					}
					else
					{
						num249 = 246;
					}
					int num448 = Dust.NewDust(vector33, 1, 1, num249, 0f, 0f, 0, default(Color), 0.25f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[num448].velocity *= 0.2f;
				}
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
        	if (Projectile.owner == Main.myPlayer)
        	{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
		}
    }
}