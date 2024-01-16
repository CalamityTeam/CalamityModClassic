using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class BarofLife : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/BarofLife");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Bar of Life");
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
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}