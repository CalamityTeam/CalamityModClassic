﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ChaotrixProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.Yelets);
            //Tooltip.SetDefault("Chaotrix");
            Projectile.width = 16;
            Projectile.scale = 1.15f;
            Projectile.height = 16;
            Projectile.penetrate = 10;
            AIType = 549;
        }
        
        public override void AI()
        {
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.OnFire, 300);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}