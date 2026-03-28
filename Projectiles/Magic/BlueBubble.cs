using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class BlueBubble : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bubble");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
        }
        
        public override void AI()
        {
        	Projectile.velocity.X *= 0.99f;
        	Projectile.velocity.Y *= 0.99f;
        	if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale += 0.005f;
			}
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 30;
			}
			if (Projectile.alpha < 0) 
			{
				Projectile.alpha = 0;
			}
			Vector2 v2 = Projectile.ai[0].ToRotationVector2();
			float num743 = Projectile.velocity.ToRotation();
			float num744 = v2.ToRotation();
			double num745 = (double)(num744 - num743);
			if (num745 > 3.1415926535897931) 
			{
				num745 -= 6.2831853071795862;
			}
			if (num745 < -3.1415926535897931) 
			{
				num745 += 6.2831853071795862;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
			if (Main.myPlayer == Projectile.owner && Projectile.timeLeft > 120) 
			{
				Projectile.timeLeft = 120;
			}
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
			int num190 = Main.rand.Next(5, 9);
			for (int num191 = 0; num191 < num190; num191++)
			{
				int num192 = Dust.NewDust(Projectile.Center, 0, 0, 206, 0f, 0f, 100, default(Color), 1.4f);
				Main.dust[num192].velocity *= 0.8f;
				Main.dust[num192].position = Vector2.Lerp(Main.dust[num192].position, Projectile.Center, 0.5f);
				Main.dust[num192].noGravity = true;
			}
        }
    }
}