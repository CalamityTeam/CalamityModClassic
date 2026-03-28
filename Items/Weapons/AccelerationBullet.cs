using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class AccelerationBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Acceleration Round");
			// Tooltip.SetDefault("Gains speed over time");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.25f;
			Item.value = 250;
			Item.rare = 1;
			Item.shoot = Mod.Find<ModProjectile>("AccelerationBullet").Type;
			Item.shootSpeed = 1f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(ItemID.MusketBall, 150);
			recipe.AddIngredient(null, "VictoryShard");
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}