using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class HellionSpike : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Hellion Spike");
            Projectile.width = 10;
            Projectile.height = 6;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            AIType = 228;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
            Projectile.velocity.Y += Projectile.ai[0];
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 44, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
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
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 44, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Venom, 200);
        }
    }
}