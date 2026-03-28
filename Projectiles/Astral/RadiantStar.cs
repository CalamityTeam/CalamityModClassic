using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
	public class RadiantStar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radiant Star");
		}

		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.friendly = true;
			Projectile.penetrate = 4;
			Projectile.timeLeft = 300;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= 1.57f;
			}
			if (Projectile.ai[0] == 1f)
			{
				float num472 = Projectile.Center.X;
				float num473 = Projectile.Center.Y;
				float num474 = 600f;
				for (int num475 = 0; num475 < 200; num475++)
				{
					if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1) && !Main.npc[num475].boss)
					{
						float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
						float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
						float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
						if (num478 < num474)
						{
							if (Main.npc[num475].position.X < num472)
							{
								Main.npc[num475].velocity.X += 0.25f;
							}
							else
							{
								Main.npc[num475].velocity.X -= 0.25f;
							}
							if (Main.npc[num475].position.Y < num473)
							{
								Main.npc[num475].velocity.Y += 0.25f;
							}
							else
							{
								Main.npc[num475].velocity.Y -= 0.25f;
							}
						}
					}
				}
			}
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] == 25f)
			{
				int numProj = 2;
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
						speed *= ((float)Main.rand.Next(30, 61) * 0.1f) * 2.5f;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("RadiantStar2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner,
							(Projectile.ai[0] == 1f ? 1f : 0f), 0f);
					}
					SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RadiantExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					Projectile.active = false;
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
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}
	}
}