using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class VictideBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/VictideBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Victide Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 15750;
		Item.rare = 2;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VictoryShard");
		recipe.AddIngredient(ItemID.Coral);
		recipe.AddIngredient(ItemID.Starfish);
		recipe.AddIngredient(ItemID.Seashell);
		recipe.AddTile(TileID.Furnaces);
		recipe.Register();
	}
}}