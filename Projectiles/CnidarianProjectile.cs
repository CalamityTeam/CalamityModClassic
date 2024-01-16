using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class CnidarianProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.CorruptYoyo);
            //Tooltip.SetDefault("Cnidarian");
            Projectile.width = 16;
            Projectile.scale = 1.15f;
            Projectile.height = 16;
            Projectile.penetrate = 6;
            AIType = 542;
        }
    }
}