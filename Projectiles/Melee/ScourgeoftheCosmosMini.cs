using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class ScourgeoftheCosmosMini : ModProjectile
    {
        private int bounce = 3;

    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Scourge Mini");
            Main.projFrames[Projectile.type] = 2;
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 3;
        }

        public override void AI()
        {
            if (Projectile.ai[1] == 1f)
            {
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
			}
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 50;
            }
            else
            {
                Projectile.extraUpdates = 0;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 1)
            {
                Projectile.frame = 0;
            }
            int num3;
            for (int num369 = 0; num369 < 1; num369 = num3 + 1)
            {
                int dustType = (Main.rand.Next(3) == 0 ? 56 : 242);
                float num370 = Projectile.velocity.X / 3f * (float)num369;
                float num371 = Projectile.velocity.Y / 3f * (float)num369;
                int num372 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num372].position.X = Projectile.Center.X - num370;
                Main.dust[num372].position.Y = Projectile.Center.Y - num371;
                Dust dust = Main.dust[num372];
                dust.velocity *= 0f;
                Main.dust[num372].scale = 0.5f;
                num3 = num369;
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - 1.57f;
            float num373 = Projectile.position.X;
            float num374 = Projectile.position.Y;
            float num375 = 100000f;
            bool flag10 = false;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 30f)
            {
                Projectile.ai[0] = 30f;
                int num4;
                for (int num376 = 0; num376 < 200; num376 = num4 + 1)
                {
                    if (Main.npc[num376].CanBeChasedBy(Projectile, false) && (!Main.npc[num376].wet || Projectile.type == 307))
                    {
                        float num377 = Main.npc[num376].position.X + (float)(Main.npc[num376].width / 2);
                        float num378 = Main.npc[num376].position.Y + (float)(Main.npc[num376].height / 2);
                        float num379 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num377) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num378);
                        if (num379 < 800f && num379 < num375 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num376].position, Main.npc[num376].width, Main.npc[num376].height))
                        {
                            num375 = num379;
                            num373 = num377;
                            num374 = num378;
                            flag10 = true;
                        }
                    }
                    num4 = num376;
                }
            }
            if (!flag10)
            {
                num373 = Projectile.position.X + (float)(Projectile.width / 2) + Projectile.velocity.X * 100f;
                num374 = Projectile.position.Y + (float)(Projectile.height / 2) + Projectile.velocity.Y * 100f;
            }
            float num380 = 10f;
            float num381 = 0.16f;
            Vector2 vector30 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num382 = num373 - vector30.X;
            float num383 = num374 - vector30.Y;
            float num384 = (float)Math.Sqrt((double)(num382 * num382 + num383 * num383));
            num384 = num380 / num384;
            num382 *= num384;
            num383 *= num384;
            if (Projectile.velocity.X < num382)
            {
                Projectile.velocity.X = Projectile.velocity.X + num381;
                if (Projectile.velocity.X < 0f && num382 > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num381 * 2f;
                }
            }
            else if (Projectile.velocity.X > num382)
            {
                Projectile.velocity.X = Projectile.velocity.X - num381;
                if (Projectile.velocity.X > 0f && num382 < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num381 * 2f;
                }
            }
            if (Projectile.velocity.Y < num383)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + num381;
                if (Projectile.velocity.Y < 0f && num383 > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num381 * 2f;
                    return;
                }
            }
            else if (Projectile.velocity.Y > num383)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - num381;
                if (Projectile.velocity.Y > 0f && num383 < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num381 * 2f;
                    return;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounce--;
            if (bounce <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

		public override void PostDraw(Color lightColor)
		{
			Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Vector2 origin = new Vector2(9f, 10f);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Melee/ScourgeoftheCosmosMiniGlow").Value, Projectile.Center - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
		}
	}
}