using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class ChaosAmulet : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/ChaosAmulet");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Chaos Amulet");
		////Tooltip.SetDefault("Attacking enemies will set them on fire");
		////Tooltip.SetDefault("Spelunker effect");
		Item.width = 20;
		Item.height = 24;
		Item.lifeRegen = 2;
		Item.value = 150000;
		Item.rare = 7;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.findTreasure = true;
		player.magmaStone = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 2);
		recipe.AddIngredient(ItemID.MagmaStone);
		recipe.AddIngredient(ItemID.SpelunkerPotion, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}