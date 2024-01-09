using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class AerialiteBar : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Aerialite Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 18750;
		Item.rare = 2;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "AerialiteOre", 3);
		recipe.AddTile(TileID.Furnaces);
		recipe.Register();
	}
}}