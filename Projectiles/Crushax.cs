using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Crushax : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Crushax");
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.scale = 0.7f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 300;
            AIType = 48;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 200);
        }
    }
}