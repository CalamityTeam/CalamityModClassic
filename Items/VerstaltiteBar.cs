using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class VerstaltiteBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/VerstaltiteBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Verstaltite Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 100000;
		Item.rare = 5;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(4);
		recipe.AddIngredient(null, "Crystallite");
		//recipe.AddIngredient(null, "CyanicOre", 6);
        recipe.AddIngredient(null, "EssenceofEleum");
        recipe.AddIngredient(ItemID.Obsidian, 9);
		recipe.AddTile(TileID.Hellforge);
        recipe.Register();
        recipe = CreateRecipe(4);
		recipe.AddIngredient(null, "Crystallite");
		//recipe.AddIngredient(null, "CyanicOre", 6);
        recipe.AddIngredient(null, "EssenceofEleum");
        recipe.AddIngredient(ItemID.Obsidian, 9);
		recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
	}
}}