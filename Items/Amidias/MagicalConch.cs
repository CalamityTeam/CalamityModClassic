using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Amidias
{
	public class MagicalConch : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magical Conch");
			// Tooltip.SetDefault("Summons a hermit crab to fight for you");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.mana = 10;
			Item.width = 34;
			Item.height = 24;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 1;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item44;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("HermitCrab").Type;
			Item.shootSpeed = 10f;
			Item.DamageType = DamageClass.Summon;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int i = Main.myPlayer;
			float num72 = Item.shootSpeed;
			int num73 = damage;
			float num74 = knockback;
			num74 = player.GetWeaponKnockback(Item, num74);
			player.itemTime = Item.useTime;
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
			num78 = 0f;
			num79 = 0f;
			vector2.X = (float)Main.mouseX + Main.screenPosition.X;
			vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, num78, num79, Mod.Find<ModProjectile>("HermitCrab").Type, num73, num74, i, 0f, 0f);
			return false;
		}
	}
}