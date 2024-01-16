using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DoGNebulaShot : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Nebula Shot");
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.2f) / 255f);
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
			}
        	if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 15;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
			for (int num121 = 0; num121 < 5; num121++)
			{
				Dust dust4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 242, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 0.6f)];
				dust4.velocity = Vector2.Zero;
				dust4.position -= Projectile.velocity / 5f * (float)num121;
				dust4.noGravity = true;
				dust4.scale = 0.6f;
				dust4.noLight = true;
			}
        }
    }
}