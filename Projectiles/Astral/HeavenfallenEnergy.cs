using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
	public class HeavenfallenEnergy : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heavenfallen Energy");
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

		public override void AI()
		{
			int num154 = 14;
			int coolDust;
			Projectile.ai[0] += 1;
			if (Projectile.ai[0] % 2 == 0)
			{
				if (Projectile.ai[0] % 4 == 0)
				{
					coolDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - num154 * 2, Projectile.height - num154 * 2, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1.5f);
				}
				else
				{
					coolDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width - num154 * 2, Projectile.height - num154 * 2, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
				}
				Main.dust[coolDust].noGravity = true;
				Main.dust[coolDust].velocity *= 0.1f;
				Main.dust[coolDust].velocity += Projectile.velocity * 0.5f;
			}

		}
	}
}