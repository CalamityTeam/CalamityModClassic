﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Earth2 : ModProjectile
    {
    	public int noTileHitCounter = 120;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Earth");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.alpha = 100;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
        }
        
        public override void AI()
		{
        	int randomToSubtract = Main.rand.Next(1, 4);
        	noTileHitCounter -= randomToSubtract;
        	if (noTileHitCounter == 0)
        	{
        		Projectile.tileCollide = true;
        	}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 20 + Main.rand.Next(40);
				if (Main.rand.NextBool(5))
				{
					SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
				}
			}
			Projectile.alpha -= 15;
			int num58 = 150;
			if (Projectile.Center.Y >= Projectile.ai[1])
			{
				num58 = 0;
			}
			if (Projectile.alpha < num58)
			{
				Projectile.alpha = num58;
			}
			Projectile.localAI[0] += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * (float)Projectile.direction;
			Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
			if (Main.rand.NextBool(16))
			{
				Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
				int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				Main.dust[num59].velocity = value3 * 0.66f;
				Main.dust[num59].position = Projectile.Center + value3 * 12f;
			}
			if (Main.rand.NextBool(48))
			{
				int num60 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), 16, 1f);
				Main.gore[num60].velocity *= 0.66f;
				Main.gore[num60].velocity += Projectile.velocity * 0.3f;
			}
			if (Projectile.ai[1] == 1f)
			{
				Projectile.light = 0.9f;
				if (Main.rand.NextBool(10))
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				}
				if (Main.rand.NextBool(20))
				{
					Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18), 1f);
					return;
				}
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 1.5f) / 255f);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int k = 0; k < 15; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Vortex, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 500);
        }
    }
}