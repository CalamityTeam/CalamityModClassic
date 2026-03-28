using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.SunkenSea
{
	public class WhirlpoolProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Riptide");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.scale = 0.75f;
			Projectile.friendly = true;
			Projectile.alpha = 150;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 99;
			Projectile.DamageType = DamageClass.Melee;
			AIType = ProjectileID.Amarok;
			Projectile.CloneDefaults(ProjectileID.CorruptYoyo);
		}

		public override void AI()
		{
			if (Main.rand.Next(1, 51) == 1)
			{
				switch (Main.rand.Next(1, 9))
				{
					case 1:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, -10, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, 1.2f/*X Increment*/, 0.2f/*Y Increment*/);
						break;
					case 2:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 5, -5, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, 0.7f/*X Increment*/, 0.7f/*Y Increment*/);
						break;
					case 3:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 10, 0, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, 0.2f/*X Increment*/, 1.2f/*Y Increment*/);
						break;
					case 4:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 5, 5, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, -0.7f/*X Increment*/, 0.7f/*Y Increment*/);
						break;
					case 5:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -0, 10, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, -1.2f/*X Increment*/, -0.2f/*Y Increment*/);
						break;
					case 6:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -5, 5, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, -0.7f/*X Increment*/, -0.7f/*Y Increment*/);
						break;
					case 7:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -10, -0, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, -0.2f/*X Increment*/, -1.2f/*Y Increment*/);
						break;
					case 8:
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -10, -10, Mod.Find<ModProjectile>("AquaStream").Type, 4, 0.0f, Projectile.owner, 0.7f/*X Increment*/, -0.7f/*Y Increment*/);
						break;
				}
			}
		}
	}
}