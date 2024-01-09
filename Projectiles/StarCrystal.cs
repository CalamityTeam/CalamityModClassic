using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class StarCrystal : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Star");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 29;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            AIType = 121;
        }

        public override void OnKill(int timeLeft)
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
	            for (int k = 0; k < 3; k++)
	            {
	            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.MartianHit, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
	            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, Mod.Find<ModProjectile>("TinyCrystal").Type, 
	            	(int)((double)Projectile.damage * 0.75), (float)((int)((double)Projectile.knockBack * 0.15)), Main.myPlayer, 0f, 0f);
	            }
        	}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 240);
        }
    }
}