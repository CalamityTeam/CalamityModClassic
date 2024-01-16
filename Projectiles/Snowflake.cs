using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Snowflake : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Snowflake");
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = 0.8f;
            Projectile.aiStyle = 9;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 70;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
            AIType = 491;
        }

        public override void AI()
        {
        	Projectile.rotation += 0.15f;
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 67, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 67, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
    		target.AddBuff(BuffID.Frostburn, 200);
        }
    }
}