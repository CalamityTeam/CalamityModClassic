using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class DiamondDust : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/DiamondDust");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Diamond Dust");
		Item.width = 13;
		Item.height = 10;
		Item.maxStack = 999;
		Item.value = 250;
		Item.rare = 1;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(50);
		recipe.AddIngredient(ItemID.Obsidian, 5);
		recipe.AddTile(TileID.Anvils);
		recipe.Register();
	}
}}