using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class IceRain : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Rain");
		}
    	
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
        }

        public override void AI()
        {
        	Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			if (Projectile.localAI[0] == 0f || Projectile.localAI[0] == 2f)
			{
				Projectile.scale += 0.01f;
				Projectile.alpha -= 50;
				if (Projectile.alpha <= 0)
				{
					Projectile.localAI[0] = 1f;
					Projectile.alpha = 0;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale -= 0.01f;
				Projectile.alpha += 50;
				if (Projectile.alpha >= 255)
				{
					Projectile.localAI[0] = 2f;
					Projectile.alpha = 255;
				}
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(200, 200, 200, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			int num3;
			for (int num373 = 0; num373 < 3; num373 = num3 + 1)
			{
				int num374 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 76, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num374].noGravity = true;
				Main.dust[num374].noLight = true;
				Main.dust[num374].scale = 0.7f;
				num3 = num373;
			}
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, 60, true);
            target.AddBuff(BuffID.Chilled, 30, true);
        }
    }
}