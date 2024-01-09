using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassic1Point2.Projectiles 
{
	public class FlamingPumpkin : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Pumpkin");
		}
    	
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 14;
			Projectile.penetrate = 2;
	        Projectile.timeLeft = 200;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
		}
		
		public override void AI()
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}
	}
}	