using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class SanguineFlare : ModProjectile
    {
    	private int x;
    	private double speed = 10;
        private float startSpeedX = 0f;
        private float startSpeedY = 0f;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flare");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.velocity.X = Projectile.velocity.X + (Main.player[Projectile.owner].velocity.X * 0.5f);
                startSpeedY = Projectile.velocity.Y + (Main.player[Projectile.owner].velocity.Y * 0.5f);
                Projectile.velocity.Y = startSpeedY;
            }
        	Projectile.localAI[0] += 1f;
        	if (Projectile.localAI[0] >= 180f)
        	{
	        	x++;
	        	speed += 0.1;
	        	Projectile.velocity.Y = startSpeedY + (float)(speed * Math.Sin(x/4));
        	}
        	Projectile.rotation += Projectile.velocity.Y * 0.02f;
        	Projectile.alpha -= 5;
        	if (Projectile.alpha < 30)
        	{
        		Projectile.alpha = 30;
        	}
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        	Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num621 = 0; num621 < 3; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 235, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 6; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 235, 0f, 0f, 100, default(Color), 1.7f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 235, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num624].velocity *= 2f;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (target.type == NPCID.TargetDummy || !target.canGhostHeal)
			{
				return;
			}
        	Player player = Main.player[Projectile.owner];
			player.statLife += 1;
    		player.HealEffect(1);
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120);
        }
    }
}