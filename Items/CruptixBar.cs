using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class CruptixBar : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Cruptix Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 150000;
		Item.rare = 7;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(4);
		//recipe.AddIngredient(null, "ChaoticOre", 3);
		recipe.AddIngredient(null, "ChaosSerpentShard");
        recipe.AddIngredient(null, "EssenceofChaos");
        recipe.AddIngredient(ItemID.ChlorophyteOre, 2);
        recipe.AddIngredient(ItemID.Hellstone, 2);
        recipe.AddIngredient(ItemID.Obsidian, 2);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}