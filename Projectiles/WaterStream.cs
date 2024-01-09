﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class WaterStream : ModProjectile
    {
    	public int addSprayTimer = 20;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Stream");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	addSprayTimer--;
        	if (addSprayTimer <= 0)
        	{
        		if (Projectile.owner == Main.myPlayer)
        		{
        			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("WaterStream2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        		}
        		addSprayTimer = 20;
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f);
			Projectile.scale -= 0.002f;
			if (Projectile.scale <= 0f)
			{
				Projectile.Kill();
			}
			if (Projectile.ai[0] <= 3f)
			{
				Projectile.ai[0] += 1f;
				return;
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.075f;
			for (int num151 = 0; num151 < 3; num151++)
			{
				float num152 = Projectile.velocity.X / 3f * (float)num151;
				float num153 = Projectile.velocity.Y / 3f * (float)num151;
				int num154 = 14;
				int num155 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num154, Projectile.position.Y + (float)num154), Projectile.width - num154 * 2, Projectile.height - num154 * 2, DustID.Water, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num155].noGravity = true;
				Main.dust[num155].velocity *= 0.1f;
				Main.dust[num155].velocity += Projectile.velocity * 0.5f;
				Dust expr_6A04_cp_0 = Main.dust[num155];
				expr_6A04_cp_0.position.X = expr_6A04_cp_0.position.X - num152;
				Dust expr_6A1F_cp_0 = Main.dust[num155];
				expr_6A1F_cp_0.position.Y = expr_6A1F_cp_0.position.Y - num153;
			}
			if (Main.rand.NextBool(8))
			{
				int num156 = 16;
				int num157 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num156, Projectile.position.Y + (float)num156), Projectile.width - num156 * 2, Projectile.height - num156 * 2, DustID.Water, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[num157].velocity *= 0.25f;
				Main.dust[num157].velocity += Projectile.velocity * 0.5f;
				return;
			}
        }
    }
}