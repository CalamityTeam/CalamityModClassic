using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ChargedBlast : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Charged Dart");
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 4;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 3f)
			{
				for (int num468 = 0; num468 < 2; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 206, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
				for (int num92 = 0; num92 < 2; num92++)
				{
					int num96 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 160, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num96].noGravity = true;
					Main.dust[num96].velocity *= 0f;
				}
				for (int num105 = 0; num105 < 2; num105++)
				{
					int num102 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 226, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num102].noGravity = true;
					Main.dust[num102].velocity *= 0f;
				}
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	int projectiles = Main.rand.Next(3);
        	for (int k = 0; k < projectiles; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 226, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, Mod.Find<ModProjectile>("ChargedBlast2").Type, 
            	(int)((double)Projectile.damage * 0.5), (float)((int)((double)Projectile.knockBack * 0.5)), Main.myPlayer, 0f, 0f);
            }
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
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 160, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	int projectiles = Main.rand.Next(3);
        	for (int k = 0; k < projectiles; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 226, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, Mod.Find<ModProjectile>("ChargedBlast2").Type, 
            	(int)((double)Projectile.damage * 0.5), (float)((int)((double)Projectile.knockBack * 0.5)), Main.myPlayer, 0f, 0f);
            }
        	Projectile.Kill();
        }
    }
}