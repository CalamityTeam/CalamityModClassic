using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class DoGBeamPortal : ModProjectile
    {
    	public int beamTimer = 180;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Beam Portal");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.95f) / 255f, ((255 - Projectile.alpha) * 1.15f) / 255f);
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 5)
			{
			   Projectile.frame = 0;
			}
            beamTimer--;
        	if (beamTimer <= 0)
        	{
        		SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
	        	float spread = 30f * 0.0174f;
				double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
				double deltaAngle = spread / 8f;
				double offsetAngle;
				int i;
				if (Projectile.owner == Main.myPlayer)
				{
					for (i = 0; i < 4; i++)
					{
                        float speed = 4f;
                        int projectileDamage = Main.expertMode ? 64 : 75;
                        if (CalamityWorldPreTrailer.death)
                        {
                            speed = 7f;
                        }
                        else if (CalamityWorldPreTrailer.revenge)
                        {
                            speed = 6f;
                        }
                        else if (Main.expertMode)
                        {
                            speed = 5f;
                        }
                        offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * speed ), (float)( Math.Cos(offsetAngle) * speed ), Mod.Find<ModProjectile>("DoGBeam").Type, projectileDamage, Projectile.knockBack, Projectile.owner, 0f, 0f);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * speed ), (float)( -Math.Cos(offsetAngle) * speed ), Mod.Find<ModProjectile>("DoGBeam").Type, projectileDamage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					}
				}
				beamTimer = 180;
        	}
            int num103 = (int)Player.FindClosest(Projectile.Center, 1, 1);
            float scaleFactor2 = Projectile.velocity.Length();
            Vector2 vector11 = Main.player[num103].Center - Projectile.Center;
            if (Vector2.Distance(Main.player[num103].Center, Projectile.Center) > 2000f)
            {
                Projectile.position.X = (float)(Main.player[num103].Center.X / 16) * 16f - (float)(Projectile.width / 2);
                Projectile.position.Y = ((float)(Main.player[num103].Center.Y / 16) * 16f - (float)(Projectile.height / 2)) - 250f;
                Projectile.ai[1] = 0f;
                beamTimer = 90;
            }
            vector11.Normalize();
            vector11 *= scaleFactor2;
            Projectile.velocity = (Projectile.velocity * 24f + vector11) / 25f;
            Projectile.velocity.Normalize();
            Projectile.velocity *= scaleFactor2;
            if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
                for (int num621 = 0; num621 < 30; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 60; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 173, 0f, 0f, 100, default(Color), 1.7f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 173, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num624].velocity *= 2f;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft < 85)
            {
                byte b2 = (byte)(Projectile.timeLeft * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            return new Color(255, 255, 255, 100);
        }
    }
}