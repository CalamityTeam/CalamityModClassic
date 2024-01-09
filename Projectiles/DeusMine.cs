﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class DeusMine : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mine");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 3000;
        }

        public override void AI()
        {
        	Projectile.velocity.X *= 0.985f;
        	Projectile.velocity.Y *= 0.985f;
        	float num395 = (float)Main.mouseTextColor / 200f - 0.35f;
			num395 *= 0.2f;
			Projectile.scale = num395 + 0.95f;
        	if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale -= 0.02f;
				Projectile.alpha += 30;
				if (Projectile.alpha >= 250)
				{
					Projectile.alpha = 255;
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 30;
				if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.localAI[0] = 0f;
				}
			}
        	if (Projectile.ai[1] == 0f)
        	{
        		Projectile.ai[1] = 1f;
        		SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
        	}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(255, Main.DiscoG, 0, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        	Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num621 = 0; num621 < 30; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 60; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 1.7f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num624].velocity *= 2f;
			}
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 300);
        }
    }
}