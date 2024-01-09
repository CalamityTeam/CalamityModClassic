using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Projectiles
{
	public class HyperiusBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Hyperius Bullet");
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			{
        		Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.6f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
			}
			if (Main.rand.Next(1) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Mod.Find<ModDust>("HBSparkle").Type, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
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
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
    		float xPos = Projectile.ai[0] > 0 ? Projectile.position.X + 800 : Projectile.position.X - 800;
    		Vector2 vector2 = new Vector2(xPos, Projectile.position.Y + Main.rand.Next(-800, 801));
    
    		float num80 = xPos;
    		float speedX = (float)target.position.X - vector2.X;
    		float speedY = (float)target.position.Y - vector2.Y;
    		float dir= (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
    		dir = 10 / num80;
    		speedX *= dir * 150;
    		speedY *= dir * 150;
    		Projectile.NewProjectile(Projectile.GetSource_OnHit(target), vector2.X, vector2.Y, speedX, speedY, Mod.Find<ModProjectile>("OMGWTH").Type, (int)((double)Projectile.damage * 1.0), 1F, Projectile.owner);
		}
	}
}