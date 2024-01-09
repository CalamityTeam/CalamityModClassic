using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class AncientFeather : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Ancient Feather");
		Item.width = 26;
		Item.height = 26;
		Item.maxStack = 999;
		Item.value = 115000;
		Item.rare = 6;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(1);
		recipe.AddIngredient(null, "WyvernFeather");
		recipe.AddIngredient(null, "LivingDewDroplet");
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}