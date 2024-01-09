﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassic1Point2.Projectiles
{
    public class VileClot : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Clot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            AIType = 1;
        }
        
        public override void AI()
        {
        	Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 1; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Demonite, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrainRot").Type, 200);
        }
    }
}