using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class TyphoonArrow : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Typhoon Arrow");
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 600;
            AIType = 1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("TyphoonBubble").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}