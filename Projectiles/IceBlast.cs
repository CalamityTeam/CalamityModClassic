using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class IceBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ice");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
			Projectile.height = 14;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
        }

        public override void AI()
        {
        	int num3;
			for (int num322 = 0; num322 < 3; num322 = num3 + 1)
			{
				int num323 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Frost, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 1.2f);
				Main.dust[num323].noGravity = true;
				Dust dust = Main.dust[num323];
				dust.velocity *= 0.3f;
				num3 = num322;
			}
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item28, Projectile.position);
				return;
			}
        }

        public override void OnKill(int timeLeft)
        {
            int num497 = 10;
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			int num3;
			for (int num498 = 0; num498 < num497; num498 = num3 + 1)
			{
				int num499 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Frost, 0f, 0f, 0, default(Color), 1f);
				if (!Main.rand.NextBool(3))
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
        	if (Main.rand.NextBool(20))
			{
				target.AddBuff(47, 60, true);
			}
        }
    }
}