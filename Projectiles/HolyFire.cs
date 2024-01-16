using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class HolyFire : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Holy Fire");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Main.projFrames[Projectile.type] = 3;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
        	if (Projectile.ai[1] == 0f)
        	{
        		Projectile.ai[1] = 1f;
        		SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        	}
        	Projectile.alpha -= 5;
        	if (Projectile.alpha <= 0)
        	{
        		Projectile.Kill();
        	}
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
        	int randomShot = Main.rand.Next(2);
        	if (randomShot == 0)
        	{
				for (i = 0; i < 1; i++ )
				{
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[projectile1].velocity.X = 5f;
				    Main.projectile[projectile1].velocity.Y = 0f;
				    Main.projectile[projectile2].velocity.X = -5f;
				    Main.projectile[projectile2].velocity.Y = 0f;
				}
        	}
        	else
        	{
        		for (i = 0; i < 1; i++ )
				{
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("HolyFire2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					Main.projectile[projectile1].velocity.X = 0f;
				    Main.projectile[projectile1].velocity.Y = 5f;
				    Main.projectile[projectile2].velocity.X = 0f;
				    Main.projectile[projectile2].velocity.Y = -5f;
				}
        	}
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 10; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 60);
        }
    }
}