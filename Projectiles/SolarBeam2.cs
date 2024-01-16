using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SolarBeam2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Solar Beam");
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 48;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 6;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 300;
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

        public override void AI()
        {
        	if (Projectile.velocity.X != Projectile.velocity.X)
			{
				Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
				Projectile.velocity.X = -Projectile.velocity.X;
			}
			if (Projectile.velocity.Y != Projectile.velocity.Y)
			{
				Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
				Projectile.velocity.Y = -Projectile.velocity.Y;
			}
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 9f)
			{
				for (int num447 = 0; num447 < 4; num447++)
				{
					Vector2 vector33 = Projectile.position;
					vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
					Projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, 1, 1, 246, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
					Main.dust[num448].velocity *= 0.2f;
				}
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}