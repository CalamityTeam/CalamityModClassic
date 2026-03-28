using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Permafrost
{
	public class ArcticBearPaw : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 44;
			Projectile.height = 44;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.coldDamage = true;
			Projectile.penetrate = 5;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arctic Bear Paw");
		}

		public override void AI()
		{
            //make pretty dust
            int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 88);
            Main.dust[index2].noGravity = true;

            if (Projectile.velocity.X > -0.05f && Projectile.velocity.X < 0.05f &
                Projectile.velocity.Y > -0.05f && Projectile.velocity.Y < 0.05f)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.velocity *= 0.96f;
            }
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(BuffID.Frostburn, 480);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 60);

            if (Main.rand.Next(3) == 0)
                target.AddBuff(BuffID.Confused, Main.rand.Next(60, 240));
        }

		public override Color? GetAlpha (Color lightColor)
		{
			return new Color(200, 200, 200, Projectile.alpha);
		}
	}
}