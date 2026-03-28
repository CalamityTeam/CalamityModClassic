using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class T1000Laser : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Laser");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
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
			Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0f, 0.7f, 0.1f);
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
        	return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
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
            int num212 = Main.rand.Next(3, 7);
			for (int num213 = 0; num213 < num212; num213++)
			{
				int num214 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.1f);
				Main.dust[num214].velocity *= 2f;
				Main.dust[num214].noGravity = true;
			}
        }
    }
}