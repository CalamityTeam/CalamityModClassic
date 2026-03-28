using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Shrines
{
	public class GladiatorSword : ModProjectile
	{
		private double rotation = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gladiator Sword");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.ignoreWater = true;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.timeLeft *= 5;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("GladiatorSword").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (!modPlayer.gladiatorSword)
			{
				Projectile.active = false;
				return;
			}
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.glSword = false;
				}
				if (modPlayer.glSword)
				{
					Projectile.timeLeft = 2;
				}
			}
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
			Vector2 vector = player.Center - Projectile.Center;
			Projectile.rotation = vector.ToRotation() - 1.57f;
			Projectile.Center = player.Center + new Vector2(80, 0).RotatedBy(rotation);
			rotation += 0.03;
			if (rotation >= 360)
			{
				rotation = 0;
			}
			Projectile.velocity.X = ((vector.X > 0f) ? -0.000001f : 0f);
		}
	}
}