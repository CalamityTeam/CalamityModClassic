using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
	public class FossilShard : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Fossil Shard");
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 120;
            AIType = 1;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 32, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	Projectile.rotation += Projectile.velocity.Y;
        	Projectile.velocity.Y *= 1.05f;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 60);
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int i = 0; i <= 3; i++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 32, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}