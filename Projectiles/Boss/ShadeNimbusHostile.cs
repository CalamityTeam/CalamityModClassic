using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class ShadeNimbusHostile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shade Nimbus");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 28;
            Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 8)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame > 5)
				{
					Projectile.frame = 0;
				}
			}
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] >= 300f)
			{
				Projectile.alpha += 5;
				if (Projectile.alpha > 255)
				{
					Projectile.alpha = 255;
					Projectile.Kill();
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
				if (Projectile.ai[0] > 8f)
				{
					Projectile.ai[0] = 0f;
					int num414 = (int)(Projectile.position.X + 14f + (float)Main.rand.Next(Projectile.width - 28));
					int num415 = (int)(Projectile.position.Y + (float)Projectile.height + 4f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(null), (float)num414, (float)num415, 0f, 5f, Mod.Find<ModProjectile>("ShaderainHostile").Type, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				}
			}
        }
    }
}