using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class Valediction : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scythe");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 15;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
        
        public override void AI()
        {
        	if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 8;
				SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
			}
        	if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 60f)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
					Projectile.netUpdate = true;
				}
                else
                {
                    float num472 = Projectile.Center.X;
                    float num473 = Projectile.Center.Y;
                    float num474 = 400f;
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
                        float num483 = 20f;
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
        	}
        	else
			{
				float num42 = 30f;
				float num43 = 5f;
				Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num44 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector2.X;
				float num45 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector2.Y;
				float num46 = (float)Math.Sqrt((double)(num44 * num44 + num45 * num45));
				if (num46 > 3000f)
				{
					Projectile.Kill();
				}
				num46 = num42 / num46;
				num44 *= num46;
				num45 *= num46;
				if (Projectile.velocity.X < num44)
				{
					Projectile.velocity.X = Projectile.velocity.X + num43;
					if (Projectile.velocity.X < 0f && num44 > 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X + num43;
					}
				}
				else if (Projectile.velocity.X > num44)
				{
					Projectile.velocity.X = Projectile.velocity.X - num43;
					if (Projectile.velocity.X > 0f && num44 < 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X - num43;
					}
				}
				if (Projectile.velocity.Y < num45)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num43;
					if (Projectile.velocity.Y < 0f && num45 > 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num43;
					}
				}
				else if (Projectile.velocity.Y > num45)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - num43;
					if (Projectile.velocity.Y > 0f && num45 < 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num43;
					}
				}
				if (Main.myPlayer == Projectile.owner)
				{
					Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
					Rectangle value2 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
					if (rectangle.Intersects(value2))
					{
						Projectile.Kill();
					}
				}
        	}
        	Projectile.rotation += 0.5f;
			return;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 600);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item21, Projectile.position);
            Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num621 = 0; num621 < 5; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 8; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 33, 0f, 0f, 100, default(Color), 3f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].velocity *= 2f;
            }
        }
    }
}