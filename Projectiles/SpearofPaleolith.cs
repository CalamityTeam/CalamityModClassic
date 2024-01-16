using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SpearofPaleolith : ModProjectile
    {
    	public int shardRainTimer = 3;
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Spear of Paleolith");
            Projectile.width = 52;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 113;
            Projectile.timeLeft = 600;
            AIType = 598;
        }
        
        public override void AI()
        {
        	shardRainTimer--;
        	if (Main.rand.Next(4) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 159, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        	if (shardRainTimer == 0)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f, Mod.Find<ModProjectile>("FossilShard").Type, (int)((double)Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner, 0f, 0f);
				shardRainTimer = 3;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int i = 0; i <= 10; i++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 159, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 120);
        }
    }
}