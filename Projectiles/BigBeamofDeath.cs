using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BigBeamofDeath : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Big Beam of Death");
            Projectile.width = 12;
            Projectile.height = 12;
            //projectile.aiStyle = 48;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 80;
        }
        
        public override void OnKill(int timeLeft)
        {
        	int numProj = 2;
            float rotation = MathHelper.ToRadians(20);
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("BigBeamofDeath2").Type, (int)((double)Projectile.damage), Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
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
				for (int num447 = 0; num447 < 1; num447++)
				{
					Vector2 vector33 = Projectile.position;
					vector33 -= Projectile.velocity * ((float)num447 * 0.25f);
					Projectile.alpha = 255;
					int num249 = 206;
					int num448 = Dust.NewDust(vector33, 1, 1, num249, 0f, 0f, 0, default(Color), 3f);
					Main.dust[num448].position = vector33;
					Main.dust[num448].velocity *= 0.1f;
				}
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
		}
    }
}