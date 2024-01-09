using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Projectiles
{
    public class JungleThorn : ModProjectile
    {
        public override void SetDefaults()
        {
            //DisplayName.SetDefault("Jungle Thorn");
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 4;
            Projectile.alpha = 55;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 360;
            Projectile.ignoreWater = true;
            AIType = 7;
        }
        
        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Venom, 360);
        }
    }
}