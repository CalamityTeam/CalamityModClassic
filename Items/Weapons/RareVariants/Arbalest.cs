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
	public class Arbalest : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arbalest");
			// Tooltip.SetDefault("Fires volleys of high-speed arrows");
		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.crit += 20;
			Item.width = 58;
			Item.height = 22;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.rare = 5;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 10f;
			Item.useAmmo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int i = 0; i < 3; i++)
			{
				float num7 = velocity.X;
				float num8 = velocity.Y;
				float SpeedX = velocity.X + (float)Main.rand.Next(-20, 21) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-20, 21) * 0.05f;
				int proj = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
				Main.projectile[proj].extraUpdates += i;
				Main.projectile[proj].noDropItem = true;
			}
			return false;
		}
	}
}