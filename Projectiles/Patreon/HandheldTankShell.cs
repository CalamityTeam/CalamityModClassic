using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Patreon
{
	public class HandheldTankShell : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tank Shell");
		}

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 11;
			Projectile.timeLeft = 1800;
		}

		public override void AI()
		{
			DrawOffsetX = -8;
			DrawOriginOffsetY = 0;
			DrawOriginOffsetX = -2;
			Projectile.rotation = Projectile.velocity.ToRotation();
			Vector2 pos = Projectile.Center;
			Projectile.localAI[0] += 1f;

			if (Projectile.localAI[0] > 7f) // projectile has had more than 8 updates, draw fire trail
			{
				for (int i = 0; i < 5; ++i)
				{
					pos -= Projectile.velocity * ((float)i * 0.25f);
					int idx = Dust.NewDust(pos, 1, 1, 158, 0f, 0f, 0, default(Color), 1f);
					Main.dust[idx].noGravity = true;
					Main.dust[idx].position = pos;
					Main.dust[idx].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[idx].velocity *= 0.3f;
				}
				return;
			}
			else // projectile has had less than 8 updates, draw smoke
			{
				for (int i = 0; i < 30; ++i)
				{
					// Pick a random type of smoke (there's a little fire mixed in)
					int dustID = -1;
					switch (Main.rand.Next(6))
					{
						case 0: dustID = 55; break;
						case 1:
						case 2: dustID = 54; break;
						default: dustID = 53; break;
					}

					// Choose a random speed and angle to belch out the smoke
					float dustSpeed = Main.rand.NextFloat(3.0f, 13.0f);
					float angleRandom = 0.06f;
					Vector2 dustVel = new Vector2(dustSpeed, 0.0f).RotatedBy(Projectile.velocity.ToRotation());
					dustVel = dustVel.RotatedBy(-angleRandom);
					dustVel = dustVel.RotatedByRandom(2.0f * angleRandom);

					// Pick a size for the smoke particle
					float scale = Main.rand.NextFloat(0.5f, 1.6f);

					// Actually spawn the smoke
					int idx = Dust.NewDust(pos, 1, 1, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
					Main.dust[idx].noGravity = true;
					Main.dust[idx].position = pos;
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			// Grenade Launcher + Lunar Flare sounds for maximum meaty explosion
			SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
			SoundEngine.PlaySound(SoundID.Item88, Projectile.Center);

			// Transform the projectile's hitbox into a big explosion
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 140);
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

			// Spawn explosion dust (separate method because it's messy)
			SpawnExplosionDust();

			// Make the explosion cause damage to nearby targets (makes projectile hit twice)
			Projectile.Damage();
		}

		// Dust copied from Rocket III
		void SpawnExplosionDust()
		{
			// Sparks and such
			Vector2 corner = new Vector2(Projectile.position.X, Projectile.position.Y);
			for (int i = 0; i < 40; i++)
			{
				int idx = Dust.NewDust(corner, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[idx].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[idx].scale = 0.5f;
					Main.dust[idx].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int i = 0; i < 70; i++)
			{
				int idx = Dust.NewDust(corner, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].velocity *= 5f;
				idx = Dust.NewDust(corner, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[idx].velocity *= 2f;
			}

			// Smoke, which counts as a Gore
			Vector2 goreVec = new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f);
			for (int i = 0; i < 3; i++)
			{
				float smokeScale = 0.33f;
				if (i == 1)
				{
					smokeScale = 0.66f;
				}
				if (i == 2)
				{
					smokeScale = 1f;
				}
				int idx = Gore.NewGore(Projectile.GetSource_FromThis(null), goreVec, default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[idx].velocity *= smokeScale;
				Main.gore[idx].velocity.X += 1f;
				Main.gore[idx].velocity.Y += 1f;

				idx = Gore.NewGore(Projectile.GetSource_FromThis(null), goreVec, default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[idx].velocity *= smokeScale;
				Main.gore[idx].velocity.X -= 1f;
				Main.gore[idx].velocity.Y += 1f;

				idx = Gore.NewGore(Projectile.GetSource_FromThis(null), goreVec, default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[idx].velocity *= smokeScale;
				Main.gore[idx].velocity.X += 1f;
				Main.gore[idx].velocity.Y -= 1f;

				idx = Gore.NewGore(Projectile.GetSource_FromThis(null), goreVec, default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[idx].velocity *= smokeScale;
				Main.gore[idx].velocity.X -= 1f;
				Main.gore[idx].velocity.Y -= 1f;
			}
		}
	}
}
