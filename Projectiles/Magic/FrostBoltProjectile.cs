using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class FrostBoltProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ball");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 2;
            Projectile.timeLeft /= 2;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.3f) / 255f, ((255 - Projectile.alpha) * 0.45f) / 255f);
        	for (int num105 = 0; num105 < 3; num105++)
			{
				float num99 = Projectile.velocity.X / 3f * (float)num105;
				float num100 = Projectile.velocity.Y / 3f * (float)num105;
				int num101 = 4;
				int num102 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num101, Projectile.position.Y + (float)num101), Projectile.width - num101 * 2, Projectile.height - num101 * 2, 92, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num102].noGravity = true;
				Main.dust[num102].velocity *= 0.1f;
				Main.dust[num102].velocity += Projectile.velocity * 0.1f;
				Dust expr_47FA_cp_0 = Main.dust[num102];
				expr_47FA_cp_0.position.X = expr_47FA_cp_0.position.X - num99;
				Dust expr_4815_cp_0 = Main.dust[num102];
				expr_4815_cp_0.position.Y = expr_4815_cp_0.position.Y - num100;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num103 = 4;
				int num104 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num103, Projectile.position.Y + (float)num103), Projectile.width - num103 * 2, Projectile.height - num103 * 2, 92, 0f, 0f, 100, default(Color), 0.6f);
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
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 92, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Frostburn, 60);
        }
    }
}