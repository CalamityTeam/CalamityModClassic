using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Typeless
{
    public class NebulaStar : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Star");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 3600;
        }

        public override void AI()
        {
        	float num944 = 1f - (float)Projectile.alpha / 255f;
			num944 *= Projectile.scale;
			Lighting.AddLight(Projectile.Center, 0.25f * num944, 0.025f * num944, 0.275f * num944);
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] >= 90f)
			{
				Projectile.localAI[0] *= -1f;
			}
			if (Projectile.localAI[0] >= 0f)
			{
				Projectile.scale += 0.003f;
			}
			else
			{
				Projectile.scale -= 0.003f;
			}
			Projectile.rotation += 0.0025f * Projectile.scale;
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
				Projectile.damage = 0;
				Projectile.ai[1] = 1f;
				if (Projectile.alpha < 255)
				{
					Projectile.alpha += 5;
					if (Projectile.alpha > 255)
					{
						Projectile.alpha = 255;
					}
				}
				else if (Projectile.owner == Main.myPlayer)
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
				if (Projectile.alpha > 50)
				{
					Projectile.alpha -= 10;
					if (Projectile.alpha < 50)
					{
						Projectile.alpha = 50;
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
				Projectile.velocity = (Projectile.velocity * 10f + vector101) / 11f;
				return;
			}
			if ((double)Projectile.velocity.Length() > 0.2)
			{
				Projectile.velocity *= 0.98f;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (Projectile.owner == Main.myPlayer && Projectile.ai[1] == 0f)
        	{
        		Vector2 value10 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				value10.Normalize();
				value10 *= 0.3f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value10.X, value10.Y, Mod.Find<ModProjectile>("NebulaDust").Type, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
        	}
        }

		public override void PostDraw(Color lightColor)
		{
			Vector2 origin = new Vector2(17f, 17f);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Typeless/NebulaStarGlow").Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
		}
	}
}