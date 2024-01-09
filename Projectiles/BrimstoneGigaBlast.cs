using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BrimstoneGigaBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Giga Blast");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 50;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	if (NPC.CountNPCS(Mod.Find<ModNPC>("SupremeCalamitas").Type) < 1)
			{
				Projectile.active = false;
				return;
			}
        	bool revenge = CalamityWorld.revenge;
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.9f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	float num953 = revenge ? 130f : 100f; //100
        	float scaleFactor12 = revenge ? 25f : 20f; //5
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
        	target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 240);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        	float spread = 45f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
	    	double deltaAngle = spread/8f;
	    	double offsetAngle;
	    	int i;
	    	if (Projectile.owner == Main.myPlayer)
	    	{
		    	for (i = 0; i < 4; i++ )
		    	{
		   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
		        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 7f ), (float)( Math.Cos(offsetAngle) * 7f ), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 1f);
		        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 7f ), (float)( -Math.Cos(offsetAngle) * 7f ), Mod.Find<ModProjectile>("BrimstoneBarrage").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 1f);
		    	}
	    	}
        	for (int dust = 0; dust <= 5; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.LifeDrain, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}