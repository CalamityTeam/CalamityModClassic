using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
	public class ShockblastRound : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Round");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
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
			Projectile.alpha = 255;
			Projectile.timeLeft = 600;
			Projectile.light = 0.5f;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Shockblast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			return true;
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
		
		public override void AI()
		{
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 120f)
			{
				Projectile.damage = (int)((double)Projectile.damage * 0.995);
				Projectile.knockBack = (float)((int)((double)Projectile.knockBack * 0.995));
			}
		}
		
		public override bool PreAI()
        {
			for (int num136 = 0; num136 < 5; num136++)
			{
				float x2 = Projectile.position.X - Projectile.velocity.X / 10f * (float)num136;
				float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num136;
				int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 185, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num137].alpha = Projectile.alpha;
				Main.dust[num137].position.X = x2;
				Main.dust[num137].position.Y = y2;
				Main.dust[num137].velocity *= 0f;
				Main.dust[num137].noGravity = true;
			}
			float num138 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
			float num139 = Projectile.localAI[0];
			if (num139 == 0f)
			{
				Projectile.localAI[0] = num138;
				num139 = num138;
			}
			return false;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Shockblast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			}
			if (target.type == NPCID.TargetDummy)
			{
				return;
			}
        	float num = (float)target.damage * 0.1f;
			if ((int)num == 0)
			{
				return;
			}
			if (Main.player[Main.myPlayer].lifeSteal <= 0f)
			{
				return;
			}
			Main.player[Main.myPlayer].lifeSteal -= num * 1.5f;
			int num2 = Projectile.owner;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(null), target.position.X, target.position.Y, 0f, 0f, Mod.Find<ModProjectile>("TransfusionTrail").Type, 0, 0f, Projectile.owner, (float)num2, num);
        }
	}
}