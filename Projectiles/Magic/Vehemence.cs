using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class Vehemence : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vehemence");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.45f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.45f) / 255f);
			for (int num457 = 0; num457 < 2; num457++)
			{
				int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 65, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num458].noGravity = true;
				Main.dust[num458].velocity *= 0.15f;
				Main.dust[num458].velocity += Projectile.velocity * 0.1f;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
        	for (int j = 0; j <= 25; j++)
        	{
        		int num459 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 65, 0f, 0f, 100, default(Color), 1f);
        		Main.dust[num459].noGravity = true;
				Main.dust[num459].velocity *= 0.1f;
        	}
        }
        
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
        	double lifeAmount = (double)target.life;
        	double lifeMax = (double)target.lifeMax;
        	double damageMult = (lifeAmount / lifeMax) * 7;
        	target.damage = (int)Math.Pow(target.damage, damageMult);
        	if (target.damage > 1000000)
        	{
		        target.damage = 1000000;
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (target.life == target.lifeMax)
        	{
        		target.AddBuff(BuffID.ShadowFlame, 12000);
	        	target.AddBuff(BuffID.Ichor, 12000);
	        	target.AddBuff(BuffID.Frostburn, 12000);
	        	target.AddBuff(BuffID.OnFire, 12000);
	        	target.AddBuff(BuffID.Poisoned, 12000);
        	}
        }
    }
}