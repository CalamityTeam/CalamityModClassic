using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class SolsticeBeam : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f);
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item60, Projectile.position);
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale -= 0.02f;
				Projectile.alpha += 30;
				if (Projectile.alpha >= 250)
				{
					Projectile.alpha = 255;
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 30;
				if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.localAI[0] = 0f;
				}
			}
			int dustType = 0;

			switch (CalamityModClassicPreTrailer.season)
			{
				case Season.Spring:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						74,
						157,
						107
					});
					break;
				case Season.Summer:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						247,
						228,
						57
					});
					break;
				case Season.Fall:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						6,
						259,
						158
					});
					break;
				case Season.Winter:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						67,
						229,
						185
					});
					break;
			}
			if (Main.rand.Next(3) == 0)
            {
            	int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f);
            	Main.dust[dust].noGravity = true;
            }
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
			byte red = 255;
			byte green = 255;
			byte blue = 255;
			switch (CalamityModClassicPreTrailer.season)
			{
				case Season.Spring:
					red = 0;
					green = 250;
					blue = 0;
					break;
				case Season.Summer:
					red = 250;
					green = 250;
					blue = 0;
					break;
				case Season.Fall:
					red = 250;
					green = 150;
					blue = 50;
					break;
				case Season.Winter:
					red = 100;
					green = 150;
					blue = 250;
					break;
			}
			return new Color(red, green, blue, Projectile.alpha);
		}
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
			int dustType = 0;

			switch (CalamityModClassicPreTrailer.season)
			{
				case Season.Spring:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						245,
						157,
						107
					});
					break;
				case Season.Summer:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						247,
						228,
						57
					});
					break;
				case Season.Fall:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						6,
						259,
						158
					});
					break;
				case Season.Winter:
					dustType = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						67,
						229,
						185
					});
					break;
			}
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            int num3;
            for (int num795 = 4; num795 < 31; num795 = num3 + 1)
            {
                float num796 = Projectile.oldVelocity.X * (30f / (float)num795);
                float num797 = Projectile.oldVelocity.Y * (30f / (float)num795);
                int num798 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num796, Projectile.oldPosition.Y - num797), 8, 8, dustType, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
                Main.dust[num798].noGravity = true;
                Dust dust = Main.dust[num798];
                dust.velocity *= 0.5f;
                num798 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num796, Projectile.oldPosition.Y - num797), 8, 8, dustType, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
                dust = Main.dust[num798];
                dust.velocity *= 0.05f;
                num3 = num795;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			int buff = Main.dayTime ? BuffID.Daybreak : Mod.Find<ModBuff>("Nightwither").Type;
			target.AddBuff(buff, 300);
		}
    }
}