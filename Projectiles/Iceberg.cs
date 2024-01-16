using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Iceberg : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Iceberg");
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 1;
            AIType = 1;
        }
        
        public override void AI()
        {
        	Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 3; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
				}
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 67, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
    		target.AddBuff(BuffID.Frostburn, 300);
    		target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 60);
        }
    }
}