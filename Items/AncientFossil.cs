using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class AncientFossil : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Ancient Fossil");
		//Tooltip.SetDefault("Increases pick speed by 35%");
		Item.width = 26;
		Item.height = 26;
		Item.value = 5000;
		Item.rare = 2;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.pickSpeed -= 0.35f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.SiltBlock, 100);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
	}
}}