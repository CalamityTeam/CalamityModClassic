using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage.RareVariants
{
	public class Quasar : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Quasar");
			// Tooltip.SetDefault("Succ");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 52;
			Item.damage = 50;
			Item.crit += 8;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.useTime = 12;
			Item.knockBack = 0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 48;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("RadiantStar").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 1f, 0f);
			return false;
		}
	}
}
