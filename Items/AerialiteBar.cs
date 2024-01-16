using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class AerialiteBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/AerialiteBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Aerialite Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 18750;
		Item.rare = 2;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "AerialiteOre", 4);
		recipe.AddTile(TileID.Furnaces);
		recipe.Register();
	}
}}