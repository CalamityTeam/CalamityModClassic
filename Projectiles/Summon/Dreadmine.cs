using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class Dreadmine : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mine");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 3600;
        }

        public override void AI()
        {
			float num945 = 1f;
			float num946 = 1f;
			if (Projectile.identity % 6 == 0)
			{
				num946 *= -1f;
			}
			if (Projectile.identity % 6 == 1)
			{
				num945 *= -1f;
			}
			if (Projectile.identity % 6 == 2)
			{
				num946 *= -1f;
				num945 *= -1f;
			}
			if (Projectile.identity % 6 == 3)
			{
				num946 = 0f;
			}
			if (Projectile.identity % 6 == 4)
			{
				num945 = 0f;
			}
			Projectile.localAI[1] += 1f;
			if (Projectile.localAI[1] > 60f)
			{
				Projectile.localAI[1] = -180f;
			}
			if (Projectile.localAI[1] >= -60f)
			{
				Projectile.velocity.X = Projectile.velocity.X + 0.002f * num946;
				Projectile.velocity.Y = Projectile.velocity.Y + 0.002f * num945;
			}
			else
			{
				Projectile.velocity.X = Projectile.velocity.X - 0.002f * num946;
				Projectile.velocity.Y = Projectile.velocity.Y - 0.002f * num945;
			}
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 5400f)
			{
				Projectile.ai[1] = 1f;
				if (Projectile.ai[0] < 5500f)
				{
                    return;
				}
				if (Projectile.owner == Main.myPlayer)
				{
					Projectile.Kill();
				}
			}
			else
			{
				float num947 = (Projectile.Center - Main.player[Projectile.owner].Center).Length() / 100f;
				if (num947 > 4f)
				{
					num947 *= 1.1f;
				}
				if (num947 > 5f)
				{
					num947 *= 1.2f;
				}
				if (num947 > 6f)
				{
					num947 *= 1.3f;
				}
				if (num947 > 7f)
				{
					num947 *= 1.4f;
				}
				if (num947 > 8f)
				{
					num947 *= 1.5f;
				}
				if (num947 > 9f)
				{
					num947 *= 1.6f;
				}
				if (num947 > 10f)
				{
					num947 *= 1.7f;
				}
				Projectile.ai[0] += num947;
				if (Projectile.alpha > 0)
				{
					Projectile.alpha -= 25;
					if (Projectile.alpha < 0)
					{
						Projectile.alpha = 0;
					}
				}
			}
			bool flag49 = false;
			Vector2 center12 = new Vector2(0f, 0f);
			float num948 = 600f;
			for (int num949 = 0; num949 < 200; num949++)
			{
				if (Main.npc[num949].CanBeChasedBy(Projectile, false))
				{
					float num950 = Main.npc[num949].position.X + (float)(Main.npc[num949].width / 2);
					float num951 = Main.npc[num949].position.Y + (float)(Main.npc[num949].height / 2);
					float num952 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num950) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num951);
					if (num952 < num948)
					{
						num948 = num952;
						center12 = Main.npc[num949].Center;
						flag49 = true;
					}
				}
			}
			if (flag49)
			{
				Vector2 vector101 = center12 - Projectile.Center;
				vector101.Normalize();
				vector101 *= 0.75f;
				Projectile.velocity = (Projectile.velocity * 10f + vector101) / 10.8f; //11
				return;
			}
			if ((double)Projectile.velocity.Length() > 0.2)
			{
				Projectile.velocity *= 0.98f;
				return;
			}
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 116);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 30; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].velocity *= 2f;
            }
            for (int num625 = 0; num625 < 3; num625++)
            {
                float scaleFactor10 = 0.33f;
                if (num625 == 1)
                {
                    scaleFactor10 = 0.66f;
                }
                if (num625 == 2)
                {
                    scaleFactor10 = 1f;
                }
                int num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num626].velocity *= scaleFactor10;
                Gore expr_13AB6_cp_0 = Main.gore[num626];
                expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
                Gore expr_13AD6_cp_0 = Main.gore[num626];
                expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
                num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num626].velocity *= scaleFactor10;
                Gore expr_13B79_cp_0 = Main.gore[num626];
                expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
                Gore expr_13B99_cp_0 = Main.gore[num626];
                expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
                num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num626].velocity *= scaleFactor10;
                Gore expr_13C3C_cp_0 = Main.gore[num626];
                expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
                Gore expr_13C5C_cp_0 = Main.gore[num626];
                expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
                num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num626].velocity *= scaleFactor10;
                Gore expr_13CFF_cp_0 = Main.gore[num626];
                expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
                Gore expr_13D1F_cp_0 = Main.gore[num626];
                expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
            }
            Projectile.Damage();
        }
    }
}