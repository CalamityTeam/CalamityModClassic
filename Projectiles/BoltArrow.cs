using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BoltArrow : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bolt Arrow");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
        }
        
        public override void AI()
        {
        	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.IceTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BoltExplosion").Type, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
        	}
        	int num212 = Main.rand.Next(10, 20);
			for (int num213 = 0; num213 < num212; num213++)
			{
				int num214 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, DustID.IceTorch, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num214].velocity *= 2f;
				Main.dust[num214].noGravity = true;
			}
        }
    }
}