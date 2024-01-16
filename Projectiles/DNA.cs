﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DNA : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("DNA");
            Projectile.width = 14;
            Projectile.height = 52;
            Projectile.aiStyle = 4;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            AIType = 493;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	if (Projectile.ai[0] == 0f)
			{
        		Projectile.alpha -= 50;
        		if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.ai[0] = 1f;
					if (Projectile.ai[1] == 0f)
					{
						Projectile.ai[1] += 1f;
						Projectile.position += Projectile.velocity * 1f;
					}
					if (Main.myPlayer == Projectile.owner)
					{
						int num48 = Projectile.type;
						if (Projectile.ai[1] >= (float)(12 + Main.rand.Next(2)))
						{
							num48 = Mod.Find<ModProjectile>("DNA2").Type;
						}
						int num49 = Projectile.damage;
						float num50 = Projectile.knockBack;
						int number = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + Projectile.velocity.X + (float)(Projectile.width / 2), Projectile.position.Y + Projectile.velocity.Y + (float)(Projectile.height / 2), Projectile.velocity.X, Projectile.velocity.Y, num48, num49, num50, Projectile.owner, 0f, Projectile.ai[1] + 1f);
						NetMessage.SendData(27, -1, -1, null, number, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
        		}
        	}
        	else
			{
				if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
				{
					for (int num55 = 0; num55 < 8; num55++)
					{
						int num56 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234, Projectile.velocity.X * 0.005f, Projectile.velocity.Y * 0.005f, 200, default(Color), 1f);
						Main.dust[num56].noGravity = true;
						Main.dust[num56].velocity *= 0.5f;
					}
				}
				Projectile.alpha += 7;
				if (Projectile.alpha >= 255)
				{
					Projectile.Kill();
					return;
				}
        	}
            if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 234, Projectile.velocity.X * 0.005f, Projectile.velocity.Y * 0.005f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 234, Projectile.oldVelocity.X * 0.005f, Projectile.oldVelocity.Y * 0.005f);
            }
        }
    }
}