using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class BrackishWater : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Water");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f);
			if (Projectile.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(SoundID.Item21, Projectile.position);
				Projectile.localAI[0] += 1f;
			}
			int randomDust = Main.rand.Next(2);
			if (randomDust == 0)
			{
				randomDust = 33;
			}
			else
			{
				randomDust = 89;
			}
			for (int num457 = 0; num457 < 3; num457++)
			{
				int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, randomDust, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num458].noGravity = true;
				Main.dust[num458].velocity *= 0.5f;
				Main.dust[num458].velocity += Projectile.velocity * 0.1f;
			}
			return;
        }
        
        public override void OnKill(int timeLeft)
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BrackishWaterBlast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.Venom, 300);
			target.AddBuff(BuffID.Poisoned, 300);
		}
    }
}