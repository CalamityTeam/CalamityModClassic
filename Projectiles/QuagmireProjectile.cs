using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class QuagmireProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Quagmire");
		}
    	
        public override void SetDefaults()
        {
        	Projectile.CloneDefaults(ProjectileID.HelFire);
            Projectile.width = 16;
            Projectile.scale = 1.25f;
            Projectile.height = 16;
            Projectile.penetrate = 8;
            AIType = 553;
        }
        
        public override void AI()
        {
        	if (Main.rand.NextBool(5))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.JungleSpore, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	if (Projectile.owner == Main.myPlayer)
        	{
	        	if (Main.rand.NextBool(10))
	        	{
	            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.35f, Projectile.velocity.Y * 0.35f, 569, (int)((double)Projectile.damage * 0.65f), Projectile.knockBack, Projectile.owner, 0f, 0f);
	        	}
	        	if (Main.rand.NextBool(30))
	        	{
	            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 570, (int)((double)Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, 0f, 0f);
	        	}
	        	if (Main.rand.NextBool(50))
	        	{
	            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f, 571, (int)((double)Projectile.damage * 0.85f), Projectile.knockBack, Projectile.owner, 0f, 0f);
	        	}
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Venom, 200);
        }
    }
}