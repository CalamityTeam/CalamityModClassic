using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Boss
{
    public class SirenSong : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Musical Note");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1800;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 0.985f;
        	Projectile.velocity.Y *= 0.985f;
        	if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale += 0.01f;
				if (Projectile.scale >= 1.1f)
				{
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale -= 0.01f;
				if (Projectile.scale <= 0.9f)
				{
					Projectile.localAI[0] = 0f;
				}
			}
        	if (Projectile.ai[1] == 0f)
        	{
        		Projectile.ai[1] = 1f;
				float soundPitch = (Main.rand.NextFloat() - 0.5f) * 0.5f;
				Main.musicPitch = soundPitch;
				SoundEngine.PlaySound(SoundID.Item26, Projectile.position);
			}
			Lighting.AddLight(Projectile.Center, 0.7f, 0.5f, 0f);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(BuffID.Confused, 120);
        }
    }
}