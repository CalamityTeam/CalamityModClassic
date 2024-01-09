using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class ChaosSerpentShard : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Chaos Serpent Shard");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 200000;
		Item.rare = 7;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "LivingShard");
        recipe.AddIngredient(null, "TwistedTendril");
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}