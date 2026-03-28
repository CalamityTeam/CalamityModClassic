using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class MortarRound : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mortar Round");
			// Tooltip.SetDefault("Large blast radius. Will destroy tiles\nUsed by normal guns");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 14;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 7.5f;
			Item.value = 500;
			Item.rare = 3;
			Item.ammo = 97;
			Item.shoot = Mod.Find<ModProjectile>("MortarRound").Type;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ItemID.RocketIV, 100);
			recipe.AddIngredient(null, "UeliaceBar");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}