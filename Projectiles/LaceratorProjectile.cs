using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class LaceratorProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lacerator");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.TheEyeOfCthulhu);
            Projectile.width = 16;
            Projectile.scale = 1.1f;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            AIType = 555;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
        }
        
        public override void AI()
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 476, (int)((double)Projectile.damage * 0.7f), Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }
    }
}