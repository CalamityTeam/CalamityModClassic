﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ShadeNimbusCloud : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Shade Nimbus");
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.netImportant = true;
			Main.projFrames[Projectile.type] = 4;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
			float num410 = Projectile.ai[0];
			float num411 = Projectile.ai[1];
			if (num410 != 0f && num411 != 0f)
			{
				bool flag12 = false;
				bool flag13 = false;
				if ((Projectile.velocity.X < 0f && Projectile.Center.X < num410) || (Projectile.velocity.X > 0f && Projectile.Center.X > num410))
				{
					flag12 = true;
				}
				if ((Projectile.velocity.Y < 0f && Projectile.Center.Y < num411) || (Projectile.velocity.Y > 0f && Projectile.Center.Y > num411))
				{
					flag13 = true;
				}
				if (flag12 && flag13)
				{
					Projectile.Kill();
				}
			}
			Projectile.rotation += Projectile.velocity.X * 0.02f;
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame > 3)
				{
					Projectile.frame = 0;
					return;
				}
			}
        }
        
        public override void OnKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
			{
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShadeNimbus").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			}
        }
    }
}