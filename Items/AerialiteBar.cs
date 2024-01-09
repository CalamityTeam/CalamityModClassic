using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class AerialiteBar : ModItem
{
	public override void SetStaticDefaults()
 	{
 		//DisplayName.SetDefault("Aerialite Bar");
 	}
	
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 18750;
		Item.rare = ItemRarityID.Green;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "AerialiteOre", 4);
		recipe.AddTile(TileID.Furnaces);
		recipe.Register();
	}
}}