using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class BrimstoneMonster : ModProjectile
    {
        public float speedAdd = 0f;
        public float speedLimit = 0f;

    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Monster");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 320;
            Projectile.height = 320;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 36000;
            Projectile.alpha = 50;
            CooldownSlot = 1;
        }

        public override void AI()
        {
			if (!CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Projectile.active = false;
				Projectile.netUpdate = true;
				return;
			}
			int choice = (int)Projectile.ai[1];
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] += 1f;
                if (choice == 0)
                {
                    speedLimit = 10f;
                }
                else if (choice == 1)
                {
                    speedLimit = 20f;
                }
                else if (choice == 2)
                {
                    speedLimit = 30f;
                }
                else
                {
                    speedLimit = 40f;
                }
            }
            if (speedAdd < speedLimit)
            {
                speedAdd += 0.04f;
            }
        	bool revenge = CalamityWorldPreTrailer.revenge;
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 3f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        	float num953 = (revenge ? 5f : 4.5f) + speedAdd; //100
        	float scaleFactor12 = (revenge ? 1.5f : 1.35f) + (speedAdd * 0.25f); //5
			float num954 = 40f;
			if (Projectile.timeLeft > 30 && Projectile.alpha > 0)
			{
				Projectile.alpha -= 25;
			}
			if (Projectile.timeLeft > 30 && Projectile.alpha < 128 && Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.alpha = 128;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			int num959 = (int)Projectile.ai[0];
			if (num959 >= 0 && Main.player[num959].active && !Main.player[num959].dead)
			{
				if (Projectile.Distance(Main.player[num959].Center) > num954)
				{
					Vector2 vector102 = Projectile.DirectionTo(Main.player[num959].Center);
					if (vector102.HasNaNs())
					{
						vector102 = Vector2.UnitY;
					}
					Projectile.velocity = (Projectile.velocity * (num953 - 1f) + vector102 * scaleFactor12) / num953;
					return;
				}
			} 
			else 
			{
				if (Projectile.ai[0] != -1f) 
				{
					Projectile.ai[0] = -1f;
					Projectile.netUpdate = true;
					return;
				}
			}
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 50, 50, Projectile.alpha);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 900);
            target.AddBuff(Mod.Find<ModBuff>("VulnerabilityHex").Type, 300, true);
        }
    }
}