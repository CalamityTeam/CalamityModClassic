using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class ScarletBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blast");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 500;
            Projectile.height = 500;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.55f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
			if (Projectile.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
				Projectile.localAI[0] += 1f;
			}
			bool flag15 = false;
			bool flag16 = false;
			if (Projectile.velocity.X < 0f && Projectile.position.X < Projectile.ai[0])
			{
				flag15 = true;
			}
			if (Projectile.velocity.X > 0f && Projectile.position.X > Projectile.ai[0])
			{
				flag15 = true;
			}
			if (Projectile.velocity.Y < 0f && Projectile.position.Y < Projectile.ai[1])
			{
				flag16 = true;
			}
			if (Projectile.velocity.Y > 0f && Projectile.position.Y > Projectile.ai[1])
			{
				flag16 = true;
			}
			if (flag15 && flag16)
			{
				Projectile.Kill();
			}
			float num461 = 25f;
			if (Projectile.ai[0] > 180f)
			{
				num461 -= (Projectile.ai[0] - 180f) / 2f;
			}
			if (num461 <= 0f)
			{
				num461 = 0f;
				Projectile.Kill();
			}
			num461 *= 0.7f;
			Projectile.ai[0] += 4f;
			int num462 = 0;
			while ((float)num462 < num461)
			{
				float num463 = (float)Main.rand.Next(-30, 35);
				float num464 = (float)Main.rand.Next(-30, 35);
				float num465 = (float)Main.rand.Next(13, 18);
				float num466 = (float)Math.Sqrt((double)(num463 * num463 + num464 * num464));
				num466 = num465 / num466;
				num463 *= num466;
				num464 *= num466;
				int num467 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 130, 0f, 0f, 100, default(Color), 1.25f);
				Main.dust[num467].noGravity = true;
				Main.dust[num467].position.X = Projectile.Center.X;
				Main.dust[num467].position.Y = Projectile.Center.Y;
				Dust expr_149DF_cp_0 = Main.dust[num467];
				expr_149DF_cp_0.position.X = expr_149DF_cp_0.position.X + (float)Main.rand.Next(-10, 11);
				Dust expr_14A09_cp_0 = Main.dust[num467];
				expr_14A09_cp_0.position.Y = expr_14A09_cp_0.position.Y + (float)Main.rand.Next(-10, 11);
				Main.dust[num467].velocity.X = num463;
				Main.dust[num467].velocity.Y = num464;
				num462++;
			}
			return;
        }
    }
}