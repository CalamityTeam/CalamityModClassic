using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class EonBeamV3 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Eon Beam");
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 200;
            AIType = 173;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 1.2f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f);
            if (Projectile.localAI[1] > 7f)
			{
				int num307 = Main.rand.Next(3);
				if (num307 == 0)
				{
					num307 = 15;
				}
				else if (num307 == 1)
				{
					num307 = 57;
				}
				else
				{
					num307 = 58;
				}
				int num308 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, num307, 0f, 0f, 100, default(Color), 1.25f);
				Main.dust[num308].velocity *= 0.1f;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 57, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 58, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 500);
	    	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 500);
	    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 500);
	    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 500);
        }
    }
}