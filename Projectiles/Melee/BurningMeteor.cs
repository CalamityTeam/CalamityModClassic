using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class BurningMeteor : ModProjectile
    {
    	public int noTileHitCounter = 120;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Meteor");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.alpha = 150;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
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
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] == 30f)
            {
                Projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)Projectile.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
                    int num9 = Dust.NewDust(Projectile.Center, 0, 0, 244, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].position = Projectile.Center + vector3;
                    Main.dust[num9].velocity = Projectile.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
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
			Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
			if (Main.rand.Next(36) == 0)
			{
				Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
				int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				Main.dust[num59].velocity = value3 * 0.66f;
				Main.dust[num59].position = Projectile.Center + value3 * 12f;
			}
			if (Projectile.ai[1] == 1f)
			{
				Projectile.light = 0.9f;
				if (Main.rand.Next(30) == 0)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				}
				if (Main.rand.Next(60) == 0)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 1.5f, Projectile.velocity.Y * 1.5f, 150, default(Color), 2f);
				}
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            for (int k = 0; k < 10; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, 0f, 0f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}