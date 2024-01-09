﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class SHPExplosion : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Explosion");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 500;
            Projectile.height = 500;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10 -
				(Main.hardMode ? 2 : 0) -
				(NPC.downedPlantBoss ? 2 : 0) -
        		(NPC.downedFishron ? 2 : 0) -
				(NPC.downedMoonlord ? 2 : 0) -
        		(CalamityWorld.downedDoG ? 1 : 0);
        }

        public override void AI()
        {
        	float num = (float)Main.rand.Next(90, 111) * 0.01f;
        	num *= Main.essScale;
			Lighting.AddLight(Projectile.Center, 5f * num, 1f * num, 4f * num);
			bool flag15 = false;
			bool flag16 = false;
			if (Projectile.velocity.X < 0f && Projectile.position.X < Projectile.ai[0])
			{
				flag15 = true;
			}
			if (Projectile.velocity.X > 0f && Projectile.position.X > Projectile.ai[0])
			{
				flag15 = true;
			}
			if (Projectile.velocity.Y < 0f && Projectile.position.Y < Projectile.ai[1])
			{
				flag16 = true;
			}
			if (Projectile.velocity.Y > 0f && Projectile.position.Y > Projectile.ai[1])
			{
				flag16 = true;
			}
			if (flag15 && flag16)
			{
				Projectile.Kill();
			}
			float num461 = 25f;
			if (Projectile.ai[0] > 180f)
			{
				num461 -= (Projectile.ai[0] - 180f) / 2f;
			}
			if (num461 <= 0f)
			{
				num461 = 0f;
				Projectile.Kill();
			}
			num461 *= 0.7f;
			Projectile.ai[0] += 4f;
			int num462 = 0;
			while ((float)num462 < num461)
			{
				float num463 = (float)Main.rand.Next(-40, 41);
				float num464 = (float)Main.rand.Next(-40, 41);
				float num465 = (float)Main.rand.Next(12, 36);
				float num466 = (float)Math.Sqrt((double)(num463 * num463 + num464 * num464));
				num466 = num465 / num466;
				num463 *= num466;
				num464 *= num466;
				int randomDust = Main.rand.Next(3);
				if (randomDust == 0)
				{
					randomDust = 246;
				}
				else if (randomDust == 1)
				{
					randomDust = 73;
				}
				else
				{
					randomDust = 187;
				}
				int num467 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, randomDust, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num467].noGravity = true;
				Main.dust[num467].position.X = Projectile.Center.X;
				Main.dust[num467].position.Y = Projectile.Center.Y;
				Dust expr_149DF_cp_0 = Main.dust[num467];
				expr_149DF_cp_0.position.X = expr_149DF_cp_0.position.X + (float)Main.rand.Next(-10, 11);
				Dust expr_14A09_cp_0 = Main.dust[num467];
				expr_14A09_cp_0.position.Y = expr_14A09_cp_0.position.Y + (float)Main.rand.Next(-10, 11);
				Main.dust[num467].velocity.X = num463;
				Main.dust[num467].velocity.Y = num464;
				num462++;
			}
			return;
        }
    }
}