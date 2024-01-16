﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class BrimstoneBolt : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Brimstone Bolt");
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 15;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight(Projectile.Center, 0.7f, 0f, 0f);
			for (int num121 = 0; num121 < 5; num121++)
			{
				Dust dust4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 235, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f)];
				dust4.velocity = Vector2.Zero;
				dust4.position -= Projectile.velocity / 5f * (float)num121;
				dust4.noGravity = true;
				dust4.scale = 0.8f;
				dust4.noLight = true;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
        }
    }
}