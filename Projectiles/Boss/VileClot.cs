using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class VileClot : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vile Clot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
        }
        
        public override void AI()
        {
        	Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 3f)
			{
				int num104 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y),
					Projectile.width, Projectile.height, 75, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, default(Color), 1f);
				Main.dust[num104].noGravity = true;
			}
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(BuffID.CursedInferno, 60);
        }
    }
}