using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class AncientFossil : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ancient Fossil");
		//Tooltip.SetDefault("Increases pick speed by 35% while underground");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 5000;
		Item.rare = ItemRarityID.Green;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
		{
			player.pickSpeed -= 0.35f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddRecipeGroup("SiltGroup", 100);
        recipe.AddTile(TileID.Furnaces);
        recipe.Register();
	}
}}