using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class MechanicalBarracuda : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Mechanical Barracuda");
            Projectile.width = 60;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.aiStyle = 39;
            Main.projFrames[Projectile.type] = 4;
            AIType = 190;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
        }
    }
}