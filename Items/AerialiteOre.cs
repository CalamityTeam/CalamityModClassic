using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class AerialiteOre : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Aerialite Ore");
		Item.width = 13;
		Item.height = 10;
		Item.maxStack = 999;
		Item.value = 3750;
		Item.rare = 1;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(3);
		recipe.AddIngredient(null, "DiamondDust", 5);
		recipe.AddIngredient(ItemID.Cloud, 3);
		recipe.AddTile(TileID.SkyMill);
		recipe.Register();
	}
}}