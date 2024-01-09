using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class ShieldoftheOcean : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/ShieldoftheOcean");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Shield of the Ocean");
		//Tooltip.SetDefault("Increased defense when submerged in liquid");
		Item.width = 24;
		Item.height = 28;
		Item.value = 50000;
		Item.rare = 2;
		Item.defense = 2;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
		{
			player.statDefense += 4;
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