using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class ArchaicPowder : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Archaic Powder");
		//Tooltip.SetDefault("50% increased mining speed, 7% damage reduction, and +3 defense while underground or in the underworld");
	}
	
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.value = 50000;
		Item.rare = ItemRarityID.Orange;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight || player.ZoneUnderworldHeight)
		{
			player.statDefense += 3;
	    	player.endurance += 0.07f;
	    	player.pickSpeed -= 0.5f;
		}
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AncientFossil");
        recipe.AddIngredient(null, "DemonicBoneAsh");
        recipe.AddIngredient(null, "AncientBoneDust", 3);
        recipe.AddIngredient(ItemID.Bone, 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}