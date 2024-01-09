﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class FrigidflashBoltProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ball");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 5;
            Projectile.timeLeft /= 2;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.02f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f);
        	for (int num92 = 0; num92 < 5; num92++)
			{
				float num93 = Projectile.velocity.X / 3f * (float)num92;
				float num94 = Projectile.velocity.Y / 3f * (float)num92;
				int num95 = 4;
				int num96 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num95, Projectile.position.Y + (float)num95), Projectile.width - num95 * 2, Projectile.height - num95 * 2, DustID.InfernoFork, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num96].noGravity = true;
				Main.dust[num96].velocity *= 0.1f;
				Main.dust[num96].velocity += Projectile.velocity * 0.1f;
				Dust expr_47FA_cp_0 = Main.dust[num96];
				expr_47FA_cp_0.position.X = expr_47FA_cp_0.position.X - num93;
				Dust expr_4815_cp_0 = Main.dust[num96];
				expr_4815_cp_0.position.Y = expr_4815_cp_0.position.Y - num94;
			}
			if (Main.rand.NextBool(5))
			{
				int num97 = 4;
				int num98 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num97, Projectile.position.Y + (float)num97), Projectile.width - num97 * 2, Projectile.height - num97 * 2, DustID.InfernoFork, 0f, 0f, 100, default(Color), 0.6f);
				Main.dust[num98].velocity *= 0.25f;
				Main.dust[num98].velocity += Projectile.velocity * 0.5f;
			}
			for (int num105 = 0; num105 < 5; num105++)
			{
				float num99 = Projectile.velocity.X / 3f * (float)num105;
				float num100 = Projectile.velocity.Y / 3f * (float)num105;
				int num101 = 4;
				int num102 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num101, Projectile.position.Y + (float)num101), Projectile.width - num101 * 2, Projectile.height - num101 * 2, DustID.Frost, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num102].noGravity = true;
				Main.dust[num102].velocity *= 0.1f;
				Main.dust[num102].velocity += Projectile.velocity * 0.1f;
				Dust expr_47FA_cp_0 = Main.dust[num102];
				expr_47FA_cp_0.position.X = expr_47FA_cp_0.position.X - num99;
				Dust expr_4815_cp_0 = Main.dust[num102];
				expr_4815_cp_0.position.Y = expr_4815_cp_0.position.Y - num100;
			}
			if (Main.rand.NextBool(5))
			{
				int num103 = 4;
				int num104 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num103, Projectile.position.Y + (float)num103), Projectile.width - num103 * 2, Projectile.height - num103 * 2, DustID.Frost, 0f, 0f, 100, default(Color), 0.6f);
				Main.dust[num104].velocity *= 0.25f;
				Main.dust[num104].velocity += Projectile.velocity * 0.5f;
			}
			if (Projectile.ai[1] >= 20f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			}
			else
			{
				Projectile.rotation += 0.3f * (float)Projectile.direction;
			}
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
				return;
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
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Frost, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.OnFire, 400);
        	target.AddBuff(BuffID.Frostburn, 400);
        }
    }
}