using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BloodScythe : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Blood Scythe");
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.aiStyle = 18;
            Projectile.alpha = 100;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 0.7f;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            AIType = 45;
        }
        
        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.35f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.075f) / 255f);
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 5, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}