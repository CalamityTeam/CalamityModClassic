using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Typeless
{
    public class NebulaDust : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dust");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 3600;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 3;
        }

        public override void AI()
        {
        	Projectile.rotation += Projectile.velocity.X * 0.02f;
			if (Projectile.velocity.X < 0f)
			{
				Projectile.rotation -= Math.Abs(Projectile.velocity.Y) * 0.02f;
			}
			else
			{
				Projectile.rotation += Math.Abs(Projectile.velocity.Y) * 0.02f;
			}
			Projectile.velocity *= 0.98f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 60f)
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
    }
}