using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class Stardust : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Stardust");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Stardust");
		Item.width = 26;
		Item.height = 18;
		Item.maxStack = 999;
		Item.value = 5000;
		Item.rare = 5;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(ItemID.FallenStar);
		recipe.AddIngredient(ItemID.PixieDust, 3);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}