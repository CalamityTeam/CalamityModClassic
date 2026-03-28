using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
	public class ApothJaws : ModProjectile
	{
        private const float degrees = (float)(Math.PI / 180) * 2;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jaws of Annihilation");
		}

		public override void SetDefaults()
		{
			Projectile.width = 124;
            Projectile.height = 64;
			Projectile.alpha = 70;
			Projectile.timeLeft = 240;
			Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1.5f;
		}

		public override void AI()
        {
			if(Projectile.timeLeft % 8 == 0)
            {
                double angle = (double)(Main.rand.Next(360)) * Math.PI / 180;
                float offsetX = Projectile.position.X + (float)(Main.rand.Next((int)Projectile.width));
                float offsetY = Projectile.position.Y + (float)(Main.rand.Next((int)Projectile.height));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(null), offsetX, offsetY, 14 * (float)(Math.Cos(angle)), 14 * (float)(Math.Sin(angle)), Mod.Find<ModProjectile>("ApothChloro").Type, Projectile.damage, Projectile.knockBack / 2, Main.myPlayer);
            }
            if (Projectile.timeLeft < 30)
                Projectile.alpha = Projectile.alpha + 6;
            else if (Projectile.timeLeft < 210)
            {
                Projectile.velocity.X *= 0.9f; 
                Projectile.velocity.Y *= 0.9f; 
            }
            else if (Projectile.timeLeft < 240)
            {
                if (Projectile.ai[1] == 0)
					Projectile.rotation += 1.3f*degrees;
				else
					Projectile.rotation -= 1.3f*degrees;
            }
            else if (Projectile.timeLeft == 240)
            {
                if (Projectile.ai[1] == 0)
					Projectile.rotation = Projectile.ai[0] - 30*degrees;
				else
				{
					Projectile.rotation = Projectile.ai[0] + 30*degrees + (float)Math.PI;
					Projectile.spriteDirection = -1;
				}
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 600, true);
            target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 600, true);
            target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 600, true);
            if (Main.rand.Next(30) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 120, true);
            }
        }
	}
}
