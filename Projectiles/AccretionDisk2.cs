using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class AccretionDisk2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Accretion Disk");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.alpha = 120;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 60;
            AIType = 52;
        }
        
        public override void AI()
        {
        	if (Main.rand.NextBool(2))
			{
				for (int num468 = 0; num468 < 1; num468++)
				{
					int num250 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.RainbowTorch, (float)(Projectile.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
					Main.dust[num250].noGravity = true;
					Main.dust[num250].velocity *= 0f;
				}
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 5;
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 100);
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 100);
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 100);
        }
    }
}