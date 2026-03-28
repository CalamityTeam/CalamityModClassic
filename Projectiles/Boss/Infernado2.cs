using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class Infernado2 : ModProjectile
    {
    	public int spawnCount = 0;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infernado");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 320;
            Projectile.height = 88;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 840;
            CooldownSlot = 1;
        }
        
        public override void AI()
        {
        	int num613 = 30;
			int num614 = 30;
			float num615 = 2f; //2.5
			int num616 = 320;
			int num617 = 88;
			if (Projectile.velocity.X != 0f)
			{
				Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.scale = ((float)(num613 + num614) - Projectile.ai[1]) * num615 / (float)(num614 + num613);
				Projectile.width = (int)((float)num616 * Projectile.scale);
				Projectile.height = (int)((float)num617 * Projectile.scale);
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[1] != -1f)
			{
				Projectile.scale = ((float)(num613 + num614) - Projectile.ai[1]) * num615 / (float)(num614 + num613);
				Projectile.width = (int)((float)num616 * Projectile.scale);
				Projectile.height = (int)((float)num617 * Projectile.scale);
			}
			if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.alpha -= 30;
				if (Projectile.alpha < 60)
				{
					Projectile.alpha = 60;
				}
			}
			else
			{
				Projectile.alpha += 30;
				if (Projectile.alpha > 150)
				{
					Projectile.alpha = 150;
				}
			}
			if (Projectile.ai[0] > 0f)
			{
				Projectile.ai[0] -= 1f;
			}
			if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
			{
				Projectile.netUpdate = true;
				Vector2 center = Projectile.Center;
				center.Y -= (float)num617 * Projectile.scale / 2f;
				float num618 = ((float)(num613 + num614) - Projectile.ai[1] + 1f) * num615 / (float)(num614 + num613);
				center.Y -= (float)num617 * num618 / 2f;
				center.Y += 2f;
                Projectile.damage = Main.expertMode ? 130 : 150;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(null), center.X, center.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
			}
			if (Projectile.ai[0] <= 0f)
			{
				float num622 = 0.104719758f;
				float num623 = (float)Projectile.width / 5f;
				num623 *= 2f;
				float num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X - num624 * (float)(-(float)Projectile.direction);
				Projectile.ai[0] -= 1f;
				num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X + num624 * (float)(-(float)Projectile.direction);
			}
        }

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            if (Projectile.timeLeft > 300)
            {
                return false;
            }
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(255, 255, 53, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}