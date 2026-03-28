using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
	public class YharonFireball : ModProjectile
	{
		private float speedX = -3f;
		private float speedX2 = -5f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon Fireball");
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.hostile = true;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 120;
			Projectile.aiStyle = 1;
			AIType = 686;
			CooldownSlot = 1;
		}

		public override bool PreAI()
		{
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] == 36f)
			{
				Projectile.localAI[0] = 0f;
				for (int l = 0; l < 12; l++)
				{
					Vector2 vector3 = Vector2.UnitX * (float)(-(float)Projectile.width) / 2f;
					vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
					vector3 = vector3.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
					int num9 = Dust.NewDust(Projectile.Center, 0, 0, 55, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num9].scale = 1.1f;
					Main.dust[num9].noGravity = true;
					Main.dust[num9].position = Projectile.Center + vector3;
					Main.dust[num9].velocity = Projectile.velocity * 0.1f;
					Main.dust[num9].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
				}
			}
			return true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, Main.DiscoG, 53, Projectile.alpha);
		}

		public override void OnKill(int timeLeft)
		{
			for (int x = 0; x < 3; x++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(null), (int)Projectile.Center.X, (int)Projectile.Center.Y, speedX, -50f, Mod.Find<ModProjectile>("YharonFireball2").Type, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				speedX += 3f;
			}
			for (int x = 0; x < 2; x++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(null), (int)Projectile.Center.X, (int)Projectile.Center.Y, speedX2, -75f, Mod.Find<ModProjectile>("YharonFireball2").Type, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				speedX2 += 10f;
			}
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 144);
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num193 = 0; num193 < 2; num193++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 50, default(Color), 1.5f);
			}
			for (int num194 = 0; num194 < 20; num194++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 0, default(Color), 2.5f);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
				num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 50, default(Color), 1.5f);
				Main.dust[num195].velocity *= 2f;
				Main.dust[num195].noGravity = true;
			}
		}
	}
}