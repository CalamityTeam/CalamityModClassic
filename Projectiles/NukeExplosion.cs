using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class NukeExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Nuke Explosion");
            Projectile.width = 1000;
            Projectile.height = 1000;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
        }
    }
}