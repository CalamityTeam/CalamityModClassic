using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BurningMeteor : ModProjectile
    {
    	public int noTileHitCounter = 120;
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Burning Meteor");
            Projectile.width = 46;
            Projectile.height = 88;
            Projectile.alpha = 150;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
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
				if (Main.rand.Next(5) == 0)
				{
					SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
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
			if (Main.rand.Next(12) == 0)
			{
				Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
				int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				Main.dust[num59].velocity = value3 * 0.66f;
				Main.dust[num59].position = Projectile.Center + value3 * 12f;
			}
			if (Projectile.ai[1] == 1f)
			{
				Projectile.light = 0.9f;
				if (Main.rand.Next(10) == 0)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				}
				if (Main.rand.Next(20) == 0)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 1.5f, Projectile.velocity.Y * 1.5f, 150, default(Color), 2f);
					return;
				}
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            for (int k = 0; k < 25; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}