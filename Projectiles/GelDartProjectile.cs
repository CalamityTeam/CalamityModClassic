using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class GelDartProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Gel Dart");
            Projectile.width = 14;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 5;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 600;
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
        
        public override void OnKill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("GelDart").Type);
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.Slimed, 100);
		}
    }
}