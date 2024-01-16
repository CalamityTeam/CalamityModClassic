using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class AbyssBallVolley2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Abyss Ball Volley V2");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.alpha = 60;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
        	if (Projectile.ai[1] == 0f)
        	{
        		Projectile.ai[1] = 1f;
        		SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
        	}
            if (Main.rand.Next(2) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(BuffID.Cursed, 50);
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 50);
        }
    }
}