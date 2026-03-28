using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class SupremeManaPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Supreme Mana Potion");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 999;
			Item.healMana = 400;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 6, 50, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SuperManaPotion);
			recipe.AddIngredient(null, "Phantoplasm");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}