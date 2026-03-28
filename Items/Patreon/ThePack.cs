using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
	public class ThePack : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Pack");
			// Tooltip.SetDefault("Fires large homing rockets that explode into more homing mini rockets when in proximity to an enemy");
		}

		public override void SetDefaults()
		{
			Item.damage = 3000;
			Item.crit += 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 96;
			Item.height = 40;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 7.5f;
			Item.value = Item.buyPrice(1, 80, 0, 0);
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shootSpeed = 24f;
			Item.shoot = Mod.Find<ModProjectile>("ThePackMissile").Type;
			Item.useAmmo = 771;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-40, 0);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ThePackMissile").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Scorpion");
			recipe.AddIngredient(ItemID.MarbleBlock, 50);
			recipe.AddIngredient(null, "CosmiliteBar", 10);
			recipe.AddIngredient(null, "ArmoredShell", 4);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}