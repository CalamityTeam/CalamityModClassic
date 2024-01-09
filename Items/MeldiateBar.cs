using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class MeldiateBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Meldiate Bar");
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 75000;
		Item.rare = ItemRarityID.Cyan;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(ItemID.Ectoplasm);
        recipe.AddIngredient(ItemID.HallowedBar);
        recipe.AddIngredient(null, "MeldBlob", 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}