using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class IceBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Blast");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
			Projectile.height = 10;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int num3;
			for (int num322 = 0; num322 < 2; num322 = num3 + 1)
			{
				int num323 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 0.6f);
				Main.dust[num323].noGravity = true;
				Dust dust = Main.dust[num323];
				dust.velocity *= 0.3f;
				num3 = num322;
			}
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item28, Projectile.position);
			}
        }

        public override void OnKill(int timeLeft)
        {
            int num497 = 10;
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			int num3;
			for (int num498 = 0; num498 < num497; num498 = num3 + 1)
			{
				int num499 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, 0f, 0f, 0, default(Color), 1f);
				if (Main.rand.Next(3) != 0)
				{
					Dust dust = Main.dust[num499];
					dust.velocity *= 2f;
					Main.dust[num499].noGravity = true;
					dust = Main.dust[num499];
					dust.scale *= 1.75f;
				}
				else
				{
					Dust dust = Main.dust[num499];
					dust.scale *= 0.5f;
				}
				num3 = num498;
			}
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, 90, true);
            target.AddBuff(BuffID.Chilled, 60, true);
            target.AddBuff(BuffID.Frozen, 30, true);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}