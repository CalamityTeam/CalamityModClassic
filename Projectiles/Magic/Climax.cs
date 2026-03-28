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
    public class Climax : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Climax");
			Main.projFrames[Projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.light = 0.5f;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 0.98f;
        	Projectile.velocity.Y *= 0.98f;
			if (Projectile.velocity.X > 0f) 
			{
				Projectile.rotation += (Math.Abs(Projectile.velocity.Y) + Math.Abs(Projectile.velocity.X)) * 0.001f;
			} 
			else
			{
				Projectile.rotation -= (Math.Abs(Projectile.velocity.Y) + Math.Abs(Projectile.velocity.X)) * 0.001f;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 6) 
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame > 4) 
				{
					Projectile.frame = 0;
				}
			}
			if (Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y)) > 2.0) 
			{
				Projectile.velocity *= 0.98f;
			}
			int num3;
			for (int num433 = 0; num433 < 1000; num433 = num3 + 1) 
			{
				num3 = num433;
			}
			int[] array = new int[20];
			int num434 = 0;
			float num435 = 400f;
			bool flag14 = false;
			for (int num436 = 0; num436 < 200; num436 = num3 + 1) 
			{
				if (Main.npc[num436].CanBeChasedBy(Projectile, false)) 
				{
					float num437 = Main.npc[num436].position.X + (float)(Main.npc[num436].width / 2);
					float num438 = Main.npc[num436].position.Y + (float)(Main.npc[num436].height / 2);
					float num439 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num437) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num438);
					if (num439 < num435 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num436].Center, 1, 1)) 
					{
						if (num434 < 20) 
						{
							array[num434] = num436;
							num434++;
						}
						flag14 = true;
					}
				}
				num3 = num436;
			}
			if (Projectile.timeLeft < 30) 
			{
				flag14 = false;
			}
			if (flag14) 
			{
				int num440 = Main.rand.Next(num434);
				num440 = array[num440];
				float num441 = Main.npc[num440].position.X + (float)(Main.npc[num440].width / 2);
				float num442 = Main.npc[num440].position.Y + (float)(Main.npc[num440].height / 2);
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] > 4f) 
				{
					Projectile.localAI[0] = 0f;
					float num443 = 8f;
					Vector2 vector33 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					vector33 += Projectile.velocity * 4f;
					float num444 = num441 - vector33.X;
					float num445 = num442 - vector33.Y;
					float num446 = (float)Math.Sqrt((double)(num444 * num444 + num445 * num445));
					num446 = num443 / num446;
					num444 *= num446;
					num445 *= num446;
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector33.X, vector33.Y, num444, num445, Mod.Find<ModProjectile>("ClimaxBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					return;
				}
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	if (Projectile.timeLeft < 30)
			{
				float num7 = (float)Projectile.timeLeft / 30f;
				Projectile.alpha = (int)(255f - 255f * num7);
			}
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
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