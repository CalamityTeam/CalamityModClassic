using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class HarpyRing : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/HarpyRing");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Harpy Ring");
		////Tooltip.SetDefault("Increased move speed");
		Item.width = 20;
		Item.height = 22;
		Item.lifeRegen = 2;
		Item.value = 50000;
		Item.rare = 4;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.moveSpeed += 0.2f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "AerialiteBar", 2);
		recipe.AddIngredient(ItemID.Feather, 5);
		recipe.AddIngredient(ItemID.FallenStar);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
	}
}}