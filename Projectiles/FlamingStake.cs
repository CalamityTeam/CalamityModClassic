using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class FlamingStake : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Flaming Stake");
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 10;
            Projectile.alpha = 255;
            Projectile.scale = 0.8f;
            Projectile.extraUpdates = 3;
            Projectile.aiStyle = 1;
            AIType = 323;
        }
        
        public override void AI()
        {
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ItemID.Stake);
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.OnFire, 260);
        }
    }
}