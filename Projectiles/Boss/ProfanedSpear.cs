using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class ProfanedSpear : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Profaned Spear");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
        	Projectile.alpha -= 3;
        	Projectile.ai[1] += 1f;
        	if (Projectile.ai[1] <= 20f)
        	{
        		Projectile.velocity.X *= 0.95f;
        		Projectile.velocity.Y *= 0.95f;
        	}
            else if (Projectile.ai[1] > 20f && Projectile.ai[1] <= 39f)
        	{
            	Projectile.velocity.X *= 1.1f;
        		Projectile.velocity.Y *= 1.1f;
        	}
            else if (Projectile.ai[1] == 40f)
            {
            	Projectile.ai[1] = 0f;
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] == 30f)
            {
                Projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)Projectile.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
                    int num9 = Dust.NewDust(Projectile.Center, 0, 0, 244, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].position = Projectile.Center + vector3;
                    Main.dust[num9].velocity = Projectile.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 150, 0, Projectile.alpha);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120);
        }
    }
}