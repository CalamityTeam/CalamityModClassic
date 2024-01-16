using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class AirSpinnerProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Valor);
            //Tooltip.SetDefault("AirSpinner");
            Projectile.width = 16;
            Projectile.scale = 1.05f;
            Projectile.height = 16;
            Projectile.penetrate = 7;
            Projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            AIType = ProjectileID.Valor;
        }
    }
}