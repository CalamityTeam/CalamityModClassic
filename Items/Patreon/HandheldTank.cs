using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
	public class HandheldTank : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Handheld Tank");
		}

		public override void SetDefaults()
		{
			Item.width = 110;
			Item.height = 46;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 1850;
			Item.crit += 15;
			Item.knockBack = 16f;
			Item.useTime = 71;
			Item.useAnimation = 71;
			Item.autoReuse = true;

			Item.useStyle = 5;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/TankCannon", SoundType.Sound);
			Item.noMelee = true;

			Item.value = Item.buyPrice(1, 40, 0, 0);
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;

			Item.shoot = Mod.Find<ModProjectile>("HandheldTankShell").Type;
			Item.shootSpeed = 6f;
			Item.useAmmo = AmmoID.Rocket;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("HandheldTankShell").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-33, 0);
		}

		public override void AddRecipes()
		{
			Recipe r = CreateRecipe();
			r.AddIngredient(null, "Shroomer");
			r.AddRecipeGroup(RecipeGroupID.IronBar, 50);
			r.AddIngredient(null, "DivineGeode", 5);
			r.AddIngredient(ItemID.TigerSkin);
			r.AddTile(TileID.LunarCraftingStation);
			r.Register();
		}
	}
}
