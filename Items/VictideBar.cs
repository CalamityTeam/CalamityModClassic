using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class VictideBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Victide Bar");
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 15750;
		Item.rare = ItemRarityID.Green;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VictoryShard");
		recipe.AddIngredient(ItemID.Coral);
		recipe.AddIngredient(ItemID.Starfish);
		recipe.AddIngredient(ItemID.Seashell);
		recipe.AddTile(TileID.Furnaces);
		recipe.Register();
	}
}}