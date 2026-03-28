using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class ExoFire : ModProjectile
    {
        public bool speedXChoice = false;
        public bool speedYChoice = false;

    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fire");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 10;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            float speedX = 1f;
            float speedY = 1f;
            if (!speedXChoice)
            {
                speedX = (Main.rand.Next(2) == 0 ? 1.03f : 0.97f);
                speedXChoice = true;
            }
            if (!speedYChoice)
            {
                speedY = (Main.rand.Next(2) == 0 ? 1.03f : 0.97f);
                speedYChoice = true;
            }
            Projectile.velocity.X *= speedX;
            Projectile.velocity.X *= speedY;
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
				int num297 = (Main.rand.Next(2) == 0 ? 107 : 234);
                if (Main.rand.Next(4) == 0)
                {
                    num297 = 269;
                }
				if (Main.rand.Next(2) == 0)
				{
                    for (int num298 = 0; num298 < 2; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                        if (Main.rand.Next(3) == 0)
                        {
                            Main.dust[num299].scale *= 1.5f;
                            Dust expr_DBEF_cp_0 = Main.dust[num299];
                            expr_DBEF_cp_0.velocity.X = expr_DBEF_cp_0.velocity.X * 1.2f;
                            Dust expr_DC0F_cp_0 = Main.dust[num299];
                            expr_DC0F_cp_0.velocity.Y = expr_DC0F_cp_0.velocity.Y * 1.2f;
                        }
                        else
                        {
                            Main.dust[num299].scale *= 0.75f;
                        }
                        Main.dust[num299].noGravity = true;
                        Dust expr_DC74_cp_0 = Main.dust[num299];
                        expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 0.8f;
                        Dust expr_DC94_cp_0 = Main.dust[num299];
                        expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 0.8f;
                        Main.dust[num299].scale *= num296;
                        Main.dust[num299].velocity += Projectile.velocity;
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
            if (Main.rand.Next(30) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 240);
            }
            target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 100);
            target.AddBuff(BuffID.CursedInferno, 100);
            target.AddBuff(BuffID.Frostburn, 100);
            target.AddBuff(BuffID.OnFire, 100);
            target.AddBuff(BuffID.Ichor, 100);
        }
    }
}