using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class MeldiateBar : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/MeldiateBar");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Meldiate Bar");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 75000;
		Item.rare = 9;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(ItemID.Ectoplasm);
        recipe.AddIngredient(ItemID.HellstoneBar);
        recipe.AddIngredient(ItemID.ChlorophyteBar);
        recipe.AddIngredient(ItemID.HallowedBar);
        recipe.AddIngredient(null, "MeldBlob", 5);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}