using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class CruptixBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cruptix Bar");
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 150000;
		Item.rare = ItemRarityID.Yellow;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(4);
		recipe.AddIngredient(null, "ChaoticOre", 12);
        recipe.AddIngredient(null, "CoreofChaos");
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
	}
}}