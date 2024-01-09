using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassic1Point2.Projectiles
{
    public class StormBolt : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bolt");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.light = 1f;
            Projectile.timeLeft = 300;
        }
        
        public override void AI()
        {
			Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Flare_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }
        
        public override void OnKill(int timeLeft)
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("StormMarkSummon").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }
    }
}