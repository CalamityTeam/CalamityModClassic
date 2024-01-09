using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class AortaProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Aorta");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Valor);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = 7;
            Projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            AIType = ProjectileID.Valor;
        }
    }
}