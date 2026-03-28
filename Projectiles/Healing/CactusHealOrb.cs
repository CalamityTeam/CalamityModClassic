using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Healing
{
    public class CactusHealOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heal");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.velocity.Y *= 0.985f;
            int num487 = Projectile.owner;
            Vector2 vector36 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num489 = Main.player[num487].Center.X - vector36.X;
            float num490 = Main.player[num487].Center.Y - vector36.Y;
            float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
            if (num491 < 50f && Projectile.position.X < Main.player[num487].position.X + (float)Main.player[num487].width && Projectile.position.X + (float)Projectile.width > Main.player[num487].position.X && Projectile.position.Y < Main.player[num487].position.Y + (float)Main.player[num487].height && Projectile.position.Y + (float)Projectile.height > Main.player[num487].position.Y)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    int num492 = 15;
                    Main.player[num487].HealEffect(num492, false);
                    Main.player[num487].statLife += num492;
                    if (Main.player[num487].statLife > Main.player[num487].statLifeMax2)
                    {
                        Main.player[num487].statLife = Main.player[num487].statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, num487, (float)num492, 0f, 0f, 0, 0, 0);
                }
                Projectile.Kill();
            }
            float num498 = Projectile.velocity.X * 0.2f * 1f;
            float num499 = -(Projectile.velocity.Y * 0.2f) * 1f;
            int num500 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 107, 0f, 0f, 100, new Color(0, 200, 0), 1.5f);
            Main.dust[num500].noGravity = true;
            Main.dust[num500].velocity *= 0f;
            Dust expr_154F9_cp_0 = Main.dust[num500];
            expr_154F9_cp_0.position.X = expr_154F9_cp_0.position.X - num498;
            Dust expr_15518_cp_0 = Main.dust[num500];
            expr_15518_cp_0.position.Y = expr_15518_cp_0.position.Y - num499;
            return;
        }
    }
}