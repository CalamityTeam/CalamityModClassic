﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class VanquisherArrow2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Arrow");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
        }
        
        public override void AI()
        {
        	Projectile.velocity *= 0.95f;
        	Projectile.alpha += 3;
        	if (Projectile.alpha >= 255)
        	{
        		Projectile.Kill();
        	}
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	int num249 = Main.rand.Next(3);
			if (num249 == 0)
			{
				num249 = 15;
			}
			else if (num249 == 1)
			{
				num249 = 57;
			}
			else
			{
				num249 = 58;
			}
            if (Main.rand.NextBool(10))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, num249, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
            float num472 = Projectile.Center.X;
			float num473 = Projectile.Center.Y;
			float num474 = 400f;
			bool flag17 = false;
			for (int num475 = 0; num475 < 200; num475++)
			{
				if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
				{
					float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
					float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
					float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
					if (num478 < num474)
					{
						num474 = num478;
						num472 = num476;
						num473 = num477;
						flag17 = true;
					}
				}
			}
			if (flag17)
			{
				float num483 = 16f;
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num484 = num472 - vector35.X;
				float num485 = num473 - vector35.Y;
				float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
				return;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 5; k++)
            {
        		int num249 = Main.rand.Next(3);
				if (num249 == 0)
				{
					num249 = 15;
				}
				else if (num249 == 1)
				{
					num249 = 57;
				}
				else
				{
					num249 = 58;
				}
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, num249, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 500);
        }
    }
}