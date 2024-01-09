using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Nanotech : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Nanotech");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.extraUpdates = 0;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
			Lighting.AddLight(Projectile.Center, new Vector3(0.075f, 0.4f, 0.15f));
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			Projectile.velocity.X *= 0.8f;
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
				Projectile.alpha += 10;
				if (Projectile.alpha >= 255) 
				{
					Projectile.alpha = 255;
					Projectile.Kill();
					return;
				}
			}
        }
        
        public override void OnKill(int timeLeft)
        {
			Vector2 arg_6751_0 = Projectile.Center;
			int num3;
			for (int num191 = 0; num191 < 5; num191 = num3 + 1)
			{
				int num192 = (int)(10f * Projectile.scale);
				int num193 = Dust.NewDust(Projectile.Center - Vector2.One * (float)num192, num192 * 2, num192 * 2, DustID.TerraBlade, 0f, 0f, 0, default(Color), 1f);
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