using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class TinyCrystal : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Tiny Crystal");
            Projectile.width = 8;
            Projectile.height = 12;
            Projectile.aiStyle = 29;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            AIType = 122;
        }

        public override void AI()
        {
            Projectile.velocity.Y += Projectile.ai[0];
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 227, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 120);
        }
    }
}