using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Amidias
{
	public class Waywasher : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Waywasher");
			// Tooltip.SetDefault("Casts inaccurate water bolts");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 4;
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("WaywasherProj").Type;
			Item.shootSpeed = 8f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-25, 26) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("WaywasherProj").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}

	}
}