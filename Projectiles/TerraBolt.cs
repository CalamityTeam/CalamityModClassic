using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class TerraBolt : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Terra Bolt");
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.extraUpdates = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 30;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	if (Main.rand.Next(10) == 0)
        	{
            	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.35f, Projectile.velocity.Y * 0.35f, Mod.Find<ModProjectile>("TerraOrb2").Type, (int)((double)Projectile.damage * 0.7f), Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
			for (int num441 = 0; num441 < 1; num441++)
			{
				Vector2 vector30 = Projectile.position;
				vector30 -= Projectile.velocity * ((float)num441 * 0.25f);
				Projectile.alpha = 255;
				int num442 = Dust.NewDust(vector30, 1, 1, 74, 0f, 0f, 0, default(Color), 0.5f);
				Main.dust[num442].position = vector30;
				Dust expr_13A3E_cp_0 = Main.dust[num442];
				expr_13A3E_cp_0.position.X = expr_13A3E_cp_0.position.X + (float)(Projectile.width / 2);
				Dust expr_13A62_cp_0 = Main.dust[num442];
				expr_13A62_cp_0.position.Y = expr_13A62_cp_0.position.Y + (float)(Projectile.height / 2);
				Main.dust[num442].scale = (float)Main.rand.Next(70, 110) * 0.007f;
				Main.dust[num442].velocity *= 0.2f;
			}
			return;
        }
    }
}