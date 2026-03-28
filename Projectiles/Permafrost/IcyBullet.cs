using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Permafrost
{
	public class IcyBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.timeLeft = 600;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.coldDamage = true;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icy Bullet");
		}

		public override void AI()
		{
            if (Main.rand.Next(3) == 0)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 88, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 1f);
                Main.dust[index2].noGravity = true;
            }
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(BuffID.Frostburn, 300);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, Projectile.alpha);
        }

        public override void OnKill(int timeLeft)
		{
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 88, 0f, 0f, 0, new Color(), 0.9f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1.5f;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                for (int index = 0; index < 2; ++index)
                {
                    float SpeedX = -Projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float SpeedY = -Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X + SpeedX, Projectile.position.Y + SpeedY, SpeedX, SpeedY, ProjectileID.CrystalShard, Projectile.damage / 2, 0f, Projectile.owner);
                }
            }
        }
	}
}