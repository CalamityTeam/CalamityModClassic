using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class DoGBeamPortal : ModProjectile
    {
    	public int beamTimer = 180;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Portal");
			Main.projFrames[Projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.alpha = 255;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
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
			if (Projectile.frame > 3)
			{
			   Projectile.frame = 0;
			}
        	beamTimer--;
        	if (beamTimer <= 0)
        	{
        		SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
	        	float spread = 30f * 0.0174f;
				double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
				double deltaAngle = spread/8f;
				double offsetAngle;
				int i;
				if (Projectile.owner == Main.myPlayer)
				{
					for (i = 0; i < 4; i++ )
					{
						offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("DoGBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("DoGBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					}
				}
				beamTimer = 180;
        	}
        	Projectile.velocity.X *= 0.985f;
        	Projectile.velocity.Y *= 0.985f;
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
			}
        	if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale -= 0.02f;
				Projectile.alpha += 30;
				if (Projectile.alpha >= 250)
				{
					Projectile.alpha = 255;
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 30;
				if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.localAI[0] = 0f;
				}
			}
        }
    }
}