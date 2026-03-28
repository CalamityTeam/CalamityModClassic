using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Healing
{
    public class SilvaOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Silva Orb");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 3;
            Projectile.light = 0.2f;
        }

        public override void AI()
        {
        	Projectile.alpha -= 2;
			if (Projectile.localAI[0] == 0f) 
			{
				Projectile.scale += 0.05f;
				if ((double)Projectile.scale > 1.2) 
				{
					Projectile.localAI[0] = 1f;
				}
			} 
			else 
			{
				Projectile.scale -= 0.05f;
				if ((double)Projectile.scale < 0.8) 
				{
					Projectile.localAI[0] = 0f;
				}
			}
			int num487 = (int)Projectile.ai[0];
			float num488 = 6f;
			Vector2 vector36 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num489 = Main.player[num487].Center.X - vector36.X;
			float num490 = Main.player[num487].Center.Y - vector36.Y;
			float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
			if (num491 < 50f && Projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && Projectile.position.X + (float)Projectile.width > Main.player[num487].position.X && Projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && Projectile.position.Y + (float)Projectile.height > Main.player[num487].position.Y)
			{
				if (Projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
				{
					int num492 = (int)Projectile.ai[1];
					Main.player[num487].HealEffect(num492, false);
					Main.player[num487].statLife += num492;
					if (Main.player[num487].statLife > Main.player[num487].statLifeMax2)
					{
						Main.player[num487].statLife = Main.player[num487].statLifeMax2;
					}
					NetMessage.SendData(66, -1, -1, null, num487, (float)num492, 0f, 0f, 0, 0, 0);
				}
				Projectile.Kill();
			}
			num491 = num488 / num491;
			num489 *= num491;
			num490 *= num491;
			Projectile.velocity.X = (Projectile.velocity.X * 15f + num489) / 16f;
			Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num490) / 16f;
			return;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(Main.DiscoR, 203, 103, Projectile.alpha);
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int num407 = 0; num407 < 5; num407++)
			{
				int num408 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 157, 0f, 0f, 0, new Color(Main.DiscoR, 203, 103), 1f);
				Main.dust[num408].noGravity = true;
				Main.dust[num408].velocity *= 1.5f;
				Main.dust[num408].scale = 1.5f;
			}
        }
    }
}