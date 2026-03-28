using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class Exocomet : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Comet");
			Main.projFrames[Projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.alpha = 50;
        }

        public override void AI()
        {
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 5)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 4)
			{
			   Projectile.frame = 0;
			}
        	float num953 = 100f * Projectile.ai[1]; //100
        	float scaleFactor12 = 20f * Projectile.ai[1]; //5
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
			if (Projectile.alpha < 40) 
			{
				int num309 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, 107, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, new Color(0, 255, 255), 0.5f);
				Main.dust[num309].velocity *= -0.25f;
				num309 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 4f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 4f), 8, 8, 107, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, new Color(0, 255, 255), 0.5f);
				Main.dust[num309].velocity *= -0.25f;
				Main.dust[num309].position -= Projectile.velocity * 0.5f;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
			Lighting.AddLight(Projectile.Center, 0f, 0.5f, 0.5f);
			if (Main.player[Projectile.owner].active && !Main.player[Projectile.owner].dead) 
			{
				if (Projectile.Distance(Main.player[Projectile.owner].Center) > num954) 
				{
					Vector2 vector102 = Projectile.DirectionTo(Main.player[Projectile.owner].Center);
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
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (Main.rand.Next(30) == 0)
	    	{
	    		target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 240);
	    	}
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 100);
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 100);
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 100);
        	target.AddBuff(BuffID.CursedInferno, 100);
			target.AddBuff(BuffID.Frostburn, 100);
			target.AddBuff(BuffID.OnFire, 100);
			target.AddBuff(BuffID.Ichor, 100);
			if (target.type == NPCID.TargetDummy)
			{
				return;
			}
			float num = (float)hit.Damage * 0.01f;
			if ((int)num == 0)
			{
				return;
			}
			if (Main.player[Main.myPlayer].lifeSteal <= 0f)
			{
				return;
			}
			Main.player[Main.myPlayer].lifeSteal -= num;
			int num2 = Projectile.owner;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(null), target.position.X, target.position.Y, 0f, 0f, Mod.Find<ModProjectile>("Exoheal").Type, 0, 0f, Projectile.owner, (float)num2, num);
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(0, 255, 255, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Zombie103, Projectile.position);
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 80);
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num193 = 0; num193 < 2; num193++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			}
			for (int num194 = 0; num194 < 20; num194++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 107, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
				num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
				Main.dust[num195].velocity *= 2f;
				Main.dust[num195].noGravity = true;
			}
        }
    }
}