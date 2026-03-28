using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class OrangeExoArrow : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arrow");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 25;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.7f, 0.3f, 0f);
			float num55 = 100f;
			float num56 = 3f;
			if (Projectile.ai[1] == 0f)
			{
				Projectile.localAI[0] += num56;
				if (Projectile.localAI[0] > num55)
				{
					Projectile.localAI[0] = num55;
				}
			}
			else
			{
				Projectile.localAI[0] -= num56;
				if (Projectile.localAI[0] <= 0f)
				{
					Projectile.Kill();
					return;
				}
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(250, 100, 0, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
        	int num147 = 0;
			int num148 = 0;
        	float num149 = (float)(TextureAssets.Projectile[Projectile.type].Value.Width - Projectile.width) * 0.5f + (float)Projectile.width * 0.5f;
        	SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
        	Microsoft.Xna.Framework.Rectangle value6 = new Microsoft.Xna.Framework.Rectangle((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
			if (Projectile.getRect().Intersects(value6))
			{
				Vector2 value7 = new Vector2(Projectile.position.X - Main.screenPosition.X + num149 + (float)num148, Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) + Projectile.gfxOffY);
				float num162 = 100f;
				float scaleFactor = 3f;
				if (Projectile.ai[1] == 1f)
				{
					num162 = (float)((int)Projectile.localAI[0]);
				}
				for (int num163 = 1; num163 <= (int)Projectile.localAI[0]; num163++)
				{
					Vector2 value8 = Vector2.Normalize(Projectile.velocity) * (float)num163 * scaleFactor;
					Microsoft.Xna.Framework.Color color29 = Projectile.GetAlpha(color25);
					color29 *= (num162 - (float)num163) / num162;
					color29.A = 0;
					Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, value7 - value8, null, color29, Projectile.rotation, new Vector2(num149, (float)(Projectile.height / 2 + num147)), Projectile.scale, spriteEffects, 0f);
				}
			}
			return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int num226 = 36;
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 188);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num227 = 0; num227 < num226; num227++)
            {
                Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
                vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
                Vector2 vector7 = vector6 - Projectile.Center;
                int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 55, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 2f);
                Main.dust[num228].noGravity = true;
                Main.dust[num228].noLight = true;
                Main.dust[num228].velocity = vector7;
            }
            for (int num193 = 0; num193 < 2; num193++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 50, default(Color), 1.5f);
            }
            for (int num194 = 0; num194 < 20; num194++)
            {
                int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num195].noGravity = true;
                Main.dust[num195].velocity *= 3f;
                num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, 0f, 0f, 50, default(Color), 1.5f);
                Main.dust[num195].velocity *= 2f;
                Main.dust[num195].noGravity = true;
            }
            Projectile.Damage();
        }
    }
}