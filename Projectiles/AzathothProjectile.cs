using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class AzathothProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Azathoth");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Kraken);
            Projectile.width = 16;
            Projectile.scale = 1.2f;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            AIType = 554;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
        }
        
        public override void AI()
        {
            if (Main.rand.NextBool(3))
        	{
            	if (Projectile.owner == Main.myPlayer)
            	{
            		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.35f, Projectile.velocity.Y * 0.35f, Mod.Find<ModProjectile>("CosmicOrb").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            	}
        	}
        }
    }
}