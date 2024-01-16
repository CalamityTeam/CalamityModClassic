using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class DragonRage : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Dragon Rage");
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 120;
        }

        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
				Projectile.localAI[0] += 1f;
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 1.4f) / 255f, ((255 - Projectile.alpha) * 0.85f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
			if (Projectile.timeLeft > 120)
			{
				Projectile.timeLeft = 120;
			}
			if (Projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (Projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (Projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (Projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				Projectile.ai[0] += 1f;
				int num297 = 244;
				if (Main.rand.Next(1) == 0)
				{
					for (int num298 = 0; num298 < 2; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if ((num297 == 244 && Main.rand.Next(3) == 0))
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 4f;
							Dust expr_DBEF_cp_0 = Main.dust[num299];
							expr_DBEF_cp_0.velocity.X = expr_DBEF_cp_0.velocity.X * 2f;
							Dust expr_DC0F_cp_0 = Main.dust[num299];
							expr_DC0F_cp_0.velocity.Y = expr_DC0F_cp_0.velocity.Y * 2f;
						}
						else
						{
							Main.dust[num299].scale *= 2.5f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
					}
				}
				if (Main.rand.Next(1) == 0)
				{
					for (int num298 = 0; num298 < 2; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if ((num297 == 244 && Main.rand.Next(3) == 0))
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 6f;
							Dust expr_DBEF_cp_0 = Main.dust[num299];
							expr_DBEF_cp_0.velocity.X = expr_DBEF_cp_0.velocity.X * 2f;
							Dust expr_DC0F_cp_0 = Main.dust[num299];
							expr_DC0F_cp_0.velocity.Y = expr_DC0F_cp_0.velocity.Y * 2f;
						}
						else
						{
							Main.dust[num299].scale *= 2.5f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
					}
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
			}
			Projectile.rotation += 0.3f * (float)Projectile.direction;
			return;	
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}