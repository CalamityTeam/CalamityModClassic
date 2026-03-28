using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
	public class RadiantStar2 : ModProjectile
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
			Projectile.penetrate = 1;
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
				float npcCenterX = 0f;
				float npcCenterY = 0f;
				float num474 = 600f;
				for (int num475 = 0; num475 < 200; num475++)
				{
					if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1) && !Main.npc[num475].boss)
					{
						npcCenterX = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
						npcCenterY = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
						float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - npcCenterX) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - npcCenterY);
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
			else
			{
				float centerX = Projectile.Center.X;
				float centerY = Projectile.Center.Y;
				float num474 = 600f;
				bool homeIn = false;
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
					{
						float num476 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
						float num477 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
						float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
						if (num478 < num474)
						{
							num474 = num478;
							centerX = num476;
							centerY = num477;
							homeIn = true;
						}
					}
				}
				if (homeIn)
				{
					float num483 = 24f;
					Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num484 = centerX - vector35.X;
					float num485 = centerY - vector35.Y;
					float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
					num486 = num483 / num486;
					num484 *= num486;
					num485 *= num486;
					Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
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
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}
	}
}