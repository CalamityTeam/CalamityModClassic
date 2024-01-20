using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class MechwormTail : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Mechworm");
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            //projectile.hide = true;
            Projectile.alpha = 255;
            //Main.projPet[projectile.type] = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public override void AI()
		{
			Lighting.AddLight((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f), 0.15f, 0.01f, 0.15f);
			Player player9 = Main.player[Projectile.owner];
			CalamityPlayer1Point1 modPlayer = player9.GetModPlayer<CalamityPlayer1Point1>();
			if ((int)Main.time % 120 == 0)
			{
				Projectile.netUpdate = true;
			}
			if (!player9.active)
			{
				Projectile.active = false;
				return;
			}
			int num1051 = 30;

			if (player9.dead)
			{
				modPlayer.mWorm = false;
			}
			if (modPlayer.mWorm)
			{
				Projectile.timeLeft = 2;
			}

			Vector2 value68 = Vector2.Zero;
			float num1064 = 0f;
			float scaleFactor17 = 0f;
			float scaleFactor18 = 1f;
			if (Projectile.ai[1] == 1f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			int chase = (int)Projectile.ai[0];
			if (chase >= 0 && Main.projectile[chase].active) //Checking the targets ID is just unnecessary overhead, this error will be easy to spot, without the need for costly checking.
			{
				value68 = Main.projectile[chase].Center;
				Vector2 arg_2DE6A_0 = Main.projectile[chase].velocity;
				num1064 = Main.projectile[chase].rotation;
				scaleFactor18 = MathHelper.Clamp(Main.projectile[chase].scale, 0f, 50f);
				scaleFactor17 = 16f;
				Main.projectile[chase].localAI[0] = Projectile.localAI[0] + 1f;
			} 
			else
			{
				Projectile.Kill();
				return;
			}
			Projectile.alpha -= 42;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Projectile.velocity = Vector2.Zero;
			Vector2 vector134 = value68 - Projectile.Center;
			if (num1064 != Projectile.rotation)
			{
				float num1068 = MathHelper.WrapAngle(num1064 - Projectile.rotation);
				vector134 = vector134.RotatedBy((double)(num1068 * 0.1f), default(Vector2));
			}
			Projectile.rotation = vector134.ToRotation() + 1.57079637f;
			Projectile.position = Projectile.Center;
			Projectile.scale = scaleFactor18;
			Projectile.width = (Projectile.height = (int)((float)num1051 * Projectile.scale));
			Projectile.Center = Projectile.position;
			if (vector134 != Vector2.Zero)
			{
				Projectile.Center = value68 - Vector2.Normalize(vector134) * scaleFactor17 * scaleFactor18;
			}
			Projectile.spriteDirection = ((vector134.X > 0f) ? 1 : -1);
			return;
		}
	}
}