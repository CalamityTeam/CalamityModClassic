using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class ShieldoftheOcean : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shield of the Ocean");
		//Tooltip.SetDefault("Increased defense by 5 when submerged in liquid");
	}
	
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 28;
		Item.value = 50000;
		Item.rare = ItemRarityID.Green;
		Item.defense = 2;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
		{
			player.statDefense += 5;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VictideBar", 5);
		recipe.AddIngredient(ItemID.Coral, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}