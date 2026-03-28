using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
	public class Cyclone : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cyclone");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 2;
			Projectile.penetrate = 2;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			Projectile.rotation += 2.5f;
			Projectile.alpha -= 5;
			if (Projectile.alpha < 50)
			{
				Projectile.alpha = 50;
			}
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
							Main.npc[num475].velocity.X += 0.05f;
						}
						else
						{
							Main.npc[num475].velocity.X -= 0.05f;
						}
						if (Main.npc[num475].position.Y < num473)
						{
							Main.npc[num475].velocity.Y += 0.05f;
						}
						else
						{
							Main.npc[num475].velocity.Y -= 0.05f;
						}
					}
				}
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(204, 255, 255, Projectile.alpha);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}