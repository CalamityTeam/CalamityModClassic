using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class RaidersTalisman : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.value = 30000;
		Item.rare = ItemRarityID.Green;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.raiderTalisman = true;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Leather, 5);
		recipe.AddIngredient(ItemID.Obsidian, 20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}