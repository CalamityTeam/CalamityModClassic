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
    public class PhantomHookShot : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantom Hook Shot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.hostile = true;
            Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.extraUpdates = 2;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			CooldownSlot = 1;
        }

        public override void AI()
        {
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] == 6f)
			{
				for (int num151 = 0; num151 < 40; num151++)
				{
					int num152 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 180, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num152].velocity *= 3f;
					Main.dust[num152].velocity += Projectile.velocity * 0.75f;
					Main.dust[num152].scale *= 1.2f;
					Main.dust[num152].noGravity = true;
				}
			}
			if (Projectile.localAI[0] > 9f)
			{
				Projectile.alpha -= 5;
				if (Projectile.alpha < 30)
				{
					Projectile.alpha = 30;
				}
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(100, 250, 250, Projectile.alpha);
        }
    }
}