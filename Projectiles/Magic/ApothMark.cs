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
	public class ApothMark : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jaws of Annihilation");
		}

		public override void SetDefaults()
		{
			Projectile.width = 100;
            Projectile.height = 100;
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
            if (Projectile.timeLeft < 30)
                Projectile.alpha = Projectile.alpha + 6;
            else if (Projectile.timeLeft < 210)
            {
                Projectile.velocity.X *= 0.9f;
                Projectile.velocity.Y *= 0.9f;
            }
            else if (Projectile.timeLeft == 240)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
                double offsetHyp = Math.Sqrt(48*48+38*38);
                double offsetRotation = Math.Atan2(-38, 48) + (double)Projectile.rotation;
                double offsetX = offsetHyp * Math.Cos(offsetRotation);
                double offsetY = offsetHyp * Math.Sin(offsetRotation);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + (float)offsetX, Projectile.Center.Y + (float)offsetY, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("ApothJaws").Type, Projectile.damage, Projectile.knockBack, Main.myPlayer, Projectile.rotation, 0f);
                offsetRotation = Math.Atan2(38, 48) + (double)Projectile.rotation;
                offsetX = offsetHyp * Math.Cos(offsetRotation);
                offsetY = offsetHyp * Math.Sin(offsetRotation);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.position.X + Projectile.width/2 + (float)offsetX, Projectile.position.Y + Projectile.height/2 + (float)offsetY, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("ApothJaws").Type, Projectile.damage, Projectile.knockBack, Main.myPlayer, Projectile.rotation, 1f);
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
