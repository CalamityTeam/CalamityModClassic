using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Placeables
{
	public class DraedonsForge : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draedon's Forge");
			// Tooltip.SetDefault("Used to craft uber-tier items");
		}
		
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 5000000;
			Item.rare = 10;
			Item.createTile = Mod.Find<ModTile>("DraedonsForge").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 20;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AdamantiteForge);
			recipe.AddIngredient(ItemID.MythrilAnvil);
			recipe.AddIngredient(ItemID.LunarCraftingStation);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "NightmareFuel", 20);
        	recipe.AddIngredient(null, "EndothermicEnergy", 20);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TitaniumForge);
			recipe.AddIngredient(ItemID.MythrilAnvil);
			recipe.AddIngredient(ItemID.LunarCraftingStation);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "NightmareFuel", 20);
        	recipe.AddIngredient(null, "EndothermicEnergy", 20);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TitaniumForge);
			recipe.AddIngredient(ItemID.OrichalcumAnvil);
			recipe.AddIngredient(ItemID.LunarCraftingStation);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "NightmareFuel", 20);
        	recipe.AddIngredient(null, "EndothermicEnergy", 20);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AdamantiteForge);
			recipe.AddIngredient(ItemID.OrichalcumAnvil);
			recipe.AddIngredient(ItemID.LunarCraftingStation);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "NightmareFuel", 20);
        	recipe.AddIngredient(null, "EndothermicEnergy", 20);
			recipe.Register();
		}
	}
}