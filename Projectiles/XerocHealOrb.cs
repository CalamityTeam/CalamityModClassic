﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class XerocHealOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Heal");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 240;
            Projectile.extraUpdates = 3;
        }
		
        public override void AI()
        {
			int num487 = (int)Projectile.ai[0];
			float num488 = 5.5f;
			Vector2 vector36 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num489 = Main.player[num487].Center.X - vector36.X;
			float num490 = Main.player[num487].Center.Y - vector36.Y;
			float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
			if (num491 < 50f && Projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && Projectile.position.X + (float)Projectile.width > Main.player[num487].position.X && Projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && Projectile.position.Y + (float)Projectile.height > Main.player[num487].position.Y)
			{
				if (Projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
				{
					int num492 = (int)Projectile.ai[1];
					Main.player[num487].HealEffect(num492, false);
					Main.player[num487].statLife += num492;
					if (Main.player[num487].statLife > Main.player[num487].statLifeMax2)
					{
						Main.player[num487].statLife = Main.player[num487].statLifeMax2;
					}
					NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, num487, (float)num492, 0f, 0f, 0, 0, 0);
				}
				Projectile.Kill();
			}
			num491 = num488 / num491;
			num489 *= num491;
			num490 *= num491;
			Projectile.velocity.X = (Projectile.velocity.X * 15f + num489) / 16f;
			Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num490) / 16f;
			for (int num497 = 0; num497 < 1; num497++)
			{
				float num498 = Projectile.velocity.X * 0.2f * (float)num497;
				float num499 = -(Projectile.velocity.Y * 0.2f) * (float)num497;
				int num500 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Enchanted_Pink, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num500].noGravity = true;
				Main.dust[num500].velocity *= 0f;
				Dust expr_154F9_cp_0 = Main.dust[num500];
				expr_154F9_cp_0.position.X = expr_154F9_cp_0.position.X - num498;
				Dust expr_15518_cp_0 = Main.dust[num500];
				expr_15518_cp_0.position.Y = expr_15518_cp_0.position.Y - num499;
			}
			return;
        }
    }
}