using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class DraedonBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Draedon Bar");
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 120000;
		Item.rare = ItemRarityID.LightPurple;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(5);
		recipe.AddIngredient(null, "PerennialOre", 15);
        recipe.AddIngredient(null, "EssenceofCinder");
        recipe.AddTile(TileID.AdamantiteForge);
        recipe.Register();
	}
}}