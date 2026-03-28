using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class Aftershock : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rock");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 34;
            Projectile.aiStyle = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 6;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            AIType = 261;
        }
    }
}