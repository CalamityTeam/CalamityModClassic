using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class HolySpear : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Holy Spear");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 900;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 815)
            {
                if (Projectile.ai[0] == 0f)
                {
                    Projectile.velocity.X = 1f;
                }
                else
                {
                    Projectile.velocity.X = -1f;
                }
            }
            else
            {
                if (Projectile.ai[0] == 0f)
                {
                    Projectile.velocity.X = 15f;
                }
                else
                {
                    Projectile.velocity.X = -15f;
                }
            }
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 1.5f) / 255f, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            if (Projectile.localAI[0] == 0f && Projectile.timeLeft < 815)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }
        }

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            if (Projectile.timeLeft > 815 || Projectile.timeLeft < 85)
            {
                return false;
            }
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft > 815)
            {
                Projectile.localAI[1] += 1f;
                byte b2 = (byte)(((int)Projectile.localAI[1]) * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            if (Projectile.timeLeft < 85)
            {
                byte b2 = (byte)(Projectile.timeLeft * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            return new Color(255, 255, 255, 100);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120);
        }
    }
}