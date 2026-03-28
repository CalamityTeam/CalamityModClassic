using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
	public class HeavenfallenStardisk : ModProjectile
	{

		public bool explode = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heavenfallen Stardisk");
		}

		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.netImportant = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

		public override void AI()
		{
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 20;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}

			for (int i = 0; i < 2; i++)
			{
				int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 0f;
			}
			for (int i = 0; i < 2; i++)
			{
				int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 0f;
			}

			Projectile.rotation += 0.5f;

			if (Main.player[Projectile.owner].position.Y != Main.player[Projectile.owner].oldPosition.Y && Projectile.ai[0] == 0f)
			{
				explode = true;
			}

			Projectile.ai[0] += 1f;

			if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 20f)
			{
				if (Main.player[Projectile.owner].channel)
				{
					float num115 = 20f;
					Vector2 vector10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num116 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
					float num117 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
					if (Main.player[Projectile.owner].gravDir == -1f)
					{
						num117 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
					}
					float num118 = (float)Math.Sqrt((double)(num116 * num116 + num117 * num117));
					num118 = (float)Math.Sqrt((double)(num116 * num116 + num117 * num117));
					if (num118 > num115)
					{
						num118 = num115 / num118;
						num116 *= num118;
						num117 *= num118;
						int num119 = (int)(num116 * 1000f);
						int num120 = (int)(Projectile.velocity.X * 1000f);
						int num121 = (int)(num117 * 1000f);
						int num122 = (int)(Projectile.velocity.Y * 1000f);
						if (num119 != num120 || num121 != num122)
						{
							Projectile.netUpdate = true;
						}
						Projectile.velocity.X = num116;
						Projectile.velocity.Y = num117;
					}
					else
					{
						int num123 = (int)(num116 * 1000f);
						int num124 = (int)(Projectile.velocity.X * 1000f);
						int num125 = (int)(num117 * 1000f);
						int num126 = (int)(Projectile.velocity.Y * 1000f);
						if (num123 != num124 || num125 != num126)
						{
							Projectile.netUpdate = true;
						}
						Projectile.velocity.X = num116;
						Projectile.velocity.Y = num117;
					}
				}
				else if (Projectile.ai[0] == 20f)
				{
					Projectile.netUpdate = true;
					float num127 = 20f;
					Vector2 vector11 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num128 = (float)Main.mouseX + Main.screenPosition.X - vector11.X;
					float num129 = (float)Main.mouseY + Main.screenPosition.Y - vector11.Y;
					if (Main.player[Projectile.owner].gravDir == -1f)
					{
						num129 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector11.Y;
					}
					float num130 = (float)Math.Sqrt((double)(num128 * num128 + num129 * num129));
					if (num130 == 0f || Projectile.ai[0] < 0f)
					{
						vector11 = new Vector2(Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2), Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2));
						num128 = Projectile.position.X + (float)Projectile.width * 0.5f - vector11.X;
						num129 = Projectile.position.Y + (float)Projectile.height * 0.5f - vector11.Y;
						num130 = (float)Math.Sqrt((double)(num128 * num128 + num129 * num129));
					}
					num130 = num127 / num130;
					num128 *= num130;
					num129 *= num130;
					Projectile.velocity.X = num128;
					Projectile.velocity.Y = num129;
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 0f;
			}
			for (int i = 0; i < 10; i++)
			{
				int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 0f;
			}

			if (explode && Main.player[Projectile.owner].position.Y != Main.player[Projectile.owner].oldPosition.Y)
			{
				if (Projectile.owner == Main.myPlayer)
				{
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					for (i = 0; i < 4; i++)
					{
						offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 2f), (float)(Math.Cos(offsetAngle) * 2f), Mod.Find<ModProjectile>("HeavenfallenEnergy").Type, (int)((double)Projectile.damage * 0.4), Projectile.knockBack, Projectile.owner, 0f, 0f);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 2f), (float)(-Math.Cos(offsetAngle) * 2f), Mod.Find<ModProjectile>("HeavenfallenEnergy").Type, (int)((double)Projectile.damage * 0.4), Projectile.knockBack, Projectile.owner, 0f, 0f);
					}
				}
			}
		}
	}
}
