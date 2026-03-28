using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class OmegaHealingPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omega Healing Potion");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 999;
			Item.healLife = 300;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 7, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "SupremeHealingPotion");
			recipe.AddIngredient(null, "BloodOrb", 10);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}