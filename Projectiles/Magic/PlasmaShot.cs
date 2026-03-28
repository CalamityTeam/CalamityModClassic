using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class PlasmaShot : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
			Projectile.extraUpdates = 10;
        }

        public override void AI()
        {
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 6f)
			{
				for (int num121 = 0; num121 < 10; num121++)
				{
					Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 107, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f)];
					dust.velocity = Vector2.Zero;
					dust.position -= Projectile.velocity / 5f * (float)num121;
					dust.noGravity = true;
					dust.noLight = true;
				}
			}
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        	int num220 = Main.rand.Next(20, 31);
        	if (Projectile.owner == Main.myPlayer)
        	{
				for (int num221 = 0; num221 < num220; num221++)
				{
					Vector2 value17 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					value17.Normalize();
					value17 *= (float)Main.rand.Next(10, 201) * 0.01f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value17.X, value17.Y, 511 + Main.rand.Next(3), Projectile.damage, 1f, Projectile.owner, 0f, (float)Main.rand.Next(-45, 1));
				}
        	}
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 107, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}