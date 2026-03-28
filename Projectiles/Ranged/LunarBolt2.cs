using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class LunarBolt2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bolt");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.scale = 1.25f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 180;
            AIType = 357;
        }

        public override void AI()
        {
        	if (Projectile.alpha < 170)
			{
				for (int num134 = 0; num134 < 10; num134++)
				{
					float x = Projectile.position.X - Projectile.velocity.X / 10f * (float)num134;
					float y = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num134;
					int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 107, 0f, 0f, 0, default(Color), 0.4f);
					Main.dust[num135].alpha = Projectile.alpha;
					Main.dust[num135].position.X = x;
					Main.dust[num135].position.Y = y;
					Main.dust[num135].velocity *= 0f;
					Main.dust[num135].noGravity = true;
				}
			}
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 25;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			return false;
        }
    }
}