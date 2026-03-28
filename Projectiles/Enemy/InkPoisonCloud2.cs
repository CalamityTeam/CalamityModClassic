using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Enemy
{
    public class InkPoisonCloud2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cloud");
            Main.projFrames[Projectile.type] = 4;
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 3600;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 9)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
            Projectile.velocity *= 0.995f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 120f)
			{
				if (Projectile.alpha < 255)
				{
					Projectile.alpha += 5;
					if (Projectile.alpha > 255)
					{
						Projectile.alpha = 255;
						return;
					}
				}
				else if (Projectile.owner == Main.myPlayer)
				{
					Projectile.Kill();
					return;
				}
			}
			else if (Projectile.alpha > 80)
			{
				Projectile.alpha -= 30;
				if (Projectile.alpha < 80)
				{
					Projectile.alpha = 80;
					return;
				}
			}
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Venom, 300);
            target.AddBuff(BuffID.Darkness, 600, true);
        }
    }
}