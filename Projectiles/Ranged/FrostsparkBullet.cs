using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
	public class FrostsparkBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bullet");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
    	
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f);
        	Projectile.localAI[0] += 1f;
        	if (Projectile.localAI[0] >= 4f)
        	{
				for (int num136 = 0; num136 < 10; num136++)
				{
					float x2 = Projectile.position.X - Projectile.velocity.X / 10f * (float)num136;
					float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num136;
					int dustType = Main.rand.Next(2);
					if (dustType == 0)
					{
						dustType = 67;
					}
					else
					{
						dustType = 6;
					}
					int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, dustType, 0f, 0f, 0, default(Color), 0.5f);
					Main.dust[num137].alpha = Projectile.alpha;
					Main.dust[num137].position.X = x2;
					Main.dust[num137].position.Y = y2;
					Main.dust[num137].velocity *= 0f;
					Main.dust[num137].noGravity = true;
				}
        	}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.OnFire, 240);
        	target.AddBuff(BuffID.Frostburn, 240);
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
        }
		
		public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BoltExplosion").Type, (int)((double)Projectile.damage * 0.5), 0f, Projectile.owner, 0f, 0f);
        	int num212 = Main.rand.Next(10, 20);
			for (int num213 = 0; num213 < num212; num213++)
			{
				int dustType = Main.rand.Next(2);
				if (dustType == 0)
				{
					dustType = 67;
				}
				else
				{
					dustType = 6;
				}
				int num214 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, dustType, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num214].velocity *= 2f;
				Main.dust[num214].noGravity = true;
			}
        }
	}
}