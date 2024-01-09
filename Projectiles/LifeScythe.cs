using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Projectiles
{
    public class LifeScythe : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //DisplayName.SetDefault("Life Scythe");
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 18;
            Projectile.alpha = 55;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
            AIType = 274;
        }
        
        public override void AI()
        {
        	{
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f);
        	}
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 75, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
			target.AddBuff(BuffID.CursedInferno, 200);
        }
    }
}