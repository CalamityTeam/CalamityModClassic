using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
	public class MechwormHead : ModProjectile
	{
		public int dust = 3;
		
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Mechworm");
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			//projectile.hide = true;
			Projectile.netImportant = true;
			//Main.projPet[projectile.type] = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.timeLeft *= 5;
			Projectile.minion = true;
		}

		public override void AI()
		{
			dust--;
        	if (dust >= 0)
        	{
        		int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 234, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
        	}
			Lighting.AddLight((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f), 0.15f, 0.01f, 0.15f);
			Player player9 = Main.player[Projectile.owner];
			CalamityPlayer modPlayer = player9.GetModPlayer<CalamityPlayer>();
			if ((int)Main.time % 120 == 0)
			{
				Projectile.netUpdate = true;
			}
			if (!player9.active)
			{
				Projectile.active = false;
				return;
			}
			int num1051 = 30;

			if (player9.dead)
			{
				modPlayer.mWorm = false;
			}
			if (modPlayer.mWorm)
			{
				Projectile.timeLeft = 2;
			}

			Vector2 center14 = player9.Center;
			float num1053 = 700f;
			float num1054 = 1000f;
			int num1055 = -1;
			if (Projectile.Distance(center14) > 2000f)
			{
				Projectile.Center = center14;
				Projectile.netUpdate = true;
			}
			bool flag65 = true;
			if (flag65)
			{
				for (int num1056 = 0; num1056 < 200; num1056++)
				{
					NPC nPC13 = Main.npc[num1056];
					if (nPC13.CanBeChasedBy(Projectile, false) && player9.Distance(nPC13.Center) < num1054)
					{
						float num1057 = Projectile.Distance(nPC13.Center);
						if (num1057 < num1053)
						{
							num1055 = num1056;
							bool arg_2D8E1_0 = nPC13.boss;
						}
					}
				}
			}
			if (num1055 != -1)
			{
				NPC nPC14 = Main.npc[num1055];
				Vector2 vector132 = nPC14.Center - Projectile.Center;
				(vector132.X > 0f).ToDirectionInt();
				(vector132.Y > 0f).ToDirectionInt();
				float scaleFactor16 = 0.4f;
				if (vector132.Length() < 600f)
				{
					scaleFactor16 = 0.6f;
				}
				if (vector132.Length() < 300f)
				{
					scaleFactor16 = 0.8f;
				}
				if (vector132.Length() > nPC14.Size.Length() * 0.75f)
				{
					Projectile.velocity += Vector2.Normalize(vector132) * scaleFactor16 * 1.5f;
					if (Vector2.Dot(Projectile.velocity, vector132) < 0.25f)
					{
						Projectile.velocity *= 0.8f;
					}
				}
				float num1058 = 30f;
				if (Projectile.velocity.Length() > num1058)
				{
					Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1058;
				}
			} else
			{
				float num1059 = 0.2f;
				Vector2 vector133 = center14 - Projectile.Center;
				if (vector133.Length() < 200f)
				{
					num1059 = 0.12f;
				}
				if (vector133.Length() < 140f)
				{
					num1059 = 0.06f;
				}
				if (vector133.Length() > 100f)
				{
					if (Math.Abs(center14.X - Projectile.Center.X) > 20f)
					{
						Projectile.velocity.X = Projectile.velocity.X + num1059 * (float)Math.Sign(center14.X - Projectile.Center.X);
					}
					if (Math.Abs(center14.Y - Projectile.Center.Y) > 10f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num1059 * (float)Math.Sign(center14.Y - Projectile.Center.Y);
					}
				} else if (Projectile.velocity.Length() > 2f)
				{
					Projectile.velocity *= 0.96f;
				}
				if (Math.Abs(Projectile.velocity.Y) < 1f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
				}
				float num1060 = 15f;
				if (Projectile.velocity.Length() > num1060)
				{
					Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1060;
				}
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
			int direction = Projectile.direction;
			Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));
			if (direction != Projectile.direction)
			{
				Projectile.netUpdate = true;
			}
			float num1061 = MathHelper.Clamp(Projectile.localAI[0], 0f, 50f);
			Projectile.position = Projectile.Center;
			Projectile.scale = 1f + num1061 * 0.01f;
			Projectile.width = (Projectile.height = (int)((float)num1051 * Projectile.scale));
			Projectile.Center = Projectile.position;
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 42;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
					return;
				}
			}
		}
	}
}
