using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class CorruptFlask : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Corrupt Flask");
		//Tooltip.SetDefault("7% increased damage reduction and +3 defense while in the corruption");
	}
	
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.value = 50000;
		Item.rare = ItemRarityID.Green;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.ZoneCorrupt)
		{
			player.statDefense += 3;
	    	player.endurance += 0.07f;
		}
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "FetidEssence", 3);
        recipe.AddIngredient(ItemID.RottenChunk, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}