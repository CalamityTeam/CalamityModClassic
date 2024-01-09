using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class HolyFire : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fire");
			Main.projFrames[Projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.tileCollide = false;
            CooldownSlot = 1;
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
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
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
        		if (Projectile.owner == Main.myPlayer)
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
        	}
        	else
        	{
        		if (Projectile.owner == Main.myPlayer)
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
        	}
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 5; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 10; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120);
        }
    }
}