using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class HolyBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Holy Blast");
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Main.projFrames[Projectile.type] = 3;
        }

        public override void AI()
        {
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 3)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 2)
			{
			   Projectile.frame = 0;
			}
        	if (Projectile.ai[1] == 0f)
        	{
        		for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
        		Projectile.ai[1] = 1f;
        		SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        	}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
        	float spread = 180f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
			double deltaAngle = spread/8f;
			double offsetAngle;
			int i;
			for (i = 0; i < 1; i++ )
			{
			   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
			   	int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[projectile1].velocity.X = 9f;
			    Main.projectile[projectile1].velocity.Y = 0f;
			    Main.projectile[projectile2].velocity.X = -9f;
			    Main.projectile[projectile2].velocity.Y = 0f;
			}
			for (i = 0; i < 1; i++ )
			{
			   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
			   	int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[projectile1].velocity.X = 6f;
			    Main.projectile[projectile1].velocity.Y = 0f;
			    Main.projectile[projectile2].velocity.X = -6f;
			    Main.projectile[projectile2].velocity.Y = 0f;
			}
			for (i = 0; i < 1; i++ )
			{
			   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
			   	int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[projectile1].velocity.X = 3f;
			    Main.projectile[projectile1].velocity.Y = 0f;
			    Main.projectile[projectile2].velocity.X = -3f;
			    Main.projectile[projectile2].velocity.Y = 0f;
			}
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            for (int k = 0; k < 25; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 60);
        }
    }
}