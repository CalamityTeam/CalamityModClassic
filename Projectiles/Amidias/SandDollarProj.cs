using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Amidias
{
	public class SandDollarProj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sand Dollar");
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 28;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 3;
			Projectile.timeLeft = 300;
			AIType = 272;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.ai[0] += 0.1f;
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}
	}
}