using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class HolyFireBullet : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bullet");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.extraUpdates = 5;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.65f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        	Projectile.localAI[0] += 1f;
        	if (Projectile.localAI[0] >= 4f)
        	{
	        	for (int num92 = 0; num92 < 2; num92++)
				{
					float num93 = Projectile.velocity.X / 3f * (float)num92;
					float num94 = Projectile.velocity.Y / 3f * (float)num92;
					int num95 = 4;
					int num96 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num95, Projectile.position.Y + (float)num95), Projectile.width - num95 * 2, Projectile.height - num95 * 2, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num96].noGravity = true;
					Main.dust[num96].velocity *= 0.1f;
					Main.dust[num96].velocity += Projectile.velocity * 0.1f;
					Dust expr_47FA_cp_0 = Main.dust[num96];
					expr_47FA_cp_0.position.X = expr_47FA_cp_0.position.X - num93;
					Dust expr_4815_cp_0 = Main.dust[num96];
					expr_4815_cp_0.position.Y = expr_4815_cp_0.position.Y - num94;
				}
				if (Main.rand.Next(10) == 0)
				{
					int num97 = 4;
					int num98 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num97, Projectile.position.Y + (float)num97), Projectile.width - num97 * 2, Projectile.height - num97 * 2, 244, 0f, 0f, 100, default(Color), 0.6f);
					Main.dust[num98].velocity *= 0.25f;
					Main.dust[num98].velocity += Projectile.velocity * 0.5f;
				}
        	}
        }

        public override void OnKill(int timeLeft)
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, (int)((double)Projectile.damage * 0.85), Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceRanged = true;
			}
            for (int k = 0; k < 10; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
        }
    }
}