using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class BeamingBolt : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Beaming Bolt");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 120;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 0.95f;
        	Projectile.velocity.Y *= 0.985f;
        	for (int dust = 0; dust < 3; dust++)
        	{
        		int randomDust = Main.rand.Next(3);
	        	if (randomDust == 0)
	        	{
	        		randomDust = 164;
	        	}
	        	else if (randomDust == 1)
	        	{
	        		randomDust = 58;
	        	}
	        	else
	        	{
	        		randomDust = 204;
	        	}
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, randomDust, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        	}
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 6; k++)
            {
            	int randomDust = Main.rand.Next(3);
	        	if (randomDust == 0)
	        	{
	        		randomDust = 164;
	        	}
	        	else if (randomDust == 1)
	        	{
	        		randomDust = 58;
	        	}
	        	else
	        	{
	        		randomDust = 204;
	        	}
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, randomDust, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            float spread = 90f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
			double deltaAngle = spread/8f;
			double offsetAngle;
			int i;
			if (Projectile.owner == Main.myPlayer)
			{
				for (i = 0; i < 4; i++ )
				{
					offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("BeamingBolt2").Type, (int)((double)Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("BeamingBolt2").Type, (int)((double)Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, 0f, 0f);
				}
			}
        	SoundEngine.PlaySound(SoundID.Item105, Projectile.position);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 7;
		}
    }
}