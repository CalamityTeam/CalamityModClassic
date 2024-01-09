using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class BarofLife : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Bar of Life");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 900000;
		Item.rare = 10;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar");
        recipe.AddIngredient(null, "DraedonBar");
        recipe.AddIngredient(null, "CruptixBar");
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}