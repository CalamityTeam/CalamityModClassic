using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
	public class NorfleetComet : ModProjectile
	{
		public int noTileHitCounter = 120;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Comet");
		}

		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			int randomToSubtract = Main.rand.Next(1, 4);
			noTileHitCounter -= randomToSubtract;
			if (noTileHitCounter == 0)
			{
				Projectile.tileCollide = true;
			}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 20 + Main.rand.Next(40);
				if (Main.rand.Next(5) == 0)
				{
					SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
				}
			}
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] == 18f)
			{
				Projectile.localAI[0] = 0f;
				for (int l = 0; l < 12; l++)
				{
					Vector2 vector3 = Vector2.UnitX * (float)(-(float)Projectile.width) / 2f;
					vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
					vector3 = vector3.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
					int num9 = Dust.NewDust(Projectile.Center, 0, 0, (Main.rand.Next(2) == 0 ? 221 : 244), 0f, 0f, 160, default(Color), 1f);
					Main.dust[num9].scale = 1.1f;
					Main.dust[num9].noGravity = true;
					Main.dust[num9].position = Projectile.Center + vector3;
					Main.dust[num9].velocity = Projectile.velocity * 0.1f;
					Main.dust[num9].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
				}
			}
			Projectile.alpha -= 15;
			int num58 = 150;
			if (Projectile.Center.Y >= Projectile.ai[1])
			{
				num58 = 0;
			}
			if (Projectile.alpha < num58)
			{
				Projectile.alpha = num58;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
			if (Main.rand.Next(16) == 0)
			{
				Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
				int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (Main.rand.Next(2) == 0 ? 221 : 244), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				Main.dust[num59].velocity = value3 * 0.66f;
				Main.dust[num59].position = Projectile.Center + value3 * 12f;
			}
			if (Projectile.ai[1] == 1f)
			{
				Projectile.light = 0.5f;
				if (Main.rand.Next(10) == 0)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (Main.rand.Next(2) == 0 ? 221 : 244), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				}
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(Main.DiscoR, 100, 255, Projectile.alpha);
		}

		public override void OnKill(int timeLeft)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("NorfleetExplosion").Type, (int)((double)Projectile.damage * 0.3), Projectile.knockBack * 0.1f, Projectile.owner, 0f, 0f);
			}
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 144);
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num193 = 0; num193 < 4; num193++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, (Main.rand.Next(2) == 0 ? 221 : 244), 0f, 0f, 50, default(Color), 1.5f);
			}
			for (int num194 = 0; num194 < 20; num194++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, (Main.rand.Next(2) == 0 ? 221 : 244), 0f, 0f, 0, default(Color), 2.5f);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
				num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, (Main.rand.Next(2) == 0 ? 221 : 244), 0f, 0f, 50, default(Color), 1.5f);
				Main.dust[num195].velocity *= 2f;
				Main.dust[num195].noGravity = true;
			}
		}
	}
}