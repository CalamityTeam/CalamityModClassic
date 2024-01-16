using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class UeliaceBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/UeliaceBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ueliace Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 218750;
		Item.rare = 8;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(null, "UelibloomOre", 12);
		recipe.AddTile(TileID.AdamantiteForge);
		recipe.Register();
	}
}}