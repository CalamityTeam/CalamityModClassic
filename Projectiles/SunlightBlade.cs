using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SunlightBlade : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Sunlight Blade");
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 18;
            Projectile.alpha = 55;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 440;
            Projectile.ignoreWater = true;
            AIType = 157;
        }
        
        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 64, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
        }
    }
}