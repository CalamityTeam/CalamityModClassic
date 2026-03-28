using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Patreon
{
    public class DivineRetribution : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Divine Retribution");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
			Projectile.scale = 0.9f;
            Projectile.timeLeft = 420;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; 
        }

        public override void AI()
        {
        	float num953 = 25f * Projectile.ai[1]; //100
        	float scaleFactor12 = 5f * Projectile.ai[1]; //5
			float num954 = 1000f;
            if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
            {
                Projectile.spriteDirection = -Projectile.direction;
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            }
            else
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            }
            Lighting.AddLight(Projectile.Center, 0.7f, 0.3f, 0f);
			if (Main.player[Projectile.owner].active && !Main.player[Projectile.owner].dead) 
			{
				if (Projectile.Distance(Main.player[Projectile.owner].Center) > num954) 
				{
					Vector2 vector102 = Projectile.DirectionTo(Main.player[Projectile.owner].Center);
					if (vector102.HasNaNs()) 
					{
						vector102 = Vector2.UnitY;
					}
					Projectile.velocity = (Projectile.velocity * (num953 - 1f) + vector102 * scaleFactor12) / num953;
					return;
				}
                float num472 = Projectile.Center.X;
                float num473 = Projectile.Center.Y;
                float num474 = 600f;
                bool flag17 = false;
                for (int num475 = 0; num475 < 200; num475++)
                {
                    if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
                    {
                        float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
                        float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
                        float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
                        if (num478 < num474)
                        {
                            num474 = num478;
                            num472 = num476;
                            num473 = num477;
                            flag17 = true;
                        }
                    }
                }
                if (flag17)
                {
                    float num483 = 9f;
                    Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num484 = num472 - vector35.X;
                    float num485 = num473 - vector35.Y;
                    float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
                    num486 = num483 / num486;
                    num484 *= num486;
                    num485 *= num486;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
                    return;
                }
            }
			else 
			{
				if (Projectile.timeLeft > 30) 
				{
					Projectile.timeLeft = 30;
				}
			}
        }

        public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 600);
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 600);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 64);
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num193 = 0; num193 < 6; num193++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0, 0);
			}
			for (int num194 = 0; num194 < 10; num194++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0, 0);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
				num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0, 0);
				Main.dust[num195].velocity *= 2f;
				Main.dust[num195].noGravity = true;
			}
			Projectile.Damage();
        }
    }
}