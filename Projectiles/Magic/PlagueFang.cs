using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class PlagueFang : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fang");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.penetrate = 9;
            Projectile.aiStyle = 1;
            AIType = 355;
        }
        
        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 163, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			for (int num301 = 0; num301 < 15; num301++)
			{
				int num302 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num302].noGravity = true;
				Main.dust[num302].velocity *= 1.2f;
				Main.dust[num302].velocity -= Projectile.oldVelocity * 0.3f;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 320);
	    	target.immune[Projectile.owner] = 2;
		}
    }
}