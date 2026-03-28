using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class SporeBomb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bomb");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
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
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 20f && Projectile.ai[0] < 40f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
				Projectile.velocity.X = Projectile.velocity.X * 0.98f;
			}
			else if (Projectile.ai[0] >= 40f && Projectile.ai[0] < 60f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y - 0.3f;
				Projectile.velocity.X = Projectile.velocity.X * 1.02f;
			}
			else if (Projectile.ai[0] >= 60f)
			{
				Projectile.ai[0] = 0f;
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(Main.DiscoR, 203, 103, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        	for (int num407 = 0; num407 < 25; num407++)
			{
				int num408 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 157, 0f, 0f, 0, new Color(Main.DiscoR, 203, 103), 1f);
				Main.dust[num408].noGravity = true;
				Main.dust[num408].velocity *= 1.5f;
				Main.dust[num408].scale = 1.5f;
			}
	        int num251 = Main.rand.Next(3, 7);
	        if (Projectile.owner == Main.myPlayer)
	        {
				for (int num252 = 0; num252 < num251; num252++)
				{
					Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					while (value15.X == 0f && value15.Y == 0f)
					{
						value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					}
					value15.Normalize();
					value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.oldPosition.X + (float)(Projectile.width / 2), Projectile.oldPosition.Y + (float)(Projectile.height / 2), value15.X, value15.Y, 569 + Main.rand.Next(3), (int)((double)Projectile.damage * 0.5), 0f, Projectile.owner, 0f, 0f);
				}
	        }
        }
    }
}