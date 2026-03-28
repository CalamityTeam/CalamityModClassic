using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class RubberMortarRound : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rubber Mortar Round");
			// Tooltip.SetDefault("Large blast radius\nWill destroy tiles on each bounce\nUsed by normal guns");
		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 14;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 7.5f;
			Item.value = 1000;
			Item.rare = 5;
			Item.ammo = 97;
			Item.shoot = Mod.Find<ModProjectile>("RubberMortarRound").Type;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(null, "MortarRound", 100);
			recipe.AddIngredient(ItemID.PinkGel, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}