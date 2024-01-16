using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class HolyFlare : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Holy Flare");
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Main.projFrames[Projectile.type] = 3;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 3)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 2)
			{
			   Projectile.frame = 0;
			}
			if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 244, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
			Projectile.velocity.Y = Projectile.velocity.Y + 0.02f;
			if (Projectile.velocity.Y > 1f) 
			{
				Projectile.velocity.Y = 1f;
			}
			if (Projectile.position.X + (float)Projectile.width < player.position.X) 
			{
				if (Projectile.velocity.X < 0f) 
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.98f;
				}
				Projectile.velocity.X = Projectile.velocity.X + 0.1f;
			} 
			else if (Projectile.position.X > player.position.X + (float)player.width) 
			{
				if (Projectile.velocity.X > 0f) 
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.98f;
				}
				Projectile.velocity.X = Projectile.velocity.X - 0.1f;
			}
			if (Projectile.velocity.X > 10f || Projectile.velocity.X < -10f) 
			{
				Projectile.velocity.X = Projectile.velocity.X * 0.97f;
			}
			Projectile.rotation = Projectile.velocity.X * 0.1f;
			return;
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 10; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 60);
        }
    }
}