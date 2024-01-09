using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class BarofLife : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Bar of Life");
	}
	
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 900000;
		Item.rare = ItemRarityID.Red;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar");
        recipe.AddIngredient(null, "DraedonBar");
        recipe.AddIngredient(null, "CruptixBar");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}