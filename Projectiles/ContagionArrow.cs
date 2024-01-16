using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class ContagionArrow : ModProjectile
    {
    	public int addBallTimer = 5;
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Contagion Arrow");
            Projectile.width = 18;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 4)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
			   Projectile.frame = 0;
			}
        	addBallTimer--;
        	if (addBallTimer <= 0)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ContagionBall").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        		addBallTimer = 5;
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
			if (Projectile.ai[0] <= 3f)
			{
				Projectile.ai[0] += 1f;
				return;
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.075f;
			for (int num151 = 0; num151 < 1; num151++)
			{
				float num152 = Projectile.velocity.X / 3f * (float)num151;
				float num153 = Projectile.velocity.Y / 3f * (float)num151;
				int num154 = 14;
				int num155 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num154, Projectile.position.Y + (float)num154), Projectile.width - num154 * 2, Projectile.height - num154 * 2, 44, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[num155].noGravity = true;
				Main.dust[num155].velocity *= 0.1f;
				Main.dust[num155].velocity += Projectile.velocity * 0.5f;
				Dust expr_6A04_cp_0 = Main.dust[num155];
				expr_6A04_cp_0.position.X = expr_6A04_cp_0.position.X - num152;
				Dust expr_6A1F_cp_0 = Main.dust[num155];
				expr_6A1F_cp_0.position.Y = expr_6A1F_cp_0.position.Y - num153;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 600);
        }
    }
}