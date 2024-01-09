using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class IceBeam : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.coldDamage = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 5;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 4;
        }

        public override void AI()
        {
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
			}
        	if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 10;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight(Projectile.Center, 0.1f, 0.1f, 0.6f);
			for (int num121 = 0; num121 < 5; num121++)
			{
				Dust dust4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueFairy, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f)];
				dust4.velocity = Vector2.Zero;
				dust4.position -= Projectile.velocity / 5f * (float)num121;
				dust4.noGravity = true;
				dust4.scale = 0.8f;
				dust4.noLight = true;
			}
			if (Projectile.light > 0f)
			{
				float num = Projectile.light;
				float num2 = Projectile.light;
				float num3 = Projectile.light;
				num2 *= 0.1f;
				num *= 0.5f;
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
		{
			if (Projectile.alpha > 200)
			{
				return Color.Transparent;
			}
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 500);
        }
    }
}