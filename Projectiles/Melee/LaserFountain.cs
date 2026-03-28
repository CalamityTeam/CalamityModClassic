using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class LaserFountain : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fountain");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
        	Projectile.localAI[0] += 1f;
        	float SpeedX = (float)Main.rand.Next(-15, 15);
        	float SpeedY = (float)Main.rand.Next(-20, -10);
        	if (Projectile.localAI[0] >= 12f)
        	{
				if (Projectile.owner == Main.myPlayer)
				{
					int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("NebulaShot").Type, (int)(350f * Main.player[Projectile.owner].GetDamage(DamageClass.Melee).Base), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[projectile1].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
					Main.projectile[projectile1].aiStyle = 1;
				}
        		Projectile.localAI[0] = 0f;
        	}
        }
    }
}