using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class DestroyerHomingLaser : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Homing Laser");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.scale = 1.8f;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        	if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 125;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
            if (Projectile.localAI[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
            }
            Projectile.localAI[1] += 1f;
            if (Projectile.localAI[1] >= 120f)
            {
                int num103 = (int)Player.FindClosest(Projectile.Center, 1, 1);
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] < 360f)
                {
                    float scaleFactor2 = Projectile.velocity.Length();
                    Vector2 vector11 = Main.player[num103].Center - Projectile.Center;
                    vector11.Normalize();
                    vector11 *= scaleFactor2;
                    Projectile.velocity = (Projectile.velocity * 24f + vector11) / 25f;
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= scaleFactor2;
                }
            }
            if (Projectile.velocity.Length() < 18f)
            {
                Projectile.velocity *= 1.02f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.alpha < 200)
            {
                return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
            }
            return Color.Transparent;
        }
    }
}