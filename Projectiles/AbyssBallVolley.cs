using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Projectiles
{
    public class AbyssBallVolley : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //DisplayName.SetDefault("Abyss Ball Volley");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.damage =  35;
            Projectile.knockBack = 2.5f;
            Projectile.hostile = true;
            Projectile.alpha = 150;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        
        public override bool PreAI()
        {
        	float speedY = 1f;
        	float speedX = 1f;
        	float spread = 45f * 0.0174f;
    		double startAngle = Math.Atan2(speedX, speedY)- spread/2;
    		double deltaAngle = spread/8f;
    		double offsetAngle;
    		int i;
    		for (i = 0; i < 4; i++ )
    		{
   				offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
    		}
    		return false;
        }

        public override void AI()
        {
        	Projectile.velocity.Y *= 0.5f;
        	Projectile.velocity.X *= 0.5f;
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.velocity.Y += Projectile.ai[0];
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 227, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
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
                Projectile.velocity *= 0.75f;
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 227, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Weak, 360);
            Projectile.ai[0] += 0.1f;
            Projectile.velocity *= 0.85f;
        }
    }
}