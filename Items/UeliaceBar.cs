using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class UeliaceBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/UeliaceBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Ueliace Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 218750;
		Item.rare = 8;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(ItemID.ChlorophyteOre, 15);
		recipe.AddIngredient(null, "EnergyOrb");
		recipe.AddTile(null, "ParticleAccelerator");
		recipe.Register();
	}
}}