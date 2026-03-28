using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class ProvidenceCrystal : ModProjectile
    {
        public float speedX = -15f;
        public float speedY = -3f;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Holy Crystal");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = CalamityWorldPreTrailer.death ? 2100 : 3600;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || NPC.CountNPCS(Mod.Find<ModNPC>("Providence").Type) < 1)
            {
                Projectile.active = false;
                Projectile.netUpdate = true;
                return;
            }
            Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2) + Main.player[Projectile.owner].gfxOffY - 360f;
            if (Main.player[Projectile.owner].gravDir == -1f)
            {
                Projectile.position.Y = Projectile.position.Y + 400f;
                Projectile.rotation = 3.14f;
            }
            else
            {
                Projectile.rotation = 0f;
            }
            Projectile.position.X = (float)((int)Projectile.position.X);
            Projectile.position.Y = (float)((int)Projectile.position.Y);
            Projectile.velocity = Vector2.Zero;
            Projectile.alpha -= 5;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.direction == 0)
            {
                Projectile.direction = Main.player[Projectile.owner].direction;
            }
            if (Projectile.alpha == 0 && Main.rand.Next(15) == 0)
            {
                Dust dust34 = Main.dust[Dust.NewDust(Projectile.Top, 0, 0, 267, 0f, 0f, 100, new Color(255, 200, Main.DiscoB), 1f)];
                dust34.velocity.X = 0f;
                dust34.noGravity = true;
                dust34.fadeIn = 1f;
                dust34.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (4f * Main.rand.NextFloat() + 26f);
                dust34.scale = 0.5f;
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= 300f)
            {
                Projectile.localAI[0] = 0f;
                SoundEngine.PlaySound(SoundID.Item109, Projectile.position);
                Projectile.netUpdate = true;
                if (Projectile.owner == Main.myPlayer)
                {
                    int num31;
                    for (int num1083 = 0; num1083 < 10; num1083 = num31 + 1)
                    {
                        float x4 = Main.rgbToHsl(new Color(255, 200, Main.DiscoB)).X;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, speedX, speedY, Mod.Find<ModProjectile>("ProvidenceCrystalShard").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, x4, (float)Projectile.whoAmI);
                        num31 = num1083;
                        speedX += 3f;
                    }
                }
                speedX = -15f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            Vector2 vector59 = Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D34 = TextureAssets.Projectile[Projectile.type].Value;
            Microsoft.Xna.Framework.Rectangle rectangle17 = texture2D34.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Microsoft.Xna.Framework.Color alpha5 = Projectile.GetAlpha(color25);
            Vector2 origin11 = rectangle17.Size() / 2f;
            float scaleFactor5 = (float)Math.Cos((double)(6.28318548f * (Projectile.localAI[0] / 60f))) + 3f + 3f;
            for (float num286 = 0f; num286 < 4f; num286 += 1f)
            {
                SpriteBatch arg_F907_0 = Main.spriteBatch;
                Texture2D arg_F907_1 = texture2D34;
                Vector2 arg_F8CE_0 = vector59;
                Vector2 arg_F8BE_0 = Vector2.UnitY;
                double arg_F8BE_1 = (double)(num286 * 1.57079637f);
                Vector2 center = default(Vector2);
                arg_F907_0.Draw(arg_F907_1, arg_F8CE_0 + arg_F8BE_0.RotatedBy(arg_F8BE_1, center) * scaleFactor5, new Microsoft.Xna.Framework.Rectangle?(rectangle17), alpha5 * 0.2f, Projectile.rotation, origin11, Projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}