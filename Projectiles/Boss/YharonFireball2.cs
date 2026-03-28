using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class YharonFireball2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon Fireball");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
			CooldownSlot = 1;
		}

        public override void AI()
        {
            if (Projectile.velocity.Y < 0f)
            {
                Projectile.velocity.Y *= 0.97f;
            }
            else
            {
                Projectile.velocity.Y *= 1.03f;
                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }
            }
            if (Projectile.velocity.Y > -1f && Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = 1f;
                Projectile.velocity.Y = 1f;
            }
            Projectile.velocity.X *= 0.995f;
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }
            if (Projectile.ai[0] >= 2f)
            {
                Projectile.alpha -= 25;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            if (Main.rand.Next(16) == 0)
            {
                Dust expr_733B = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 55, 0f, 0f, 200, default(Color), 1f);
                expr_733B.scale *= 0.7f;
                expr_733B.velocity += Projectile.velocity * 0.25f;
            }
            if (Main.rand.Next(12) == 0 && Projectile.oldPos[9] != Vector2.Zero)
            {
                Dust expr_73D4 = Dust.NewDustDirect(Projectile.oldPos[9], Projectile.width, Projectile.height, 55, 0f, 0f, 50, default(Color), 1f);
                expr_73D4.scale *= 0.85f;
                expr_73D4.velocity += Projectile.velocity * 0.15f;
                expr_73D4.color = Color.Purple;
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] == 36f)
            {
                Projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)Projectile.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
                    int num9 = Dust.NewDust(Projectile.Center, 0, 0, 55, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].position = Projectile.Center + vector3;
                    Main.dust[num9].velocity = Projectile.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
                }
            }
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			if (Projectile.velocity.Y < -16f)
			{
				return false;
			}
			return true;
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, Main.DiscoG, 53, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14.WithVolumeScale(0.5f), Projectile.position);
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 144);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num193 = 0; num193 < 2; num193++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 100, default(Color), 1.5f);
            }
            for (int num194 = 0; num194 < 20; num194++)
            {
                int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num195].noGravity = true;
                Main.dust[num195].velocity *= 3f;
                num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num195].velocity *= 2f;
                Main.dust[num195].noGravity = true;
            }
            Projectile.Damage();
        }
    }
}