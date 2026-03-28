using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class IonBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blast");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	Projectile.velocity.X *= 1.015f;
        	Projectile.velocity.Y *= 1.015f;
        	if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 3;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 1f, 0f, 0.2f);
			float num55 = 100f;
			float num56 = 3f;
			if (Projectile.ai[1] == 0f)
			{
				Projectile.localAI[0] += num56;
				if (Projectile.localAI[0] > num55)
				{
					Projectile.localAI[0] = num55;
				}
			}
			else
			{
				Projectile.localAI[0] -= num56;
				if (Projectile.localAI[0] <= 0f)
				{
					Projectile.Kill();
					return;
				}
			}
        }

        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("IonExplosion").Type, Projectile.damage, 0f, Projectile.owner, 0f, 0f);
        	}
            int num212 = Main.rand.Next(15, 30);
			for (int num213 = 0; num213 < num212; num213++)
			{
				int num214 = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, 130, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num214].velocity *= 2f;
				Main.dust[num214].noGravity = true;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120);
        }
    }
}