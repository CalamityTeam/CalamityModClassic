using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class Crystallite : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Crystallite");
		Item.width = 30;
		Item.height = 32;
		Item.maxStack = 999;
		Item.value = 100000;
		Item.rare = 5;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "Stardust");
        recipe.AddIngredient(ItemID.CrystalShard, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}