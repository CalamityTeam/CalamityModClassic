﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Needler : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Needle");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 3;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.extraUpdates = 2;
            Projectile.aiStyle = 93;
            AIType = 514;
        }

        public override void AI()
        {
        	Projectile.alpha -= 10;
			if (Projectile.alpha < 0) 
			{
				Projectile.alpha = 0;
			}
        	Projectile.localAI[1] += 1f;
			if (Projectile.localAI[1] > 4f)
			{
				for (int num468 = 0; num468 < 2; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Poisoned, 0f, 0f, 100, default(Color), 0.75f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 1;
        }

        public override void OnKill(int timeLeft)
        {
        	Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num621 = 0; num621 < 3; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Poisoned, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 5; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.JungleGrass, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.7f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.JungleSpore, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1f);
				Main.dust[num624].velocity *= 2f;
			}
        }
    }
}