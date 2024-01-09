﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class SpatialSpear3 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Spear");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 1f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 30);
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 30);
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 30);
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 30);
        	int numProj = 2;
            float rotation = MathHelper.ToRadians(20);
            if (Projectile.owner == Main.myPlayer)
            {
	            for (int i = 0; i < numProj + 1; i++)
	            {
	                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
	                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("SpatialSpear4").Type, (int)((double)Projectile.damage * 0.3f), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
	            }
            }
        }
    }
}