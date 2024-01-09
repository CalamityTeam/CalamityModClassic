using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class AmbrosialAmpoule : ModItem
{
	
	public override void SetDefaults()
	{
		Item.defense = 15;
		Item.width = 20;
		Item.height = 20;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Red;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.beeResist = true;
		modPlayer.aAmpoule = true;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CorruptFlask");
        recipe.AddIngredient(null, "ArchaicPowder");
        recipe.AddIngredient(null, "RadiantOoze");
        recipe.AddIngredient(null, "HoneyDew");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(null, "CrimsonFlask");
        recipe.AddIngredient(null, "ArchaicPowder");
        recipe.AddIngredient(null, "RadiantOoze");
        recipe.AddIngredient(null, "HoneyDew");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}