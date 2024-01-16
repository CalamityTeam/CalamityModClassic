using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class LaserFountain : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Fountain");
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
        	if (Projectile.localAI[0] >= 5f)
        	{
        		int choice = Main.rand.Next(2);
        		if (choice == 0)
        		{
        			int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("NebulaShot").Type, 180, Projectile.knockBack, Projectile.owner, 0f, 0f);
        			Main.projectile[projectile1].DamageType = DamageClass.Melee;
        			Main.projectile[projectile1].aiStyle = 1;
        		}
        		else
        		{
                    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("IceBeam").Type, 180, Projectile.knockBack, Projectile.owner, 0f, 0f);
        			Main.projectile[projectile2].DamageType = DamageClass.Melee;
        			Main.projectile[projectile2].aiStyle = 1;
        		}
        		Projectile.localAI[0] = 0f;
        	}
        }
    }
}