using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class BloodGeyser : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blood Geyser");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.hostile = true;
            if (CalamityWorldPreTrailer.death)
            {
                Projectile.tileCollide = false;
            }
            Projectile.timeLeft = 300;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            AIType = 1;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 5, 0f, 0f, 100, default(Color), 0.5f);
            Main.dust[num469].noGravity = true;
            Main.dust[num469].velocity *= 0f;
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BurningBlood").Type, 120);
        }
    }
}