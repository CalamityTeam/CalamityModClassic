using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class TheGodsGambitProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Kraken);
            //Tooltip.SetDefault("The God's Gambit");
            Projectile.width = 16;
            Projectile.scale = 1.15f;
            Projectile.height = 16;
            Projectile.penetrate = 6;
            AIType = 554;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(8) == 0)
        	{
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 406, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.Slimed, 200);
		}
    }
}