using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class UnholyCore : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Unholy Core");
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 58750;
		Item.rare = ItemRarityID.Pink;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CharredOre", 4);
		recipe.AddIngredient(ItemID.Hellstone, 4);
		recipe.AddTile(TileID.AdamantiteForge);
		recipe.Register();
	}
}}