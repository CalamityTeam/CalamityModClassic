using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
	public class BrimstoneLaserSummon : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Laser");
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 2;
			Projectile.aiStyle = 1;
			AIType = 100;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.minion = true;
			Projectile.minionSlots = 0;
			Projectile.penetrate = 1;
			Projectile.alpha = 120;
			Projectile.timeLeft = 300;
		}

		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
			Projectile.velocity.X *= 1.05f;
			Projectile.velocity.Y *= 1.05f;
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Projectile.velocity.Y += Projectile.ai[0];
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 60);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 50, 50, Projectile.alpha);
		}
	}
}