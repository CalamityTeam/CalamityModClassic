using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BrimstoneBarrage : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Brimstone Barrage");
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Main.projFrames[Projectile.type] = 5;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 1.005f;
        	Projectile.velocity.Y *= 1.005f;
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 5)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 4)
			{
			   Projectile.frame = 0;
			}
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
			for (int num151 = 0; num151 < 1; num151++)
			{
				float num152 = Projectile.velocity.X / 3f * (float)num151;
				float num153 = Projectile.velocity.Y / 3f * (float)num151;
				int num154 = 14;
				int num155 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num154, Projectile.position.Y + (float)num154), Projectile.width - num154 * 2, Projectile.height - num154 * 2, 235, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[num155].noGravity = true;
				Main.dust[num155].velocity *= 0.1f;
				Main.dust[num155].velocity += Projectile.velocity * 0.5f;
				Dust expr_6A04_cp_0 = Main.dust[num155];
				expr_6A04_cp_0.position.X = expr_6A04_cp_0.position.X - num152;
				Dust expr_6A1F_cp_0 = Main.dust[num155];
				expr_6A1F_cp_0.position.Y = expr_6A1F_cp_0.position.Y - num153;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int dust = 0; dust <= 10; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 235, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}