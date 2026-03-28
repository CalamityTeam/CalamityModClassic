using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Amidias
{
	public class WaywasherProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Waywasher Blast");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.alpha = 255;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 300;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, 0f, 0.1f, 0.7f);
			for (int num105 = 0; num105 < 2; num105++)
			{
				float num99 = Projectile.velocity.X / 3f * (float)num105;
				float num100 = Projectile.velocity.Y / 3f * (float)num105;
				int num101 = 4;
				int num102 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num101, Projectile.position.Y + (float)num101), Projectile.width - num101 * 2, Projectile.height - num101 * 2, 33, 0f, 0f, 0, new Color(0, 142, 255), 1.2f);
				Main.dust[num102].noGravity = true;
				Main.dust[num102].velocity *= 0.1f;
				Main.dust[num102].velocity += Projectile.velocity * 0.1f;
				Dust expr_47FA_cp_0 = Main.dust[num102];
				expr_47FA_cp_0.position.X = expr_47FA_cp_0.position.X - num99;
				Dust expr_4815_cp_0 = Main.dust[num102];
				expr_4815_cp_0.position.Y = expr_4815_cp_0.position.Y - num100;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num103 = 4;
				int num104 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num103, Projectile.position.Y + (float)num103), Projectile.width - num103 * 2, Projectile.height - num103 * 2, 33, 0f, 0f, 0, new Color(0, 142, 255), 0.6f);
				Main.dust[num104].velocity *= 0.25f;
				Main.dust[num104].velocity += Projectile.velocity * 0.5f;
			}
			if (Projectile.ai[1] >= 20f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			}
			else
			{
				Projectile.rotation += 0.3f * (float)Projectile.direction;
			}
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 33, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, new Color(0, 142, 255), 1f);
			}
		}
	}
}