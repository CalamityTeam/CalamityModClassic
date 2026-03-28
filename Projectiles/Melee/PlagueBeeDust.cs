using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class PlagueBeeDust : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dust");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.4f) / 255f, ((255 - Projectile.alpha) * 0.01f) / 255f);
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
                if (Projectile.ai[0] % 2f == 0f)
                {
                    int spawnX = (int)(Projectile.width / 2);
                    int spawnY = (int)(Projectile.width / 2);
                    int bee = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + (float)Main.rand.Next(-spawnX, spawnX), Projectile.Center.Y + (float)Main.rand.Next(-spawnY, spawnY), 
                        Projectile.velocity.X, Projectile.velocity.Y, Main.player[Projectile.owner].beeType(),
                        Main.player[Projectile.owner].beeDamage(Projectile.damage / 3), Main.player[Projectile.owner].beeKB(0f), Projectile.owner, 0f, 0f);
                    Main.projectile[bee].penetrate = 1;
                }
				int num297 = 89;
				if (Main.rand.Next(1) == 0)
				{
					for (int num298 = 0; num298 < 2; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
						if ((num297 == 89 && Main.rand.Next(3) == 0))
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 2.5f;
							Dust expr_DBEF_cp_0 = Main.dust[num299];
							expr_DBEF_cp_0.velocity.X = expr_DBEF_cp_0.velocity.X * 2f;
							Dust expr_DC0F_cp_0 = Main.dust[num299];
							expr_DC0F_cp_0.velocity.Y = expr_DC0F_cp_0.velocity.Y * 2f;
						}
						else
						{
							Main.dust[num299].scale *= 2f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 1.2f;
						Main.dust[num299].scale *= num296;
						if (num297 == 75)
						{
							Main.dust[num299].velocity += Projectile.velocity;
							if (!Main.dust[num299].noGravity)
							{
								Main.dust[num299].velocity *= 0.5f;
							}
						}
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
        	target.immune[Projectile.owner] = 5;
	    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 180);
		}
    }
}