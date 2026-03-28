using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Terracotta : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra-cotta");
			/* Tooltip.SetDefault("Causes enemies to erupt into healing projectiles on death\n" +
				"Enemies explode on hit"); */
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.damage = 125;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 5.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 60;
			Item.value = Item.buyPrice(0, 80, 0, 0);
			Item.rare = 8;
			Item.shootSpeed = 5f;
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			float spread = 180f * 0.0174f;
			double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed) - spread / 2;
			double deltaAngle = spread / 8f;
			double offsetAngle;
			int i;
			if (target.life <= 0)
			{
				for (i = 0; i < 1; i++)
				{
					float randomSpeedX = (float)Main.rand.Next(3);
					float randomSpeedY = (float)Main.rand.Next(3, 5);
					offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
					int projectile1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("Terracotta").Type, 0, 0f, Main.myPlayer);
					int projectile2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("Terracotta").Type, 0, 0f, Main.myPlayer);
					int projectile3 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("Terracotta").Type, 0, 0f, Main.myPlayer);
					Main.projectile[projectile1].velocity.X = -randomSpeedX;
					Main.projectile[projectile1].velocity.Y = -randomSpeedY;
					Main.projectile[projectile2].velocity.X = randomSpeedX;
					Main.projectile[projectile2].velocity.Y = -randomSpeedY;
					Main.projectile[projectile3].velocity.X = 0f;
					Main.projectile[projectile3].velocity.Y = -randomSpeedY;
				}
			}
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("TerracottaExplosion").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 246);
			}
		}
	}
}
