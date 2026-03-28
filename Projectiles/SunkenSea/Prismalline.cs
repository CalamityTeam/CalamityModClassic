using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.SunkenSea
{
	public class Prismalline : ModProjectile
	{
		public bool hitEnemy = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Prismalline");
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.aiStyle = 113;
			Projectile.timeLeft = 180;
			AIType = 598;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= 1.57f;
			}
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] == 40f)
			{
				int numProj = 4;
				int numSpecProj = 0;
				float rotation = MathHelper.ToRadians(50);
				if (Projectile.owner == Main.myPlayer)
				{
					for (int i = 0; i < numProj + 1; i++)
					{
						Vector2 speed = new Vector2((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
						while (speed.X == 0f && speed.Y == 0f)
						{
							speed = new Vector2((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
						}
						speed.Normalize();
						speed *= ((float)Main.rand.Next(30, 61) * 0.1f) * 2f;
						if (numSpecProj < 2 && !hitEnemy)
						{
							Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("Prismalline3").Type, (int)((double)Projectile.damage * 1.25), Projectile.knockBack, Projectile.owner, 0f, 0f);
							++numSpecProj;
						}
						else
						{
							Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("Prismalline2").Type, (int)((double)Projectile.damage * 0.75), Projectile.knockBack, Projectile.owner, 0f, 0f);
						}
					}
				}
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 154, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			hitEnemy = true;
		}
	}
}