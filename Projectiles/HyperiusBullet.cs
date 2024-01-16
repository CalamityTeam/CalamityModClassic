using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
	public class HyperiusBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Hyperius Bullet");
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
			for (int num136 = 0; num136 < 10; num136++)
			{
				float x2 = Projectile.position.X - Projectile.velocity.X / 10f * (float)num136;
				float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num136;
				int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 235, 0f, 0f, 0, default(Color), 0.5f);
				Main.dust[num137].alpha = Projectile.alpha;
				Main.dust[num137].position.X = x2;
				Main.dust[num137].position.Y = y2;
				Main.dust[num137].velocity *= 0f;
				Main.dust[num137].noGravity = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
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
    		Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector2.X, vector2.Y, speedX, speedY, Mod.Find<ModProjectile>("OMGWTH").Type, (int)((double)Projectile.damage), 1f, Projectile.owner);
		}
	}
}