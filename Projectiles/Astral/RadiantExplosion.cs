using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
    public class RadiantExplosion : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Explosion");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
        
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int i = 0; i < 5; i++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
				for (int i = 0; i < 5; i++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	for (int n = 0; n < 3; n++)
			{
				float x = target.position.X + (float)Main.rand.Next(-400, 400);
				float y = target.position.Y - (float)Main.rand.Next(500, 800);
				Vector2 vector = new Vector2(x, y);
				float num13 = target.position.X + (float)(target.width / 2) - vector.X;
				float num14 = target.position.Y + (float)(target.height / 2) - vector.Y;
				num13 += (float)Main.rand.Next(-100, 101);
				int num15 = 25;
				int projectileType = Main.rand.Next(3);
				if (projectileType == 0)
				{
					projectileType = Mod.Find<ModProjectile>("AstralStar").Type;
				}
				else if (projectileType == 1)
				{
					projectileType = ProjectileID.HallowStar;
				}
				else
				{
					projectileType = ProjectileID.FallingStar;
				}
				float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
				num16 = (float)num15 / num16;
				num13 *= num16;
				num14 *= num16;
				int num17 = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), x, y, num13, num14, projectileType, (int)((double)Projectile.damage * 0.75), 5f, Projectile.owner, 2f, 0f);
			}
        }
    }
}