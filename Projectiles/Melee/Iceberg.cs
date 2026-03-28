using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
	public class Iceberg : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Iceberg");
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
		}

		public override void AI()
		{
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 3; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 67, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			double newDamageMult = 1.0 - ((double)Projectile.timeLeft / 300.0);
			target.damage = (int)((double)target.damage * newDamageMult);
			modifiers.Knockback.Base = 0f;
			if (modifiers.ToHitInfo(target.damage, true, modifiers.Knockback.Base, false, 0f).Crit || target.buffImmune[Mod.Find<ModBuff>("GlacialState").Type])
				target.damage *= 2;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			int debuffDuration = 300 - Projectile.timeLeft;
			if (Projectile.timeLeft < 270)
			{
				target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, debuffDuration);
			}
		}
	}
}