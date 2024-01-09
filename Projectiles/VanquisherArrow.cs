using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class VanquisherArrow : ModProjectile
    {
    	public int projCount = 10;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Arrow");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = 1;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	projCount--;
        	if (projCount <= 0)
        	{
        		if (Projectile.owner == Main.myPlayer)
        		{
        			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("VanquisherArrow2").Type, (int)((double)Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner, 0f, 0f);
        		}
        		projCount = 10;
        	}
        	int num249 = Main.rand.Next(3);
			if (num249 == 0)
			{
				num249 = 15;
			}
			else if (num249 == 1)
			{
				num249 = 57;
			}
			else
			{
				num249 = 58;
			}
            if (Main.rand.NextBool(10))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, num249, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 5; k++)
            {
        		int num249 = Main.rand.Next(3);
				if (num249 == 0)
				{
					num249 = 15;
				}
				else if (num249 == 1)
				{
					num249 = 57;
				}
				else
				{
					num249 = 58;
				}
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, num249, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 500);
        }
    }
}