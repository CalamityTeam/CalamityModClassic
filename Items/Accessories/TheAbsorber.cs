using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class TheAbsorber : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 15;
			Item.width = 20;
			Item.height = 24;
			Item.value = 3000000;
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			modPlayer.aSpark = true;
			modPlayer.gShell = true;
			modPlayer.fCarapace = true;
			modPlayer.absorber = true;
			player.statManaMax2 += 30;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GrandGelatin");
			recipe.AddIngredient(null, "SeaShell");
			recipe.AddIngredient(null, "CrawCarapace");
			recipe.AddIngredient(null, "FungalCarapace");
			recipe.AddIngredient(null, "GiantTortoiseShell");
			recipe.AddIngredient(null, "AmidiasSpark");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(null, "GrandGelatin");
			recipe.AddIngredient(null, "SeaShell");
			recipe.AddIngredient(null, "FungalCarapace");
			recipe.AddIngredient(null, "GiantShell");
			recipe.AddIngredient(null, "GiantTortoiseShell");
			recipe.AddIngredient(null, "AmidiasSpark");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}