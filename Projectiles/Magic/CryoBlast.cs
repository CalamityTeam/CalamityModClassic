using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class CryoBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blast");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 600;
        }
        
        public override void AI()
		{
        	if (Projectile.scale <= 3.6f)
        	{
        		Projectile.scale *= 1.01f;
				Projectile.width = (int)(16f * Projectile.scale);
				Projectile.height = (int)(32f * Projectile.scale);
			}
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	Lighting.AddLight(Projectile.Center, 0.5f, 0.5f, 0.5f);
        	Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f)
			{
				for (int num468 = 0; num468 < 3; num468++)
				{
					int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 66, 0f, 0f, 100, default(Color), Projectile.scale);
					Main.dust[num469].noGravity = true;
					Main.dust[num469].velocity *= 0f;
					int num470 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 185, 0f, 0f, 100, default(Color), Projectile.scale);
					Main.dust[num470].noGravity = true;
					Main.dust[num470].velocity *= 0f;
				}
			}
        }

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 360);
			target.AddBuff(BuffID.Frostburn, 360);
		}
    }
}