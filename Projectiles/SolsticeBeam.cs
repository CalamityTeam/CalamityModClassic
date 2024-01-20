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
    public class SolsticeBeam : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
			if (Projectile.ai[1] == 0f) 
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item60, Projectile.position);		
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
        	int dustType = 0;
        	if (CalamityWorld1Point2.spring)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					74,
					157,
					107
				});
        	}
        	else if (CalamityWorld1Point2.summer)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					247,
					228,
					57
        		});
        	}
        	else if (CalamityWorld1Point2.fall)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					6,
					259,
					158
				});
        	}
        	else if (CalamityWorld1Point2.winter)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					67,
					229,
					185
				});
        	}
            if (Main.rand.NextBool(3))
            {
            	int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f);
            	Main.dust[dust].noGravity = true;
            }
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	byte red = 255;
        	byte green = 255;
        	byte blue = 255;
        	if (CalamityWorld1Point2.spring)
        	{
        		red = 0;
        		green = 250;
        		blue = 0;
        	}
        	else if (CalamityWorld1Point2.summer)
        	{
        		red = 250;
        		green = 250;
        		blue = 0;
        	}
        	else if (CalamityWorld1Point2.fall)
        	{
        		red = 250;
        		green = 150;
        		blue = 50;
        	}
        	else if (CalamityWorld1Point2.winter)
        	{
        		red = 100;
        		green = 150;
        		blue = 250;
        	}
        	return new Color(red, green, blue, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
        	int dustType = 0;
        	if (CalamityWorld1Point2.spring)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					245,
					157,
					107
				});
        	}
        	else if (CalamityWorld1Point2.summer)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					247,
					228,
					57
        		});
        	}
        	else if (CalamityWorld1Point2.fall)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					6,
					259,
					158
				});
        	}
        	else if (CalamityWorld1Point2.winter)
        	{
        		dustType = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					67,
					229,
					185
				});
        	}
            for (int k = 0; k < 10; k++)
            {
            	int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.oldVelocity.X * 0.05f, Projectile.oldVelocity.Y * 0.05f);
            	Main.dust[dust].noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (Main.dayTime)
	    	{
	    		target.AddBuff(BuffID.Daybreak, 300);
	    	}
	    	else
	    	{
	    		target.AddBuff(Mod.Find<ModBuff>("Nightwither").Type, 300);
	    	}
        }
    }
}