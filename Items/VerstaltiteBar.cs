using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class VerstaltiteBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/VerstaltiteBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Verstaltite Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 100000;
		Item.rare = 5;
	}
	
	public override void AddRecipes()
	{
        Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(null, "Stardust", 4);
		recipe.AddIngredient(null, "CryonicOre", 12);
        recipe.AddIngredient(null, "EssenceofEleum");
		recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
	}
}}