using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
	public class GleamingBolt2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bolt");
		}

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.alpha = 255;
			Projectile.timeLeft = 120;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override void AI()
		{
			Projectile.velocity.X *= 0.985f;
			Projectile.velocity.Y *= 0.985f;
			for (int dust = 0; dust < 2; dust++)
			{
				int randomDust = Main.rand.Next(2);
				if (randomDust == 0)
				{
					randomDust = 64;
				}
				else
				{
					randomDust = 204;
				}
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, randomDust, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}

		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 3; k++)
			{
				int randomDust = Main.rand.Next(2);
				if (randomDust == 0)
				{
					randomDust = 64;
				}
				else
				{
					randomDust = 204;
				}
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, randomDust, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}
	}
}