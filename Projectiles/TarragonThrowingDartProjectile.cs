using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class TarragonThrowingDartProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Tarragon Throwing Dart");
            Projectile.width = 34;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 3;
            Projectile.aiStyle = 113;
            Projectile.timeLeft = 600;
            AIType = 598;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 75, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 2)
            {
                Projectile.Kill();
            }
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("TarragonThrowingDart").Type);
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.CursedInferno, 200);
			target.AddBuff(BuffID.OnFire, 200);
        }
    }
}