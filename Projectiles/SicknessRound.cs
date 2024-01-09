using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
	public class SicknessRound : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Round");
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
			Projectile.light = 0.25f;
			Projectile.extraUpdates = 1;
			AIType = ProjectileID.Bullet;
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
		
		public override bool PreAI()
        {
			for (int num136 = 0; num136 < 7; num136++)
			{
				float x2 = Projectile.position.X - Projectile.velocity.X / 10f * (float)num136;
				float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num136;
				int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.TerraBlade, 0f, 0f, 0, default(Color), 1f);
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
			target.AddBuff(BuffID.Poisoned, 360);
			target.AddBuff(BuffID.Venom, 360);
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360);
        }
		
		public override void OnKill(int timeLeft)
        {
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Sickness").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
	            for (int k = 0; k < 3; k++)
	            {
	            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.TerraBlade, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
	            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, Mod.Find<ModProjectile>("SicknessRound2").Type, 
	            	(int)((double)Projectile.damage * 0.5), (float)((int)((double)Projectile.knockBack * 0.5)), Main.myPlayer, 0f, 0f);
	            }
			}
        }
	}
}