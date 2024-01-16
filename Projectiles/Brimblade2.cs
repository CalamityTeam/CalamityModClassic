using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Brimblade2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Brimblade");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 300;
            AIType = 52;
        }
        
        public override void AI()
        {
			int num250 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 235, (float)(Projectile.direction * 2), 0f, 150, default(Color), 1.3f);
			Main.dust[num250].noGravity = true;
			Main.dust[num250].velocity *= 0f;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 235, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}