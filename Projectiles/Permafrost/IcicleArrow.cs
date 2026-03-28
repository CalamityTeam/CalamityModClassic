using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Permafrost
{
	public class IcicleArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.coldDamage = true;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 1;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icicle Arrow");
		}

		public override void AI()
		{
            //icicle dust
            if (Main.rand.Next(2) == 0)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 1.1f);
                Main.dust[index2].noGravity = true;
            }
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(BuffID.Frostburn, 300);
        }

		public override void OnKill(int timeLeft)
		{
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, 0f, 0f, 0, new Color(), 1f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1.5f;
                Main.dust[index2].scale *= 0.9f;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                for (int index = 0; index < 3; ++index)
                {
                    float SpeedX = -Projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float SpeedY = -Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X + SpeedX, Projectile.position.Y + SpeedY, SpeedX, SpeedY, ProjectileID.CrystalShard, Projectile.damage / 2, 0f, Projectile.owner);
                }
            }
        }
	}
}