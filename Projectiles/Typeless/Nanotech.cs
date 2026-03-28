using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Typeless
{
    public class Nanotech : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nanotech");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
			Lighting.AddLight(Projectile.Center, new Vector3(0.075f, 0.4f, 0.15f));
			Projectile.rotation += Projectile.velocity.X * 0.2f;
			if (Projectile.velocity.X > 0f) 
			{
				Projectile.rotation += 0.08f;
			} 
			else 
			{
				Projectile.rotation -= 0.08f;
			}
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] > 30f) 
			{
				Projectile.alpha += 5;
				if (Projectile.alpha >= 255) 
				{
					Projectile.alpha = 255;
					Projectile.Kill();
					return;
				}
			}
            float num472 = Projectile.Center.X;
            float num473 = Projectile.Center.Y;
            float num474 = 600f;
            bool flag17 = false;
            for (int num475 = 0; num475 < 200; num475++)
            {
                if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
                {
                    float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
                    float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
                    float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
                    if (num478 < num474)
                    {
                        num474 = num478;
                        num472 = num476;
                        num473 = num477;
                        flag17 = true;
                    }
                }
            }
            if (flag17)
            {
                float num483 = 20f;
                Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num484 = num472 - vector35.X;
                float num485 = num473 - vector35.Y;
                float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
                num486 = num483 / num486;
                num484 *= num486;
                num485 *= num486;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
                return;
            }
        }
        
        public override void OnKill(int timeLeft)
        {
			int num3;
			for (int num191 = 0; num191 < 2; num191 = num3 + 1)
			{
				int num192 = (int)(10f * Projectile.scale);
				int num193 = Dust.NewDust(Projectile.Center - Vector2.One * (float)num192, num192 * 2, num192 * 2, 107, 0f, 0f, 0, default(Color), 1f);
				Dust dust20 = Main.dust[num193];
				Vector2 value8 = Vector2.Normalize(dust20.position - Projectile.Center);
				dust20.position = Projectile.Center + value8 * (float)num192 * Projectile.scale;
				if (num191 < 30)
				{
					dust20.velocity = value8 * dust20.velocity.Length();
				}
				else
				{
					dust20.velocity = value8 * (float)Main.rand.Next(45, 91) / 10f;
				}
				dust20.color = Main.hslToRgb((float)(0.40000000596046448 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
				dust20.color = Color.Lerp(dust20.color, Color.White, 0.3f);
				dust20.noGravity = true;
				dust20.scale = 0.7f;
				num3 = num191;
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(0, 255 - Projectile.alpha, 0, 0);
        }
    }
}