using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SirenSong : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Siren's Song");
            Projectile.width = 26;
            Projectile.height = 52;
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
				Projectile.scale += 0.02f;
				if (Projectile.scale >= 1.25f)
				{
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale -= 0.02f;
				if (Projectile.scale <= 0.75f)
				{
					Projectile.localAI[0] = 0f;
				}
			}
        	if (Projectile.ai[1] == 0f)
        	{
        		Projectile.ai[1] = 1f;
        		SoundEngine.PlaySound(SoundID.Item26, Projectile.position);
        	}
            if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 89, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(BuffID.Confused, 60);
        }
    }
}