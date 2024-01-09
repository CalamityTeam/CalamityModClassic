using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class HarpyRing : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Harpy Ring");
		//Tooltip.SetDefault("Increased movement speed");
	}
	
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 22;
		Item.lifeRegen = 2;
		Item.value = 50000;
		Item.rare = ItemRarityID.LightRed;
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