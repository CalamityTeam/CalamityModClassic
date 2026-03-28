using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Amidias
{
	public class Shell : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shell");
		}

		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 5;
			Projectile.aiStyle = 1;
		}

		public override void AI()
		{
			Projectile.velocity.X *= 0.9995f;
			Projectile.velocity.Y *= 0.9995f;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 34;
			Projectile.height = 18;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 10; num621++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 14, Projectile.oldVelocity.X / 4, Projectile.oldVelocity.Y / 4, 0, new Color(0, 255, 255), 1.5f);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
			}
		}
	}
}