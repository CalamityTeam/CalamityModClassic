using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class AzathothProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Kraken);
            //Tooltip.SetDefault("Azathoth");
            Projectile.width = 16;
            Projectile.scale = 1.2f;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            AIType = 554;
        }
        
        public override void AI()
        {
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.Next(3) == 0)
        	{
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.35f, Projectile.velocity.Y * 0.35f, Mod.Find<ModProjectile>("CosmicOrb").Type, (int)((double)Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }
    }
}