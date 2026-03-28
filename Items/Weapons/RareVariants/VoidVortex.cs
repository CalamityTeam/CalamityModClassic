using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class VoidVortex : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Void Vortex");
			// Tooltip.SetDefault("Fires a circular spread of magnetic orbs around the mouse cursor");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 300;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 60;
			Item.width = 130;
			Item.height = 130;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.value = Item.buyPrice(1, 80, 0, 0);
			Item.rare = 10;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Climax2").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float num72 = Item.shootSpeed;
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
			}
			float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			float num81 = num80;
			if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
			{
				num78 = (float)player.direction;
				num79 = 0f;
				num80 = num72;
			}
			else
			{
				num80 = num72 / num80;
			}
			vector2 += new Vector2(num78, num79);
			float spread = 45f * 0.0174f;
			double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
			double deltaAngle = spread / 8f;
			double offsetAngle;
			int i;
			float passedVar = 1f;
			for (i = 0; i < 4; i++)
			{
				offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), type, damage, knockback, player.whoAmI, passedVar, 0f);
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), type, damage, knockback, player.whoAmI, -passedVar, 0f);
				passedVar += 1f;
			}
			return false;
		}
	}
}