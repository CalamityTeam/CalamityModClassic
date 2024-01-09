using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class SanguineFlare : ModProjectile
    {
    	public int x;
    	public double speed = 10;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Flare");
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
        	Projectile.localAI[0] += 1f;
        	if (Projectile.localAI[0] >= 180f)
        	{
	        	x++;
	        	speed += 0.1;
	        	Projectile.velocity.Y = (float)(speed * Math.Sin(x/4));
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
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 6; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1.7f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num624].velocity *= 2f;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
        	Player player = Main.player[Projectile.owner];
			player.statLife += 2;
    		player.HealEffect(2);
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120);
        }
    }
}