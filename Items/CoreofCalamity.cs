using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class CoreofCalamity : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Core of Calamity");
		Item.width = 34;
		Item.height = 40;
		Item.maxStack = 5;
		Item.value = 500000;
		Item.rare = 10;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CoreofCinder", 5);
        recipe.AddIngredient(null, "CoreofEleum", 5);
        recipe.AddIngredient(null, "CoreofChaos", 5);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}