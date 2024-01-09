﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BrimstoneLaser : ModProjectile
    {
    	public int splitTimer = 60;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Laser");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.scale = 2f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 120;
            AIType = 100;
        }

        public override void AI()
        {
        	splitTimer--;
        	if (splitTimer <= 0)
        	{
	        	int numProj = 2;
	            float rotation = MathHelper.ToRadians(20);
	            if (Projectile.owner == Main.myPlayer)
	            {
		            for (int i = 0; i < numProj + 1; i++)
		            {
		                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
		                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("BrimstoneLaserSplit").Type, (int)((double)Projectile.damage), Projectile.knockBack, Projectile.owner, 0f, 0f);
		            }
	            }
	            Projectile.Kill();
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
        	Projectile.velocity.X *= 1.02f;
        	Projectile.velocity.Y *= 1.02f;
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.LifeDrain, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 60);
        }
    }
}