using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class DoGGravBomb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bomb");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.scale = 0.5f;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.95f) / 255f, ((255 - Projectile.alpha) * 1.15f) / 255f);
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 6)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 5)
			{
			   Projectile.frame = 0;
			}
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item117, Projectile.position);
			}
        	float num953 = 110f;
			float scaleFactor12 = 17f;
			float num954 = 40f;
			if (Projectile.timeLeft > 30 && Projectile.alpha > 0) 
			{
				Projectile.alpha -= 25;
			}
			if (Projectile.timeLeft > 30 && Projectile.alpha < 128 && Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height)) 
			{
				Projectile.alpha = 128;
			}
			if (Projectile.alpha < 0) 
			{
				Projectile.alpha = 0;
			}
			int num959 = (int)Projectile.ai[0];
			if (num959 >= 0 && Main.player[num959].active && !Main.player[num959].dead) 
			{
				if (Projectile.Distance(Main.player[num959].Center) > num954) 
				{
					Vector2 vector102 = Projectile.DirectionTo(Main.player[num959].Center);
					if (vector102.HasNaNs()) 
					{
						vector102 = Vector2.UnitY;
					}
					Projectile.velocity = (Projectile.velocity * (num953 - 1f) + vector102 * scaleFactor12) / num953;
					return;
				}
			} 
			else 
			{
				if (Projectile.timeLeft > 30) 
				{
					Projectile.timeLeft = 30;
				}
				if (Projectile.ai[0] != -1f) 
				{
					Projectile.ai[0] = -1f;
					Projectile.netUpdate = true;
					return;
				}
			}
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(BuffID.VortexDebuff, 300, true);
		}

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item125, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 3; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 6; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
        }
    }
}