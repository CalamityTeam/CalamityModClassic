using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class ScourgeoftheCosmos : ModProjectile
    {
        public int bounce = 3;

    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Scourge");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }
        
        public override void AI()
        {
            if (Projectile.ai[1] == 1f)
            {
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
			}
            if (Projectile.alpha <= 200)
            {
                int num3;
                for (int num20 = 0; num20 < 2; num20 = num3 + 1)
                {
                    int dustType = (Main.rand.Next(3) == 0 ? 56 : 242);
                    float num21 = Projectile.velocity.X / 4f * (float)num20;
                    float num22 = Projectile.velocity.Y / 4f * (float)num20;
                    int num23 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num23].position.X = Projectile.Center.X - num21;
                    Main.dust[num23].position.Y = Projectile.Center.Y - num22;
                    Dust dust = Main.dust[num23];
                    dust.velocity *= 0f;
                    Main.dust[num23].scale = 0.7f;
                    num3 = num20;
                }
            }
            Projectile.alpha -= 50;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 180f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
                Projectile.velocity.X = Projectile.velocity.X * 0.97f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
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
                SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.position);
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                if (Projectile.owner == Main.myPlayer)
                {
                    int num626 = 1;
                    if (Main.rand.Next(10) == 0)
                    {
                        num626++;
                    }
                    int num3;
                    for (int num627 = 0; num627 < num626; num627 = num3 + 1)
                    {
                        float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                        float num629 = (float)Main.rand.Next(-35, 36) * 0.02f;
                        num628 *= 10f;
                        num629 *= 10f;
                        if (!Projectile.CountsAsClass(DamageClass.Melee))
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X, Projectile.position.Y, num628, num629, Mod.Find<ModProjectile>("ScourgeoftheCosmosMini").Type, (int)((double)Projectile.damage * 0.7), (float)((int)((double)Projectile.knockBack * 0.35)), Main.myPlayer, 0f, 1f);
                        }
                        else
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X, Projectile.position.Y, num628, num629, Mod.Find<ModProjectile>("ScourgeoftheCosmosMini").Type, (int)((double)Projectile.damage * 0.7), (float)((int)((double)Projectile.knockBack * 0.35)), Main.myPlayer, 0f, 0f);
                        }
                        num3 = num627;
                    }
                }
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.position);
            int num3;
            for (int num622 = 0; num622 < 10; num622 = num3 + 1)
            {
                int dustType = (Main.rand.Next(3) == 0 ? 56 : 242);
                int num623 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, 0f, 0f, 0, default(Color), 1f);
                Dust dust = Main.dust[num623];
                dust.scale *= 1.1f;
                Main.dust[num623].noGravity = true;
                num3 = num622;
            }
            for (int num624 = 0; num624 < 15; num624 = num3 + 1)
            {
                int dustType = (Main.rand.Next(3) == 0 ? 56 : 242);
                int num625 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, 0f, 0f, 0, default(Color), 1f);
                Dust dust = Main.dust[num625];
                dust.velocity *= 2.5f;
                dust = Main.dust[num625];
                dust.scale *= 0.8f;
                Main.dust[num625].noGravity = true;
                num3 = num624;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                int num626 = 3;
                if (Main.rand.Next(10) == 0)
                {
                    num626++;
                }
                for (int num627 = 0; num627 < num626; num627 = num3 + 1)
                {
                    float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float num629 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    num628 *= 10f;
                    num629 *= 10f;
                    if (!Projectile.CountsAsClass(DamageClass.Melee))
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X, Projectile.position.Y, num628, num629, Mod.Find<ModProjectile>("ScourgeoftheCosmosMini").Type, (int)((double)Projectile.damage * 0.7), (float)((int)((double)Projectile.knockBack * 0.35)), Main.myPlayer, 0f, 1f);
                    }
                    else
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X, Projectile.position.Y, num628, num629, Mod.Find<ModProjectile>("ScourgeoftheCosmosMini").Type, (int)((double)Projectile.damage * 0.7), (float)((int)((double)Projectile.knockBack * 0.35)), Main.myPlayer, 0f, 0f);
                    }
                    num3 = num627;
                }
            }
        }
    }
}